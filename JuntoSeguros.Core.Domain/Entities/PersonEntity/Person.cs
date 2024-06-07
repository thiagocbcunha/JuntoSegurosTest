namespace JuntoSeguros.Domain.Entities;

public class Person
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime BirthDate { get; }
    public virtual Gender Gender { get; }
}