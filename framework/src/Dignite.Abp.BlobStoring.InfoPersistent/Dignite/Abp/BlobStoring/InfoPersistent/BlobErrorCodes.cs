namespace Dignite.Abp.BlobStoring.InfoPersistent;

public static class BlobStoringErrorCodes
{
    public const string UserNotAuthorized = "Dignite.Abp.BlobStoring:010001";
    public const string IncompatibleFileType = "Dignite.Abp.BlobStoring:010002";
    public const string CanNotGetFileType = "Dignite.Abp.BlobStoring:010003";
    public const string ImageSizeShouldNotBeLessThanPreset = "Dignite.Abp.BlobStoring:010004";
}
