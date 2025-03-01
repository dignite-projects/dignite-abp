import { FormControl, Validators } from "@angular/forms";

export class FileExplorerConfig {


    /**文件容器名称 */
    'FileExplorer.FileContainerName': any = ['', [Validators.required]];
      /**多选 */
    'FileExplorer.UploadFileMultiple': any = [false, []];
  

    constructor(data?: FileExplorerConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }


    }
}