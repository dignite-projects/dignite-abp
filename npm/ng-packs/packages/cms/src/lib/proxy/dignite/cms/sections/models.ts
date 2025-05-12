import type { FieldDto1 } from '../fields/models';

export interface EntryFieldDto {
  fieldId?: string;
  field: FieldDto1;
  displayName?: string;
  required: boolean;
  showInList: boolean;
  enableSearch: boolean;
}

export interface EntryFieldTabDto {
  name?: string;
  fields: EntryFieldDto[];
}
