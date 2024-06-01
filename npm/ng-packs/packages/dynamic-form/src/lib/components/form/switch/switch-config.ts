import { FormControl, Validators } from "@angular/forms";

export class SwitchConfig {


    /**默认值 */
    'Switch.Default': any = [false, []];
    constructor(data?: SwitchConfig) {
        if (data) {
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    this[key] = data[key];
                }
            }
        }
    }
}