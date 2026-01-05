using System.Collections.Generic;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace Dignite.Abp.UserPoints.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    private readonly UserPointsTestData _testData;

    public FakeCurrentPrincipalAccessor(UserPointsTestData testData)
    {
        this._testData = testData;
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return GetPrincipal();
    }

    private ClaimsPrincipal GetPrincipal()
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(AbpClaimTypes.UserId, _testData.User1Id.ToString()),
                    new Claim(AbpClaimTypes.UserName, _testData.User1UserName),
                    new Claim(AbpClaimTypes.Email, "admin@abp.io")
                }
            )
        );
    }
}
