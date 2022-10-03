using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UmotaRedEye.Models;
using UmotaRedEye.Models.Dto;

namespace UmotaRedEye.Controllers.api
{
    public class StokDataController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public StokDataController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]   
        public ServiceResponse SaveStok([FromBody]BarcodeRequest barcodeRequest)
        {
            try
            {
                var turkishProduct = barcodeRequest.Barcode.StartsWith("869");
                ResponseType responseType = turkishProduct ? ResponseType.Success : ResponseType.Error;

                return new ServiceResponse(responseType, "Barkod kaydedildi", barcodeRequest.Barcode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse(ResponseType.Error, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Accepted();
        }
    }
}
