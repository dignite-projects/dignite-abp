import type { ExtensibleEntityDto } from '@abp/ng.core';

export interface CmsUserDto extends ExtensibleEntityDto<string> {
  tenantId?: string;
  userName?: string;
  name?: string;
  surname?: string;
}
