using Microsoft.AspNetCore.Antiforgery;
using Lanting.IDCode.Controllers;

namespace Lanting.IDCode.Web.Host.Controllers
{
    public class AntiForgeryController : IDCodeControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
