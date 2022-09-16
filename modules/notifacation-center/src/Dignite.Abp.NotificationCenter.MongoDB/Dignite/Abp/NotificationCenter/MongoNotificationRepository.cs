using System;
using Dignite.Abp.NotificationCenter.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter;

public class MongoNotificationRepository : MongoDbRepository<NotificationCenterMongoDbContext, Notification, Guid>, INotificationRepository
{
    public MongoNotificationRepository(
        IMongoDbContextProvider<NotificationCenterMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}