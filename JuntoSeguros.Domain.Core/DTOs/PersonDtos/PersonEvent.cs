namespace JuntoSeguros.Domain.Core.DTOs.PersonDtos;

public class PersonEvent : Event
{
    public Guid PersonId { get; set; }
    public int VersionNum { get; set; }
    public Gender Gender { get; set; }
}