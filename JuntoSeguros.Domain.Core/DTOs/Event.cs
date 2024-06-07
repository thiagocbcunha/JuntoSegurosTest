namespace JuntoSeguros.Domain.Core.DTOs;

public abstract class Event
{
    public string CreateBy { get; init; }
    public DateTime CreateDate { get; init; }
}