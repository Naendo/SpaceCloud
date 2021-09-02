using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Coworking.Application.Interfaces;

namespace Coworking.Application.Services
{
    public class HashService : IHashService
    {
        private readonly Encoding _encoding;

        private readonly string _pepper;

        public HashService(string pepper)
        {
            _pepper = pepper;
            _encoding = Encoding.UTF8;
        }

        public Guid Salt => Guid.NewGuid();


        public HashModel EncryptPasswordWithoutGivenSalt(string password)
        {
            using (var hash = SHA256.Create())
            {
                var credentials = new HashModel
                {
                    Password = password,
                    Salt = CreateSalt()
                };

                credentials.Password = ConvertByteArrayToString(hash.ComputeHash(Combine(credentials)));
                return credentials;
            }
        }

        public bool TryEncryptPasswordWithoutGivenSalt(string password, out HashModel? hashedCredentials)
        {
            try
            {
                using var hash = SHA256.Create();
                var credentials = new HashModel
                {
                    Password = password,
                    Salt = CreateSalt()
                };

                hashedCredentials = new HashModel
                {
                    Password = ConvertByteArrayToString(hash.ComputeHash(Combine(credentials))),
                    Salt = credentials.Salt
                };
                return true;
            }
            catch (Exception)
            {
                hashedCredentials = default;
                return false;
            }
        }


        public HashModel EncryptPasswordWithGivenSalt(HashModel credentials)
        {
            using var hash = SHA256.Create();
            credentials.Password = ConvertByteArrayToString(hash.ComputeHash(Combine(credentials)));
            return credentials;
        }

        public bool TryEncryptPasswordWithGivenSalt(HashModel credentials, out HashModel? hashedCredentials)
        {
            try
            {
                using var hash = SHA256.Create();
                hashedCredentials = new HashModel
                {
                    Password = ConvertByteArrayToString(hash.ComputeHash(Combine(credentials))),
                    Salt = credentials.Salt
                };
                return true;
            }
            catch (Exception)
            {
                hashedCredentials = null;
                return false;
            }
        }


        private Guid CreateSalt()
        {
            return Guid.NewGuid();
        }

        private byte[] Combine(HashModel credentials)
        {
            return credentials.Salt.ToByteArray()
                .Concat(_encoding.GetBytes(credentials.Password))
                .Concat(_encoding.GetBytes(_pepper)).ToArray();
        }

        private static string ConvertByteArrayToString(byte[] array)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < array.Length; i++) sb.Append(array[i].ToString("x2"));

            return sb.ToString();
        }
    }
}