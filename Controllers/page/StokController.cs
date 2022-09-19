using Microsoft.AspNetCore.Mvc;

namespace UmotaRedEye.Controllers
{
    public class StokController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public StokController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult StokGiris()
        {
            return View();
        }

    }
}
