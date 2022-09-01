using System;

namespace Dignite.Abp.Files;

public interface ICurrentFile
{
    bool IsAvailable { get; }

    IFile FileDescriptor { get; }

    IDisposable Current(IFile fileDescriptor);
}
