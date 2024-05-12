import type { FieldDtoEntity } from '../fields/models';

export interface EntryFieldDto {
  fieldId?: string;
  field: FieldDtoEntity;
  displayName?: string;
  required: boolean;
  showOnList: boolean;
}

export interface EntryFieldTabDto {
  name?: string;
  fields: EntryFieldDto[];
}
