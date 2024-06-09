using System.Data;
using System.Data.SqlClient;
using JuntoSeguros.Infra.Dapper.Contracts;
using Microsoft.Extensions.Configuration;

namespace JuntoSeguros.Infra.Dapper.Connection
{
    public class DapperConnectionFactory(IConfiguration configuration) : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            return new SqlConnection(configuration.GetConnectionString("JuntoSegurosOnboarding"));
        }
    }
}
