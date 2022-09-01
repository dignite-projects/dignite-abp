

using System;

namespace Dignite.Abp.FieldCustomizing.Fields.Upload
{
    public class File
    {
        public File()
        {
        }

        public File(Guid id, string entityType, string entityId, string containerName, string blobName, long binarySize, string fileName)
        {
            Id = id;
            EntityType = entityType;
            EntityId = entityId;
            ContainerName = containerName;
            BlobName = blobName;
            BinarySize = binarySize;
            FileName = fileName;
        }
        public Guid Id { get; set; }

        public virtual string EntityType { get;  set; }

        public virtual string EntityId { get;  set; }

        public string ContainerName { get;  set; }

        public string BlobName { get;  set; }

        public long BinarySize { get;  set; }

        public string FileName { get;  set; }
    }
}
