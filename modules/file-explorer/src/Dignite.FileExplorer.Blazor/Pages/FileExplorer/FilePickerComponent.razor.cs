using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Components;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class FilePickerComponent
{
    protected FileExplorerModal _fileExplorerModal;
    public List<FileDescriptorDto> FileDescriptors { get; set; }
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
    /// Event triggered when a modal window for selecting files is opened
    /// </summary>
    /// <example>
    /// When opening the Select File Mode window, judge whether the blog post already exists. 
    /// If not, create a draft
    /// </example>
    [Parameter]
    public EventCallback OpeningFileExplorerModal { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (!EntityId.IsNullOrEmpty())
        {
            FileDescriptors = (await FileDescriptorAppService.GetListAsync(new GetFilesInput
            {
                SkipCount = 0,
                ContainerName = ContainerName,
                EntityId = EntityId,
                MaxResultCount = 1000
            })).Items.ToList();
        }
        else
        {
            FileDescriptors = new List<FileDescriptorDto>();
        }

        await base.OnInitializedAsync();
    }



    protected virtual Task SelectFilesAsync(List<FileDescriptorDto> files)
    {
        if (files != null && files.Any())
        {
            if (!Multiple)
            {
                FileDescriptors = files;
            }
            else
            {
                FileDescriptors.AddRange(files);
            }
        }
        return Task.CompletedTask;
    }

    protected virtual async Task OpenFileExplorerModalAsync()
    {
        await OpeningFileExplorerModal.InvokeAsync();

        await _fileExplorerModal.OpenAsync(ContainerName, EntityId);
    }
}
