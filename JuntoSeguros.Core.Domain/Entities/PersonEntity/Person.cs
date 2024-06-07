namespace JuntoSeguros.Domain.Entities.PersonEntity;

public class Person : Entity<Guid>
{
    public string Name { get; init; }
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; private set; }
    public void SetGender(Gender gender) => Gender = gender;
}