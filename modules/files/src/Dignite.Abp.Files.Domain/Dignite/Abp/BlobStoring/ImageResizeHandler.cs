using System.Threading.Tasks;
using Dignite.Abp.Files;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Resize uploaded images to fit predefined values
/// </summary>
public class ImageResizeHandler : IBlobHandler, ITransientDependency
{
    private readonly ICurrentFile _currentFile;

    public ImageResizeHandler(ICurrentFile currentFile)
    {
        _currentFile = currentFile;
    }

    public async Task ExecuteAsync(BlobHandlerContext context)
    {
        var position = context.BlobStream.Position;
        var configuration = context.ContainerConfiguration.GetImageResizeConfiguration();

        if (ImageFormatHelper.IsValidImage(context.BlobStream, ImageFormatHelper.AllowedImageUploadFormats))
        {
            // IsValidImage may change the position of the stream
            if (context.BlobStream.CanSeek)
            {
                context.BlobStream.Position = position;
            }

            using (Image image = await Image.LoadAsync(context.BlobStream))
            {

                if (configuration.ImageSizeMustBeLargerThanPreset)
                {
                    if (image.Width < configuration.ImageWidth || image.Height < configuration.ImageHeight)
                    {
                        throw new BusinessException(
                            code: "Dignite.Abp.BlobStoring:010004",
                            message: "Image size must be larger than Preset!",
                            details: "Uploaded image must be larger than: " + configuration.ImageWidth + "x" + configuration.ImageHeight
                        );
                    }
                }

                if (image.Width > configuration.ImageWidth || image.Height > configuration.ImageHeight)
                {
                    image.Metadata.ExifProfile = null;
                    image.Metadata.XmpProfile = null;
                    image.Mutate(x =>
                    {
                        x.Resize(new ResizeOptions()
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(configuration.ImageWidth, configuration.ImageHeight)
                        });
                    });

                    var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder()
                    {
                        Quality = 90
                    };

                    context.BlobStream.Position = 0;
                    image.Save(context.BlobStream, encoder);

                    // Length of clipping stream
                    context.BlobStream.SetLength(context.BlobStream.Position);
                    context.BlobStream.Position = 0;

                    _currentFile.File.Resize(context.BlobStream.Length);
                }
            }
        }
    }
}