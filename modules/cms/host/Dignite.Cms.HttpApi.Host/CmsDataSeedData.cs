using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Cms;

public class CmsDataSeedData : ISingletonDependency
{
    public Guid TitleFieldId { get; } = Guid.NewGuid();
    public string TitleFieldName = "title";
    public Guid TextboxFieldId { get; } = Guid.NewGuid();
    public string TextboxFieldName = "TextboxFieldName";
    public Guid SwitchFieldId { get; } = Guid.NewGuid();
    public string SwitchFieldName = "switch";

    public Guid ImageFieldId { get; } = Guid.NewGuid();
    public string ImageFieldName = "image";

    public Guid BlogCategoryFieldId { get; } = Guid.NewGuid();
    public string BlogCategoryFieldName = "BlogCategory";
    public string BlogCategoryFieldItem1Value = "company-news";
    public string BlogCategoryFieldItem2Value = "tutorials";
    public string BlogCategoryFieldItem3Value = "essays";

    public Guid CkeditorFieldId { get; } = Guid.NewGuid();
    public string CkeditorFieldName = "Ckeditor";

    public Guid ServiceMatrixFieldId { get; }= Guid.NewGuid();
    public string ServiceMatrixFieldName = "Services";
    public string ServiceItemBlockName = "service-item";
    public string ServiceItemName = "name";
    public string ServiceItemDescription = "description";

    public Guid HomeSectionId { get; } = Guid.NewGuid();
    public string HomeSectionName = "home";
    public string HomeSectionRoute = "/{slug}";
    public string HomeSectionTemplate = "HomePage";
    public Guid BlogSectionId { get; } = Guid.NewGuid();
    public string BlogSectionName = "blog-index";
    public string BlogSectionRoute = "blog";
    public string BlogSectionTemplate = "Blog/Index";
    public Guid BlogPostSectionId { get; } = Guid.NewGuid();
    public string BlogPostSectionName = "blog-post";
    public string BlogPostSectionRoute = "blog/{publishTime:yyyy}/{publishTime:MM}/{slug}";
    public string BlogPostSectionTemplate = "Blog/Entry";
    public Guid ServiceSectionId { get; } = Guid.NewGuid();
    public string ServiceSectionName = "services";
    public string ServiceSectionRoute = "service/{slug}";
    public string ServiceEntryTemplate = "Service/Entry";
    public Guid ContactSectionId { get; } = Guid.NewGuid();
    public string ContactSectionName = "contact";
    public string ContactSectionRoute = "contact";
    public string ContactEntryTemplate = "Contact/Page";

    public Guid HomeSectionEntryTypeId { get; } = Guid.NewGuid();
    public string HomeSectionEntryTypeName = "home";
    public Guid BlogSectionEntryTypeId { get; } = Guid.NewGuid();
    public string BlogSectionEntryTypeName = "blog-index";
    public Guid BlogPostSectionEntryTypeId { get; } = Guid.NewGuid();
    public string BlogPostSectionEntryTypeName = "blog-post";

    public Guid ServiceSectionEntryTypeId { get; } = Guid.NewGuid();
    public string ServiceSectionEntryTypeName = "service";

    public Guid ContactSectionEntryTypeId { get; } = Guid.NewGuid();
    public string ContactSectionEntryTypeName = "contact";

    public string EntryDefaultCulture = "en";
}
