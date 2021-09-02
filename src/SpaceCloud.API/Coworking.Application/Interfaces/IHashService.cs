using System;
using Coworking.Application.Services;

namespace Coworking.Application.Interfaces
{
    public interface IHashService
    {
        Guid Salt { get; }
        HashModel EncryptPasswordWithGivenSalt(HashModel credentials);
        HashModel EncryptPasswordWithoutGivenSalt(string password);
        bool TryEncryptPasswordWithGivenSalt(HashModel credentials, out HashModel? hashedCredentials);
        bool TryEncryptPasswordWithoutGivenSalt(string password, out HashModel? hashedCredentials);
    }
}