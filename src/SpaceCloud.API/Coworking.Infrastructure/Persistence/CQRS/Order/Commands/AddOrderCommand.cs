using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.Services;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using MediatR;

namespace Coworking.Infrastructure
{
    public class AddOrderCommand : IRequest
    {
        public AddOrderCommand(IEnumerable<InputAddOrderDto> items)
        {
            Items = items;
        }

        public IEnumerable<InputAddOrderDto> Items { get; }

        public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
        {
            private readonly OrderRepository _repository;
            private readonly MailService _mailService;
            private readonly IBackgroundTaskQueue _backgroundTaskQueue;
            private readonly ITokenGenerator _tokenGenerator;
            private readonly UserRepository _userRepository;
            private readonly UserAccessor _userAccessor;

            public AddOrderCommandHandler(OrderRepository repository, UserAccessor userAccessor,
                MailService mailService,
                IBackgroundTaskQueue backgroundTaskQueue,
                ITokenGenerator tokenGenerator,
                UserRepository userRepository)
            {
                _repository = repository;
                _mailService = mailService;
                _backgroundTaskQueue = backgroundTaskQueue;
                _tokenGenerator = tokenGenerator;
                _userRepository = userRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();
                var order = new Order
                {
                    UserId = payload.UserId,
                    OrderNr = _tokenGenerator.GenerateOrderNr()
                };

                await _repository.AddAsync(order);

                await _repository.AppendCartToOrderAsync(request.Items.Select(x => new Cart
                {
                    ProductId = x.ProductId,
                    Usage = x.Usage,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    OrderId = order.OrderId
                }));

                var user = await _userRepository.ReadUserById(payload.UserId);

                _backgroundTaskQueue.Enqueue(async x =>
                {
                    var mail = await _mailService.BuildMailAsync(new BookingCreatedMailViewModel
                    {
                        FirstName = user.FirstName,
                        ImageUri = "https://workmate.blob.core.windows.net/images/Logo_email.png",
                        LastName = user.LastName,
                        Mail = user.Identity.Mail,
                        OrderNr = order.OrderNr,
                        OrderUri = "",
                        Subject = "SpaceCloud | Order from " + DateTime.Now.ToShortDateString(),
                    }, MailType.BookingCreated);

                    await mail.SendMail(x);
                });


                return Unit.Value;
            }
        }
    }
}