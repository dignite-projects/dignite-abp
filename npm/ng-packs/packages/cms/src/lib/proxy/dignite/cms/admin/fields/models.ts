import type { FieldDto1 } from '../../fields/models';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateFieldInput extends CreateOrUpdateFieldInputBase {
}

export interface CreateOrUpdateFieldGroupInput {
  name: string;
}

export interface CreateOrUpdateFieldInputBase {
  groupId?: string;
  displayName: string;
  name: string;
  description?: string;
  formControlName: string;
  formConfiguration: Record<string, object>;
}

export interface FieldDto extends FieldDto1 {
  groupId?: string;
  groupName?: string;
  creationTime?: string;
  creatorId?: string;
  lastModifierId?: string;
  lastModificationTime?: string;
}

export interface FieldGroupDto extends EntityDto<string> {
  name?: string;
}

export interface GetFieldGroupsInput {
}

export interface GetFieldsInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  groupId?: string;
}

export interface UpdateFieldInput extends CreateOrUpdateFieldInputBase {
}
