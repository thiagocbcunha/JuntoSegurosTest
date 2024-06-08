namespace JuntoSeguros.Enterprise.Library.Contracts;

public interface IEnterpriseSecurity
{
    string GetHash(string value);
}