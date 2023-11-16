using System;
using System.Collections.Generic;
using Dignite.CmsKit.GlobalFeatures;
using JetBrains.Annotations;

namespace Volo.Abp.GlobalFeatures;

public static class GlobalModuleFeaturesDictionaryCmsKitExtensions
{
    public static GlobalCmsKitFeatures DigniteCmsKit(
        [NotNull] this GlobalModuleFeaturesDictionary modules)
    {
        Check.NotNull(modules, nameof(modules));

        return modules
                .GetOrAdd(
                    GlobalCmsKitFeatures.ModuleName,
                    _ => new GlobalCmsKitFeatures(modules.FeatureManager)
                )
            as GlobalCmsKitFeatures;
    }

    public static GlobalModuleFeaturesDictionary DigniteCmsKit(
        [NotNull] this GlobalModuleFeaturesDictionary modules,
        [NotNull] Action<GlobalCmsKitFeatures> configureAction)
    {
        Check.NotNull(configureAction, nameof(configureAction));

        configureAction(modules.DigniteCmsKit());

        return modules;
    }
}
