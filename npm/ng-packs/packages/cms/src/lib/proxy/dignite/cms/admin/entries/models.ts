import type { CustomizableObject } from '../../../abp/data/models';
import type { ExtensibleAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { EntryStatus } from '../../entries/entry-status.enum';
import type { CmsUserDto } from '../../../../volo/cms-kit/users/models';

export interface CreateEntryInput extends CreateOrUpdateEntryInputBase {
  initialVersionId?: string;
}

export interface CreateOrUpdateEntryInputBase extends CustomizableObject {
  entryTypeId: string;
  culture: string;
  slug: string;
  parentId?: string;
  draft: boolean;
  publishTime: string;
  versionNotes?: string;
}

export interface CultureExistWithSingleSectionInput {
  culture: string;
  sectionId: string;
  entryTypeId: string;
}

export interface EntryDto extends ExtensibleAuditedEntityDto<string> {
  sectionId?: string;
  entryTypeId?: string;
  culture?: string;
  slug?: string;
  publishTime?: string;
  status: EntryStatus;
  parentId?: string;
  order: number;
  initialVersionId?: string;
  isActivatedVersion: boolean;
  versionNotes?: string;
  author: CmsUserDto;
  concurrencyStamp?: string;
}

export interface GetEntriesInput extends PagedAndSortedResultRequestDto {
  culture: string;
  sectionId: string;
  entryTypeId?: string;
  startPublishDate?: string;
  expiryPublishDate?: string;
  status?: EntryStatus;
  creatorId?: string;
  queryingByFieldsJson?: string;
}

export interface MoveEntryInput {
  parentId?: string;
  order: number;
}

export interface SlugExistsInput {
  culture: string;
  sectionId: string;
  slug?: string;
}

export interface UpdateEntryInput extends CreateOrUpdateEntryInputBase {
  concurrencyStamp?: string;
}
