using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Dignite.Abp.BlazoriseUI.Components;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.Content;
using Volo.Abp.Users;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class FileExplorerModal
{
    protected readonly IFileDescriptorAppService FileDescriptorAppService;
    protected Modal _modal;

    protected AbpExtensibleDataGrid<FileDescriptorDto> FileDataGridRef;
    private List<TableColumn> FileTableColumns => TableColumns.Get<FileExplorerModal>();

    protected string ContainerName { get; set; }

    protected string EntityId { get; set; }
    protected BlobHandlerConfigurationDto Configuration { get; set; }

    protected long MaxFileSize = long.MaxValue;
    public virtual IFileEntry[] Files { get; protected set; }

    [Parameter] 
    public EventCallback<List<FileDescriptorDto>> SelectFiles { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    public FileExplorerModal(IFileDescriptorAppService fileDescriptorAppService)
    {
        ObjectMapperContext = typeof(FileExplorerBlazorModule);
        LocalizationResource = typeof(FileExplorerResource);

        FileDescriptorAppService = fileDescriptorAppService;
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<FileExplorerModal>()
            .AddRange(new EntityAction[]
            {
                new EntityAction
                {
                    Text = L["Rename"],
                    Visible = (data) => CurrentUser.GetId() == data.As<FileDescriptorDto>().CreatorId,
                    Clicked = async (data) => await OpenEditModalAsync(data.As<FileDescriptorDto>())
                },
                new EntityAction
                {
                    Text = L["Delete"],
                    Visible = (data) => CurrentUser.GetId() == data.As<FileDescriptorDto>().CreatorId,
                    Clicked = async (data) => await DeleteEntityAsync(data.As<FileDescriptorDto>()),
                    ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<FileDescriptorDto>())
                }
            });

        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetTableColumnsAsync()
    {
        FileTableColumns
            .AddRange(new TableColumn[]
            {
                new TableColumn
                {
                    Title = L["Name"],
                    Data = nameof(FileDescriptorDto.Name),
                    Sortable = true,
                },
                new TableColumn
                {
                    Title = L["Size"],
                    Data = nameof(FileDescriptorDto.Size),
                    Sortable = true,
                },
                new TableColumn
                {
                    Title = L["EntityId"],
                    Data = nameof(FileDescriptorDto.EntityId),
                    Sortable = true,
                },
                new TableColumn
                {
                    Title = L["CreationTime"],
                    Data = nameof(FileDescriptorDto.CreationTime),
                    Sortable = true,
                },
                new TableColumn
                {
                    Title = L["Actions"],
                    Actions = EntityActions.Get<FileExplorerModal>(),
                }
            });

        return base.SetEntityActionsAsync();
    }

    protected override Task UpdateGetListInputAsync()
    {
        GetListInput.ContainerName = ContainerName;
        GetListInput.CreatorId = CurrentUser.Id;
        //GetListInput.EntityId = EntityId;

        return base.UpdateGetListInputAsync();
    }

    public virtual async Task OpenAsync(string containerName, string entityId)
    {
        try
        {
            CurrentPage = 1;
            ContainerName = containerName;
            EntityId = entityId;


            Configuration = await FileDescriptorAppService.GetBlobHandlerConfiguration(ContainerName);
            MaxFileSize = Configuration.MaximumBlobSize == 0 ? long.MaxValue : (Configuration.MaximumBlobSize * 1024);

            await GetEntitiesAsync();
            await InvokeAsync(_modal.Show);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }


    private async Task OnFileChangedAsync(FileChangedEventArgs e)
    {
        this.Files = e.Files;
        foreach (var file in e.Files)
        {
            var input = new CreateFileInput();
            input.ContainerName = ContainerName;
            input.EntityId = EntityId;
            input.File = new RemoteStreamContent(
                                                file.OpenReadStream(long.MaxValue),
                                                file.Name,
                                                file.Type
                                                );

            await FileDescriptorAppService.CreateAsync(input);
        }
    }

    protected virtual async Task SelectFilesAsync()
    {
        try
        {
            await InvokeAsync(() => {
                SelectFiles.InvokeAsync(FileDataGridRef.SelectedItems);
                _modal.Hide();
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected Task CloseModal()
    {
        return InvokeAsync(_modal.Hide);
    }

    protected virtual Task ClosingModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }
}
