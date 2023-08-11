using FrontEndServer.Shared.Dtos;

namespace FrontEndServer.Server.Data.Services.GuidGenerator
{
    public interface IGuidGenerator
    {
        string GenerateAndValidateGuid(IResponseCookies cookies);

        string ReadValidateAndSetGuidCookie(IResponseCookies cookies);
        string GetGeneratedGuid();

    }
}
