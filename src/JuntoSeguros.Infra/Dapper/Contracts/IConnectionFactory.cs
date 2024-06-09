using System.Data;

namespace JuntoSeguros.Infra.Dapper.Contracts;

public interface IConnectionFactory
{
    IDbConnection Connection();
}