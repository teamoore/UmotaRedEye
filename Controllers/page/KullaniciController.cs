using Microsoft.AspNetCore.Mvc;
using UmotaRedEye.Models.Domain;
using UmotaRedEye.Models.Dto;
using UmotaRedEye.Service;

namespace UmotaRedEye.Controllers.page
{
    public class KullaniciController : Controller
    {
        private readonly KullaniciService _kullaniciService;
        public KullaniciController(IConfiguration _configuration)
        {
            _kullaniciService = new KullaniciService(_configuration);
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestViewModel request)
        {
            var k = _kullaniciService.GetKullaniciByLogin(request.Email, request.Sifre);
            if (k != null)
            {
                HttpContext.Session.SetString("kullaniciId", k.Id.ToString());
                HttpContext.Session.SetString("fullName", String.Format("{0} {1}", k.Adi, k.Soyadi));
                return RedirectToAction("StokGiris", "Stok");
            }
            else
            {
                request.ErrorMessage = "Kullanıcı adı ve/veya şifre hatalı girdiniz";
                return RedirectToAction("LoginPage",request);
            }
                

        }

        public IActionResult LoginPage(LoginRequestViewModel model)
        {        
            ViewBag.KullaniciId = HttpContext.Session.GetString("kullaniciId");
            return View(model);
        }

        public async Task<IActionResult> KullaniciListesi()
        {
            var kl = await _kullaniciService.GetKullaniciList();

            var model = new KullaniciListesiViewModel();
            model.kullaniciList = kl;

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
