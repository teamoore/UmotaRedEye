using System.Data.SqlClient;
using System.Data;
using UmotaRedEye.Models.Domain;
using Dapper;

namespace UmotaRedEye.Service
{
    public class MalzemeService
    {
        private readonly string _connectionString;

        public MalzemeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UmotaSalkimDb");
        }

        public async Task<IEnumerable<Malzeme>> GetMalzemeList()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.QueryAsync<Malzeme>("select Id,MalzemeKodu,MalzemeAdi from Malzeme with (nolock)");

                conn.Close();

                return result;
            }
        }
    }
}
