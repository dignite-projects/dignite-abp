import { Rest, RestService } from "@abp/ng.core";

export class isBase64UploadAdapter {
  public reader = new FileReader();
  restService:RestService
  loader: any;
  ImagesContainerName: any;
  Base64: boolean = false
  constructor(
    loader: any,
    ImagesContainerName: any,
    restService: RestService
  ) {
    this.loader = loader;
    this.ImagesContainerName = ImagesContainerName
    this.restService=restService
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
            directoryId: '',
            entityId: ''
          }).subscribe((response:any)=>{
            resolve({ default: response.url })
          },(error)=>{
            reject(error);
          })
        });
      }

    });
  }

  createFile = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, any>({
      method: 'POST',
      url: '/api/file-explorer/files',
      params: { containerName: input.containerName, cellName: input.cellName, directoryId: input.directoryId, entityId: input.entityId },
      body: input.file,
    },
      { apiName: 'FileExplorer', ...config });

}