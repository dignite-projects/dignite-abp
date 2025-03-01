import { Injectable } from '@angular/core';
import { FormAdminService } from '../proxy/dignite/cms/admin/dynamic-forms/form-admin.service';

@Injectable({
  providedIn: 'root',
})
export class FormControlsService {
  constructor(private _FormAdminService: FormAdminService) {}
  /**需要查询的动态表单类型 */
  enableSearchTypeList: any[] = [];
  /**不需要展示的动态表单类型 */
  disableshowinTypeList: any[] = ['Table', 'FileExplorer', 'Matrix'];

  /**获取动态表单类型 */
  getDynamicFormType() {
    return new Promise((resolve, rejects) => {
      this._FormAdminService.getFormControls().subscribe((res: any) => {
        // this.dynamicFormTypeList = res.items;
        this.enableSearchTypeList = res.items.filter(el => el.enableSearch).map(el => el.name);
        resolve(res.items);
      });
    });
  }

  async getEnableSearchTypeList() {
    if (this.enableSearchTypeList.length === 0) {
      await this.getDynamicFormType();
    }
    return this.enableSearchTypeList;
  }
  getdisableshowinTypeList() {
    return this.disableshowinTypeList;
  }
}
