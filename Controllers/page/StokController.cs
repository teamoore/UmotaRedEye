using Microsoft.AspNetCore.Mvc;
using UmotaRedEye.Models.Domain;
using UmotaRedEye.Models.Dto;
using UmotaRedEye.Service;

namespace UmotaRedEye.Controllers
{
    public class StokController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MalzemeService _malzemeService;
        private readonly DepoService _depoService;
        private readonly MalzemeHareketService _malzemeHareketService;

        public StokController(ILogger<HomeController> logger, IConfiguration configuration)
        {     
            _logger = logger;
            _malzemeService = new MalzemeService(configuration);
            _depoService = new DepoService(configuration);
            _malzemeHareketService = new MalzemeHareketService(configuration);
        }

        public async Task<IActionResult> StokGiris(MalzemeHareketViewModel model)
        {
            if (model == null)
                model = new MalzemeHareketViewModel();

            if (string.IsNullOrEmpty(model.FisId))
                model.FisId = Guid.NewGuid().ToString();
            else
                model.MalzemeHareketler = await _malzemeHareketService.GetMalzemeHareketList(model.FisId);
            
            model.Depolar = await _depoService.GetDepoList();
            model.Malzemeler = await _malzemeService.GetMalzemeList();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMalzemeHareket(MalzemeHareketViewModel model)
        {
            var mh = new MalzemeHareket(model.DepoId, model.MalzemeId, model.Barcode, 1, model.FisId);
            await _malzemeHareketService.Save(mh);

            return RedirectToAction("StokGiris",model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteMalzemeHareket(string id,string fisId)
        {
            await _malzemeHareketService.Delete(id);
            var model = new MalzemeHareketViewModel();
            model.FisId = fisId;

            return RedirectToAction("StokGiris",model);
        }

    }
}
