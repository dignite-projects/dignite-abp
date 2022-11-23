using System;
using System.Collections.Generic;
using System.Linq;
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

    protected ExtensibleDataGrid<FileDescriptorDto> FileDataGridRef;
    private List<TableColumn> FileTableColumns => TableColumns.Get<FileExplorerModal>();

    protected string ContainerName { get; set; }

    protected string EntityId { get; set; }
    protected BlobHandlerConfigurationDto Configuration { get; set; }

    protected long MaxFileSize = long.MaxValue;
    protected virtual List<FileUpload> Files { get; set; }

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

    protected override Task OnDataGridReadAsync(DataGridReadDataEventArgs<FileDescriptorDto> e)
    {
        if (ContainerName.IsNullOrEmpty())
        {
            return Task.CompletedTask;
        }
        else
        {
            return base.OnDataGridReadAsync(e);
        }
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<FileExplorerModal>()
            .AddRange(new EntityAction[]
            {
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

    protected override void Dispose(bool disposing)
    {
        Files=null;
        base.Dispose(disposing);
    }

    public virtual async Task OpenAsync(string containerName, string entityId)
    {
        try
        {
            CurrentPage = 1;
            ContainerName = containerName;
            EntityId = entityId;


            await InvokeAsync(_modal.Show);
            await GetEntitiesAsync();
            Configuration = await FileDescriptorAppService.GetBlobHandlerConfiguration(ContainerName);
            MaxFileSize = Configuration.MaximumBlobSize == 0 ? long.MaxValue : (Configuration.MaximumBlobSize * 1024);

        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }


    private async Task OnFileChangedAsync(FileChangedEventArgs e)
    {
        if (e.Files.Any())
        {
            Files = new List<FileUpload>();
            foreach (var file in e.Files)
            {
                var fu = new FileUpload(file);
                if (file.Size > MaxFileSize)
                {
                    fu.Status = FileUploadStatus.Fail;
                    fu.ErrorMessage = L["ExceedsMaximumSize"];
                }

                Files.Add(fu);
            }

            // start upload
            await UploadAsync();
        }
    }

    protected virtual async Task UploadAsync()
    {
        foreach (var file in Files)
        {
            if (file.Status != FileUploadStatus.Ready)
                continue;

            file.Status = FileUploadStatus.Progressing;
            var input = new CreateFileInput();
            input.ContainerName = ContainerName;
            input.EntityId = EntityId;
            input.File = new RemoteStreamContent(
                                                file.File.OpenReadStream(long.MaxValue),
                                                file.File.Name,
                                                file.File.Type
                                                );
            try
            {
                var fd = await FileDescriptorAppService.CreateAsync(input);
                file.Status = FileUploadStatus.Success;
                var newEntities = Entities.ToList();
                newEntities.Insert(0, fd);
                Entities = newEntities;
                if (Multiple)
                {
                    if (FileDataGridRef.SelectedItems == null)
                    {
                        FileDataGridRef.SelectedItems = new List<FileDescriptorDto>();
                    }
                    FileDataGridRef.SelectedItems.Add(fd);
                }
                else
                {
                    FileDataGridRef.SelectedItem = fd;
                }
            }
            catch (Exception ex)
            {
                file.Status = FileUploadStatus.Fail;
                file.ErrorMessage = ex.Message;
            }
        }

        if (!Files.Any(f => f.Status == FileUploadStatus.Fail))
        {
            Files = null;
        }
    }

    protected virtual async Task SelectFilesAsync()
    {
        try
        {
            if (Multiple)
            {
                await InvokeAsync(() =>
                {
                    SelectFiles.InvokeAsync(FileDataGridRef.SelectedItems);
                    _modal.Hide();
                });
            }
            else
            {
                await InvokeAsync(() =>
                {
                    SelectFiles.InvokeAsync(
                        new List<FileDescriptorDto>
                        {
                            FileDataGridRef.SelectedItem
                        });
                    _modal.Hide();
                });
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected Task CloseModal()
    {
        Files = null;
        return InvokeAsync(_modal.Hide);
    }

    protected virtual Task ClosingModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }
}
