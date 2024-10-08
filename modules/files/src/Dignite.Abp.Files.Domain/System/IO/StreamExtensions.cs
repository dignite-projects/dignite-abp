using System.Security.Cryptography;
using System.Text;

namespace System.IO;

public static class StreamExtensions
{
    public static string Md5(this Stream stream)
    {
        var md5 = MD5.Create();
        md5.ComputeHash(stream);
        byte[] b = md5.Hash;
        md5.Clear();
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < b.Length; i++)
        {
            sb.Append(b[i].ToString("X2"));
        }

        //Ensure that the starting position of the data flow is 0
        if (stream.Position > 0)
        {
            stream.Position = 0;
        }

        return sb.ToString();
    }
}