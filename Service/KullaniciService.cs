using Dapper;
using System.Data;
using System.Data.SqlClient;
using UmotaRedEye.Models.Domain;

namespace UmotaRedEye.Service
{
    public class KullaniciService
    {
        private readonly IConfiguration _configuration;
        public KullaniciService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void AddUser(Kullanici kullanici)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("UmotaSalkimDb")))
            {
                conn.Open();

                conn.Execute("insert into Kullanici (Adi,Soyadi,Email,Sifre,Status,Admin,OlusturmaTarihi,DepoId) " +
                    "values (@Adi,@Soyadi,@Email,@Sifre,@Status,@Admin,getdate(),@DepoId)",
                    kullanici);

                conn.Close();
            }
        }

        public Kullanici GetKullaniciByLogin(string email, string sifre)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("UmotaSalkimDb")))
            {
                conn.Open();

                var result = conn.QueryFirstOrDefault<Kullanici>("select Id,Adi,Soyadi,Email,Sifre,DepoId,Admin,OlusturulmaTarihi from Kullanici where Email = @email and Sifre = @sifre",new {email = email, sifre = sifre});

                conn.Close();

                return result;
            }
        }
    }
}
