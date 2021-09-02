using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.Services;
using Coworking.Application.ViewModels;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;

namespace Coworking.Infrastructure
{
    public class AcceptOrderCommand : IRequest<OutputGetOrderDto>
    {
        public AcceptOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        private int OrderId { get; }

        public class AcceptOrderCommandHandler : IRequestHandler<AcceptOrderCommand, OutputGetOrderDto>
        {
            private readonly UserAccessor _accessor;
            private readonly IBackgroundTaskQueue _backgroundTaskQueue;
            private readonly CurrencyService _currencyService;
            private readonly InvoiceFactory _invoiceFactory;
            private readonly LocationRepository _locationRepository;
            private readonly MailService _mailService;
            private readonly ProductRepository _productRepository;
            private readonly TeamupService _teamupService;
            private readonly InvoiceRepository _invoiceRepository;
            private readonly SettingsRepository _settingsRepository;


            public AcceptOrderCommandHandler(InvoiceRepository invoiceRepository, InvoiceFactory invoiceFactory,
                MailService mailService, IBackgroundTaskQueue backgroundTaskQueue, UserAccessor accessor,
                CurrencyService currencyService, SettingsRepository settingsRepository,
                LocationRepository locationRepository, ProductRepository productRepository,
                TeamupService teamupService)
            {
                _invoiceRepository = invoiceRepository;
                _invoiceFactory = invoiceFactory;
                _mailService = mailService;
                _backgroundTaskQueue = backgroundTaskQueue;
                _accessor = accessor;
                _currencyService = currencyService;
                _settingsRepository = settingsRepository;
                _locationRepository = locationRepository;
                _productRepository = productRepository;
                _teamupService = teamupService;
            }


            public async Task<OutputGetOrderDto> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
            {
                var payload = _accessor.BuildPayload();
                var order = await _invoiceRepository.FindOrderByOrderIdAsync(request.OrderId);

                ValidateOrder(order!);

                order.IsAccepted = true;

                var invoiceNr = await ReadInvoiceNr(payload.CompanyId);

                var invoiceViewModel = await InvoiceViewModelFactory(payload, order, invoiceNr);


                var mailViewModel = new SendInvoiceMailViewModel
                {
                    ImageUri = "https://workmate.blob.core.windows.net/images/Logo_email.png",
                    Mail = order.User.Identity.Mail,
                    Name = order.User.FirstName + " " + order.User.LastName,
                    ProductAmount = 1,
                    ProductImageUri = order.CartItems.FirstOrDefault().Product.ImageUri,
                    ProductPrice = order.CartItems.FirstOrDefault().Product.Price,
                    ProductTitle = order.CartItems.FirstOrDefault().Product.Name,
                    Subject = $"Ihre SpaceCloud.cc Bestellung vom {order.CreatedAt.ToShortDateString()}"
                };

                var result = await GenerateAndStorePdfAsync(invoiceViewModel, payload.CompanyId, invoiceNr);

                await AddSpaceCoins(order, payload.CompanyId);

                //Invoicing
                var invoice = new Invoice
                {
                    PdfUri = result.path,
                    InvoiceNr = invoiceNr,
                    Order = order
                };

                await _invoiceRepository.AddAsync(invoice);

                _backgroundTaskQueue.Enqueue(async stoppingToken =>
                {
                    var mailProxy = await _mailService.BuildMailAsync(mailViewModel, MailType.SendInvoice);
                    await mailProxy.AddAttachmentAsync(result.buffer, result.pdfName, stoppingToken);
                    await mailProxy.SendMail(stoppingToken);
                });

                return new OutputGetOrderDto
                {
                    FirstName = invoice.Order.User.FirstName,
                    LastName = invoice.Order.User.LastName,
                    UserId = invoice.Order.UserId,
                    Status = invoice.Order.Status,
                    OrderId = invoice.OrderId,
                    Price = order.GetPrice(),
                    Created = invoice.CreatedAt
                };
            }


            private async Task<int> ReadInvoiceNr(int companyId)
            {
                return await _settingsRepository.ReadAndUpdateInvoiceNrAsync(companyId);
            }

