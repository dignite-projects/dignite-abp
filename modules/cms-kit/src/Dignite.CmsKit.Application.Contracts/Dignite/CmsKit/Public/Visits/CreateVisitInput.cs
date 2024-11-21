using System;
using System.ComponentModel.DataAnnotations;
using Dignite.CmsKit.Visits;
using Volo.Abp.Validation;

namespace Dignite.CmsKit.Public.Visits;

[Serializable]
public class CreateVisitInput
{
    [Required]
    [DynamicMaxLength(typeof(VisitConsts), nameof(VisitConsts.MaxUserAgentLength))]
    public string UserAgent { get; set; }

    [Required]
    [DynamicMaxLength(typeof(VisitConsts), nameof(VisitConsts.MaxClientIpAddressLength))]
    public string ClientIpAddress { get; set; }

    /// <summary>
    /// Represents the length of time a user browsing
    /// </summary>
    [Required]
    public int Duration { get; set; } = 1;
}
