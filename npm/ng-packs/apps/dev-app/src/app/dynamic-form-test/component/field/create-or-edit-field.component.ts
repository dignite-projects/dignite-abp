import { LocalizationService } from '@abp/ng.core';
import { Component, Input, ViewChild, ViewContainerRef, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { FieldAdminService } from '../../../proxy/dignite/cms/admin/fields';
import { ApiService } from '../../services';
import { FieldControlGroupInterfaces, getExcludeAssignControl } from '@dignite-ng/expand.dynamic-form';

@Component({
  selector: 'app-create-or-edit-field',
  templateUrl: './create-or-edit-field.component.html',
  styleUrl: './create-or-edit-field.component.scss'
})
export class CreateOrEditFieldComponent {

  /**表单控件模板-动态表单配置组件 */
  @ViewChild('FormDynamicRef', { read: ViewContainerRef, static: true }) FormDynamicRef: ViewContainerRef;

  private _ApiService = inject(ApiService)
  constructor(
    // private _FieldAdminService: FieldAdminService,
    // private _LocalizationService: LocalizationService,

  ) {
    // if (this._FieldAbstractsService.fieldGroupList.length == 0) {
    //   this._FieldAbstractsService.getfieldGroupList()
    // }
  }
  /**表单字段类型组 */
  FieldControlGroup: FieldControlGroupInterfaces[] = getExcludeAssignControl()


  /**表单实体 */
  _Entity: FormGroup | undefined
  @Input()
  public set Entity(v: FormGroup | undefined) {
    if (v) {
      this._Entity = v;
      this.dataLoaded();
    }
  }
  /**表单名称控件 */
  get formControlName() {
    return this._Entity.get('formControlName')
  }
  /**选择的表单信息 */
  _selected_copy: any
  _selected: any
  @Input()
  public set selected(v: any) {
    if (v) {
      this._selected = v || '';
      this._selected_copy = v
      this.dataLoaded()
    }
  }


  async dataLoaded() {
    console.log(this.FieldControlGroup, 'dataLoaded');

    // if (this._FieldAbstractsService.fromControlList.length == 0) {
    //   await this._FieldAbstractsService.getFromControlList()
    // }

    if (this._Entity) {
      if (!this.formControlName.value) {
        this._Entity.patchValue({
          formControlName: this.FieldControlGroup[0]?.name,
        })
      }
      // this._Entity.setControl('name', new FormControl(this.nameInput.value||'', {
      //   validators: Validators.required,
      //   asyncValidators: [this.repetitionAsyncValidator()] ,
      //   updateOn: 'blur'
      // }))
    }
  }



  /**name表单控件 */
  get nameInput() {
    return this._Entity.get('name')
  }

  nameInputBlur(event) {
    let value = event.target.value
    this.nameInput.patchValue(value)
  }

  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    let value = event.target.value
    let pinyin = this._ApiService.chineseToPinyin(value)
    let nameInput = this.nameInput
    if (nameInput.value) return
    nameInput.patchValue(pinyin)
  }
  // /**定义异步验证方法 */
  // repetitionAsyncValidator() {
  //   return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
  //     return new Promise(resolve => {
  //       if (ctrl.value == this._selected?.name||!ctrl.value) {
  //         resolve(null);
  //         return
  //       }
  //       this._FieldAdminService.nameExists(ctrl.value).subscribe(res => {
  //         if (res) {
  //           resolve({ repetition: this._LocalizationService.instant(`Cms::FieldName{0}AlreadyExist`, ctrl.value) });
  //         } else {
  //           resolve(null);
  //         }
  //       })
  //     });
  //   };
  // }





}
