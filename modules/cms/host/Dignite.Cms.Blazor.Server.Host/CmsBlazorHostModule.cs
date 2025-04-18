﻿using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Dignite.Abp.AspNetCore.Components.Server.PureTheme;
using Dignite.Abp.AspNetCore.Components.Server.PureTheme.Bundling;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;
using Dignite.Cms.Admin.Blazor.Server;
using Dignite.Cms.Blazor.Server.Host.Menus;
using Dignite.Cms.EntityFrameworkCore;
using Dignite.Cms.Localization;
using Dignite.Cms.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sakura.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Data;
using Volo.Abp.Emailing;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Cms.Blazor.Server.Host;

[DependsOn(
    typeof(CmsEntityFrameworkCoreModule),
    typeof(CmsApplicationModule),
    typeof(CmsHttpApiModule),
    typeof(AbpAspNetCoreMvcUiPureThemeModule),
    typeof(AbpAspNetCoreComponentsServerPureThemeModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountWebIdentityServerModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpIdentityBlazorServerModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpTenantManagementBlazorServerModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpSettingManagementBlazorServerModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpBlobStoringFileSystemModule),
    typeof(CmsAdminBlazorServerModule)
)]
public class CmsBlazorHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(CmsResource),
                typeof(CmsDomainModule).Assembly,
                typeof(CmsDomainSharedModule).Assembly,
                typeof(CmsApplicationModule).Assembly,
                typeof(CmsApplicationContractsModule).Assembly,
                typeof(CmsBlazorHostModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<AbpBundlingOptions>(options =>
        {
                // MVC UI
                options.StyleBundles.Configure(
                PureThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

                //BLAZOR UI
                options.StyleBundles.Configure(
                BlazorPureThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        bundle.AddFiles("/Dignite.Cms.Blazor.Server.Host.styles.css");
                }
            );
        });

        context.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "Cms";
            });

        context.Services.ForwardIdentityAuthenticationForBearer();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CmsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Dignite.Cms.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Dignite.Cms.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Dignite.Cms.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Dignite.Cms.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsBlazorHostModule>(hostingEnvironment.ContentRootPath);
            });
        }

        context.Services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cms API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });


        var supportedCultures = new List<CultureInfo>();
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("ja", "ja", "日本語"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));

            supportedCultures.AddRange(options.Languages.Select(l => new CultureInfo(l.CultureName)));
        });
        Configure<RequestLocalizationOptions>(options => {
            options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.AddInitialRequestCultureProvider(new RouteDataRequestCultureProvider()); // Set the current language through the culture value in the route
        });


        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(CmsBlazorHostModule).Assembly;
        });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
            });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif


        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });


        Configure<AbpThemingOptions>(options =>
        {
            options.DefaultThemeName = PureTheme.Name;
        });

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = Directory.GetCurrentDirectory() + "/wwwroot";
                });
            });
        });



        if (!hostingEnvironment.IsDevelopment())
        {
            Configure<AbpTenantResolveOptions>(options =>
            {
                options.AddDomainTenantResolver("{0}.top");
            });
        }

        // Add default bootstrap-styled pager implementation
        context.Services.AddBootstrapPagerGenerator(options =>
        {
            // Use default pager options
            options.ConfigureDefault();
        });
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseRequestLocalization();
        app.UseCors();
        app.UseAuthentication();
        app.UseJwtTokenMiddleware();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cms API");
        });
        app.UseConfiguredEndpoints();

        using (var scope = context.ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<IDataSeeder>()
                .SeedAsync();
        }
    }
}
