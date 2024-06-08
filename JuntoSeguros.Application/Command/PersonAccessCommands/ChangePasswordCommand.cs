namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class ChangePasswordCommand(Guid personId, string userName, string password, string lastPassword, DateTime lastChange, bool actived) : BaseCommand(personId, userName, lastPassword, lastChange, actived)
{
    public string NewPassword { get; private set; } = password;
}