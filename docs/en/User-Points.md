# User Points Module

```json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
```

The User Points Module is built upon the [Points Core](Points.md) module. It is a complete Abp application module designed for managing user points and point consumption.

## Installation

- Install the `Dignite.Abp.UserPoints.Domain.Shared` NuGet package in the `Domain.Shared` project. Add `UserPointsDomainSharedModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.UserPoints.Domain` NuGet package in the Domain project. Add `UserPointsDomainModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

{{if DB == "EF"}}

- If you are using Entity Framework Core (EF), install the `Dignite.Abp.UserPoints.EntityFrameworkCore` NuGet package. Add `builder.ConfigureUserPoints()` to the `OnModelCreating()` method:

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
        modelBuilder.ConfigureUserPoints(); // Add this line to configure the Abp.UserPoints Module
    }
    ```

    Open Visual Studio's Package Manager Console, select `DbMigrations` as the default project, and run the following command to add a migration for the user points module:

    ```csharp
    add-migration Add_AbpUserPoints_Module
    ```

    Now update the database:

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

- If you are using MongoDB, install the `Dignite.Abp.UserPoints.MongoDB` NuGet package. Add `builder.ConfigureUserPoints()` to the `OnModelCreating()` method:

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
        modelBuilder.ConfigureUserPoints(); // Add this line to configure the Abp.UserPoints Module
    }
    ```

{{end}}

- Install the `Dignite.Abp.UserPoints.Application.Contracts` NuGet package in the Application.Contracts project. Add `UserPointsApplicationContractsModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.UserPoints.Application` NuGet package in the Application project. Add `UserPointsApplicationModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.UserPoints.HttpApi` NuGet package in the HttpApi project. Add `UserPointsHttpApiModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.Abp.UserPoints.HttpApi.Client` NuGet package in the HttpApi project. Add `UserPointsHttpApiClientModule` to the `[DependsOn(...)]` property list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## User Points Options

`DignitePointsBlockOptions` is the options class for user points.

- Factor: The point-to-currency exchange rate. It is usually understood as how many points are equivalent to 1 unit of currency. The default value is 1. The point value for users must be a multiple of the point factor. For example, if you set the point factor to 100, the user's point balance can be 100, 200, 300, and so on in multiples of 100.

Example code:

```csharp
Configure<DignitePointsBlockOptions>(options =>
{
    options.Factor = 100;
});
```

## User Points Management

The `UserPointsItemManager` class provides a series of methods to manage user points:

- Create User Points

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

  - pointsType: The type of user points, an enum with two values:
    - General: General points, all general points can be merged for consumption.
    - Specialized: Specialized points, require specifying the points workflow when consuming points.
  - pointsDefinitionName: The name of the points definition for user points. This name corresponds to the `Name` of [defined points](Points.md#defined-points).
  - pointsWorkflowName: The name of the points workflow for user points. This name corresponds to the `Name` of [defined points](Points.md#defined-points).
  - points: The value of user points.
  - expirationDate: The point's expiration date.
  - userId: The ID of the user who owns the points.
  - tenantId: The ID of the tenant to which the points belong.

- Delete User Points

  ```csharp
  Task DeleteAsync(UserPointsItem pointsItem)
  ```

## User Points Consumption Management

The `UserPointsOrderManager` class provides a series of methods to manage user points consumption:

- Create Points Order

  ```csharp
  Task<UserPointsOrder> CreateAsync(
        int points,
        string businessOrderType,
        string businessOrderNumber,
        Guid userId,
        PointsType pointsType = PointsType.General,
        string pointsDefinitionName = null,
        string pointsWorkflowName = null,
        Guid? tenantId = null)
  ```

  - points: The number of points to consume.
  - businessOrderType: The type of the business order.
  - businessOrderNumber: The business order number.
  - userId: The ID of the user who is consuming the points.
  - pointsType: The type of points to consume, an enum with two values:
    - General: General points are consumed with type `General`.
    - Specialized: Specialized points, where you must specify the points definition name and points workflow name.
  - pointsDefinitionName: The name of the points definition for point consumption. This name corresponds to the `Name` of [defined points](Points.md#defined-points).
  - pointsWorkflowName: The name of the points workflow for point consumption. This name corresponds to the `Name` of [defined points](Points.md#defined-points).
  - tenantId: The ID of the tenant to which the points belong.

- Delete Points Order

  ```csharp
  Task DeleteAsync(UserPointsOrder pointsOrder, bool shouldRollbackPoints)
  ```

  - pointsOrder: The points order entity.
  - shouldRoll

backPoints: Whether to refund the points.

- Find Points Order by Business Order Number

  ```csharp
  Task<UserPointsOrder> FindByBusinessOrderAsync(string businessOrderType, string businessOrderNumber)
  ```

  - businessOrderType: The type of the business order.
  - businessOrderNumber: The business order number.

## APIs

- Get Total Available Points for a User

  ```csharp
  Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input)
  ```

  - ExpirationDate [optional]: The point expiration date.
  - PointsDefinitionName [optional]: The points definition name.
  - PointsWorkflowName [optional]: The points workflow name.

  If `PointsDefinitionName` and `PointsWorkflowName` are not set, it will retrieve all general points.

- Get User Points Item List

  ```csharp
  Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input)
  ```

  - StartTime [optional]: The start time for point creation.
  - EndTime [optional]: The end time for point creation.
  - PointsDefinitionName [optional]: The points definition name.
  - PointsWorkflowName [optional]: The points workflow name.

  If `PointsDefinitionName` and `PointsWorkflowName` are not set, it will retrieve all general points.
  