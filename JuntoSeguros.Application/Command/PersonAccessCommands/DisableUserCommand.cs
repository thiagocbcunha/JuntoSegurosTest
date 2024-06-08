using MediatR;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class DisableUserCommand(Guid personId, string userName, string password, DateTime lastChange) : BaseCommand(personId, userName, password, lastChange)
{
}