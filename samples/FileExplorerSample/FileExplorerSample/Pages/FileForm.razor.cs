using Dignite.FileExplorer.Files;

namespace FileExplorerSample.Pages;

public partial class FileForm
{
    string EntityId = "6526e1c4-deeb-3110-f979-3a07b0bb8d49";
    List<FileDescriptorDto> Files = new();

    protected override async Task OnInitializedAsync()
    {
        Files = (await FileDescriptorAppService.GetListAsync(new GetFilesInput
        {
            ContainerName = Services.SampleContainer.SampleContainerName,
            SkipCount = 0,
            MaxResultCount = 100,
            EntityId = EntityId
        })).Items.ToList();
        await base.OnInitializedAsync();
    }
}
