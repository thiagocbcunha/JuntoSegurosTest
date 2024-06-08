namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class ChangePasswordCommand(Guid personId, string Email, string password, string lastPassword, DateTime lastChange, bool actived) : BaseCommand(personId, Email, lastPassword, lastChange, actived)
{
    public string NewPassword { get; private set; } = password;
}