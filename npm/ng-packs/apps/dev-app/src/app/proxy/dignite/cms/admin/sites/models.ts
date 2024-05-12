import type { ExtensibleAuditedEntityDto, ExtensibleObject } from '@abp/ng.core';
import type { SiteLanguageDto } from '../../sites/models';

export interface CreateOrUpdateSiteInputBase extends ExtensibleObject {
  displayName: string;
  name: string;
  languages: SiteLanguageInput[];
  host: string;
  isActive: boolean;
}

export interface CreateSiteInput extends CreateOrUpdateSiteInputBase {
}

export interface GetSitesInput {
  filter?: string;
  isActive?: boolean;
}

export interface SiteDto extends ExtensibleAuditedEntityDto<string> {
  displayName?: string;
  name?: string;
  languages: SiteLanguageDto[];
  host?: string;
  isActive: boolean;
  concurrencyStamp?: string;
}

export interface SiteLanguageInput {
  isDefault: boolean;
  cultureName: string;
}

export interface UpdateSiteInput extends CreateOrUpdateSiteInputBase {
  concurrencyStamp?: string;
}
