using System.Collections.Immutable;
using Dignite.Abp.SettingsGrouping;
using Microsoft.Extensions.Localization;
using SettingManagementSample.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace SettingManagementSample;

public class PackageIdentitySettingDefinitionProvider : AbpIdentitySettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }
    public SettingDefinitionGroup Group => new SettingDefinitionGroup(
        TestSettingNames.IdentitySettingGroupName, 
        L(TestSettingNames.IdentitySettingGroupName),
        icon: "fas fa fa-users"
        );

    public override void Define(ISettingDefinitionContext context)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        base.Define(new SettingDefinitionContext(settings));

        string[] passwordSettingGroup = new string[] { IdentitySettingNames.Password.RequiredLength };
        string[] lockoutSettingGroup = new string[] { IdentitySettingNames.Lockout.MaxFailedAccessAttempts};

        settings.GetValueOrDefault(IdentitySettingNames.Password.RequiredLength)
            .UseTextboxForm(tb =>
            {
                tb.Required = true;
                tb.Description = L("PasswordRequiredLengthDescription").Localize(StringLocalizerFactory);
            });

        settings.GetValueOrDefault(IdentitySettingNames.Lockout.MaxFailedAccessAttempts)
            .UseTextboxForm(tb =>
            {
                tb.Required = true;
                tb.Description = L("MaxFailedAccessAttemptsDescription").Localize(StringLocalizerFactory);
            });

        //Add inherited settings
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.PasswordSettingsSubGroupName, L(TestSettingNames.PasswordSettingsSubGroupName),L("PasswordSettingsSubGroupDescription"), "fa fa-key"),
            settings.Values.Where(sd=> passwordSettingGroup.Contains(sd.Name)).ToArray()
        );
        context.Add(
            new SettingDefinitionGroup(TestSettingNames.LockoutSettingsSubGroupName, L(TestSettingNames.LockoutSettingsSubGroupName),icon: "fa fa-unlock-alt"),
            settings.Values.Where(sd => lockoutSettingGroup.Contains(sd.Name)).ToArray()
        );
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SettingManagementSampleResource>(name);
    }
}
