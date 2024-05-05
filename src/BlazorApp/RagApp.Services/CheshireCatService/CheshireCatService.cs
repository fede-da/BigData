using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace RagApp.Services.CheshireCatService
{
    public class CheshireCatService : ICheshireCatService
    {
        IHttpClientFactory _clientFactory;

        public CheshireCatService(IHttpClientFactory clientFactory){
        _clientFactory = clientFactory;
        }

        public async Task<FileUploadResult> sendFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new System.ArgumentException("File is null or empty.");
            }

            using var httpClient = _clientFactory.CreateClient();
            using var form = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();
            using var content = new StreamContent(fileStream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = file.FileName
            };
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(content);

            var response = await httpClient.PostAsync("http://127.0.0.1:5000/forward-file", form);
            var readContent = await response.Content.ReadAsStringAsync();

            return new FileUploadResult
            {
                Success = response.IsSuccessStatusCode,
                Message = response.IsSuccessStatusCode ? "File uploaded successfully" : "Failed to upload file",
                StatusCode = (int)response.StatusCode,
                Content = readContent
            };
        }
        void System.IDisposable.Dispose() { }
    }
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }

}
