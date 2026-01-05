namespace Dignite.Abp.UserPoints;

public enum UserPointAccountStatus
{
    /// <summary>
    /// 正常
    /// </summary>
    Active = 1,

    /// <summary>
    /// 冻结（不可交易）
    /// </summary>
    Frozen = 2,

    /// <summary>
    /// 禁用（违规等）
    /// </summary>
    Disabled = 3
}
