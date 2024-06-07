using JuntoSeguros.Infra.Contracts;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JuntoSeguros.Infra.Connection
{
    public class DapperConnectionFactory(IConfiguration configuration) : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            return new SqlConnection(configuration.GetConnectionString("JuntoSegurosOnboarding"));
        }
    }
}
