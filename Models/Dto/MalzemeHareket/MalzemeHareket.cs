namespace UmotaRedEye.Models.Dto
{
    public class MalzemeHareket
    {
        public int Id { get; set; }
        public int DepoId { get; set; }
        public int Depo2Id { get; set; }
        public string DepoAdi { get; set; }
        public string Depo2Adi { get; set; }
        public int FisTuru { get; set; }
        public string Barcode { get; set; }
        public string MalzemeKodu { get; set; }
        public decimal Miktar { get; set; }
        public int OlusturanKullaniciId { get; set; }
        public DateTime FisTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }
}
