using Microsoft.AspNetCore.Mvc.Rendering;
using UmotaRedEye.Models.Domain;

namespace UmotaRedEye.Models.Dto
{
    public class MalzemeHareketViewModel : BaseViewModel
    {
        public IEnumerable<Malzeme> Malzemeler { get; set; }
        public IEnumerable<Depo> Depolar { get; set; }
        public IEnumerable<MalzemeHareket> MalzemeHareketler { get; set; }
        public int MalzemeId { get; set; }
        public int DepoId { get; set; }
        public string FisId { get; set; }
        public string Barcode { get; set; }

        #region Dropdownlar için Yardımcı Getter

        public IEnumerable<SelectListItem> DepolarList
        {
            get
            {
                List<SelectListItem> d = new List<SelectListItem>();
                d.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });

                if (Depolar != null)
                {
                    foreach (var item in Depolar.ToList())
                    {
                        d.Add(new SelectListItem { Text = item.DepoAdi, Value = item.Id.ToString() });
                    }

                }

                return d;
            }
        }

        public IEnumerable<SelectListItem> MalzemeList
        {
            get
            {
                List<SelectListItem> d = new List<SelectListItem>();
                d.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });

                if (Malzemeler != null)
                {
                    foreach (var item in Malzemeler.ToList())
                    {
                        d.Add(new SelectListItem { Text = String.Format("({0}) {1}", item.MalzemeKodu, item.MalzemeAdi), Value = item.Id.ToString() });
                    }

                }

                return d;
            }
        }

        #endregion
    }
}
