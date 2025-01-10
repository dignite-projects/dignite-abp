﻿using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class RegionalizationManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RegionalizationManagementInstallerModule>();
        });
    }
}
