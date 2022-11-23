using Blazorise;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public class FileUpload
{
    public FileUpload(IFileEntry file)
    {
        File = file;
        Status = FileUploadStatus.Ready;
    }

    public IFileEntry File { get; }

    public string ErrorMessage { get; set; }

    public FileUploadStatus Status { get; set; }
}
