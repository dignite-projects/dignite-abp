import type { AuditedEntityDto, ExtensibleAuditedEntityDto, ExtensibleObject, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { SectionType } from '../../sections/section-type.enum';
import type { EntryFieldTabDto } from '../../sections/models';

export interface CreateEntryTypeInput extends CreateOrUpdateEntryTypeInputBase {
  sectionId: string;
}

export interface CreateOrUpdateEntryTypeInputBase {
  displayName: string;
  name: string;
  fieldTabs: EntryFieldTabInput[];
}

export interface CreateOrUpdateSectionInputBase extends ExtensibleObject {
  type: SectionType;
  displayName: string;
  name: string;
  isDefault: boolean;
  isActive: boolean;
  route?: string;
  template?: string;
}

export interface CreateSectionInput extends CreateOrUpdateSectionInputBase {
}

export interface EntryFieldInput {
  fieldId: string;
  displayName: string;
  required: boolean;
  showInList: boolean;
  enableSearch: boolean;
}

export interface EntryFieldTabInput {
  name: string;
  fields: EntryFieldInput[];
}

export interface EntryTypeDto extends AuditedEntityDto<string> {
  displayName?: string;
  name?: string;
  fieldTabs: EntryFieldTabDto[];
}

export interface EntryTypeNameExistsInput {
  sectionId: string;
  name: string;
}

export interface GetSectionsInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  isActive?: boolean;
}

export interface SectionDto extends ExtensibleAuditedEntityDto<string> {
  type: SectionType;
  displayName?: string;
  name?: string;
  isDefault: boolean;
  isActive: boolean;
  route?: string;
  template?: string;
  concurrencyStamp?: string;
  entryTypes: EntryTypeDto[];
}

export interface SectionNameExistsInput {
  name: string;
}

export interface SectionRouteExistsInput {
  route: string;
}

export interface UpdateEntryTypeInput extends CreateOrUpdateEntryTypeInputBase {
}

export interface UpdateSectionInput extends CreateOrUpdateSectionInputBase {
  concurrencyStamp?: string;
}
