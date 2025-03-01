import { FormControl, Validators } from "@angular/forms";

export class EntryConfig {


    /**多选 */
    'Entry.Multiple': any = [false, []];
    /**占位符 */
    'Entry.Placeholder': any = ['', []];
    /**版块id */
    'Entry.SectionId': any = ['', []];

    constructor(data?: EntryConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}