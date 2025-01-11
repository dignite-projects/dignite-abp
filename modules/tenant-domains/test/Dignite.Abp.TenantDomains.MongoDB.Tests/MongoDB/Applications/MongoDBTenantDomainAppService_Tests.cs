using Dignite.Abp.TenantDomains.MongoDB;
using Xunit;

namespace Dignite.Abp.TenantDomains.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBTenantDomainAppService_Tests : TenantDomainAppService_Tests<AbpTenantDomainsMongoDbTestModule>
{

}
