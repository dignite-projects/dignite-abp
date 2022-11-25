using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazorise;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Components;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class FilePickerComponent
{
    protected FileExplorerModal _fileExplorerModal;
    protected readonly IFileDescriptorAppService FileDescriptorAppService;

    public FilePickerComponent(IFileDescriptorAppService fileDescriptorAppService)
    {
        FileDescriptorAppService = fileDescriptorAppService;
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

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public List<FileDescriptorDto> SelectedFiles { get; set; }

    [Parameter] 
    public EventCallback<List<FileDescriptorDto>> SelectedFilesChanged { get; set; }

    /// <summary>
    /// Event triggered when a modal window for selecting files is opened
    /// </summary>
    /// <example>
    /// When opening the Select File Mode window, judge whether the blog post already exists. 
    /// If not, create a draft
    /// </example>
    [Parameter]
    public EventCallback OpeningFileExplorerModal { get; set; }

    [Parameter]
    public RenderFragment<List<FileDescriptorDto>> FileDescriptorsContent { get; set; }


    /// <summary>
    /// Validation handler used to validate selected value.
    /// </summary>
    [Parameter] public Action<ValidatorEventArgs> Validator { get; set; }

    /// <summary>
    /// Asynchronously validates the selected value.
    /// </summary>
    [Parameter] public Func<ValidatorEventArgs, CancellationToken, Task> AsyncValidator { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (!EntityId.IsNullOrEmpty() && SelectedFiles==null)
        {
            SelectedFiles = (await FileDescriptorAppService.GetListAsync(new GetFilesInput
            {
                SkipCount = 0,
                ContainerName = ContainerName,
                EntityId = EntityId,
                MaxResultCount = 1000
            })).Items.ToList();
            await InvokeAsync(() =>
            {
                SelectedFilesChanged.InvokeAsync(SelectedFiles);
            });
        }
        await base.OnInitializedAsync();
    }

    protected virtual async Task SelectFilesAsync(List<FileDescriptorDto> files)
    {
        if (files != null && files.Any())
        {
            if (!Multiple)
            {
                SelectedFiles = files;
            }
            else
            {
                SelectedFiles = SelectedFiles == null ? new List<FileDescriptorDto>() : SelectedFiles;
                foreach (var file in files)
                {
                    if (!SelectedFiles.Any(fd => fd.Id == file.Id))
                    {
                        SelectedFiles.Add(file);
                    }
                }
            }
            await InvokeAsync(() =>
            {
                SelectedFilesChanged.InvokeAsync(SelectedFiles);
            });
        }
    }

    protected virtual async Task OpenFileExplorerModalAsync()
    {
        await OpeningFileExplorerModal.InvokeAsync();

        await _fileExplorerModal.OpenAsync(ContainerName, EntityId);
    }

    protected internal virtual async Task RemoveFileAsync(FileDescriptorDto fileDescriptor)
    {
        if (fileDescriptor.EntityId == EntityId)
        {
            /*
             * Prompt the user whether to delete the original file when removing the file
             * True: delete the original file
               False: Remove file information only
             * */
            if (await Message.Confirm(L["FileWillBeDeletedMessage"]))
            {
                await FileDescriptorAppService.DeleteAsync(fileDescriptor.Id);
            }
        }
        await RemoveFileItem(fileDescriptor);
    }

    protected virtual async Task RemoveFileItem(FileDescriptorDto fileDescriptor)
    {
        SelectedFiles.RemoveAll(fd => fd.Id == fileDescriptor.Id);
        await InvokeAsync(() =>
        {
            SelectedFilesChanged.InvokeAsync(SelectedFiles);
        });
    }
}

