# Tag Helpers

为帮助Web开发者快速开发网站，Dignite Cms 提供了一系列Tag Helpers，用于简化数据获取和展示的开发流程。

## 条目列表

`cms-entry-list`用于获取条目列表，向分部视图传递`EntryListViewModel`类型viewmodel。

### `cms-entry-list`示例

基本用法：

```xml
<cms-entry-list                 
                section-name="blog-post"
                partial-name="blog/_post-list">
</cms-entry-list>
```

`_post-list.cshtml` 代码

```html
@using Dignite.Cms.Public.Web.Models;
@using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
@model EntryListViewModel
@{
    var pagerModel = new PagerModel(Model.TotalCount, 10, Model.PageIndex, Model.PageSize, Context.Request.Path);
}
 <div class="list-group mb-5">
    @foreach (var entry in Model.Entries)
    {
        <a section="Model.Section" entry="entry" class="list-group-item list-group-item-action">
            <div class="d-flex w-100 justify-content-between mt-2">
                <h5 class="mb-1">@entry.Title</h5>
                <span class="text-muted">@entry.PublishTime</span>
            </div>
        </a>
    }
</div>
<nav aria-label="Pagination navigation" class=" mb-5">
    <div class="flex-grow-1">
        <abp-paginator model="pagerModel" show-info="false" />
    </div>
</nav>
```

返回10条条目：

```xml
<cms-entry-list                 
                section-name="blog-post"
                current-page="10"
                partial-name="blog/_post-list">
</cms-entry-list>
```

指定当前语言：

```xml
<cms-entry-list                
                section-name="blog-post"
                culture="@Model.Entry.Culture"
                partial-name="blog/_post-list">
</cms-entry-list>
```

按按字段值查询：

> 按博客帖子分类获取条目列表

```csharp
@using Dignite.Abp.Data;
@using Dignite.Cms.Public.Web.Models;
@model EntryViewModel
@{
    //current category
    var category = Context.Request.Query["category"].ToString();
    IList<QueryingByField> parameters = null;
    if (!category.IsNullOrWhiteSpace())
    {
        parameters = new List<QueryingByField>
        {
            new QueryingByField("BlogCategory",category)
        };
    }
}
```

```xml
<cms-entry-list                
                section-name="blog-post"
                querying-by-fields="parameters"
                partial-name="blog/_post-list">
</cms-entry-list>
```

### `cms-entry-list`的参数

