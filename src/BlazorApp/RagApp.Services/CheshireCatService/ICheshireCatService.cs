using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RagApp.Services.CheshireCatService
{
    public interface ICheshireCatService : IDisposable
    {
        Task<FileUploadResult> sendFile(IFormFile file);
    }
}
