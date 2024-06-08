using MediatR;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class BaseCommand(Guid personId, string email, string password, DateTime createDate, bool actived, bool isNew = false) : IRequest
{
    protected bool IsNew { get; set; } = isNew;
    public bool Actived { get; set; } = actived;
    public Guid PersonId { get; set; } = personId;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public DateTime CreateDate { get; set; } = createDate;

    public static explicit operator PersonAccess(BaseCommand command)
    {
        var personAccess = new PersonAccess(command.Email, new PersonAccessEvent(command.Actived, command.Password, command.CreateDate), command.IsNew);
        personAccess.SetId(command.PersonId);

        return personAccess;
    }
}