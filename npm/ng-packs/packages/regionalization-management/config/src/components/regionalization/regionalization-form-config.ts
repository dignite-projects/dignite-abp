import { FormArray, Validators } from '@angular/forms';

export class RegionalizationFormConfig {
  /**默认区域名称 */
  defaultCultureName: any = ['', [Validators.required]];
  /**可用区域名称 */
  availableCultureNames: any = [[], [Validators.required]];
  
  constructor() {}
}
