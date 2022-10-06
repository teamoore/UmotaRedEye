using Dapper;
using System.Data;
using System.Data.SqlClient;
using UmotaRedEye.Models.Domain;

namespace UmotaRedEye.Service
{
    public class KullaniciService
    {
        private readonly string _connectionString;
        public KullaniciService(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UmotaSalkimDb");
        }
        public Kullanici GetKullaniciByLogin(string email, string sifre)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = conn.QueryFirstOrDefault<Kullanici>("select Id,Adi,Soyadi,Email,Sifre,DepoId,Admin,OlusturmaTarihi from Kullanici where Email = @email and Sifre = @sifre", new { email = email, sifre = sifre });

                conn.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Kullanici>> GetKullaniciList()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.QueryAsync<Kullanici>("select Id,Adi,Soyadi,Email,Sifre,DepoId,Admin,OlusturmaTarihi from Kullanici");
                conn.Close();

                return result;
            }
        }

        public async Task<Kullanici> GetKullanici(int id)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.QueryFirstOrDefaultAsync<Kullanici>("select Id,Adi,Soyadi,Email,Sifre,DepoId,Admin,OlusturmaTarihi from Kullanici where Id = @id", new { id = id });

                conn.Close();

                return result;
            }
        }
        public async Task<bool> AddKullanici(Kullanici kullanici)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.ExecuteAsync("insert into Kullanici (Adi,Soyadi,Email,Sifre,Status,Admin,OlusturmaTarihi,DepoId) " +
                    "values (@Adi,@Soyadi,@Email,@Sifre,@Status,@Admin,getdate(),@DepoId)",
                    kullanici);

                conn.Close();

                return result > 0;
            }
        }
        public async Task<bool> SaveKullanici(Kullanici kullanici)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.ExecuteAsync("update Kullanici set Adi = @Adi,Soyadi = @Soyadi,Email = @Email,Sifre = @Sifre,DepoId = @DepoId,Admin = @Admin where Id = @Id",kullanici);

                conn.Close();

                return result > 0;
            }
        }
    }
}
