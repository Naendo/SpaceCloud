using System;

namespace Coworking.Application
{
    public interface ITokenGenerator
    {
        (string Token, DateTime Expires) GenerateAndSetPasswordResetToken();

        string GenerateSchedulerAccessToken();

        string GenerateOrderNr();
    }
}