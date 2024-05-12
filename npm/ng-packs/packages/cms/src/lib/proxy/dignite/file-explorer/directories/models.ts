import type { ExtensibleAuditedEntityDto, ExtensibleEntityDto } from '@abp/ng.core';

export interface CreateDirectoryInput {
  containerName: string;
  name: string;
  parentId?: string;
}

export interface DirectoryDescriptorDto extends ExtensibleAuditedEntityDto<string> {
  containerName?: string;
  name?: string;
  parentId?: string;
  order: number;
}

export interface DirectoryDescriptorInfoDto extends ExtensibleEntityDto<string> {
  containerName?: string;
  name?: string;
  parentId?: string;
  order: number;
  children: DirectoryDescriptorInfoDto[];
}

export interface GetDirectoriesInput {
  containerName: string;
}

export interface MoveDirectoryInput {
  parentId?: string;
  order: number;
}

export interface UpdateDirectoryInput {
  name: string;
}
