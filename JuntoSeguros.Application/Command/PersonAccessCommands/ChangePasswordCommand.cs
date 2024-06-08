using MediatR;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class ChangePasswordCommand(Guid personId, string userName, string password, string lastPassword, DateTime lastChange) : BaseCommand(personId, userName, lastPassword, lastChange)
{
    public string NewPassword { get; private set; } = password;
}