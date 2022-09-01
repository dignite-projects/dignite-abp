using System.Collections.Generic;

namespace Dignite.Abp.TestBase.Logging;

public interface ICanLogOnObject
{
    List<string> Logs { get; }
}
