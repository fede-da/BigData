using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace RagApp.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath;

        public FileService()
        {
            // string iWebHostEnvironment
            _basePath = "iWebHostEnvironment";
        }

        public async Task SaveFileAsync(IFormFile file, string path)
        {
            var filePath = Path.Combine(_basePath, path);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public FileStream GetFile(string path)
        {
            var filePath = Path.Combine(_basePath, path);
            return new FileStream(filePath, FileMode.Open);
        }

        // Implement other methods similarly
    }

}
