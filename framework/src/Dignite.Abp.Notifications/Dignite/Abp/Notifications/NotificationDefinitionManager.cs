using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Features;

namespace Dignite.Abp.Notifications;

/// <summary>
/// Implements <see cref="INotificationDefinitionManager"/>.
/// </summary>
public abstract class NotificationDefinitionManager : INotificationDefinitionManager
{
    protected Lazy<IDictionary<string, NotificationDefinition>> NotificationDefinitions { get; }
    protected NotificationOptions Options { get; }
    protected IFeatureChecker FeatureChecker { get; }
    protected IServiceProvider ServiceProvider { get; }

    public NotificationDefinitionManager(
        IOptions<NotificationOptions> options,
        IServiceProvider serviceProvider,
        IFeatureChecker featureChecker
        )
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
        FeatureChecker = featureChecker;
        NotificationDefinitions = new Lazy<IDictionary<string, NotificationDefinition>>(CreateNotificationDefinitions, true);
    }

    /// <summary>
    /// Get defined notifications
    /// </summary>
    /// <returns></returns>
    public virtual IReadOnlyList<NotificationDefinition> GetAll()
    {
        return NotificationDefinitions.Value.Values.ToImmutableList();
    }

    /// <summary>
    /// Get defined notifications
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="AbpException"></exception>
    public virtual NotificationDefinition Get(string name)
    {
        var notificationDefinition = GetOrNull(name);
        if (notificationDefinition == null)
        {
            throw new AbpException("Undefined notification: " + name);
        }

        return notificationDefinition;
    }

    /// <summary>
    /// Get defined notifications
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual NotificationDefinition GetOrNull(string name)
    {
        Check.NotNull(name, nameof(name));
        return NotificationDefinitions.Value.GetOrDefault(name);
    }

    /// <summary>
    /// Get all available notification definitions for userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId)
    {
        var availableDefinitions = new List<NotificationDefinition>();
        var notificationDefinitions = GetAll().Where(nd=>nd.EntityType==null);

        foreach (var notificationDefinition in notificationDefinitions)
        {
            if (await FeatureCheckAsync(notificationDefinition)
                && await PermissionCheckAsync(notificationDefinition, userId)
                )
            {
                availableDefinitions.Add(notificationDefinition);
            }
        }
        return availableDefinitions.ToImmutableList();
    }

    /// <summary>
    /// Judge whether the specified notification definition is available
    /// </summary>
    /// <param name="name"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public virtual async Task<bool> IsAvailableAsync(string name, Guid userId)
    {
        var notificationDefinition = GetOrNull(name);
        if (notificationDefinition == null)
        {
            return true;
        }

        return (await FeatureCheckAsync(notificationDefinition)
            && await PermissionCheckAsync(notificationDefinition, userId)
            );
    }

    /// <summary>
    /// Check whether the user has the right to subscribe and receive notification of definition
    /// </summary>
    /// <param name="notificationDefinition"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected abstract Task<bool> PermissionCheckAsync(NotificationDefinition notificationDefinition, Guid userId);

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

    protected virtual async Task<bool> FeatureCheckAsync(NotificationDefinition notificationDefinition)
    {
        if (notificationDefinition.FeatureName != null)
        {
            var result = await FeatureChecker.GetAsync(notificationDefinition.FeatureName, false);
            if (!result)
            {
                return false;
            }
        }
        return true;
    }
}