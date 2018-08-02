using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lanting.IDCode.Web.Commons
{
    public class HtmlFileResult : IActionResult
    {
        public HtmlFileResult(string filePath, string contentType)
        {

            FilePath = filePath;
            ContentType = contentType;
        }
        public string ContentType { get; private set; }
        public string FilePath { get; private set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;
            response.ContentType = "text/html";
            using (var fileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
            {
                await fileStream.CopyToAsync(context.HttpContext.Response.Body);
            }
        }

    }
}