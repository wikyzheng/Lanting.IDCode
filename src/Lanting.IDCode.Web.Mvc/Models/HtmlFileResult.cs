using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lanting.IDCode.Web.Commons
{
    public class HtmlFileResult : IActionResult
    {
        public HtmlFileResult(string filePath, string contentType, string antiCode)
        {
            FilePath = filePath;
            ContentType = contentType;
            AntiCode = antiCode;
        }
        public string ContentType { get; private set; }
        public string FilePath { get; private set; }
        public string AntiCode { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;
            response.ContentType = "text/html";

            //using (var fileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
            //{
            //    await fileStream.CopyToAsync(context.HttpContext.Response.Body);
            //}
            var fileContent = await File.ReadAllTextAsync(FilePath);
            fileContent = fileContent.Replace("{系统生成}", AntiCode);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(fileContent);
            await response.Body.WriteAsync(buffer, 0, buffer.Length);
            await response.Body.FlushAsync();
        }

    }
}