using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Domain.Dtos;

public class PersonAccessDto : MongoEntity
{
    public Guid PersonId { get; set; }
    public bool Actived { get; set; }
    public string UserName { get; set; }
    public DateTime CreateDate { get; set; }
    public string EncryptedPass { get; set; }

    public static explicit operator PersonAccess(PersonAccessDto personAccessDto)
    {
        var personAccessEvent = new PersonAccessEvent(personAccessDto.Actived, personAccessDto.EncryptedPass, personAccessDto.CreateDate);
        var personAccess = new PersonAccess(personAccessDto.UserName, personAccessEvent);
        personAccess.SetId(personAccessDto.Id);

        return personAccess;
    }
}
