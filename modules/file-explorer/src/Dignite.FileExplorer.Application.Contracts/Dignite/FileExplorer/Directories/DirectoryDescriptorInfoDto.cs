using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptorInfoDto: ExtensibleEntityDto<Guid>, IEquatable<DirectoryDescriptorInfoDto>
{
    public DirectoryDescriptorInfoDto() : base()
    {
        Children = new ObservableCollection<DirectoryDescriptorInfoDto>();
    }

    /// <summary>
    /// Container name of blob
    /// </summary>
    public string ContainerName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Guid? ParentId { get; set; }
    public int Order { get; set; }

    public ObservableCollection<DirectoryDescriptorInfoDto> Children { get; set; }


    public bool Equals(DirectoryDescriptorInfoDto other)
    {
        return this.Id == other.Id;
    }

    public void AddChild(DirectoryDescriptorInfoDto ou)
    {
        ou.ParentId = this.Id;
        this.Children.Add(ou);
    }

    public void RemoveChild(DirectoryDescriptorInfoDto ou)
    {
        this.Children.RemoveAll(c => ou.Id == c.Id);
    }
}