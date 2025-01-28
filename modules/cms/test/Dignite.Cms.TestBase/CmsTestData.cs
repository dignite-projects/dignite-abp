using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Cms;

public class CmsTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public Guid User2Id { get; } = Guid.NewGuid();

    public Guid FieldGroupId { get; } = Guid.NewGuid();

    public Guid TextboxFieldId { get; } = Guid.NewGuid();
    public string TextboxFieldName = "TextboxFieldName";
    public Guid SelectFieldId { get; } = Guid.NewGuid();
    public string SelectFieldName = "SelectFieldName";
    public string SelectFieldItem1Value = "item1";
    public string SelectFieldItem2Value = "item2";
    public string SelectFieldItem3Value = "item3";

    public Guid SingleSectionId { get; } = Guid.NewGuid();
    public string SingleSectionName = "SingleSectionName";
    public Guid ChannelSectionId { get; } = Guid.NewGuid();
    public string ChannelSectionName = "ChannelSectionName";
    public string ChannelSectionRoute = "blog/{slug}";
    public Guid StructureSectionId { get; } = Guid.NewGuid();
    public string StructureSectionName = "StructureSectionName";
    public string StructureSectionRoute = "doc/{slug}";

    public Guid SingleSectionEntryTypeId { get; } = Guid.NewGuid();
    public string SingleSectionEntryTypeName = "SingleSectionEntryTypeName";
    public Guid ChannelSectionEntryTypeId { get; } = Guid.NewGuid();
    public string ChannelSectionEntryTypeName = "ChannelSectionEntryTypeName";

    public Guid StructureSectionEntryTypeId { get; } = Guid.NewGuid();
    public string StructureSectionEntryTypeName = "StructureSectionEntryTypeName";

    public string EntryDefaultCulture = "en";
    public Guid SingleSection_EntryId { get; } = Guid.NewGuid();
    public string SingleSection_EntrySlug = "index";
    public Guid ChannelSection_Entry1Id { get; } = Guid.NewGuid();
    public string ChannelSection_Entry1Slug = "entry-1-slug";

    public Guid ChannelSection_Entry2Id { get; } = Guid.NewGuid();
    public string ChannelSection_Entry2Slug = "entry-2-slug";
    public Guid ChannelSection_Entry2VisionEntryId { get; }=Guid.NewGuid();


    public Guid StructureSection_Entry1Id { get; } = Guid.NewGuid();
    public string StructureSection_Entry1Slug = "entry-1-slug";

    public Guid StructureSection_Entry2Id { get; } = Guid.NewGuid();
    public string StructureSection_Entry2Slug = "entry-2-slug";

}
