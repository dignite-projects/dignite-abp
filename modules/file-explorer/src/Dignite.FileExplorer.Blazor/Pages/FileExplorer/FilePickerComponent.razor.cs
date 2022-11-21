using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

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

    [Parameter]
    public List<FileDescriptorDto> FileDescriptors { get; set; }


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
                MaxResultCount = int.MaxValue
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
