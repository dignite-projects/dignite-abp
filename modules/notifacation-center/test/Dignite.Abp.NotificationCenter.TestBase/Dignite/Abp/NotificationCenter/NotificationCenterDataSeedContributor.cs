using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Dignite.Abp.NotificationCenter;

public class NotificationCenterDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserNotificationRepository _userNotificationRepository;
    private readonly INotificationSubscriptionRepository _notificationSubscriptionRepository;
    private readonly NotificationCenterTestData _notificationCenterTestData;
    private readonly IClock _clock;

    public NotificationCenterDataSeedContributor(
        IGuidGenerator guidGenerator, 
        ICurrentTenant currentTenant, 
        INotificationRepository notificationRepository, 
        IUserNotificationRepository userNotificationRepository, 
        INotificationSubscriptionRepository notificationSubscriptionRepository,
        NotificationCenterTestData notificationCenterTestData,
        IClock clock)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _notificationRepository = notificationRepository;
        _userNotificationRepository = userNotificationRepository;
        _notificationSubscriptionRepository = notificationSubscriptionRepository;
        _notificationCenterTestData = notificationCenterTestData;
        _clock = clock;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedNotificationsAsync();
            await SeedNotificationSubscriptionAsync();
        }
    }


    private async Task SeedNotificationsAsync()
    {
        var notification = new Notification(
            _guidGenerator.Create(),
            _notificationCenterTestData.Notification1Name,
            new MessageNotificationData("message"),
            _notificationCenterTestData.EntityType1Name,
            _notificationCenterTestData.Entity1Id,
            NotificationSeverity.Info,
            _clock.Now,
            _currentTenant.Id);
        var userNotification = new UserNotification(
            _notificationCenterTestData.User1Id,
            notification.Id,
            UserNotificationState.Unread,
            _currentTenant.Id);

        await _notificationRepository.InsertAsync(notification, true);
        await _userNotificationRepository.InsertAsync(userNotification, true);
    }

    private async Task SeedNotificationSubscriptionAsync()
    {
        var ns = new NotificationSubscription(
            _guidGenerator.Create(),
            _notificationCenterTestData.Notification1Name,
            _notificationCenterTestData.EntityType1Name,
            _notificationCenterTestData.Entity1Id,
            _clock.Now,
            _currentTenant.Id
            );
        await _notificationSubscriptionRepository.InsertAsync(ns, true);
    }
}