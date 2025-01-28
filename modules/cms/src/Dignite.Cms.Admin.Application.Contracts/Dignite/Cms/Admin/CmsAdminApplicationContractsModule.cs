using Dignite.Abp.DynamicForms.CkEditor;
using Dignite.Abp.DynamicForms.FileExplorer;
using Dignite.Abp.RegionalizationManagement;
using Volo.CmsKit.Admin;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Admin;

[DependsOn(
    typeof(CmsCommonApplicationContractsModule),
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(AbpRegionalizationManagementApplicationContractsModule),
    typeof(AbpDynamicFormsFileExplorerModule),
    typeof(AbpDynamicFormsCkEditorModule),
    typeof(FileExplorerApplicationContractsModule)
    )]
public class CmsAdminApplicationContractsModule : AbpModule
{

}
