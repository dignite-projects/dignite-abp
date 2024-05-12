import type { CreateFileInput, FileContainerConfigurationDto, FileDescriptorDto, GetFilesInput, ImageResizeInput, UpdateFileInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { FileResult } from '../../../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class FileDescriptorService {
  apiName = 'FileExplorer';
  

  create = (input: CreateFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileDescriptorDto>({
      method: 'POST',
      url: '/api/file-explorer/files',
      params: { containerName: input.containerName, cellName: input.cellName, directoryId: input.directoryId, entityId: input.entityId },
      body: input.file,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/file-explorer/files/${id}`,
    },
    { apiName: this.apiName,...config });
  

  download = (containerName: string, blobName: string, fileName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileResult>({
      method: 'GET',
      url: `/api/file-explorer/files/download/${containerName}/${blobName}`,
      params: { fileName },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileDescriptorDto>({
      method: 'GET',
      url: `/api/file-explorer/files/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getFileContainerConfiguration = (containerName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileContainerConfigurationDto>({
      method: 'GET',
      url: '/api/file-explorer/files/configuration',
      params: { containerName },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetFilesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FileDescriptorDto>>({
      method: 'GET',
      url: '/api/file-explorer/files',
      params: { containerName: input.containerName, directoryId: input.directoryId, creatorId: input.creatorId, filter: input.filter, entityId: input.entityId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getStream = (containerName: string, blobName: string, imageResize?: ImageResizeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: `/api/file-explorer/files/${containerName}/${blobName}`,
      params: { width: imageResize.width, height: imageResize.height },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileDescriptorDto>({
      method: 'PUT',
      url: `/api/file-explorer/files/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
