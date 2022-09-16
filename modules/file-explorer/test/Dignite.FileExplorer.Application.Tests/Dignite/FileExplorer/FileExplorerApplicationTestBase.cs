using System;
using Dignite.FileExplorer.EntityFrameworkCore;

namespace Dignite.FileExplorer;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */

public abstract class FileExplorerApplicationTestBase : FileExplorerTestBase<FileExplorerApplicationTestModule>
{
    protected virtual void UsingDbContext(Action<IFileExplorerDbContext> action)
    {
        using (var dbContext = GetRequiredService<IFileExplorerDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<IFileExplorerDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<IFileExplorerDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}