- `site-id`：指定站点的Id
- `section-name`：指定版块的名称
- `current-page`：指定分页数字，默认值：1
- `max-result-count`：指定返回结果的数量，默认值：20
- `culture`：指定语言，默认值为站点的默认语言
- `querying-by-fields`：按字段值查询条目（请参阅[进阶开发](advanced-development.md#按字段值查询条目)文档）
- `filter`：在条目`title`中过滤字符串
- `partial-name`：指定分部视图名称

### `EntryListViewModel`类的参数

- `Section`：条目列表所属版块Dto
- `Entries`：条目Dto列表
- `TotalCount`：条目总数
- `PageIndex`：分页索引数值
- `PageSize`：每页条目数量
- `CurrentPage`：当前分页数
- `TotalPage`：总分页数

## 条目

`cms-entry`用于获取单条条目，向视图传递`EntryViewModel`类型viewmodel。

### `cms-entry`示例

```xml
<cms-entry    
    section-name="contact" 
    culture="@Model.Entry.Culture" 
    slug="index" 
    partial-name="_contact-section">
</cms-entry>
```

`_contact-section.cshtml` 代码

```html
@using Dignite.Cms.Public.Web.Models;
@model EntryViewModel
<section class="container mb-5">
    <h2 class="fs-1 fw-semibold lh-base mb-3">@Model.Entry.Title</h2>
    <div class="lead mb-5">
        <cms-entry-field field-name="TextboxFieldName" entry="Model"></cms-entry-field>
    </div>
    <a class="underline-animate ms-3 btn btn-outline-primary" section="@Model.Section" entry="Model.Entry">@localizer["contact-us"]</a>
</section>
```

### `cms-entry`的参数

- `site-id`：指定站点的Id
- `section-name`：指定版块的名称
- `culture`：指定语言，默认值：站点的默认语言
- `slug`：指定条目的别名
- `partial-name`：指定分部视图名称

### `EntryViewModel`属性

- `Entry`：类型为`EntryDto`的条目
- `Section`：类型为`SectionDto`的版块

## 条目的字段值

`cms-entry-field`用于条目字段的展示，向视图传递`EntryFieldViewModel`类型viewmodel。

### `cms-entry-field`示例

基本用法：

```xml
<cms-entry-field field-name="TextboxFieldName" entry="Model"></cms-entry-field>
```

输出当前页面中`Entry`条目字段名称为`TextboxFieldName`的值，默认情况下，将向`/Views/Shared/TextEdit.cshtml`视图传递`EntryFieldViewModel`类型viewmodel。

指定字段分部视图页面：

```xml
<cms-entry-field field-name="image" entry="Model" partial-name="_banner"></cms-entry-field>
```

`_banner.cshtml`

```c#
@using Dignite.Abp.Data
@using Dignite.Cms.Public.Web.Models;
@using Dignite.Abp.DynamicForms;
@using Dignite.FileExplorer.Files;
@model EntryFieldViewModel
@{
    var files = Model.Entry.GetField<List<FileDescriptorDto>>(Model.Field.Name);
    FileDescriptorDto coverImage = (files != null && files.Any()) ? files[0] : null;
    var imgUrl =  $"/api/file-explorer/files/{coverImage?.ContainerName}/{coverImage?.BlobName}?width=1650&height=800";
}
@if (coverImage != null)
{
    <img src="@imgUrl" class="img-fluid rounded" alt="@coverImage?.Name">
}
```

### `cms-entry-field`的参数

- `field-name`：条目中的字段名称
- `entry`：类型为`EntryViewModel`的实例，通常情况下为当前页面的ViewModel
- `partial-name`：用于展示字段值的分部视图页面

### `EntryFieldViewModel`属性

- `Field`：类型为`FormField`的实例
- `Entry`：接口`IHasCustomFields`的实例

> `EntryDto`是`IHasCustomFields`接口的实例。

### 预设的字段视图

Dignite Cms Mvc为动态字段内置了视图文件，以动态字段类型命名，放置在`Dignite.Cms.Public.Web`项目`/Views/Shared/`目录下：

![动态字段视图](images/Dynamic-Forms-view.png)

## 字段

`cms-field`用于字段值的展示，向视图传递`EntryFieldViewModel`类型viewmodel。

以本示例中`服务项目`条目`/Views/Entry/Service/Entry.cshtml`为例：

```xml
<cms-entry-field field-name="Services" entry="Model"></cms-entry-field>
```

`Services`是一个矩阵类型的字段，先调用`Dignite.Cms.Public.Web`项目`/Views/Shared/Matrix.cshtml`视图文件，然后在内部调用`Fields/Matrix/{矩阵块名称}`的视图。

在`服务项目`条目示例中调用`Fields/Matrix/service-item`视图，完整视图路径为:`/Views/Shared/Fields/Matrix/service-item.cshtml`：

```c#
@using Dignite.Cms.Public.Web.Models;
@model MatrixBlockViewModel
@{
    var nameField = Model.Type.Fields.First(fd => fd.Name == "name");
    var descriptionField = Model.Type.Fields.First(fd => fd.Name == "description");
}
<h2 class="fs-2 mb-3 fw-bold lh-base">
    <cms-field field="nameField" entry="Model.Block"></cms-field>
</h2>
<div class="rich-text mb-5">
    <cms-field field="descriptionField" entry="Model.Block"></cms-field>
</div>
```

### `cms-field`的参数

- `field`：类型为`FormField`的实例
- `entry`：接口`IHasCustomFields`的实例
- `partial-name`：用于展示字段值的分部视图页面

## 条目的链接

### 基于条目对象

```c#
@model EntryListViewModel
@foreach (var entry in Model.Entries)
{
    <a section="Model.Section" entry="entry">
        <h5>@entry.Title</h5>
    </a>
}
```

### 基于路径

```xml
<a entry-path="~/blog">
    Blog
</a>
```

可选参数：

- `culture`：指定条目的语言
- `host`：指定站点主机地址

## 版块

`cms-section`用于版块数据的调用，向视图传递`SectionDto`类型viewmodel。

```xml
<cms-section section-name="blog-post" partial-name="_blog-post-index">
</cms-section>
```
