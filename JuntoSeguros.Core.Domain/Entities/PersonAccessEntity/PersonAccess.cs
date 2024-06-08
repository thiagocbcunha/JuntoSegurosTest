using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Exceptions;

namespace JuntoSeguros.Domain.Entities.PersonAccessEntity;

public class PersonAccess(string userName, PersonAccessEvent personAccessEvent) : Entity<Guid>
{
    public Guid PersonId => Id;
    public bool Changed { get; private set; }
    public string UserName { get; init; } = userName;
    public PersonAccessEvent PersonAccessEvent { get; init; } = personAccessEvent;

    public void Enable()
    {
        Changed = PersonAccessEvent.Actived != true;
        PersonAccessEvent.Enable();
    }
    public void Disable()
    {
        Changed = PersonAccessEvent.Actived != true;
        PersonAccessEvent.Disable();
    }
    public void ChangePassword(string value)
    {
        if (!PersonAccessEvent.Actived)
            throw new BusinessException("User not actived.");

        if ((DateTime.Now - PersonAccessEvent.CreateDate).Days < 30)
            throw new BusinessException("User alredy chanded password before 30 days.");

        if (value == PersonAccessEvent.EncryptedPass)
            throw new BusinessException("New password is the same old password.");

        PersonAccessEvent.SetPassword(value);

        Changed = true;
    }

    public static explicit operator PersonAccessDto(PersonAccess personAccess)
    {
        return new PersonAccessDto()
        {
            Id = personAccess.PersonId,
            UserName = personAccess.UserName,
            PersonId = personAccess.PersonId,
            Actived = personAccess.PersonAccessEvent.Actived,
            CreateDate = personAccess.PersonAccessEvent.CreateDate,
            EncryptedPass = personAccess.PersonAccessEvent.EncryptedPass,
        };
    }
}