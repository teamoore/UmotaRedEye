using Microsoft.AspNetCore.Mvc;
using UmotaRedEye.Models.Domain;
using UmotaRedEye.Models.Dto;
using UmotaRedEye.Service;

namespace UmotaRedEye.Controllers.page
{
    public class KullaniciController : Controller
    {
        private IConfiguration Configuration;
        private KullaniciService _kullaniciService;
        public KullaniciController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _kullaniciService = new KullaniciService(Configuration);
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestViewModel request)
        {
            var k = _kullaniciService.GetKullaniciByLogin(request.Email, request.Sifre);
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
