using Volo.CmsKit.Entities;

namespace Dignite.CmsKit.Visits;
public class VisitConsts
{
    public static int MaxEntityTypeLength { get; set; } = CmsEntityConsts.MaxEntityTypeLength;

    public static int MaxEntityIdLength { get; set; } = CmsEntityConsts.MaxEntityIdLength;
    public static int MaxClientIpAddressLength { get; set; } = 64;

    public static int MaxBrowserInfoLength { get; set; } = 512;
    public static int MaxDeviceInfoLength { get; set; } = 128;
}
