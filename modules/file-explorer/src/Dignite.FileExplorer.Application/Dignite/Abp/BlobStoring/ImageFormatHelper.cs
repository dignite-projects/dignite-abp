﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;

namespace Dignite.Abp.BlobStoring;

public class ImageFormatHelper
{
    public static readonly ICollection<IImageFormat> AllowedImageUploadFormats = new Collection<IImageFormat>
        {
            JpegFormat.Instance,
            PngFormat.Instance,
            GifFormat.Instance,
            BmpFormat.Instance,
            WebpFormat.Instance,
        };

    public static string AllowedImageFormatsJoint => string.Join(",", AllowedImageUploadFormats.Select(x => x.Name));


    public static bool IsValidImage(string mimeType, ICollection<IImageFormat> validFormats)
    {
        try
        {
            return validFormats.Any(img => img.MimeTypes.Any(mt => mimeType.Equals(mt)));
        }
        catch
        {
            return false;
        }
    }
}