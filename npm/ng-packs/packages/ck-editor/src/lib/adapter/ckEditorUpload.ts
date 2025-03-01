import { ConfigStateService, EnvironmentService, Rest, RestService } from '@abp/ng.core';

export class isBase64UploadAdapter {
  public reader = new FileReader();
  configState: ConfigStateService;
  environment: EnvironmentService;
  restService: RestService;
  loader: any;
  ImagesContainerName: any;
  Base64: boolean = false;
  constructor(input: {
    loader: any;
    ImagesContainerName: any;
    restService: any;
    configState: any;
    environment: any;
  }) {
    this.loader = input.loader;
    this.ImagesContainerName = input.ImagesContainerName;
    this.restService = input.restService;
    this.configState = input.configState;
    this.environment = input.environment;
  }
  public upload() {
    return new Promise((resolve, reject) => {
      const reader = this.reader;

      if (this.Base64) {
        reader.addEventListener('load', () => {
          resolve({ default: reader.result });
        });

        reader.addEventListener('error', err => {
          reject(err);
        });

        reader.addEventListener('abort', () => {
          reject();
        });

        this.loader.file.then((file: Blob) => {
          reader.readAsDataURL(file);
        });
      } else {
        this.loader.file.then((file: File) => {
          var formData = new FormData();
          formData.append('file', file, file.name);
          this.createFile({
            file: formData,
            containerName: this.ImagesContainerName,
            // directoryId: '',
            // entityId: ''
          }).subscribe(
            (response: any) => {
              let tenantId = this.configState.getDeep('currentUser.tenantId');
              const environment = this.environment.getEnvironment();
              let imgUrl = `${environment.apis.default.url}/api/file-explorer/files/${this.ImagesContainerName}/${response.blobName}?__tenant=${tenantId}`;
              resolve({ default: imgUrl });
            },
            error => {
              reject(error);
            }
          );
        });
      }
    });
  }

  createFile = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, any>(
      {
        method: 'POST',
        url: '/api/file-explorer/files',
        params: {
          containerName: input.containerName,
          cellName: input.cellName,
          directoryId: input.directoryId,
          entityId: input.entityId,
        },
        body: input.file,
      },
      { apiName: 'FileExplorer', ...config }
    );
}
