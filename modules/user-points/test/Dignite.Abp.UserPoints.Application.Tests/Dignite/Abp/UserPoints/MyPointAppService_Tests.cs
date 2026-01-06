using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class MyPointAppService_Tests : UserPointsApplicationTestBase
{
    private readonly IMyPointAppService _myPointAppService;

    public MyPointAppService_Tests()
    {
        _myPointAppService = GetRequiredService<IMyPointAppService>();
    }

    [Fact]
    public async Task GetAccountsAsync()
    {
        var accounts = await _myPointAppService.GetAccountsAsync();
        accounts.Items.Count.ShouldBe(2);
        accounts.Items.Sum(a => a.CurrentBalance).ShouldBe(20);
    }

    [Fact]
    public async Task GetTransactionsAsync()
    {
        var transactions = await _myPointAppService.GetListAsync(new GetUserPointTransactionsInput());

        transactions.TotalCount.ShouldBeGreaterThan(0);
    }
}
