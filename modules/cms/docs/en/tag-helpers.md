# Tag Helpers

To assist web developers in quickly developing websites, Dignite Cms provides a series of Tag Helpers to simplify the development process of data retrieval and display.

## Entry List

`cms-entry-list` is used to retrieve a list of entries, passing the `EntryListViewModel` type view model to the partial view.

### Example of `cms-entry-list`

Basic usage:

```xml
<cms-entry-list                 
                section-name="blog-post"
                partial-name="blog/_post-list">
</cms-entry-list>
```

`_post-list.cshtml` code

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

Return 10 entries:

```xml
<cms-entry-list                 
                section-name="blog-post"
                current-page="10"
                partial-name="blog/_post-list">
</cms-entry-list>
```

Specify the current language:

```xml
<cms-entry-list                 
                section-name="blog-post"
                culture="@Model.Entry.Culture"
                partial-name="blog/_post-list">
</cms-entry-list>
```

Query by field value:

> Retrieve a list of entries based on blog post category

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

### Parameters of `cms-entry-list`

- `site-id`: Specifies the ID of the site
- `section-name`: Specifies the name of the section
- `current-page`: Specifies the page number, default value: 1
- `max-result-count`: Specifies the number of results to return, default value: 20
- `culture`: Specifies the language, default value is the default language of the site
- `querying-by-fields`: Query entries by field values (refer to [Advanced Development](advanced-development.md#querying-entries-by-fields) document)
- `filter`: Filter strings in the entry `title`
- `partial-name`: Specifies the partial view name

### Parameters of `EntryListViewModel` class

- `Section`: The section DTO to which the list of entries belongs
- `Entries`: The list of entry DTOs
- `TotalCount`: Total number of entries
- `PageIndex`: Page index value
- `PageSize`: Number of entries per page
- `CurrentPage`: Current page number
- `TotalPage`: Total number of pages

## Entry

`cms-entry` is used to retrieve a single entry, passing the `EntryViewModel` type view model to the view.

### Example of `cms-entry`

```xml
<cms-entry     
    section-name="contact" 
    culture="@Model.Entry.Culture" 
    slug="index" 
    partial-name="_contact-section">
</cms-entry>
```

`_contact-section.cshtml` code

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

### Parameters of `cms-entry`

- `site-id`: Specifies the ID of the site
- `section-name`: Specifies the name of the section
- `culture`: Specifies the language, default value: the default language of the site
- `slug`: Specifies the alias of the entry
- `partial-name`: Specifies the partial view name

### Properties of `EntryViewModel`

- `Entry`: The entry DTO of type `EntryDto`
- `Section`: The section DTO of type `SectionDto`

## Entry Field

`cms-entry-field` is used to display entry fields, passing the `EntryFieldViewModel` type view model to the view.

### Example of `cms-entry-field`

Basic usage:

```xml
<cms-entry-field field-name="TextboxFieldName" entry="Model"></cms-entry-field>
```

Outputs the value of the `TextboxFieldName` field in the `Entry` entry on the current page. By default, it passes the `EntryFieldViewModel` type view model to the `/Views/Shared/TextEdit.cshtml` view.

Specify the field partial view page:

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

### Parameters of `cms-entry-field`

- `field-name`: The name of the field in the entry
- `entry`: An instance of type `EntryViewModel`, usually the ViewModel of the current page
- `partial-name`: The partial view page used to display the field value

### Properties of `EntryFieldViewModel`

- `Field`: An instance of type `FormField`
- `Entry`: An instance of type implementing `IHasCustomFields`

> `EntryDto` is an instance of `IHasCustomFields` interface.

### Predefined Field Views

Dignite Cms Mvc has built-in view files for dynamic fields, named after the dynamic field type, placed in the `/Views/Shared/` directory of the `Dignite.Cms.Public.Web` project:

![Dynamic Field Views](images/Dynamic-Forms-view.png)

## Field

`cms-field` is used to display field values, passing the `EntryFieldViewModel` type view model to the view.

Taking the `Service Project` entry `/Views/Entry/Service/Entry.cshtml` as an example:

```xml
<cms-entry-field field-name="Services" entry="Model"></cms-entry-field>
```

`Services` is a matrix type field, which first calls the `Dignite.Cms.Public.Web` project `/Views/Shared/Matrix.cshtml` view file, and then internally calls the view of `Fields/Matrix/{matrix-block-name}`.

In the `Service Project` entry example, call the `Fields/Matrix/service-item` view, the complete view path is: `/Views/Shared/Fields/Matrix/service-item.cshtml`:

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

### Parameters of `cms-field`

- `field`: An instance of type `FormField`
- `entry`: An instance of type `IHasCustomFields`
- `partial-name`: The partial view page used to display the field value

## Entry Links

### Based on Entry Object

```c#
@model EntryListViewModel
@foreach (var entry in Model.Entries)
{
    <a section="Model.Section" entry="entry">
        <h5>@entry.Title</h5>
    </a>
}
```

### Based on Path

```xml
<a entry-path="~/blog">
    Blog
</a>
```

Optional parameters:

- `culture`: Specifies the language of the entry
- `host`: Specifies the site host address

## Section

`cms-section` is used to call section data, passing the `SectionDto` type view model to the view.

```xml
<cms-section section-name="blog-post" partial-name="_blog-post-index">
</cms-section>
```
