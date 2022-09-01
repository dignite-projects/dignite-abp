using System.Security.Cryptography;

namespace System.IO;

public static class StreamExtensions
{
    public static string ToMd5(this Stream stream)
    {
        using (var md5 = MD5.Create())
        {
            var position = stream.Position;
            var hash = md5.ComputeHash(stream);


            if (stream.CanSeek)
            {
                stream.Position = position;
            }

            var base64String = Convert.ToBase64String(hash);
            return base64String;
        }
    }
}
