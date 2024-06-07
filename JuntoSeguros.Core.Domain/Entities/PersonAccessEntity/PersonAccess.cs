using System.Text;
using System.Security.Cryptography;
using JuntoSeguros.Domain.Exceptions;

namespace JuntoSeguros.Domain.Entities.PersonAccessEntity;

public class PersonAccess(string userName, bool actived, string encryptedPass, DateTime createDate, bool changed = true) : Entity<Guid>
{
    public Guid PersonId => Id;
    public string UserName { get; init; } = userName;
    public bool Changed { get; private set; } = changed;
    public bool Actived { get; private set; } = actived;
    public DateTime CreateDate { get; private set; } = createDate;
    public string EncryptedPass { get; private set; } = encryptedPass;

    public void Enable()
    {
        Changed = Actived != true;
        Actived = true;
    }
    public void Disable()
    {
        Changed = Actived != false;
        Actived = false;
    }
    public void ChangePassword(string value)
    {
        if (!Actived)
            throw new BusinessException("User not actived");

        if ((DateTime.Now - CreateDate).Days < 30)
            throw new BusinessException("User alredy chanded password before 30 days.");

        var newPass = EncondingPass(value);

        if (newPass == EncryptedPass)
            throw new BusinessException("New password is the same.");

        EncryptedPass = newPass;

        Changed = true;
    }

    private string EncondingPass(string value)
    {
        var tmpSource = ASCIIEncoding.ASCII.GetBytes(value);
        return ByteArrayToString(new MD5CryptoServiceProvider().ComputeHash(tmpSource));
    }

    private string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length - 1; i++)
            sOutput.Append(arrInput[i].ToString("X2"));

        return sOutput.ToString();
    }
}