using Dignite.Abp.MultiTenancyDomains.MongoDB;
using Xunit;

namespace Dignite.Abp.MultiTenancyDomains.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBTenantDomainAppService_Tests : TenantDomainAppService_Tests<AbpMultiTenancyDomainsMongoDbTestModule>
{

}
