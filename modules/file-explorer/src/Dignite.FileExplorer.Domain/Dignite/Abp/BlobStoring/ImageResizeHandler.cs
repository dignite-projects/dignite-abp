using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Imaging;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Resize uploaded images to fit predefined values
/// </summary>
public class ImageResizeHandler : IFileHandler, ITransientDependency
{
    private readonly IImageResizer _imageResizer;
    private readonly IImageCompressor _imageCompressor;

    public ImageResizeHandler(IImageResizer imageResizer, IImageCompressor imageCompressor)
    {
        _imageResizer = imageResizer;
        _imageCompressor = imageCompressor;
    }

    public async Task ExecuteAsync(FileHandlerContext context)
    {
        var configuration = context.ContainerConfiguration.GetImageResizeConfiguration();

        if (ImageFormatHelper.IsValidImage(context.BlobStream, ImageFormatHelper.AllowedImageUploadFormats))
        {
            using (Image image = await Image.LoadAsync(context.BlobStream))
            {
                context.BlobStream.Position = 0;
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
                    var resizeResult = await _imageResizer.ResizeAsync(
                        context.BlobStream,
                        new ImageResizeArgs(configuration.ImageWidth, configuration.ImageHeight, ImageResizeMode.Max),
                        context.File.MimeType
                        );
                    if (resizeResult.State == ImageProcessState.Done)
                    {
                        context.BlobStream = resizeResult.Result;
                    }
                    else
                    {
                        if (resizeResult.Result is not null && context.BlobStream != resizeResult.Result && resizeResult.Result.CanRead)
                        {
                            context.BlobStream = resizeResult.Result;
                        }
                        else
                        {
                            throw new BusinessException(
                                code: "Dignite.Abp.BlobStoring:010006",
                                message: resizeResult.State.ToString()
                            );
                        }
                    }
                }

                //Compressed image
                var compressResult = await _imageCompressor.CompressAsync(
                    context.BlobStream, /* A stream object that represents an image */
                    mimeType: context.File.MimeType
                );

                if (compressResult.State == ImageProcessState.Done)
                {
                    context.BlobStream = compressResult.Result;
                }
                else
                {
                    if (compressResult.Result is not null && context.BlobStream != compressResult.Result && compressResult.Result.CanRead)
                    {
                        context.BlobStream = compressResult.Result;
                    }
                    else
                    {
                        throw new BusinessException(
                            code: "Dignite.Abp.BlobStoring:010006",
                            message: compressResult.State.ToString()
                        );
                    }
                }
            }
        }
    }
}