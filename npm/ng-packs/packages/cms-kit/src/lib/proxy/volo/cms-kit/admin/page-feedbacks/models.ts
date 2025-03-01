import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetPageFeedbackListInput extends PagedAndSortedResultRequestDto {
  entityType?: string;
  entityId?: string;
  isHandled?: boolean;
  isUseful?: boolean;
  url?: string;
}

export interface PageFeedbackDto extends EntityDto<string> {
  entityType?: string;
  entityId?: string;
  url?: string;
  isUseful: boolean;
  userNote?: string;
  isHandled: boolean;
  adminNote?: string;
  tenantId?: string;
  creationTime?: string;
}

export interface PageFeedbackSettingDto extends EntityDto<string> {
  entityType?: string;
  emailAddresses?: string;
  tenantId?: string;
}

export interface UpdatePageFeedbackDto {
  isHandled: boolean;
  adminNote?: string;
}

export interface UpdatePageFeedbackSettingDto {
  id?: string;
  entityType?: string;
  emailAddresses?: string;
}

export interface UpdatePageFeedbackSettingsInput {
  settings: UpdatePageFeedbackSettingDto[];
}
