using FrontEndServer.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using System;

namespace FrontEndServer.Server.Data.Services.GuidGenerator
{
    public class GuidGenerator : IGuidGenerator
    {
        private readonly string _generatedGuid;

        public GuidGenerator()
        {
            _generatedGuid = Guid.NewGuid().ToString();
        }
        public string GenerateAndValidateGuid(IResponseCookies cookies)
        {
            string guid = Guid.NewGuid().ToString();
            Validate();
            cookies.Append("UserUUID", guid, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(30)
            });
            return guid;
        }

        public string GetGeneratedGuid() => _generatedGuid;

        public string ReadValidateAndSetGuidCookie(IResponseCookies cookies)
        {
            string guid = GetGeneratedGuid();

            cookies.Append("UserUUID", guid, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(30)
            });
            return guid;
        }

        // TODO: Implement this methodS
        private void Validate() { }
        
    }
}
