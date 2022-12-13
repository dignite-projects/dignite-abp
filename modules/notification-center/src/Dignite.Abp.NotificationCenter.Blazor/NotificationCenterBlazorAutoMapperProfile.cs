using AutoMapper;
using Dignite.Abp.Notifications.Components;

namespace Dignite.Abp.NotificationCenter.Blazor;

public class NotificationCenterBlazorAutoMapperProfile : Profile
{
    public NotificationCenterBlazorAutoMapperProfile()
    {
        CreateMap<UserNotificationDto, NotificationNavigationContext>();
    }
}