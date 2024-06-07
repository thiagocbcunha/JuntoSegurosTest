using System.Data;

namespace JuntoSeguros.Infra.Contracts;

public interface IConnectionFactory
{
    IDbConnection Connection();
}