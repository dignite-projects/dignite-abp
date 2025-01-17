﻿using System;
using System.ComponentModel.DataAnnotations;
using Dignite.CmsKit.Visits;
using Volo.Abp.Validation;

namespace Dignite.CmsKit.Public.Visits;

[Serializable]
public class CreateVisitInput
{
    [DynamicMaxLength(typeof(VisitConsts), nameof(VisitConsts.MaxBrowserInfoLength))]
    public string? BrowserInfo { get; set; }

    [DynamicMaxLength(typeof(VisitConsts), nameof(VisitConsts.MaxDeviceInfoLength))]
    public string? DeviceInfo { get; set; }

    [Required]
    [DynamicMaxLength(typeof(VisitConsts), nameof(VisitConsts.MaxClientIpAddressLength))]
    public string? ClientIpAddress { get; set; }

    /// <summary>
    /// Represents the length of time a user browsing
    /// </summary>
    [Required]
    public int Duration { get; set; } = 1;
}
