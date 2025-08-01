namespace Dignite.Publisher.BlobStoring;
public static class PublisherBlobHelper
{
    /// <summary>
    /// Generates the URL for a post cover image with specified dimensions.
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetPostCoverUrl(string blobName, int width = 412, int height = 308)
    {
        return $"/api/file-explorer/files/{PublisherBlobContainerConsts.CoverBlobsContainerName}/{blobName}?width={width}&height={height}";
    }

    /// <summary>
    /// Generates the URL for a content image with specified width.
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="width"></param>
    /// <returns></returns>
    public static string GetContentImageUrl(string blobName, int width = 760)
    {
        return $"/api/file-explorer/files/{PublisherBlobContainerConsts.ContentImagesBlobContainerName}/{blobName}?width={width}";
    }


    public static string GetVideoUrl(string blobName)
    {
        return $"/api/file-explorer/files/{PublisherBlobContainerConsts.VideosBlobContainerName}/{blobName}";
    }
}
