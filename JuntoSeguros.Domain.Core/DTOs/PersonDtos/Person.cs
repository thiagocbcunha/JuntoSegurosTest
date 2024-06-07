namespace JuntoSeguros.Domain.Core.DTOs.PersonDtos;

public class Person: TEntity<Guid>
{
    public string Name { get; init; }
    public DateTime BirthDate { get; init; }
    public virtual List<PersonEvent> PersonEvents { get; init; }
}