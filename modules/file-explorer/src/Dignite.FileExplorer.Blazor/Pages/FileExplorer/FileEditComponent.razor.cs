using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Dignite.FileExplorer.Files;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class FileEditComponent
{
    protected readonly IFileDescriptorAppService FileDescriptorAppService;

    /// <summary>
    /// 
    /// </summary>
    protected virtual BlobHandlerConfigurationDto Configuration { get; private set; }

    protected long MaxFileSize = long.MaxValue;

    public virtual List<FileDescriptorDto> FileDescriptors { get; protected set; }

    public virtual List<IFileEntry> Files { get; protected set; }

    public FileEditComponent(IFileDescriptorAppService fileDescriptorAppService)
    {
        FileDescriptorAppService = fileDescriptorAppService;
        Configuration = new BlobHandlerConfigurationDto();
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
    public EventCallback<FileChangedEventArgs> Changed { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Configuration = await FileDescriptorAppService.GetBlobHandlerConfiguration(ContainerName);
        MaxFileSize = Configuration.MaximumBlobSize == 0 ? long.MaxValue : (Configuration.MaximumBlobSize*1024);

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

        await base.OnInitializedAsync();
    }

    private async Task OnFileChangedAsync(FileChangedEventArgs e)
    {
        Files = e.Files.ToList();
        if (!Multiple)
        {
            FileDescriptors.Clear();
        }
        await Changed.InvokeAsync(e);
    }
}
