﻿using Xunit;



namespace Dignite.Abp.TenantDomains.MongoDB;
[Collection(MongoTestCollection.Name)]
public class MongoDBTenantDomainRepository_Tests : TenantDomainRepository_Tests<AbpTenantDomainsMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
