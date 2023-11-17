using Volo.CmsKit.Entities;

namespace Dignite.CmsKit.Visits;
public class VisitConsts
{
    public static int MaxEntityTypeLength { get; set; } = CmsEntityConsts.MaxEntityTypeLength;

    public static int MaxEntityIdLength { get; set; } = CmsEntityConsts.MaxEntityIdLength;
    public static int MaxClientIpAddressLength { get; set; } = 15;

    public static int MaxUserAgentLength { get; set; } = 256;
}
