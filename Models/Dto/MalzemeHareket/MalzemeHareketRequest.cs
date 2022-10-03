namespace UmotaRedEye.Models.Dto
{
    public class MalzemeHareketRequest : BaseRequest
    {
        public string Barcode { get; private set; }
        public string MalzemeKodu { get; private set; }
        public int DepoId { get; private set; }
        public decimal Miktar { get; private set; }

        public MalzemeHareketRequest(string barcode, string malzemeKodu, int depoId, decimal miktar)
        {
            Barcode = barcode;
            MalzemeKodu = malzemeKodu;
            Miktar = miktar;
            DepoId = depoId;
        }
    }
}
