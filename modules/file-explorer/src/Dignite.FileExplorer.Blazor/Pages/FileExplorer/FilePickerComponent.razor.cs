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

    [Parameter]
    public string CellName { get; set; }

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
    public List<FileDescriptorDto> FileDescriptors { get; set; }

    [Parameter] 
    public EventCallback<List<FileDescriptorDto>> FileDescriptorsChanged { get; set; }

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
        if (!EntityId.IsNullOrEmpty() && FileDescriptors==null)
        {
            FileDescriptors = (await FileDescriptorAppService.GetListAsync(new GetFilesInput
            {
                SkipCount = 0,
                ContainerName = ContainerName,
                EntityId = EntityId,
                MaxResultCount = 1000
            })).Items.ToList();
            await InvokeAsync(() =>
            {
                FileDescriptorsChanged.InvokeAsync(FileDescriptors);
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
                FileDescriptors = files;
            }
            else
            {
                FileDescriptors = FileDescriptors == null ? new List<FileDescriptorDto>() : FileDescriptors;
                foreach (var file in files)
                {
                    if (!FileDescriptors.Any(fd => fd.Id == file.Id))
                    {
                        FileDescriptors.Add(file);
                    }
                }
            }
            await InvokeAsync(() =>
            {
                FileDescriptorsChanged.InvokeAsync(FileDescriptors);
            });
        }
    }

    protected virtual async Task OpenFileExplorerModalAsync()
    {
        await OpeningFileExplorerModal.InvokeAsync();

        await _fileExplorerModal.OpenAsync(ContainerName, CellName, EntityId);
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
            if (await Message.Confirm(L["DeletionConfirmationMessage", fileDescriptor.Name]))
            {
                await FileDescriptorAppService.DeleteAsync(fileDescriptor.Id);
            }
        }
        await RemoveFileItem(fileDescriptor);
    }

    protected virtual async Task RemoveFileItem(FileDescriptorDto fileDescriptor)
    {
        FileDescriptors.RemoveAll(fd => fd.Id == fileDescriptor.Id);
        await InvokeAsync(() =>
        {
            FileDescriptorsChanged.InvokeAsync(FileDescriptors);
        });
    }
}

