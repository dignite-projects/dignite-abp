import { Injectable } from '@angular/core';
import { FieldGroupAdminService } from '../proxy/admin/fields/field-group-admin.service';
import { FieldGroupDto } from '../proxy/admin/fields/models';
import { finalize } from 'rxjs/operators';
import { FormAdminService } from '../proxy/admin/dynamic-forms';

@Injectable({
  providedIn: 'root'
})
export class FieldAbstractsService {

  constructor(
    private _FieldGroupAdminService: FieldGroupAdminService,
    private _FormAdminService: FormAdminService,
  ) { }
  /**字段分组列表 */
  fieldGroupList: FieldGroupDto[] = []
  /**获取字段分组列表 */
  getfieldGroupList() {
    this._FieldGroupAdminService.getList({}).subscribe(res => {
      this.fieldGroupList = res.items
    })
  }

  /**表单控件类型 */
  fromControlList: any[] = []
  /**获取表单控件类型 */
  getFromControlList() {
    return new Promise((resolve, reject) => {
      this._FormAdminService.getFormControls({}).subscribe(res => {
        this.fromControlList = res.items
        resolve(res.items)
      })
    })
  }
  getExcludeAssignControl(typeName?) {
    // return this.fromControlList.filter(el => el.name !== typeName)
    return this.fromControlList
  }

}
