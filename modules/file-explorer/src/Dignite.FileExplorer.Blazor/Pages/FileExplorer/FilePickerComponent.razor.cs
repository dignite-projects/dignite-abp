using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Components;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class FilePickerComponent
{
    protected FileExplorerModal _fileExplorerModal;

    public FilePickerComponent()
    {
        LocalizationResource = typeof(FileExplorerResource);
    }


    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string ContainerName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string EntityId { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public List<FileDescriptorDto> Files { get; set; }


    [Parameter]
    public EventCallback OpeningFileExplorerModal { get; set; }


    protected virtual Task SelectFilesAsync(IReadOnlyList<FileDescriptorDto> files)
    {
        Files.AddRange(files);
        return Task.CompletedTask;
    }

    protected virtual async Task OpenFileExplorerModalAsync()
    {
        await OpeningFileExplorerModal.InvokeAsync();

        await _fileExplorerModal.OpenAsync(ContainerName, EntityId);
    }
}
