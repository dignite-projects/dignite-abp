using System.Security.Cryptography;

namespace System.IO;

public static class StreamExtensions
{
    public static string Md5(this Stream stream)
    {
        //Ensure that the starting position of the data flow is 0
        if (stream.Position > 0)
        {
            stream.Position = 0;
        }

        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(stream);
            stream.Position = 0;
            var base64String = Convert.ToBase64String(hash);
            return base64String;
        }
    }
}