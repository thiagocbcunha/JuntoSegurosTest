using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Domain.Dtos;

public class PersonDto : MongoEntity
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }

    public static explicit operator Person(PersonDto personDto)
    {
        var person = new Person()
        {
            Name = personDto.Name,
            BirthDate = personDto.BirthDate,
        };

        person.SetId(personDto.Id);
        person.SetGender(personDto.Gender);

        return person;
    }
}