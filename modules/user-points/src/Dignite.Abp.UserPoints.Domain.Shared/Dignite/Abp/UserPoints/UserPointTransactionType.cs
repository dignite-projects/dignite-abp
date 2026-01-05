namespace Dignite.Abp.UserPoints;

/// <summary>
/// 积分交易类型
/// </summary>
public enum UserPointTransactionType
{
    /// <summary>
    /// 获得积分（用户正常获取）
    /// 如：购物、签到、完成任务、邀请好友等
    /// </summary>
    Earn = 1,

    /// <summary>
    /// 消费积分（用户主动使用）
    /// 如：兑换商品、抵扣订单、购买权益等
    /// </summary>
    Spend = 2,

    /// <summary>
    /// 积分过期（系统自动）
    /// 超过有效期的积分自动失效
    /// </summary>
    Expire = 3,

    /// <summary>
    /// 管理员调整（人工操作）
    /// 如：补偿、纠错、活动奖励等
    /// 可正可负，需要记录操作人和原因
    /// </summary>
    Adjust = 4,

    /// <summary>
    /// 退回积分（业务回滚）
    /// 如：订单退款、取消兑换、撤销交易等
    /// </summary>
    Refund = 5,

    /// <summary>
    /// 冻结（临时锁定）
    /// 如：订单待确认期间、争议处理期间
    /// 不影响总余额，但不可用
    /// </summary>
    Freeze = 6,

    /// <summary>
    /// 解冻（恢复可用）
    /// 冻结积分解除锁定
    /// </summary>
    Unfreeze = 7,

    /// <summary>
    /// 系统回收（惩罚性扣除）
    /// 如：用户违规、作弊被扣除积分
    /// </summary>
    Reclaim = 8,

    /// <summary>
    /// 清零（账户重置）
    /// 如：年度清零、账户注销等
    /// </summary>
    Clear = 9
}
