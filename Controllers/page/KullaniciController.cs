using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
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

                var claims = new List<Claim>();
                var claim = new Claim(ClaimTypes.NameIdentifier, request.Email);
                claims.Add(claim);
                claim = new Claim(ClaimTypes.Name, request.Email);
                claims.Add(claim);

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                  principal,
                                                                  authProperties);


                string returnUrl = HttpContext.Request.Query["ReturnUrl"];
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
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

        [Authorize]
        public async Task<IActionResult> KullaniciListesi()
        {
            var kl = await _kullaniciService.GetKullaniciList();

            var model = new KullaniciListesiViewModel();
            model.kullaniciList = kl;

            return View(model);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public IActionResult SaveKullanici(KullaniciEditViewModel model)
        {
            var r = _kullaniciService.SaveKullanici(model.kullanici);

            return RedirectToAction("KullaniciListesi");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddKullanici(KullaniciEditViewModel model)
        {
            var r = _kullaniciService.AddKullanici(model.kullanici);

            return RedirectToAction("KullaniciListesi");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }
    }
}
