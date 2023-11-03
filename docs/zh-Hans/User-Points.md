# 用户积分模块

````json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
````

用户积分模块是基于 [积分核心](Points.md) 模块构建的，它是一个完整的 Abp 应用模块，旨在管理用户积分和积分消费。

## 安装

- 将 `Dignite.Abp.UserPoints.Domain.Shared` Nuget 包安装到 `Domain.Shared` 项目中

    添加 `UserPointsDomainSharedModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.UserPoints.Domain` Nuget 包安装到 Domain 项目中

    添加 `UserPointsDomainModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

{{if DB == "EF"}}

- 将 `Dignite.Abp.UserPoints.EntityFrameworkCore` Nuget 包安装到 EntityFrameworkCore 项目中

    添加 `UserPointsEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureUserPoints()` 到 `OnModelCreating()` 方法中:

    ```csharp
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureUserPoints(); //Add this line to configure the Abp.UserPoints Module
    }
    ```

    打开 Visual Studio 的 包管理控制台 选择 `DbMigrations` 做为默认项目. 然后编写以下命令为文档模块添加迁移.

    ```csharp
    add-migration Add_AbpUserPoints_Module
    ```

    现在更新数据库

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

- 将 `Dignite.Abp.UserPoints.MongoDB` Nuget 包安装到 MongoDB 项目中

    添加 `UserPointsEntityMongoDBModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

    添加 `builder.ConfigureUserPoints()` 到 `OnModelCreating()` 方法中:

    ```csharp
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureUserPoints(); //Add this line to configure the Abp.UserPoints Module
    }
    ```

{{end}}

- 将 `Dignite.Abp.UserPoints.Application.Contracts` Nuget 包安装到 Application.Contracts 项目中

    添加 `UserPointsApplicationContractsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.UserPoints.Application` Nuget 包安装到 Application 项目中

    添加 `UserPointsApplicationModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.UserPoints.HttpApi` Nuget 包安装到 HttpApi 项目中

    添加 `UserPointsHttpApiModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.UserPoints.HttpApi.Client` Nuget 包安装到 HttpApi 项目中

    添加 `UserPointsHttpApiClientModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

## 用户积分选项

`DignitePointsBlockOptions`是用户积分的选项类。

- Factor:
  积分价值兑换系数，通常可以理解为 1 元货币可以兑换多少积分，默认值为1。
  用户积分的单位积分值必须是积分系数的倍数，例如：将积分系数设置为100，用户积分数可以是100、200、300等100的倍数。

示例代码：

```csharp
Configure<DignitePointsBlockOptions>(options =>
{
    options.Factor = 100;
});
```

## 用户积分管理

`UserPointsItemManager` 类提供了一系列方法管理用户积分：

- 创建用户积分

  ```csharp
  Task<UserPointsItem> CreateAsync(
        PointsType pointsType,
        string pointsDefinitionName,
        string pointsWorkflowName,
        int points,
        DateTime expirationDate,
        Guid userId,
        Guid? tenantId)
  ```

  - pointsType:用户积分的类型，是一个枚举值，包含两个值：
    - General：通用积分，所有的通用积分可合并在一起消费。
    - Specialized：专用积分，在消费积分时需要指定积分工作流。
  - pointsDefinitionName:用户积分的积分定义名称，该名称即[定义积分](Points.md#定义积分)的`Name`.
  - pointsWorkflowName:用户积分的积分工作流名称，该名称即[定义积分](Points.md#定义积分)的`PointsWorkflow`的`Name`.
  - points:用户积分值
  - expirationDate:积分到期时间
  - userId:积分的所属用户Id
  - tenantId:积分的所属租户Id

- 删除用户积分

  ```csharp
  Task DeleteAsync(UserPointsItem pointsItem)
  ```

## 用户积分消费管理

`UserPointsOrderManager` 类型提供一系列方法管理用户积分消费：

- 创建积分订单
  
  ```csharp
  Task<UserPointsOrder> CreateAsync(
        int points,
        string businessOrderType,
        string businessOrderNumber, 
        Guid userId,
        PointsType pointsType = PointsType.General, 
        string pointsDefinitionName = null, 
        string pointsWorkflowName = null,
        Guid? tenantId=null)
  ```
  
  - points:消费积分值
  - businessOrderType:业务订单类型
  - businessOrderNumber:业务订单编号
  - userId:积分消费用户Id
  - pointsType:消费积分的类型，是一个枚举值，包含两个值：
    - General：通用积分，消费积分类型为`General`的积分。
    - Specialized：专用积分，必须指定积分定义名称和积分工作流名称。
  - pointsDefinitionName:消费积分的积分定义名称，该名称即[定义积分](Points.md#定义积分)的`Name`.
  - pointsWorkflowName:消费积分的积分工作流名称，该名称即[定义积分](Points.md#定义积分)的`PointsWorkflow`的`Name`.
  - tenantId:消费积分的所属租户Id

- 删除积分订单

  ```csharp
  Task DeleteAsync(UserPointsOrder pointsOrder, bool shouldRollbackPoints)
  ```

  - pointsOrder:积分订单实体对象
  - shouldRollbackPoints:是否退还积分

- 按业务订单号查询积分订单

  ```csharp
  Task<UserPointsOrder> FindByBusinessOrderAsync(string businessOrderType, string businessOrderNumber)
  ```

  - businessOrderType:业务订单类型
  - businessOrderNumber:业务订单编号

## APIs

- 获取用户可用积分数量
  
  ```csharp
  Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input)
  ```
  
  - ExpirationDate[可选]:积分有效期
  - PointsDefinitionName[可选]:积分定义名称
  - PointsWorkflowName[可选]:积分工作流名称

  如没有设定`PointsDefinitionName`和`PointsWorkflowName`的值，则获取所有通用积分

- 获取用户积分明细列表
  
  ```csharp
  Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input)
  ```
  
  - StartTime[可选]:查询创建积分的起始时间
  - EndTime[可选]:查询创建积分的截止时间
  - PointsDefinitionName[可选]:积分定义名称
  - PointsWorkflowName[可选]:积分工作流名称

  如没有设定`PointsDefinitionName`和`PointsWorkflowName`的值，则获取所有通用积分
  