import { Injectable } from '@angular/core';
import { CreateFieldInput, CreateOrUpdateFieldGroupInput, FieldAdminService, FieldGroupAdminService, GetFieldsInput, UpdateFieldInput } from '../proxy/dignite/cms/admin/fields';
import { FormAdminService } from '../proxy/dignite/cms/admin/dynamic-forms';

@Injectable({
  providedIn: 'root'
})
export class FieldsDataService {

  constructor(
    private _fieldGroupService:FieldGroupAdminService,
    private _formService:FormAdminService,
    private _fieldService:FieldAdminService
  ) { }
  /**
   * 字段分组列表
   */
  fieldGroupsList:any[] = [];
  /**
   * 字段控件类型列表
   */
  fieldControlsTypesList:any[] = [];
  /**
   * 获取字段分组
   */
  getfieldGroups(newList = false){
    return new Promise((resolve,reject)=>{
      if(this.fieldGroupsList.length > 0&&newList == false){
        resolve(this.fieldGroupsList);
        return;
      }
      this._fieldGroupService.getList({}).subscribe(res=>{
        this.fieldGroupsList = res.items;
        resolve(res.items);
      },()=>{
        reject([]);
      })
    })
  }

  /**
   * 获取字段类型列表
   */
  getControlsfieldTypes():Promise<any[]>{
    return new Promise((resolve,reject)=>{
      if(this.fieldControlsTypesList.length > 0){
        resolve(this.fieldControlsTypesList);
        return;
      }
      this._formService.getFormControls({}).subscribe(res=>{
        this.fieldControlsTypesList = res.items;
        resolve(res.items);
      },()=>{
        reject([]);
      })
    })
  }

  /**创建分组 */
  createFieldGroup(input:CreateOrUpdateFieldGroupInput){
    return this._fieldGroupService.create(input);
  }
  /**编辑分组 */
  updateFieldGroup(id:string,input:CreateOrUpdateFieldGroupInput){
    return this._fieldGroupService.update(id,input);
  }
  /**删除分组 */
  deleteFieldGroup(id:string){
    return this._fieldGroupService.delete(id);
  }

  /**获取字段列表 */
  getFieldsList(input:GetFieldsInput){
    return this._fieldService.getList(input);
  }
  /**创建字段 */
  createField(input:CreateFieldInput){
    return this._fieldService.create(input);
  }
  /**编辑字段 */
  updateField(id:string,input:UpdateFieldInput){
    return this._fieldService.update(id,input);
  }
  /**获取字段详情 */
  getFieldDetail(id:string){
    return this._fieldService.get(id);
  }
  /**字段验证 */
  nameExists(name:string){
    return this._fieldService.nameExists(name);
  }
  /**删除字段 */
  deleteField(id:string){
    return this._fieldService.delete(id);
  }

}
