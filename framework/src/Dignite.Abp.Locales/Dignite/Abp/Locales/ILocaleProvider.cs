using System.Threading.Tasks;

namespace Dignite.Abp.Locales;
public interface ILocaleProvider
{
    Task<LocaleInfo> GetLocaleAsync();
}