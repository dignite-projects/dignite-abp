using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace Dignite.Abp.NotificationCenter.Samples
{
    public class SampleAppService_Tests : NotificationCenterApplicationTestBase
    {
        private readonly INotificationsAppService _sampleAppService;
        private readonly ICurrentUser _currentUser;

        public SampleAppService_Tests()
        {
            _sampleAppService = GetRequiredService<INotificationsAppService>();
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
