

using Dignite.CmsKit.BlobStoring;

namespace Dignite.CmsKit.Public.Web.Helpers;

public static class BlobHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoBlobName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetBrandLogoUrl(string logoBlobName, int? width = null, int? height = null)
    {
        return GetBlobUrl(BlobContainerConsts.BrandLogoContainerName, logoBlobName, width, height);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logoReverseBlobName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetBrandLogoReverseUrl(string logoReverseBlobName, int? width = null, int? height = null)
    {
        return GetBlobUrl(BlobContainerConsts.BrandLogoContainerName, logoReverseBlobName, width, height);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iconBlobName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetBrandIconUrl(string iconBlobName, int? width = null, int? height = null)
    {
        return GetBlobUrl(BlobContainerConsts.BrandLogoContainerName, iconBlobName, width, height);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetBlobUrl(string containerName, string blobName, int? width = null, int? height = null)
    {
        var url = $"/api/file-explorer/files/{containerName}/{blobName}";
        if (width.HasValue || height.HasValue)
        {
            url += "?";
        }
        if (width.HasValue)
        {
            url += $"width={width}";
        }
        if (height.HasValue)
        {
            if (width.HasValue)
            {
                url += "&";
            }
            url += $"height={height}";
        }

        return url;
    }
}
