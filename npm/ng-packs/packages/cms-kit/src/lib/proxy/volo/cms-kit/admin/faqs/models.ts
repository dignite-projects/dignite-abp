import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateFaqQuestionDto {
  sectionId: string;
  title: string;
  text: string;
  order: number;
}

export interface CreateFaqSectionDto {
  groupName: string;
  name: string;
  order: number;
}

export interface FaqGroupInfoDto {
  name: string;
}

export interface FaqQuestionDto extends FullAuditedEntityDto<string> {
  sectionId?: string;
  title?: string;
  text?: string;
  order: number;
}

export interface FaqQuestionListFilterDto extends PagedAndSortedResultRequestDto {
  sectionId?: string;
  filter?: string;
}

export interface FaqSectionDto extends FullAuditedEntityDto<string> {
  groupName?: string;
  name?: string;
  order: number;
}

export interface FaqSectionListFilterDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface FaqSectionWithQuestionCountDto {
  id?: string;
  groupName?: string;
  name?: string;
  order: number;
  questionCount: number;
  creationTime?: string;
}

export interface UpdateFaqQuestionDto {
  title: string;
  text: string;
  order: number;
}

export interface UpdateFaqSectionDto {
  groupName: string;
  name: string;
  order: number;
}
