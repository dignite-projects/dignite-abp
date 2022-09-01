using AutoMapper;
using Dignite.Abp.Notifications;

namespace Dignite.Abp.NotificationCenter
{
    public class NotificationCenterApplicationAutoMapperProfile : Profile
    {
        public NotificationCenterApplicationAutoMapperProfile()
        {
            CreateMap<NotificationSubscriptionInfo, NotificationSubscriptionDto>()
                .ForMember(m => m.DisplayName, y => y.Ignore())
                .ForMember(m => m.Description, y => y.Ignore());
        }
    }
}