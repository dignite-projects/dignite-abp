
namespace Dignite.Abp.UserPoints;

public class DignitePointsBlockOptions
{
    /// <summary>
    /// Points value conversion Factor, usually can be understood as how many points can be exchanged for 1 yuan of currency.
    /// Default value: 1;
    /// </summary>
    /// <remarks>
    /// The unit value of user points must be a multiple of the Factor, for example: the Factor is 100, the number of user points can be a multiple of 100\200\300 and so on.
    /// </remarks>
    public int Factor { get; set; } = 1;
}
