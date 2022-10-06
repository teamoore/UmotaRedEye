using Microsoft.AspNetCore.Mvc;

namespace UmotaRedEye.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Authenticate(HttpContext httpContext)
        {
            if (httpContext == null)
                return Ok();

            var kid = httpContext.Session.GetString("kullaniciId");

            if (kid == null)
                return RedirectToAction("LoginPage", "Kullanici");

            return Ok();
        }
    }
}
