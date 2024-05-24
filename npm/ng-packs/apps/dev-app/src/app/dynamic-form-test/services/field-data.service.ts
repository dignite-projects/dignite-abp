import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FieldDataService {


  constructor() { }
  /**字段列表 id使用事假戳*/
  fieldList: any[] = [
    {
      "id":'1716518886674',
      "displayName": "FieldDisplayName",
      "name": "fielddisplayname",
      "description": "",
      "formControlName": "TextEdit",
      "formConfiguration": {
        "TextEdit.Placeholder": "Placeholder",
        "TextEdit.Mode": 0,
        "TextEdit.CharLimit": "111111111"
      },
      "creationTime": "2024-05-24T02:08:29.469Z",
      "extraProperties": {
        "fielddisplayname": "string",
      },
    }
  ]
  /**获取字段列表 */
  getFieldList(condition): Observable<any> {
    let list=this.fieldList.filter(el=>el.displayName.includes(condition.filter))
    // console.log(list,condition);
    
    list.slice(condition.skipCount,condition.maxResultCount)
    return of({
      totalCount:list.length,
      items:list
    });
  }
  /**添加字段列表 */
  addFieldList(input: any) {
    return new Promise((resolve) => {
      input.creationTime = new Date()
      input.id = new Date().getTime()
      this.fieldList.push(input)
      resolve(input)
    })
  }
  /**检查字段唯一名称在字段列表中是否已经存在 */
  checkNameExist(name: string) {
    return this.fieldList.find(el => el.name == name) ? true : false
  }
  /**获取指定字段 */
  getFieldId(id): Observable<any>{
    return of(this.fieldList.find(el => el.id == id));
  }
  /**删除指定字段 */
  deleteField(id) {
    let findIndex = this.fieldList.findIndex(el => el.id == id)
    this.fieldList.splice(findIndex, 1)
  }
  /**修改指定字段 */
  editField(id, input): Observable<any> {

    let list=this.fieldList
    let findIndex = this.fieldList.findIndex(el => el.id == id)
    input.id=id
    input.creationTime=list[findIndex].creationTime
    list[findIndex]=input
    return of(input)
  }
  /**修改指定字段下的控件值 */
  setFieldExtraProperties(id,input): Observable<any>{
    let list=this.fieldList
    let findIndex = this.fieldList.findIndex(el => el.id == id)
    input.id=id
    input.creationTime=list[findIndex].creationTime
    list[findIndex]={...list[findIndex],...input}
    return of(list[findIndex])
  }


}
