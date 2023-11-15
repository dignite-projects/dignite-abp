using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.CmsKit;

public class CmsKitTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public Guid User2Id { get; } = Guid.NewGuid();

    public string EntityType1 { get; } = "EntityName1";

    public string EntityType2 { get; } = "EntityName2";

    public string EntityId1 { get; } = "1";

    public string EntityId2 { get; } = "2";
}
