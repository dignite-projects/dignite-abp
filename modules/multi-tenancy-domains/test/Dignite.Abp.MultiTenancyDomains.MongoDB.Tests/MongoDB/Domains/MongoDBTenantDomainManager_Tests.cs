using Xunit;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBTenantDomainManager_Tests : TenantDomainManager_Tests<AbpMultiTenancyDomainsMongoDbTestModule>
{

}
