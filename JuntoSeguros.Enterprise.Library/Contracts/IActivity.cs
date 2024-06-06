namespace JuntoSeguros.Enterprise.Library.Contracts;

public interface IActivity: IDisposable
{
    ITag? Tag { get; }
}