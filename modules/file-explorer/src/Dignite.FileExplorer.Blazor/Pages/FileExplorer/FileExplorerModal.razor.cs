using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Dignite.Abp.BlazoriseUI.Components;
using Dignite.FileExplorer.Directories;
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
    protected readonly IDirectoryDescriptorAppService DirectoryDescriptorAppService;
    protected Modal _modal;

    protected ExtensibleDataGrid<FileDescriptorDto> FileDataGridRef;
    private List<TableColumn> FileTableColumns => TableColumns.Get<FileExplorerModal>();

    protected string ContainerName { get; set; }

    protected string CellName { get; set; }

    protected string EntityId { get; set; }
    protected FileContainerConfigurationDto Configuration { get; set; }

    protected long MaxFileSize = long.MaxValue;
    protected List<FileUpload> Files { get; set; }

    protected IDictionary<string, object> DirectoryTreeComponentParameters;

    protected DirectoryDescriptorInfoDto CurrentDirectory;

    protected IReadOnlyList<DirectoryDescriptorInfoDto> AllDirectories;

    [Parameter] 
    public EventCallback<List<FileDescriptorDto>> SelectFiles { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    public FileExplorerModal(IFileDescriptorAppService fileDescriptorAppService, IDirectoryDescriptorAppService directoryDescriptorAppService)
    {
        ObjectMapperContext = typeof(FileExplorerBlazorModule);
        LocalizationResource = typeof(FileExplorerResource);

        FileDescriptorAppService = fileDescriptorAppService;
        DirectoryDescriptorAppService = directoryDescriptorAppService;
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
                    Icon= "fa-trash",
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
                    Title = L["FileName"],
                    Data = nameof(FileDescriptorDto.Name),
                    Sortable = true,
                },
                new TableColumn
                {
                    Title = L["FileSize"],
                    Data = nameof(FileDescriptorDto.Size),
                    Sortable = true,
                    ValueConverter=(data)=> FileSizeFormatter.FormatSize(data.As<FileDescriptorDto>().Size)
                },
                new TableColumn
                {
                    Title = L["Directory"],
                    Data = nameof(FileDescriptorDto.DirectoryId),
                    Sortable = true,
                    ValueConverter = (data)=> data.As<FileDescriptorDto>().DirectoryId.HasValue? 
                                                                            AllDirectories.FindById(data.As<FileDescriptorDto>().DirectoryId.Value)?.Name
                                                                            :null
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

    public virtual async Task OpenAsync(string containerName, string cellName, string entityId)
    {
        try
        {
            CurrentPage = 1;
            ContainerName = containerName;
            CellName = cellName;
            EntityId = entityId;
            GetListInput.DirectoryId = null;

            await InvokeAsync(_modal.Show);

            AllDirectories = (await DirectoryDescriptorAppService.GetListAsync(new GetDirectoriesInput { ContainerName=containerName})).Items;
            await GetEntitiesAsync();
            Configuration = await FileDescriptorAppService.GetFileContainerConfigurationAsync(ContainerName);
            MaxFileSize = Configuration.MaxBlobSize == 0 ? long.MaxValue : (Configuration.MaxBlobSize * 1024);

            //
            DirectoryTreeComponentParameters = new Dictionary<string, object>
            {
                { nameof(DirectoryTreeComponent.ContainerName), containerName },
                { nameof(DirectoryTreeComponent.Configuration), Configuration },
                { nameof(DirectoryTreeComponent.SelectedDirectory), null},
                { nameof(DirectoryTreeComponent.SelectedDirectoryChanged), EventCallback.Factory.Create<DirectoryDescriptorInfoDto>(
                                            this, 
                                            SelectedDirectoryChanged
                                            )}
            };
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
                    fu.ErrorMessage = L["ExceedsMaximumSize", FileSizeFormatter.FormatSize(file.Size)];
                }

                Files.Add(fu);
            }

            //When a new directory is created and the directory structure of this component is not updated synchronously, the directory structure is updated here
            if (GetListInput.DirectoryId.HasValue
                && AllDirectories.FindById(GetListInput.DirectoryId.Value) == null
                )
            {
                AllDirectories = (await DirectoryDescriptorAppService.GetListAsync(new GetDirectoriesInput { ContainerName = ContainerName })).Items;
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
            input.CellName = CellName; 
            input.DirectoryId = GetListInput.DirectoryId;
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
                    CloseModal();
                });
            }
            else
            {
                await InvokeAsync(() =>
                {
                    if (FileDataGridRef.SelectedItem != null)
                    {
                        SelectFiles.InvokeAsync(
                            new List<FileDescriptorDto>
                            {
                            FileDataGridRef.SelectedItem
                            });
                    }
                    CloseModal();
                });
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task SelectedDirectoryChanged(DirectoryDescriptorInfoDto e)
    {
        FileDataGridRef.SelectedItem = null;
        FileDataGridRef.SelectedItems = null;
        DirectoryTreeComponentParameters.Remove(nameof(DirectoryTreeComponent.SelectedDirectory));
        DirectoryTreeComponentParameters.Add(nameof(DirectoryTreeComponent.SelectedDirectory), e);
        CurrentDirectory = e;
        GetListInput.DirectoryId = e == null ? null : e.Id;
        BreadcrumbItems = e == null ? null : e.GetParentList(AllDirectories)
                                            .Select(dd => new Volo.Abp.BlazoriseUI.BreadcrumbItem(dd.Name,dd.Id.ToString()))
                                            .ToList();
        await GetEntitiesAsync();
    }


    protected override void Dispose(bool disposing)
    {
        DirectoryTreeComponentParameters = null;
        Files = null;
        CurrentDirectory = null;
        base.Dispose(disposing);
    }

    protected Task CloseModal()
    {
        DirectoryTreeComponentParameters = null;
        Files = null;
        CurrentDirectory=null;
        return InvokeAsync(_modal.Hide);
    }

    protected virtual Task ClosingModal(ModalClosingEventArgs eventArgs)
    {
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;
        return Task.CompletedTask;
    }
}
