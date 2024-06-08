using JuntoSeguros.Domain.Dtos;

namespace JuntoSeguros.Domain.Entities.PersonEntity;

public class Person : Entity<Guid>
{
    public string Name { get; init; }
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; private set; }
    public void SetGender(Gender gender) => Gender = gender;

    public static explicit operator PersonDto(Person person)
    {
        return new PersonDto()
        {
            Id = person.Id,
            Name = person.Name,
            Gender = person.Gender,
            BirthDate = person.BirthDate,
        };
    }
}