import { mapEnumToOptions } from '@abp/ng.core';

export enum SectionType {
  Single = 0,
  Structure = 1,
  Channel = 2,
}

export const sectionTypeOptions = mapEnumToOptions(SectionType);
