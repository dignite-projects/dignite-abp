import type { ExtensibleObject } from '@abp/ng.core';

export interface CmsUserDto extends ExtensibleObject {
  id?: string;
  userName?: string;
  name?: string;
  surname?: string;
}

export interface CommentDto extends ExtensibleObject {
  id?: string;
  entityType?: string;
  entityId?: string;
  text?: string;
  repliedCommentId?: string;
  creatorId?: string;
  creationTime?: string;
  author: CmsUserDto;
  concurrencyStamp?: string;
  url?: string;
}

export interface CommentWithDetailsDto extends ExtensibleObject {
  id?: string;
  entityType?: string;
  entityId?: string;
  text?: string;
  creatorId?: string;
  creationTime?: string;
  replies: CommentDto[];
  author: CmsUserDto;
  concurrencyStamp?: string;
}

export interface CreateCommentInput extends ExtensibleObject {
  text: string;
  repliedCommentId?: string;
  captchaToken?: string;
  captchaAnswer: number;
  url?: string;
  idempotencyToken: string;
}

export interface UpdateCommentInput extends ExtensibleObject {
  text: string;
  concurrencyStamp?: string;
  captchaToken?: string;
  captchaAnswer: number;
}
