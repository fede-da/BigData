using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace RagApp.Services
{
    public interface IFileService
    {
        Task SaveFileAsync(IFormFile file, string path);
        public FileStream GetFile(string path);
    }
}
