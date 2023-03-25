using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.BlazoriseUI.Components;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Dignite.FileExplorer.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Dignite.FileExplorer.Blazor.Pages.FileExplorer;
public partial class DirectoryTreeComponent
{
    protected IList<DirectoryDescriptorInfoDto> AllDirectories = new List<DirectoryDescriptorInfoDto>();
    protected IList<DirectoryDescriptorInfoDto> ExpandedNodes = new List<DirectoryDescriptorInfoDto>();

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
            var result = await AppService.GetMyAsync(ContainerName);

            Entities = result.Items;

            AllDirectories = Entities.ToList();
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


    protected virtual async void OnDroped(DropNode<DirectoryDescriptorInfoDto> dropNode)
    {
        if (dropNode.Node.Id == dropNode.Target.Id)
            return;

        //
        await MoveAsync(dropNode.Node, dropNode.Target, dropNode.Area);

        await this.InvokeAsync(() => this.StateHasChanged());
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
        if (!NewEntity.ParentId.HasValue) //add root directory
        {
            await GetEntitiesAsync();
            ExpandedNodes.Clear();
        }
        else
        {
            var currentNode = AllDirectories.FindById(NewEntity.ParentId.Value);

            var children = (await AppService.GetListAsync(new GetDirectoriesInput
            {
                CreatorId=CurrentUser.Id,
                ContainerName = ContainerName,
                ParentId = currentNode.Id
            })).Items;

            currentNode.Children.Clear();
            foreach (var ou in children)
            {
                currentNode.AddChild(ou);
            }

            //Expand current node
            if (!ExpandedNodes.Any(n => n.Id == currentNode.Id))
            {
                ExpandedNodes.Add(currentNode);
            }
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
            }
            else
            {
                await GetEntitiesAsync();
                SelectedDirectory = null;
            }
        }
    }

    protected override async Task OnDeletedEntityAsync()
    {
        await InvokeAsync(StateHasChanged);
        await Notify.Success(L["SuccessfullyDeleted"]);
    }

    protected virtual async Task MoveAsync(DirectoryDescriptorInfoDto source, DirectoryDescriptorInfoDto target, DragEnterNodePosition position)
    {
        // forbid moving to child nodes
        if (source.Children.FindById(target.Id)!=null)
        {
            await Message.Error(L[FileExplorerErrorCodes.Directories.ForbidMovingToChild]);
            return;
        }

        //
        if (source.ParentId.HasValue)
        {
            var parent=AllDirectories.FindById(source.ParentId.Value);
            parent.RemoveChild(source);
        }
        else
        {
            AllDirectories.RemoveAll(ou => ou.Id == source.Id);
        }

        //
        if (position == DragEnterNodePosition.Inside)
        {
            source.ParentId = target.Id;
            if (target.HasChildren)
            {
                if (!target.Children.Any())
                {
                    var children = (await AppService.GetListAsync(new GetDirectoriesInput
                    {
                        CreatorId=CurrentUser.Id,
                        ContainerName= source.ContainerName,
                        ParentId = target.Id
                    })).Items;

                    foreach (var ou in children)
                    {
                        target.AddChild(ou);
                    }
                }
            }

            //
            target.AddChild(source);
            //Expand current node
            if (!ExpandedNodes.Any(n => n.Id == target.Id))
            {
                ExpandedNodes.Add(target);
            }
        }
        else if (position == DragEnterNodePosition.Bottom)
        {
            if (target.ParentId.HasValue)
            {
                source.ParentId = target.ParentId;
                AllDirectories.FindById(target.ParentId.Value).Children.InsertAfter(target, source);
            }
            else
            {
                source.ParentId = null;
                AllDirectories.InsertAfter(target, source);
            }
        }

        // move api
        await AppService.MoveAsync(
            source.Id,
            new MoveDirectoryInput
            {
                TargetId = target.Id,
                Position = position == DragEnterNodePosition.Inside ? DirectoryMovePosition.Inside : DirectoryMovePosition.Bottom
            });
    }
}
