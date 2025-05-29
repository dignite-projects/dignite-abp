namespace Dignite.Abp.TenantDomainManagement;

public enum DomainStatus
{
    /// <summary>
    /// 待验证
    /// </summary>
    PendingVerification,

    /// <summary>
    /// 活跃
    /// </summary>
    Active,

    /// <summary>
    /// 未激活
    /// </summary>
    Inactive,

    /// <summary>
    /// 已暂停
    /// </summary>
    Suspended,

    /// <summary>
    /// 验证失败
    /// </summary>
    VerificationFailed
}
