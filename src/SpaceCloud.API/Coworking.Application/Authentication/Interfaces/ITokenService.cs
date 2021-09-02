namespace Coworking.Application.Authentication
{
    public interface ITokenService
    {
        OutputAuthorizeDto CreateToken(JwtPayloadModel payloadModel);
    }
}