using Microsoft.Data.SqlClient;
using System.Data;

namespace AplicacaoEscola.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}
