import type { FieldDto } from '../fields/models';

export interface EntryFieldDto {
  fieldId?: string;
  field: FieldDto;
  displayName?: string;
  required: boolean;
  showInList: boolean;
  enableSearch: boolean;
}

export interface EntryFieldTabDto {
  name?: string;
  fields: EntryFieldDto[];
}
