# 进阶开发

## 按字段值查询条目

### `cms-entry-list`

`cms-entry-list` 有一个按字段值查询条目的参数(`querying-by-fields`)，该参数是`QueryingByField`类实例的列表，`QueryingByField`包含两个参数：

- `Name`：字段名称
- `Value`：用于查询的字段值，根据字段的不同，该值有不同的形式：

  - `TextFieldQuerying`：在字段中进行是否包含`Value`方式过滤。
  - `SwitchFieldQuerying`：`Value`必须可以转换为`bool`型，判断值是否等于`Value`的方式过滤。
  - `NumericFieldQuerying`：`Value`使用`-`分隔最小值和最大值，过滤字段值大于`Value`最小值和小于最大值。
  - `SelectFieldQuerying`：`Value`使用`,`分隔多个`Guid`值，过滤字段值是否含有`Value`中的`Guid`值。
  - `EntryFieldQuerying`：`Value`使用`,`分隔多个`Guid`值，过滤字段值是否含有`Value`中的`Guid`值。

    > 更多支持的查询方式在未来版本中提供。

### `GetListAsync(GetEntriesInput input)` 方法

在`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中有一个名为`QueryingByFieldsJson`字符串参数，该参数是`QueryingByField`列表序列化形式。

事实上`cms-entry-list`内部是将`QueryingByField`列表序列化为JSON，传递给`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中的`QueryingByFieldsJson`参数。

## 根据域名解析当前租户

Dignite Cms 提供根据域名确定当前租户的功能：

1. 将 `Dignite.Cms.AspNetCore.MultiTenancy` NuGet 包安装到 `Web Site` 项目中。

   将 `CmsAspNetCoreMultiTenancyModule` 添加到您的 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的 `[DependsOn(...)]` 属性列表中。

2. 将 `Web Site` 的 `Module`文件中添加如下配置：

```csharp
Configure<AbpTenantResolveOptions>(options =>
{
    // Resolve current tenant by domain name
    options.AddCmsDomainTenantResolver();
});
```

关于确定当前租户的官方文档请参见： [Determining the Current Tenant](https://abp.io/docs/latest/framework/architecture/multi-tenancy#determining-the-current-tenant)
