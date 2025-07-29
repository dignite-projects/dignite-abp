using System;
using System.Collections.Generic;
using Dignite.Publisher.GlobalFeatures;
using JetBrains.Annotations;

namespace Volo.Abp.GlobalFeatures;
public static class GlobalModuleFeaturesDictionaryPublisherExtensions
{
    public static GlobalPublisherFeatures Publisher(
        [NotNull] this GlobalModuleFeaturesDictionary modules)
    {
        Check.NotNull(modules, nameof(modules));

        return modules
                .GetOrAdd(
                    GlobalPublisherFeatures.ModuleName,
                    _ => new GlobalPublisherFeatures(modules.FeatureManager)
                )
            as GlobalPublisherFeatures;
    }

    public static GlobalModuleFeaturesDictionary Publisher(
        [NotNull] this GlobalModuleFeaturesDictionary modules,
        [NotNull] Action<GlobalPublisherFeatures> configureAction)
    {
        Check.NotNull(configureAction, nameof(configureAction));

        configureAction(modules.Publisher());

        return modules;
    }
}
