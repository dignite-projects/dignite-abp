using Blazorise;
using Dignite.Abp.FieldCustomizing.Fields.Upload;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Blazor.Components.FieldCustomizing
{
    public class UploadFieldValue
    {
        public UploadFieldValue(List<IFileEntry> newFiles, List<File> existingFiles)
        {
            NewFiles = newFiles;
            ExistingFiles = existingFiles;
        }

        public List<IFileEntry> NewFiles { get; private set; }

        public List<File> ExistingFiles { get; private set; }
    }
}
