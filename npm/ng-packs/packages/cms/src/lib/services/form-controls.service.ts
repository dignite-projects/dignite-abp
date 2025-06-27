import { Injectable } from '@angular/core';
import { FormAdminService } from '../proxy/dignite/cms/admin/dynamic-forms/form-admin.service';
import { FieldsDataService } from './fields-data.service';

@Injectable({
  providedIn: 'root',
})
export class FormControlsService {
  constructor(private _FormAdminService: FormAdminService,private _service:FieldsDataService) {}
  /**需要查询的动态表单类型 */
  enableSearchTypeList: any[] = [];
  /**不需要展示的动态表单类型 */
  disableshowinTypeList: any[] = ['Table', 'FileExplorer', 'Matrix'];

  /**获取动态表单类型 */
  getDynamicFormType() {
    // eslint-disable-next-line no-async-promise-executor
    return new Promise(async (resolve, rejects) => {
     const items:any= await this._service.getControlsfieldTypes()
      this.enableSearchTypeList = items.filter(el => el.enableSearch).map(el => el.name);
     resolve(items);
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
