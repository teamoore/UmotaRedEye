namespace UmotaRedEye.Models.Domain
{
    public class MalzemeHareket
    {
        public int Id { get; set; }
        public int MalzemeId { get; set; }
        public int DepoId { get; set; }
        public int? Depo2Id { get; set; }
        public int FisTuru { get; set; }
        public string FisId { get; set; }
        public string Barcode { get; set; }
        public decimal Miktar { get; set; }
        public int OlusturanKullaniciId { get; set; }
        public DateTime? FisTarihi { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }

        public MalzemeHareket()
        {

        }

        public MalzemeHareket(int depoId, int malzemeId, string barcode, int olusturanKullaniciId, string fisId)
        {
            DepoId = depoId;
            Barcode = barcode;
            MalzemeId = malzemeId;
            Miktar = 1;
            OlusturanKullaniciId = olusturanKullaniciId;
            OlusturmaTarihi = DateTime.Now;
            FisId = fisId;
        }
    }
}
