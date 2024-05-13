using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;

namespace RagApp.Services.CheshireCatService
{
    public interface ICheshireCatService : IDisposable
    {
        Task<FileUploadResult> sendFile(IBrowserFile file);
    }
}
