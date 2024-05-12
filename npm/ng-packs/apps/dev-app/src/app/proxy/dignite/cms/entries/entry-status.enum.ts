import { mapEnumToOptions } from '@abp/ng.core';

export enum EntryStatus {
  Draft = 0,
  Published = 1,
}

export const entryStatusOptions = mapEnumToOptions(EntryStatus);
