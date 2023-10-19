using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;
public class GetMyOrdersInput: PagedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? EndTime { get; set; }
}
