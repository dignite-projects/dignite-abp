using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace Dignite.Abp.NotificationCenter
{
    public class NotificationAppService_Tests : NotificationCenterApplicationTestBase
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly ICurrentUser _currentUser;

        public NotificationAppService_Tests()
        {
            _notificationAppService = GetRequiredService<INotificationAppService>();
            _currentUser = GetRequiredService<ICurrentUser>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var user = _currentUser;
            //var result = await _sampleAppService.GetAsync();
            //result.Value.ShouldBe(42);
        }

        [Fact]
        public async Task GetAuthorizedAsync()
        {
            //var result = await _sampleAppService.GetAuthorizedAsync();
            //result.Value.ShouldBe(42);
        }
    }
}
