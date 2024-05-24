import {  Validators } from "@angular/forms";
export class CreateOrUpdateSitesInputBase {
    displayName: any = ['', [Validators.required]];
    name: any = ['', [Validators.required]];
    host: any = ['https://', [Validators.required]];
    isActive: any = [true, []];
    languages:any = [[], [Validators.required]]
    constructor(data?: CreateOrUpdateSitesInputBase) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}

