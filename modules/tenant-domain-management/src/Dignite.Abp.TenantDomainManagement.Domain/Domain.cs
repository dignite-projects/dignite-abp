using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantDomainManagement
{
    /// <summary>
    /// 
    /// </summary>
    public class Domain : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        #region 构造函数

        protected Domain()
        {

        }

        public Domain(Guid id, string name, string business, Guid? tenantId)
            : base(id)
        {
            Business = Check.NotNullOrWhiteSpace(business, nameof(business), TenantDomainConsts.MaxBusinessLength);
            SetName(name);
            TenantId = tenantId;
        }

        #endregion

        #region 租户信息

        /// <summary>
        /// 租户标识
        /// </summary>
        public Guid? TenantId { get; }

        #endregion

        /// <summary>
        /// 业务类型:  Host|Website|Travely|...
        /// </summary>
        public string Business { get; protected set; }

        /// <summary>
        /// 域名信息
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 域名状态
        /// </summary>
        public DomainStatus Status { get; protected set; }

        /// <summary>
        /// 是否是主要的
        /// </summary>
        public bool IsPrimary { get; set; }

        public void SetName(string name)
        {
            Name =
                Check.NotNullOrWhiteSpace(name, nameof(name), TenantDomainConsts.MaxNameLength);
        }
    }
}
