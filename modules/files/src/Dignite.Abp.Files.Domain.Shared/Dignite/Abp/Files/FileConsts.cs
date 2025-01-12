namespace Dignite.Abp.Files;

public static class FileConsts
{
    /// <summary>
    /// Default value: 64
    /// </summary>
    public static int MaxContainerNameLength { get; set; } = 64;

    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxBlobNameLength { get; set; } = 256;

    /// <summary>
    /// Default value: 128
    /// </summary>
    public static int MaxNameLength { get; set; } = 128;

    /// <summary>
    /// Default value: 128
    /// </summary>
    public static int MaxMimeTypeLength { get; set; } = 128;

    /// <summary>
    /// Default value: 32
    /// </summary>
    public static int MaxMd5Length { get; set; } = 32;
}