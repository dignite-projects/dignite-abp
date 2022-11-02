using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.FileExplorer.Files;

public class FakeEntity : Entity<Guid>
{
    public FakeEntity(Guid id) : base(id)
    {
    }
}