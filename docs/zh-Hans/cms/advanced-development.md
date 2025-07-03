# 进阶开发

## 按字段值查询条目

### `cms-entry-list`

`cms-entry-list` 有一个按字段值查询条目的参数(`querying-by-fields`)，该参数是`QueryingByField`类实例的列表，`QueryingByField`包含两个参数：

- `Name`：字段名称
- `Value`：用于查询的字段值，根据字段的不同，该值有不同的形式：

  - `TextFieldQuerying`：在字段中进行是否包含`Value`方式过滤。
  - `SwitchFieldQuerying`：`Value`必须可以转换为`bool`型，判断值是否等于`Value`的方式过滤。
  - `NumericFieldQuerying`：`Value`使用`-`分隔最小值和最大值，过滤字段值大于最小值和小于最大值。
  - `SelectFieldQuerying`：`Value`使用`,`分隔多个`Guid`值，过滤字段值是否含有`Value`中的`Guid`值。
  - `EntryFieldQuerying`：`Value`使用`,`分隔多个`Guid`值，过滤字段值是否含有`Value`中的`Guid`值。

### `GetListAsync(GetEntriesInput input)` 方法

在`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中有一个名为`QueryingByFieldsJson`字符串参数，该参数是`QueryingByField`列表序列化形式。

事实上`cms-entry-list`内部是将`QueryingByField`列表序列化为JSON，传递给`IEntryPublicAppService` `GetListAsync(GetEntriesInput input)`方法中的`QueryingByFieldsJson`参数。
