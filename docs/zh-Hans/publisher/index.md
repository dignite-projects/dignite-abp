## 创建新的PostType

1. 在 Domain.Shared 项目中配置 `GlobalFeatures` 和 `Features`
2. 在 Application.Contracts 项目中，继承 `IPostDeserializer` 接口，开发反序列化器
3. 在 Admin.HttpApi 项目中，继承 `IPostAdminDeserializer` 接口，开发反序列化器
4. 在 Admin.Application 项目中，继承 `IPostBuilder` 接口，开发创建或更新Post的Builder