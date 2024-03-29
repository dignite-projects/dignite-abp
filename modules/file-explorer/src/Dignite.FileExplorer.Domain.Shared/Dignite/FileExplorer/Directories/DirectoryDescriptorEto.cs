﻿using System;
using Volo.Abp.EventBus;

namespace Dignite.FileExplorer.Directories
{
    [EventName("Dignite.FileExplorer.Directories.DirectoryDescriptor")]
    [Serializable]
    public class DirectoryDescriptorEto
    {
        public Guid Id { get; set; }

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

        /// <summary>
        ///
        /// </summary>
        public int Order { get; set; }

        public Guid? TenantId { get; set; }
    }
}