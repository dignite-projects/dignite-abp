
namespace Dignite.Abp.BlobStoring.InfoPersistent;

public interface ICurrentBlobInfoAccessor
{
    IBlobInfo Current { get; set; }
}
