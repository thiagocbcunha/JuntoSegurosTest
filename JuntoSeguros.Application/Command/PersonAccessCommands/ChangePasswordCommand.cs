namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class ChangePasswordCommand(Guid personId, string Email, string password, string lastPassword, DateTime CreateDate, bool actived) : BaseCommand(personId, Email, lastPassword, CreateDate, actived)
{
    public string NewPassword { get; private set; } = password;
}