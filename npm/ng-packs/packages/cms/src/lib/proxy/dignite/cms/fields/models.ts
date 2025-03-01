import type { EntityDto } from '@abp/ng.core';

export interface FieldDto extends EntityDto<string> {
  name?: string;
  displayName?: string;
  description?: string;
  formControlName?: string;
  formConfiguration: Record<string, object>;
}
