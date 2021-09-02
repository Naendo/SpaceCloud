using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class InitializeDbCommand : IRequest
    {
        public class InitalizeDbCommandHandler : IRequestHandler<InitializeDbCommand>
        {
            private readonly TestRepository _repository;

            public InitalizeDbCommandHandler(TestRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(InitializeDbCommand request, CancellationToken cancellationToken)
            {
                var result = await _repository.GetAsync();
                if (result is null)
                    throw new BadRequestException("test/init", HttpMethod.Post, "db-already-initialized");

                var user = new User
                {
                    FirstName = "Init",
                    LastName = "Init",
                    Identity = new Identity
                    {
                        Hash = "Init",
                        Salt = Guid.NewGuid().ToString(),
                        Mail = "init",
                        Role = UserType.Worker
                    },
                    Location = new CompanyLocation
                    {
                        Address = "Init",
                        City = "Init",
                        Zip = "Init",
                        Company = new Company
                        {
                            Name = "InitCompany",
                            Settings = new CompanySettings
                            {
                                Iban = "AT64 1298 1298 8199",
                                Uid = "1892379812739",
                                LogoUri =
                                    "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/corporate-company-logo-design-template-2402e0689677112e3b2b6e0f399d7dc3_screen.jpg?ts=1561532453",
                                PhoneNumber = "+43 129 123 912",
                                StartingInvoiceNr = new Random().Next(10000, 100000),
                                WebsiteUri = "https://spacecloud.cc"
                            }
                        }
                    }
                };

                await _repository.AddAsync(user);


                return Unit.Value;
            }
        }
    }
}