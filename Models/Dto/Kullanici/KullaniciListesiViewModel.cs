using UmotaRedEye.Models.Domain;

namespace UmotaRedEye.Models.Dto
{
    public class KullaniciListesiViewModel : BaseViewModel
    {
        public IEnumerable<Kullanici> kullaniciList { get; set; }
    }
}
