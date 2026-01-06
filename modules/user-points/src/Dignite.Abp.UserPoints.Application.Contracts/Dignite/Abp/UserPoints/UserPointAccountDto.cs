using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Serializable]
public class UserPointAccountDto:EntityDto<Guid>
{

    public string PointTypeName { get; set; }

    public string DisplayName { get; set; }

    /// <summary>
    /// 当前可用余额（快照字段，避免每次聚合计算）
    /// </summary>
    public int CurrentBalance { get; set; }

    /// <summary>
    /// 冻结余额（预留扣减、争议处理等）
    /// </summary>
    public int FrozenBalance { get; set; }

    /// <summary>
    /// 累计获得（生命周期内总获得）
    /// </summary>
    public int TotalEarned { get; set; } = 0;

    /// <summary>
    /// 累计消费（生命周期内总消费）
    /// </summary>
    public int TotalSpent { get; set; } = 0;

    /// <summary>
    /// 累计过期（生命周期内总过期）
    /// </summary>
    public int TotalExpired { get; set; } = 0;

    /// <summary>
    /// 最后交易时间
    /// </summary>
    public DateTime? LastTransactionTime { get; set; }

    /// <summary>
    /// 账户状态
    /// </summary>
    public UserPointAccountStatus Status { get; set; }
}
