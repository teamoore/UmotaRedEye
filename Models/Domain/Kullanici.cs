namespace UmotaRedEye.Models.Domain
{
    public class Kullanici
    {
        public int Id { get; set; }
        public int DepoId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public int Status { get; set; }
        public bool Admin { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime SonGirisTarihi { get; set; }

        public Kullanici()
        {

        }

        public Kullanici(int id,string adi,string soyadi,string email, string sifre,bool admin)
        {
            this.Id = id;
            this.Adi = adi;
            this.Soyadi = soyadi;
            this.Email = email;
            this.Sifre = sifre;
            this.Admin = admin;
            this.OlusturmaTarihi = DateTime.Now;
        }
    }
}
