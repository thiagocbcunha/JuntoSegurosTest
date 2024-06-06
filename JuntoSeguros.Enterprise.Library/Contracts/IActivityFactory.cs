namespace JuntoSeguros.Enterprise.Library.Contracts;

public interface IActivityFactory
{
    ITag? Tag { get; }
    IActivity Start(string identify);
}