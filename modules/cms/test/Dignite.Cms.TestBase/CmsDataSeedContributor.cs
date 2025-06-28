using Dignite.Abp.Data;
using Dignite.Abp.DynamicForms.Select;
using Dignite.Abp.DynamicForms.TextEdit;
using Dignite.Cms.Entries;
using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;
using Volo.CmsKit.Users;

namespace Dignite.Cms;

public class CmsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IClock _clock;
    private readonly ICmsUserRepository _cmsUserRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly CmsTestData _cmsTestData;
    private readonly ISectionRepository _sectionRepository;
    private readonly IEntryTypeRepository _entryTypeRepository;
    private readonly IFieldGroupRepository _fieldGroupRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IEntryRepository _entryRepository;

    public CmsDataSeedContributor(IClock clock, ICmsUserRepository cmsUserRepository, ICurrentTenant currentTenant, CmsTestData cmsTestData, 
        ISectionRepository sectionRepository, IEntryTypeRepository entryTypeRepository, 
        IFieldGroupRepository fieldGroupRepository, IFieldRepository fieldRepository, IEntryRepository entryRepository)
    {
        _clock = clock;
        _cmsUserRepository = cmsUserRepository;
        _currentTenant = currentTenant;
        _cmsTestData = cmsTestData;
        _sectionRepository = sectionRepository;
        _entryTypeRepository = entryTypeRepository;
        _fieldGroupRepository = fieldGroupRepository;
        _fieldRepository = fieldRepository;
        _entryRepository = entryRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedUsersAsync();
            await SeedFieldGroupAsync();
            await SeedFieldsAsync();
            await SeedSectionsAsync();
            await SeedEntryTypesAsync();
            await SeedEntriesAsync();
        }
    }

    private async Task SeedUsersAsync()
    {
        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_cmsTestData.User1Id, "user1",
            "user1@dignite.com",
            "user", "1")),
            autoSave: true);

        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_cmsTestData.User2Id, "user2",
            "user2@dignite.com",
            "user", "2")),
            autoSave: true);
    }

    private async Task SeedFieldGroupAsync()
    { 
        await _fieldGroupRepository.InsertAsync(new FieldGroup(_cmsTestData.FieldGroupId, "FieldGroup", null), autoSave: true);
    }

    private async Task SeedFieldsAsync()
    {
        var textboxFormConfiguration = new TextEditConfiguration();
        textboxFormConfiguration.CharLimit = 256;
        textboxFormConfiguration.Mode = TextEditMode.SingleLine;
        await _fieldRepository.InsertAsync(
            new Field(
                _cmsTestData.TextboxFieldId,
                _cmsTestData.FieldGroupId,
                _cmsTestData.TextboxFieldName,
                "Textbox Field","", 
                TextEditFormControl.ControlName,
                textboxFormConfiguration.ConfigurationDictionary,
                null),
            autoSave: true);

        var selectFormConfiguration = new SelectConfiguration();
        selectFormConfiguration.Multiple = false;
        selectFormConfiguration.Options = new List<SelectListItem> {
            new SelectListItem("Item 1",_cmsTestData.SelectFieldItem1Value,true),
            new SelectListItem("Item 2",_cmsTestData.SelectFieldItem2Value,false),
            new SelectListItem("Item 2",_cmsTestData.SelectFieldItem3Value,false)
        };
        await _fieldRepository.InsertAsync(
            new Field(
                _cmsTestData.SelectFieldId,
                _cmsTestData.FieldGroupId,
                _cmsTestData.SelectFieldName,
                "Select Field","",
                SelectFormControl.ControlName,
                selectFormConfiguration.ConfigurationDictionary,
                null
                ),
            autoSave:true
            );
    }

    private async Task SeedSectionsAsync()
    {
        await _sectionRepository.InsertAsync(
            new Section(
                _cmsTestData.SingleSectionId,
                SectionType.Single, 
                "Single Section", 
                _cmsTestData.SingleSectionName,
                true,
                true,
                "/","home", null),
            autoSave: true);

        await _sectionRepository.InsertAsync(
            new Section(
                _cmsTestData.ChannelSectionId,
                SectionType.Channel,
                "Channel Section",
                _cmsTestData.ChannelSectionName,
                false,
                true,
                _cmsTestData.ChannelSectionRoute,
                "blog/entry",null),
            autoSave: true);

        await _sectionRepository.InsertAsync(
            new Section(
                _cmsTestData.StructureSectionId,
                SectionType.Structure,
                "Channel Section",
                _cmsTestData.StructureSectionName,
                false,
                true,
                _cmsTestData.StructureSectionRoute,
                "blog/entry", null),
            autoSave: true);
    }
    private async Task SeedEntryTypesAsync()
    {
        await _entryTypeRepository.InsertAsync(
            new EntryType(
                _cmsTestData.SingleSectionEntryTypeId,
                _cmsTestData.SingleSectionId,
                "Single Section Entry Type",
                _cmsTestData.SingleSectionEntryTypeName,
                new List<EntryFieldTab> { 
                    new EntryFieldTab(
                        "Entry Field Tab", 
                        new List<EntryField>{ 
                            new EntryField(
                                _cmsTestData.TextboxFieldId,
                                "Textbox Field",
                                true,
                                true,
                                true
                                ),
                            new EntryField(
                                _cmsTestData.SelectFieldId,
                                "Select Field",
                                true,
                                true,
                                true
                                )
                        })
                    },
                null),
            autoSave: true);

        await _entryTypeRepository.InsertAsync(
            new EntryType(
                _cmsTestData.ChannelSectionEntryTypeId,
                _cmsTestData.ChannelSectionId,
                "Channel Section Entry Type",
                _cmsTestData.ChannelSectionEntryTypeName,
                new List<EntryFieldTab> {
                    new EntryFieldTab(
                        "Entry Field Tab",
                        new List<EntryField>{
                            new EntryField(
                                _cmsTestData.TextboxFieldId,
                                "Author",
                                true,
                                true,
                                true
                                ),
                            new EntryField(
                                _cmsTestData.SelectFieldId,
                                "Select Field",
                                true,
                                true,
                                true
                                )
                        })
                    },
                null),
            autoSave: true);


        await _entryTypeRepository.InsertAsync(
            new EntryType(
                _cmsTestData.StructureSectionEntryTypeId,
                _cmsTestData.StructureSectionId,
                "Structure Section Entry Type",
                _cmsTestData.StructureSectionEntryTypeName,
                new List<EntryFieldTab> {
                    new EntryFieldTab(
                        "Entry Field Tab",
                        new List<EntryField>{
                            new EntryField(
                                _cmsTestData.TextboxFieldId,
                                "Author",
                                true,
                                true,
                                true
                                ),
                            new EntryField(
                                _cmsTestData.SelectFieldId,
                                "Select Field",
                                true,
                                true,
                                true
                                )
                        })
                    },
                null),
            autoSave: true);
    }
    private async Task SeedEntriesAsync()
    {
        var singleSection_Entry = new Entry(
                _cmsTestData.SingleSection_EntryId,
                _cmsTestData.SingleSectionId,
                _cmsTestData.SingleSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.SingleSection_EntrySlug,
                _clock.Now,
                EntryStatus.Published,
                null,
                1,
                null,
                "",
                null
                );
        singleSection_Entry.SetField(_cmsTestData.TextboxFieldName, "An excellent program.");
        singleSection_Entry.SetField(
            _cmsTestData.SelectFieldName, 
            new List<string> 
            { 
                _cmsTestData.SelectFieldItem1Value 
            });

        await _entryRepository.InsertAsync(
            singleSection_Entry,
            autoSave:true
            );


        var channelSection_Entry1 = new Entry(
                _cmsTestData.ChannelSection_Entry1Id,
                _cmsTestData.ChannelSectionId,
                _cmsTestData.ChannelSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.ChannelSection_Entry1Slug,
                _clock.Now,
                EntryStatus.Published,
                null,
                1,
                null,
                "",
                null
                );
        channelSection_Entry1.SetField(_cmsTestData.TextboxFieldName, "Tanaka");
        channelSection_Entry1.SetField(
            _cmsTestData.SelectFieldName,
            new List<string>
            {
                _cmsTestData.SelectFieldItem2Value,
                _cmsTestData.SelectFieldItem3Value
            });

        await _entryRepository.InsertAsync(
            channelSection_Entry1,
            autoSave: true
            );




        var channelSection_Entry2 = new Entry(
                _cmsTestData.ChannelSection_Entry2Id,
                _cmsTestData.ChannelSectionId,
                _cmsTestData.ChannelSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.ChannelSection_Entry2Slug,
                _clock.Now.AddSeconds(1),
                EntryStatus.Published,
                null,
                2,
                null,
                "",
                null
                );
        channelSection_Entry2.SetField(_cmsTestData.TextboxFieldName, "Tanaka");
        channelSection_Entry2.SetField(
            _cmsTestData.SelectFieldName,
            new List<string>
            {
                _cmsTestData.SelectFieldItem1Value,
                _cmsTestData.SelectFieldItem2Value,
                _cmsTestData.SelectFieldItem3Value
            });

        await _entryRepository.InsertAsync(
            channelSection_Entry2,
            autoSave: true
            );

        var channelSection_Entry3VisionEntry = new Entry(
                _cmsTestData.ChannelSection_Entry2VisionEntryId,
                _cmsTestData.ChannelSectionId,
                _cmsTestData.ChannelSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.ChannelSection_Entry2Slug,
                _clock.Now.AddSeconds(1),
                EntryStatus.Published,
                null,
                2,
                _cmsTestData.ChannelSection_Entry2Id,
                "",
                null
                );
        channelSection_Entry3VisionEntry.SetField(_cmsTestData.TextboxFieldName, "Tanaka");
        channelSection_Entry3VisionEntry.SetField(
            _cmsTestData.SelectFieldName,
            new List<string>
            {
                _cmsTestData.SelectFieldItem1Value,
                _cmsTestData.SelectFieldItem2Value,
                _cmsTestData.SelectFieldItem3Value
            });
        channelSection_Entry3VisionEntry.SetIsActivatedVersion(false);
        await _entryRepository.InsertAsync(
            channelSection_Entry3VisionEntry,
            autoSave: true
            );



        var structureSection_Entry1 = new Entry(
                _cmsTestData.StructureSection_Entry1Id,
                _cmsTestData.StructureSectionId,
                _cmsTestData.StructureSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.StructureSection_Entry1Slug,
                _clock.Now,
                EntryStatus.Published,
                null,
                1,
                null,
                "",
                null
                );
        structureSection_Entry1.SetField(_cmsTestData.TextboxFieldName, "Tanaka");
        structureSection_Entry1.SetField(
            _cmsTestData.SelectFieldName,
            new List<string>
            {
                _cmsTestData.SelectFieldItem2Value,
                _cmsTestData.SelectFieldItem3Value
            });

        await _entryRepository.InsertAsync(
            structureSection_Entry1,
            autoSave: true
            );


        var structureSection_Entry2 = new Entry(
                _cmsTestData.StructureSection_Entry2Id,
                _cmsTestData.StructureSectionId,
                _cmsTestData.StructureSectionEntryTypeId,
                _cmsTestData.EntryDefaultCulture,
                _cmsTestData.StructureSection_Entry2Slug,
                _clock.Now,
                EntryStatus.Published,
                null,
                1,
                null,
                "",
                null
                );
        structureSection_Entry2.SetField(_cmsTestData.TextboxFieldName, "Tanaka");
        structureSection_Entry2.SetField(
            _cmsTestData.SelectFieldName,
            new List<string>
            {
                _cmsTestData.SelectFieldItem2Value,
                _cmsTestData.SelectFieldItem3Value
            });

        await _entryRepository.InsertAsync(
            structureSection_Entry2,
            autoSave: true
            );
    }
}
