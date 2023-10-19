
namespace Dignite.Abp.UserPoints;

public class DignitePointsBlockOptions
{
    /// <summary>
    /// Points Redemption Factor,i.e. how much money can be redeemed for $1.
    /// Default value: 1;
    /// </summary>
    public int Factor { get; set; } = 1;
}
