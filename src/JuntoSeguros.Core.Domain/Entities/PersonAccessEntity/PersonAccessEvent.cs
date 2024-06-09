namespace JuntoSeguros.Domain.Entities.PersonAccessEntity;

public class PersonAccessEvent(bool actived, string encryptedPass, DateTime createDate)
{
    public bool Actived { get; private set; } = actived;
    public DateTime CreateDate { get; private set; } = createDate;
    public string EncryptedPass { get; private set; } = encryptedPass;

    public void Enable() => Actived = true;
    public void Disable() => Actived = false;
    public void SetPassword(string encryptedPass) => EncryptedPass = encryptedPass;
}