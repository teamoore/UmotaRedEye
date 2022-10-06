using System.Data.SqlClient;
using System.Data;
using UmotaRedEye.Models.Domain;
using Dapper;

namespace UmotaRedEye.Service
{
    public class MalzemeHareketService
    {
        private readonly string _connectionString;

        public MalzemeHareketService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UmotaSalkimDb");
        }

        public async Task<IEnumerable<MalzemeHareket>> GetMalzemeHareketList(string fisId)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "select * from MalzemeHareket where FisId = '" + fisId + "'";

                var result = await conn.QueryAsync<MalzemeHareket>(sql);

                conn.Close();

                return result;
            }
        }

        public async Task<bool> Save(MalzemeHareket malzemeHareket)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.ExecuteAsync("insert into MalzemeHareket (FisId,DepoId,FisTuru,Barcode,MalzemeId,Miktar,FisTarihi,OlusturanKullaniciId,OlusturmaTarihi) values (@FisId,@DepoId,@FisTuru,@Barcode,@MalzemeId,@Miktar,@FisTarihi,@OlusturanKullaniciId,@OlusturmaTarihi)", malzemeHareket);

                conn.Close();

                return result == 1;
            }
        }

        public async Task<bool> Delete(string Id)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.ExecuteAsync("delete from MalzemeHareket where Id = @Id", new {Id = Id});

                conn.Close();

                return result == 1;
            }
        }
    }
}
