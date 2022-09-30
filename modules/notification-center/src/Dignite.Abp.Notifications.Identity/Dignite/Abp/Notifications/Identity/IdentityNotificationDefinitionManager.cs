using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Dignite.Abp.Notifications.Identity;

public class IdentityNotificationDefinitionManager: NotificationDefinitionManager, ISingletonDependency
{
    private readonly IIdentityUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUser _currentUser;
    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
    private readonly ICurrentTenant _currentTenant;

    public IdentityNotificationDefinitionManager(
        IOptions<NotificationOptions> options,
        IServiceProvider serviceProvider,
        IFeatureChecker featureChecker,
        IIdentityUserRepository userRepository,
        IAuthorizationService authorizationService,
        ICurrentUser currentUser,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        ICurrentTenant currentTenant
        ) : base(
            options,
            serviceProvider,
            featureChecker
            )
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
        _currentPrincipalAccessor = currentPrincipalAccessor;
        _currentTenant = currentTenant;
    }


    protected override async Task<bool> PermissionCheckAsync(NotificationDefinition notificationDefinition, Guid userId)
    {
        if (!notificationDefinition.PermissionName.IsNullOrEmpty())
        {
            AuthorizationResult result;
            if (_currentUser.Id == userId)
            {
                result = await _authorizationService.AuthorizeAsync(notificationDefinition.PermissionName);
            }
            else
            {
                var userRoles = await _userRepository.GetRolesAsync(userId);
                var rolesClaims = userRoles.Select(ur => new Claim(AbpClaimTypes.Role, ur.Name)).ToArray();
                var claimsIdentity = new ClaimsIdentity(new Claim[] {
                            new Claim(AbpClaimTypes.UserId,userId.ToString()),
                            new Claim(AbpClaimTypes.TenantId,_currentTenant.Id?.ToString())
                    });
                claimsIdentity.AddClaims(rolesClaims);

                //Switch current user identity
                using (_currentPrincipalAccessor.Change(new ClaimsPrincipal(claimsIdentity)))
                {
                    result = await _authorizationService.AuthorizeAsync(notificationDefinition.PermissionName);
                }
            }
            if (!result.Succeeded)
            {
                return false;
            }
        }
        return true;
    }
}

