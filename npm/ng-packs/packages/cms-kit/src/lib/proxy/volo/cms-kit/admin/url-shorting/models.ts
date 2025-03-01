import type { CreationAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateShortenedUrlDto {
  source: string;
  target: string;
  isRegex: boolean;
}

export interface GetShortenedUrlListInput extends PagedAndSortedResultRequestDto {
  shortenedUrlFilter?: string;
}

export interface ShortenedUrlDto extends CreationAuditedEntityDto<string> {
  source?: string;
  target?: string;
  isRegex: boolean;
}

export interface UpdateShortenedUrlDto {
  target: string;
}
