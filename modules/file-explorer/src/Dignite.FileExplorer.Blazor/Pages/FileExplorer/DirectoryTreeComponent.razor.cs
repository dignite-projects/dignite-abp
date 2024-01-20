using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazorise;
using Dignite.Abp.BlazoriseUI.Components;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using static System.Collections.Specialized.BitVector32;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class DirectoryTreeComponent
{
    protected ExtensibleTreeView<DirectoryDescriptorInfoDto> ExtensibleTreeViewRef;
    protected ObservableCollection<DirectoryDescriptorInfoDto> AllDirectories = new ();
    protected ObservableCollection<DirectoryDescriptorInfoDto> ExpandedNodes = new ();

    [Parameter] public DirectoryDescriptorInfoDto SelectedDirectory { get; set; }
    [Parameter] public EventCallback<DirectoryDescriptorInfoDto> SelectedDirectoryChanged { get; set; }
    [Parameter] public string ContainerName { get; set; }
    [Parameter] public FileContainerConfigurationDto Configuration { get; set; }

    public DirectoryTreeComponent()
    {
        LocalizationResource = typeof(FileExplorerResource);
    }
    protected override async Task OnInitializedAsync()
    {
        HasCreatePermission = Configuration.CreateDirectoryPermissionName.IsNullOrEmpty() ? false : await AuthorizationService.IsGrantedAsync(Configuration.CreateDirectoryPermissionName);
        HasUpdatePermission = HasCreatePermission;
        HasDeletePermission = HasCreatePermission;
        await GetEntitiesAsync();
        await base.OnInitializedAsync();
    }
    protected override async Task GetEntitiesAsync()
    {
        try
        {
            var result = await AppService.GetListAsync(
                new GetDirectoriesInput { 
                    ContainerName = ContainerName,
                });
            Entities = result.Items;
            AllDirectories = new ObservableCollection<DirectoryDescriptorInfoDto>(Entities);
            SelectedDirectory = null;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task OnSelectDescriptorChanged(DirectoryDescriptorInfoDto args)
    {
        SelectedDirectory = args;
        await SelectedDirectoryChanged.InvokeAsync(args);
    }


    protected virtual async Task OnDroped(DropNode<DirectoryDescriptorInfoDto> dropNode)
    {
        var source = dropNode.Source;
        var target = dropNode.Target;
        var position = dropNode.Position;

        // forbid moving to child nodes
        if (dropNode.Source.Children.FindById(target.Id) != null)
        {
            await Message.Error(L[FileExplorerErrorCodes.Directories.ForbidMovingToChild]);
            return;
        }

        //
        if (source.ParentId.HasValue)
        {
            var parent = AllDirectories.FindById(source.ParentId.Value);
            parent.RemoveChild(source);
        }
        else
        {
            AllDirectories.RemoveAll(ou => ou.Id == source.Id);
        }

        //
        if (position == DragEnterNodePosition.Inside)
        {
            Guid parentId = target.Id;
            int order = target.Children.Max(x => x.Order as int?) ?? 0;
            order = order + 1;
            source.ParentId = parentId;
            source.Order = order;

            //
            target.AddChild(source);
            //Expand current node
            if (!ExpandedNodes.Any(n => n.Id == target.Id))
            {
                ExpandedNodes.Add(target);
            }
            await MoveInDatabaseAsync(source.Id, parentId, order);
        }
        else if (position == DragEnterNodePosition.Bottom)
        {
            Guid? parentId = null;
            int order = target.Order + 1;
            if (target.ParentId.HasValue)
            {
                var parent = AllDirectories.FindById(target.ParentId.Value);
                parentId = parent.Id;

                source.ParentId = parentId;
                source.Order = order;
                MoveNode(source, target, parent.Children);
            }
            else
            {
                parentId = null;

                source.ParentId = parentId;
                source.Order = order;
                MoveNode(source, target,AllDirectories);
            }
            await MoveInDatabaseAsync(source.Id, parentId, order);
        }

        //
        await OnSelectDescriptorChanged(source);
        await ExtensibleTreeViewRef.Reload();
    }

    private void MoveNode(DirectoryDescriptorInfoDto source, DirectoryDescriptorInfoDto target, ObservableCollection<DirectoryDescriptorInfoDto> list)
    {
        foreach (var item in list.Where(i => i.Order > target.Order && i.Id != source.Id))
        {
            item.Order = item.Order + 1;
        }
        list.InsertAfter(target, source);
    }

    protected virtual async Task OnAddRootDirectoryClicked()
    {
        await OpenCreateModalAsync();
        NewEntity.ParentId = null;
        NewEntity.ContainerName = ContainerName;
    }

    protected virtual async Task OnAddSubDirectoryClicked(DirectoryDescriptorInfoDto node)
    {
        await OpenCreateModalAsync();
        NewEntity.ParentId = node.Id;
        NewEntity.ContainerName = ContainerName;
    }

    protected virtual async Task OnEditDirectoryClicked(DirectoryDescriptorInfoDto node)
    {
        await OpenEditModalAsync(node);
    }

    protected override UpdateDirectoryInput MapToEditingEntity(DirectoryDescriptorDto entityDto)
    {
        var entity = base.MapToEditingEntity(entityDto);
        return entity;
    }

    protected override async Task OnCreatedEntityAsync()
    {
        if (NewEntity.ParentId.HasValue)
        {
            var result = (await AppService.GetListAsync(
                new GetDirectoriesInput
                {
                    ContainerName = ContainerName,
                })).Items.FindById(NewEntity.ParentId.Value);
            var parent = AllDirectories.FindById(NewEntity.ParentId.Value);

            await InvokeAsync(() =>
            {
                foreach (var item in result.Children)
                {
                    if (!parent.Children.Any(d => d.Id == item.Id))
                    {
                        parent.Children.Add(item);
                    }
                }
            });

            //Expand current node
            if (!ExpandedNodes.Any(n => n.Id == parent.Id))
            {
                ExpandedNodes.Add(parent);
            }
            await ExtensibleTreeViewRef.Reload();
        }
        else //add root directory
        {
            await GetEntitiesAsync();
        }
        await InvokeAsync(CreateModal.Hide);
    }

    protected override async Task OnUpdatedEntityAsync()
    {
        var currentNode = AllDirectories.FindById(EditingEntityId);
        currentNode.Name = EditingEntity.Name;

        await InvokeAsync(EditModal.Hide);
    }

    protected async override Task DeleteEntityAsync(DirectoryDescriptorInfoDto node)
    {
        if (await Message.Confirm(L["DeletionConfirmationMessage", node.Name]))
        {
            await base.DeleteEntityAsync(node);

            //
            if (node.ParentId.HasValue)
            {
                var parent = AllDirectories.FindById(node.ParentId.Value);
                parent.RemoveChild(node);
                SelectedDirectory = parent;
                await OnSelectDescriptorChanged(SelectedDirectory);
                await ExtensibleTreeViewRef.Reload();
            }
            else
            {
                AllDirectories.Remove(node);
                SelectedDirectory = null;
                await OnSelectDescriptorChanged(SelectedDirectory);
            }
        }
    }

    protected override async Task OnDeletedEntityAsync()
    {
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task MoveInDatabaseAsync(Guid sourceId,Guid? parentId,int order)
    {
        // move api
        await AppService.MoveAsync(
            sourceId,
            new MoveDirectoryInput(parentId, order)
            );
    }
    private Task CreateNameExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var name = Convert.ToString(e.Value);
        if (!name.IsNullOrEmpty())
        {
            if (NewEntity.ParentId.HasValue)
            {
                var parent = AllDirectories.FindById(NewEntity.ParentId.Value);
                e.Status = parent.Children.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["DirectoryName{0}AlreadyExist", name];
            }
            else
            {
                e.Status = AllDirectories.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["DirectoryName{0}AlreadyExist", name];
            }
        }
        else
        {
            e.Status = ValidationStatus.Error;
        }
        return Task.CompletedTask;
    }
    private Task EditNameExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var name = Convert.ToString(e.Value);
        if (!name.IsNullOrEmpty())
        {
            if (SelectedDirectory.ParentId.HasValue)
            {
                var parent = AllDirectories.FindById(SelectedDirectory.ParentId.Value);
                e.Status = parent.Children.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && x.Id!=SelectedDirectory.Id)
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["DirectoryName{0}AlreadyExist", name];
            }
            else
            {
                e.Status = AllDirectories.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && x.Id != SelectedDirectory.Id)
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["DirectoryName{0}AlreadyExist", name];
            }
        }
        else
        {
            e.Status = ValidationStatus.Error;
        }
        return Task.CompletedTask;
    }
}
