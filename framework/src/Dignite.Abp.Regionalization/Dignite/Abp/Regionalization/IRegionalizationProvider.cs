using System.Threading.Tasks;

namespace Dignite.Abp.Regionalization;
public interface IRegionalizationProvider
{
    Task<Regionalization> GetRegionalizationAsync();
}