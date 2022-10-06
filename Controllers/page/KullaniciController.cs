using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UmotaRedEye.Models.Domain;
using UmotaRedEye.Models.Dto;
using UmotaRedEye.Service;

namespace UmotaRedEye.Controllers.page
{
    public class KullaniciController : Controller
    {
        private readonly KullaniciService _kullaniciService;
        private readonly DepoService _depoService;
        public KullaniciController(IConfiguration _configuration)
        {
            _kullaniciService = new KullaniciService(_configuration);
            _depoService = new DepoService(_configuration);
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
            return View(model);
        }

        public async Task<IActionResult> KullaniciListesi()
        {
            var kl = await _kullaniciService.GetKullaniciList();

            var model = new KullaniciListesiViewModel();
            model.kullaniciList = kl;

            return View(model);
        }

        public async Task<IActionResult> KullaniciEdit(int kullaniciId)
        {
            var model = new KullaniciEditViewModel();
            model.kullanici = await _kullaniciService.GetKullanici(kullaniciId);
            List<SelectListItem> depolar = new List<SelectListItem>();
            var depolist = await _depoService.GetDepoList();

            depolar.Add(new SelectListItem { Text = "", Value = "0" });
            foreach (var item in depolist.ToList())
            {   
                depolar.Add(new SelectListItem { Text = item.DepoAdi, Value = item.Id.ToString() });
            }

            ViewBag.Depolar = depolar;
            return View(model);
        }

        public async Task<IActionResult> KullaniciAdd()
        {
            var model = new KullaniciEditViewModel();
            model.kullanici = new Kullanici();

            List<SelectListItem> depolar = new List<SelectListItem>();
            var depolist = await _depoService.GetDepoList();

            depolar.Add(new SelectListItem { Text = "", Value = "0" });
            foreach (var item in depolist.ToList())
            {
                depolar.Add(new SelectListItem { Text = item.DepoAdi, Value = item.Id.ToString() });
            }

            ViewBag.Depolar = depolar;

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveKullanici(KullaniciEditViewModel model)
        {
            var r = _kullaniciService.SaveKullanici(model.kullanici);

            return RedirectToAction("KullaniciListesi");
        }

        [HttpPost]
        public IActionResult AddKullanici(KullaniciEditViewModel model)
        {
            var r = _kullaniciService.AddKullanici(model.kullanici);

            return RedirectToAction("KullaniciListesi");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }
    }
}
