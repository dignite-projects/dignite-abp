# 進階開發

## 按字段值查詢項目

### `cms-entry-list`

`cms-entry-list` 有一個按字段值查詢項目的參數(`querying-by-fields`)，該參數是`QueryingByField`類實例的列表，`QueryingByField`包含兩個參數：

- `Name`：字段名稱
- `Value`：用於查詢的字段值，根據字段的不同，該值有不同的形式：

  - `TextFieldQuerying`：在字段中進行是否包含`Value`方式過濾。
  - `SwitchFieldQuerying`：`Value`必須可以轉換為`bool`型，判斷值是否等於`Value`的方式過濾。
  - `NumericFieldQuerying`：`Value`使用`-`分隔最小值和最大值，過濾字段值大於`Value`最小值和小於最大值。
  - `SelectFieldQuerying`：`Value`使用`,`分隔多個`Guid`值，過濾字段值是否含有`Value`中的`Guid`值。
  - `EntryFieldQuerying`：`Value`使用`,`分隔多個`Guid`值，過濾字段值是否含有`Value`中的`Guid`值。

    > 更多支持的查詢方式在未來版本中提供。

### `GetListAsync(GetEntriesInput input)` 方法

在`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中有一個名為`QueryingByFieldsJson`字符串參數，該參數是`QueryingByField`列表序列化形式。

事實上`cms-entry-list`內部是將`QueryingByField`列表序列化為JSON，傳遞給`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中的`QueryingByFieldsJson`參數。

## 根據域名解析當前租戶

Dignite Cms 提供根據域名確定當前租戶的功能：

1. 將 `Dignite.Cms.AspNetCore.MultiTenancy` NuGet 套件安裝到 `Web Site` 專案中。

   將 `CmsAspNetCoreMultiTenancyModule` 添加到您的 [模組類](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 屬性列表中。

2. 在 `Web Site` 的 `Module` 文件中添加如下配置：

```csharp
Configure<AbpTenantResolveOptions>(options =>
{
    // 根據域名解析當前租戶
    options.AddCmsDomainTenantResolver();
});
```

關於確定當前租戶的官方文檔請參見： [Determining the Current Tenant](https://abp.io/docs/latest/framework/architecture/multi-tenancy#determining-the-current-tenant)
