import type { AuditedEntityDto, ExtensibleAuditedEntityDto, ExtensibleObject, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { SectionType } from '../../sections/section-type.enum';
import type { EntryFieldTabDto } from '../../sections/models';
import type { SiteDto } from '../sites/models';

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
  siteId: string;
}

export interface EntryFieldInput {
  fieldId: string;
  displayName: string;
  required: boolean;
  showOnList: boolean;
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
  siteId?: string;
  filter?: string;
  isActive?: boolean;
}

export interface SectionDto extends ExtensibleAuditedEntityDto<string> {
  siteId?: string;
  site: SiteDto;
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
  siteId: string;
  name: string;
}

export interface SectionRouteExistsInput {
  siteId: string;
  route: string;
}

export interface UpdateEntryTypeInput extends CreateOrUpdateEntryTypeInputBase {
}

export interface UpdateSectionInput extends CreateOrUpdateSectionInputBase {
  concurrencyStamp?: string;
}