            private async Task<InvoiceViewModel> InvoiceViewModelFactory(UserModel payload, Order order, int invoiceNr)
            {
                //ViewModeling
                var settings = await _settingsRepository.GetSettingsByCompanyId(payload.CompanyId);
                var location = await _locationRepository.GetCompanyLocationByLocationIdAsync(payload.LocationId);

                var cartList = new List<InvoicePurchaseViewModel>();

                var orderdItems = order.CartItems.OrderBy(x => x.ProductId)
                    .GroupBy(x => x.ProductId);

                foreach (var group in orderdItems)
                {
                    var cartItem = group.FirstOrDefault();
                    string unit;
                    int serviceDuration;

                    var timeSpan = cartItem.EndDate - cartItem.StartDate;

                    var product = await _productRepository.ReadRoomByProductId(cartItem.ProductId);

                    var item = new InvoicePurchaseViewModel();

                    if (product != null)
                    {
                        item.TUnit = "pro Stunde";
                        item.ServiceDuration = timeSpan.Hours;
                        item.UnitPrice = product.Product.Price;
                        item.TProductName = product.Product.Name;
                        //await HandleTeamupService(product.ProductId, cartItem, order.User);
                    }
                    else
                    {
                        var desk = await _productRepository.ReadDeskByProductId(cartItem.ProductId);

                        unit = "per " + desk.IntervalType;

                        serviceDuration = desk.IntervalType switch
                        {
                            DeskIntervalType.Day => (int) timeSpan.TotalDays,
                            DeskIntervalType.Month => (int) timeSpan.TotalDays / 30,
                            DeskIntervalType.Quartal => (int) timeSpan.TotalDays / 90,
                            DeskIntervalType.Year => (int) timeSpan.TotalDays / 360,
                            DeskIntervalType.Custom => throw new NotImplementedException(),
                            _ => throw new NotImplementedException()
                        };

                        item.ServiceDuration = serviceDuration;
                        item.TProductName = desk.Product.Name;
                        item.TUnit = unit;
                        item.UnitPrice = desk.Product.Price;

                        if (desk.ProductId == 65)
                        {
                            item.ServiceDuration = 1;
                            item.TProductName = desk.Product.Name;
                            item.TUnit = "per Month";
                            item.UnitPrice = desk.Product.Price;
                        }
                    }


                    cartList.Add(item);
                }


                return new InvoiceViewModel
                {
                    TContactEmail = settings.ContactMail,
                    TContactName = "Moritz Bickel",
                    TContactPhone = settings.PhoneNumber,
                    TCoworkingAddress = location.Address,
                    TCoworkingBic = settings.BIC,
                    TUid = settings.Uid,
                    TCoworkingBankName = settings.BankName,
                    TCoworkingCity = location.City,
                    TCoworkingCountry = "Vorarlberg",
                    TCoworkingState = "Österreich",
                    TCoworkingIban = settings.Iban,
                    TInvoiceNr = invoiceNr.ToString(),
                    TCoworkingName = location.Company.Name,
                    TCoworkingPlz = location.Zip,
                    TUserAddress = "Musterstraße 36",
                    TUserCity = "Bregenz",
                    TUserCountry = "Vorarlberg",
                    TUserPlz = "6900",
                    TUserName = order.User.FirstName + " " + order.User.LastName,
                    TPurchase = cartList
                };
            }

            private static void ValidateOrder(Order order)
            {
                if (order is null)
                    throw new NotFoundException("order/accept", HttpMethod.Post, "order-id-not-found");
                if (order.IsAccepted)
                    throw new BadRequestException("order/accept", HttpMethod.Post, "order-already-accepted");
                if (order.IsDeclined)
                    throw new BadRequestException("order/accept", HttpMethod.Post, "order-already-declined");
            }

            private async Task AddSpaceCoins(Order order, int companyId)
            {
                var amountOfDesksInOrder = await _invoiceRepository.GetAmountOfSubscriptionInOrder(order);
                var settings = await _settingsRepository.GetSettingsByCompanyId(companyId);

                //SpaceCoins
                for (var i = 0; i < amountOfDesksInOrder * settings.CurrencyAmountPerSubscription; i++)
                    await _invoiceRepository.AddCoin(_currencyService.SpaceCoinFactory(order.UserId));
            }


            private async Task<(byte[] buffer, string path, string pdfName)> GenerateAndStorePdfAsync(
                InvoiceViewModel invoiceViewModel,
                int companyId, int invoiceNr)
            {
                var companyName = await _invoiceRepository.GetCompanyNameByCompanyIdAsync(companyId);
                var pdfName = await _invoiceFactory.CreateInvoiceName(companyName, invoiceNr);

                var parsedHtml = await _invoiceFactory.BuildInvoiceAsync(invoiceViewModel);
                return await _invoiceFactory.SaveHtmlAsPdfAsync(parsedHtml, pdfName);
            }


            private async Task HandleTeamupService(int productId, Cart cartItem, User user)
            {
                if (_accessor.BuildPayload().Tenant != "dev") return;

                var room = await _productRepository.ReadRoomByProductId(productId);
                if (room is null) return;

                var subId = await _teamupService.GetSubcalendarIdAsync(room.Product.Name);


                var result = await _teamupService.AddMeetingOnSubCalendarAsync(new MeetingModel
                {
                    notes = cartItem.Usage,
                    end_dt = cartItem.EndDate,
                    start_dt = cartItem.StartDate,
                    who = "",
                    subcalendar_id = subId,
                    title = "404 - NotImplementedYet"
                });

                if (!result.IsSuccessStatusCode)
                    throw new BadRequestException(nameof(HandleTeamupService), HttpMethod.Put, "Add-Meeting-Failed");
            }
        }
    }
}