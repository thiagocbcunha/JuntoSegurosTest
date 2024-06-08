using JuntoSeguros.Enterprise.Library.Contracts;
using System.Text;
using System.Security.Cryptography;

namespace JuntoSeguros.Enterprise.Library.Security;

public class EnterpriseSecurity : IEnterpriseSecurity
{
    public string GetHash(string value)
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
