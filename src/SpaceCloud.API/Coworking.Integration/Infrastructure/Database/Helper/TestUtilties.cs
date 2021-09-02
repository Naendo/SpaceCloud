using System.Net.Http;
using System.Text;
using Coworking.Application.Interfaces;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using Coworking.Infrastructure.Persistence;
using Newtonsoft.Json;

namespace Coworking.Integration.Tests.Infrastructure
{
    public static class TestUtilties
    {
        public const string TEST_ORDERER_KEY = "TestOrder_";


        public static StringContent InitializeContent<T>(T content) where T : class
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }


        public static void InitializeDbForTests(WorkingContext context, IHashService hashService)
        {
            //(1) Add Company
            var company = new Company
            {
                Name = "TestCompany"
            };

            context.Companies.Add(company);
            context.SaveChanges();
            //(2) Add Location

            var location = new CompanyLocation
            {
                Address = "Test",
                City = "Test",
                Zip = "TestZip",
                CompanyId = company.CompanyId
            };
            context.CompanyLocations.Add(location);
            context.SaveChanges();

            var locationId = location.LocationId;


            //(3) Add User with Admin-permissions
            var user = new User
            {
                LocationId = locationId,
                FirstName = "test",
                LastName = "test"
            };

            var credentials = hashService.EncryptPasswordWithoutGivenSalt("test");

            var identity = new Identity
            {
                Hash = credentials.Password,
                Mail = "test",
                Salt = credentials.SaltWithStringFormat,
                Role = UserType.Administrator,
                StayLogged = true
            };

            user.Identity = identity;

            context.Users.Add(user);
            context.SaveChanges();


            //(5) Add Tickets
            context.Tickets.Add(new Ticket
            {
                UserId = user.UserId,
                Subject = "TestTicket",
                Content = "TestTicket"
            });
            context.SaveChanges();


            //(6) Add Rooms
            context.Rooms.Add(new Room
            {
                Capacity = 1,
                Product = new Product
                {
                    Description = "testroom",
                    ImageUri = "testuri",
                    IsEnabled = true,
                    LastModifiedByUserId = user.UserId,
                    Name = "nice test",
                    LocationId = locationId,
                    Price = 10
                }
            });

            context.SaveChanges();


            InitalizeTestEntity();

            void InitalizeTestEntity()
            {
                TestEntity.Identity = identity;
                TestEntity.User = user;
                TestEntity.Company = company;
                TestEntity.Location = location;
            }
        }
    }
}