using System.Security.Cryptography;
using System.Text;

namespace PlanBee.University_portal.backend.Domain.Utils;

public static class Encryptor
{
    public static string Md5Hash(this string text)
    {
        //compute hash from the bytes of text
        var result = MD5.HashData(Encoding.ASCII.GetBytes(text));

        var strBuilder = new StringBuilder();
        foreach (var b in result)
            //change it into 2 hexadecimal digits
            //for each byte
            strBuilder.Append(b.ToString("x2"));

        return strBuilder.ToString();
    }
}