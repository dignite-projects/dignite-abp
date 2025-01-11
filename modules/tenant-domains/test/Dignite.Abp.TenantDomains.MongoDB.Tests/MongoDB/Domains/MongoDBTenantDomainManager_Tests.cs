using Xunit;

namespace Dignite.Abp.TenantDomains.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBTenantDomainManager_Tests : TenantDomainManager_Tests<AbpTenantDomainsMongoDbTestModule>
{

}
