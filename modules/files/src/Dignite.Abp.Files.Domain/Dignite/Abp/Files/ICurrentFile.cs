using System;

namespace Dignite.Abp.Files;

public interface ICurrentFile
{
    bool IsAvailable { get; }

    IFile File { get; }

    IDisposable Current(IFile fileDescriptor);
}