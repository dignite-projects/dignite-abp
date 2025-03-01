import type { CreateDirectoryInput, DirectoryDescriptorDto, DirectoryDescriptorInfoDto, GetDirectoriesInput, MoveDirectoryInput, UpdateDirectoryInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FileDescriptorService {
  apiName = 'FileExplorer';
  

  create = (input: CreateDirectoryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectoryDescriptorDto>({
      method: 'POST',
      url: '/api/file-explorer/directories',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/file-explorer/directories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectoryDescriptorDto>({
      method: 'GET',
      url: `/api/file-explorer/directories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetDirectoriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DirectoryDescriptorInfoDto>>({
      method: 'GET',
      url: '/api/file-explorer/directories',
      params: { containerName: input.containerName },
    },
    { apiName: this.apiName,...config });
  

  move = (id: string, input: MoveDirectoryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectoryDescriptorDto>({
      method: 'PUT',
      url: `/api/file-explorer/directories/${id}/move`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateDirectoryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectoryDescriptorDto>({
      method: 'PUT',
      url: `/api/file-explorer/directories/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
