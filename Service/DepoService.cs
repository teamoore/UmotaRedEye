using Dapper;
using System.Data;
using System.Data.SqlClient;
using UmotaRedEye.Models.Domain;
using UmotaRedEye.Models.Dto.Depo;


namespace UmotaRedEye.Service
{
    public class DepoService
    {
        private readonly string _connectionString;
        public DepoService(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UmotaSalkimDb");
        }

        public async Task<IEnumerable<Depo>> GetDepoList()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.QueryAsync<Depo>("select Id,DepoAdi from Depo with(nolock)");
                conn.Close();

                return result;
            }
        }

    }
}
