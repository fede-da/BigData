using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;


namespace RagApp.Services.CheshireCatService
{
    public class CheshireCatService : ICheshireCatService
    {
        IHttpClientFactory _clientFactory;

        public CheshireCatService(IHttpClientFactory clientFactory){
        _clientFactory = clientFactory;
        }

        public async Task<FileUploadResult> sendFile(IBrowserFile file)
        {
            if (file == null || file.Size == 0)
            {
                throw new System.ArgumentException("File is null or empty.");
            }
            // Replace below with current ip address before sending the request
            string localMachineIpAddress = "192.168.1.56";
            string port = "5000";
            using var httpClient = _clientFactory.CreateClient();
            using var form = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();
            using var content = new StreamContent(fileStream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = file.Name
            };
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(content);

            var flaskUrl = $"http://{localMachineIpAddress}:{port}/api/forward-file";
            var response = await httpClient.PostAsync(flaskUrl, form);
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
