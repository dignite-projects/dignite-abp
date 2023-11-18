using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.CmsKit.Public.Visits;

[Serializable]
public class CreateVisitInput
{
    [Required]
    public string UserAgent { get; set; }

    [Required]
    public string ClientIpAddress { get; set; }

    /// <summary>
    /// Represents the length of time a user browsing
    /// </summary>
    [Required]
    public int Duration { get; set; } = 1;
}
