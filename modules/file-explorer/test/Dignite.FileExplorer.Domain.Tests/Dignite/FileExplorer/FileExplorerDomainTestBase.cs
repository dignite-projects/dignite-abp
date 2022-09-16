using System;
using Dignite.FileExplorer.EntityFrameworkCore;

namespace Dignite.FileExplorer;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */

public abstract class FileExplorerDomainTestBase : FileExplorerTestBase<FileExplorerDomainTestModule>
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