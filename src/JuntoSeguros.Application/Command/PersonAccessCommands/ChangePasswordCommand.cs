namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class ChangePasswordCommand(Guid personId, string email, string password, string newPassword, DateTime CreateDate, bool actived) : BaseCommand(personId, email, password, CreateDate, actived)
{
    public string NewPassword { get; set; } = newPassword;
}