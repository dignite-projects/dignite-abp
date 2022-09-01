using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationDefinitionManager"/>.
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        protected Lazy<IDictionary<string, NotificationDefinition>> NotificationDefinitions { get; }
        protected NotificationOptions Options { get; }
        protected IFeatureChecker FeatureChecker { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected ICurrentUser CurrentUser { get; }
        protected INotificationStore NotificationStore { get; }

        public NotificationDefinitionManager(
            IOptions<NotificationOptions> options,
            IServiceProvider serviceProvider,
            IFeatureChecker featureChecker,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IAuthorizationService authorizationService,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            INotificationStore notificationStore
            )
        {
            ServiceProvider = serviceProvider;
            Options = options.Value;
            FeatureChecker = featureChecker;
            AuthorizationService = authorizationService;
            CurrentPrincipalAccessor = currentPrincipalAccessor;
            CurrentTenant = currentTenant;
            CurrentUser = currentUser;
            NotificationStore = notificationStore;
            NotificationDefinitions = new Lazy<IDictionary<string, NotificationDefinition>>(CreateNotificationDefinitions, true);
        }

        public virtual NotificationDefinition Get(string name)
        {
            Check.NotNull(name, nameof(name));

            var setting = GetOrNull(name);

            if (setting == null)
            {
                throw new AbpException("Undefined notification: " + name);
            }

            return setting;
        }

        public virtual IReadOnlyList<NotificationDefinition> GetAll()
        {
            return NotificationDefinitions.Value.Values.ToImmutableList();
        }

        public virtual NotificationDefinition GetOrNull(string name)
        {
            return NotificationDefinitions.Value.GetOrDefault(name);
        }

        protected virtual IDictionary<string, NotificationDefinition> CreateNotificationDefinitions()
        {
            var settings = new Dictionary<string, NotificationDefinition>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var providers = Options
                    .DefinitionProviders
                    .Select(p => scope.ServiceProvider.GetRequiredService(p) as INotificationDefinitionProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    provider.Define(new NotificationDefinitionContext(settings));
                }
            }

            return settings;
        }
        public async Task<bool> IsAvailableAsync(string name,Guid userId)
        {
            var notificationDefinition = GetOrNull(name);
            if (notificationDefinition == null)
            {
                return true;
            }

            return (await FeatureCheckAsync(notificationDefinition)
                && await PermissionCheckAsync(notificationDefinition,userId)
                );
        }


        protected async Task<bool> FeatureCheckAsync(NotificationDefinition notificationDefinition)
        {
            if (notificationDefinition.FeatureName != null)
            {
                var result = await FeatureChecker.GetAsync(notificationDefinition.FeatureName,false);
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }

        protected async Task<bool> PermissionCheckAsync(NotificationDefinition notificationDefinition, Guid userId)
        {
            if (!notificationDefinition.PermissionName.IsNullOrEmpty())
            {
                AuthorizationResult result;
                if (CurrentUser.Id == userId)
                {
                    result = await AuthorizationService.AuthorizeAsync(notificationDefinition.PermissionName);
                }
                else
                {
                    var userRoles = await NotificationStore.GetUserRoles(userId);
                    var rolesClaims = userRoles.Select(ur => new Claim(AbpClaimTypes.Role, ur)).ToArray();
                    var claimsIdentity = new ClaimsIdentity(new Claim[] {
                                new Claim(AbpClaimTypes.UserId,userId.ToString()),
                                new Claim(AbpClaimTypes.TenantId,CurrentTenant.Id?.ToString())
                        });
                    claimsIdentity.AddClaims(rolesClaims);

                    //Switch current user identity
                    using (CurrentPrincipalAccessor.Change(new ClaimsPrincipal(claimsIdentity)))
                    {
                        result = await AuthorizationService.AuthorizeAsync(notificationDefinition.PermissionName);
                    }
                }
                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId)
        {
            var availableDefinitions = new List<NotificationDefinition>();

            foreach (var notificationDefinition in GetAll())
            {
                if (await FeatureCheckAsync(notificationDefinition)
                    && await PermissionCheckAsync(notificationDefinition, userId))
                {
                    availableDefinitions.Add(notificationDefinition);
                }
            }
            return availableDefinitions.ToImmutableList();
        }


    }
}
