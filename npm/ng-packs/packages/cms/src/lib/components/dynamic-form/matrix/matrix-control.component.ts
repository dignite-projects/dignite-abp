/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { KeysConvertToLowercaseService } from '../../../services';
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'df-matrix-control',
  templateUrl: './matrix-control.component.html',
  styleUrls: ['./matrix-control.component.scss'],
})
export class MatrixControlComponent {
  constructor(private fb: FormBuilder, private _KeysConvertToLowercaseService: KeysConvertToLowercaseService) {}

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    if (v) {
      for (const key in v.field?.formConfiguration) {
        if (Array.isArray(v.field?.formConfiguration[key])) {
          v.field.formConfiguration[key] = this._KeysConvertToLowercaseService.get(
            v.field?.formConfiguration[key],
          );
        }
      }

      this._fields = v;
    }
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    if (v) {
      v = this._KeysConvertToLowercaseService.get(v);
      this._selected = v;
    }
  }
  /**语言 */
  _culture: any;
  @Input()
  public set culture(v: any) {
    this._culture = v;
  }
  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    if (v) {
      this.dataLoaded();
    }
  }

  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /** 获取父级字段代表的表单组*/
  extraProperties: FormGroup | undefined;

  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity && this._parentFiledName) {
      this.extraProperties = this._entity.get(this._parentFiledName) as FormGroup;
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
      if (this._selected) {
        this.fieldNameControl.patchValue(this._selected);
        for (const index of Object.keys(this.fieldNameControl.controls)) {
          const item = this.fieldNameControl.at(Number(index)) as FormGroup;
          item.get('isOpen').patchValue(false);
        }
      }
      this._selected = '';
      // console.log(this.fieldNameControl, 'fieldNameControl', this._fields.field.name);
    }
  }

  formConfiguration: any;
  /**获取表格字段代表的控件 */
  fieldNameControl: FormArray | undefined;

  AfterInit() {
    return new Promise((resolve, rejects) => {
      const formConfiguration = this._fields.field.formConfiguration;
      const ValidatorsArray = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      const newArrayGroup = this.fb.array([], ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newArrayGroup);
      this.fieldNameControl = this.extraProperties.get(this._fields.field.name) as FormArray;
      if (this._fields.required) {
        this.fieldNameControl.addValidators(this.hasValueValidator);
        // for (const element of formConfiguration.MatrixBlockTypes) {
        //   element.required = true;
        // }
      }

      if (this._selected) {
        this._selected.forEach(el => {
          this.addMatrixControl(
            formConfiguration.MatrixBlockTypes.find(item => item.name == el.matrixBlockTypeName),
          );
        });
        // this.isAllExpanded=false;
      }else{
        this.isAllExpanded=true;
      }


      this.MatrixBlockTypesList = formConfiguration.MatrixBlockTypes;
      resolve(true);
    });
  }

  /**是否带有值验证器 */
  hasValueValidator(g: FormArray) {
    // 检查是否有任何控件包含有效值
    const hasValue = g.controls.some(element => {
      const extraProperties = element.get('extraProperties');
      return Object.keys(extraProperties.value).some(key => {
        const value = extraProperties.value[key];
        // 检查数组类型值
        if (Array.isArray(value)) {
          return value.length > 0;
        }
        // 检查非空值
        return value !== null && value !== undefined && value !== '';
      });
    });

    // 根据验证结果设置表单状态
    g.controls.forEach(element => {
      const extraProperties = element.get('extraProperties');
      Object.keys(extraProperties.value).forEach(key => {
        if (!hasValue) {
          // 如果没有值，设置所有字段为必填
          extraProperties.get(key).setValidators(Validators.required);
          extraProperties.get(key).setErrors({ required: true });
        } else {
          // 如果有值，清除错误
          extraProperties.get(key).setErrors(null);
        }
      });
    });

    // 返回验证结果
    return hasValue? null : { required: true };
    // return hasValue? null : null;
  }

  /**矩阵列表 */
  MatrixBlockTypesList: any[] = [];

  /**增加指定矩阵控件项 */
  addMatrixControl(item) {
    this.fieldNameControl.push(
      new FormGroup({
        extraProperties: new FormGroup({}),
        matrixBlockTypeName: new FormControl(item.name),
        displayName: new FormControl(item.displayName),
        isOpen: new FormControl(true),
      }),
    );
  }

  /**展开，收缩矩阵 */
  ExpandChange(index) {
    const fieldNameControlItem = this.fieldNameControl.controls[index] as FormGroup;
    fieldNameControlItem.get('isOpen').patchValue(!fieldNameControlItem.get('isOpen').value);
  }
  /**删除矩阵控件 */
  deleteMatrixControl(index, item) {
    this.fieldNameControl.removeAt(index);
  }
  /**调整表格位置 */
  drop(event: any) {
    moveItemInArray(this.fieldNameControl.controls, event.previousIndex, event.currentIndex);
    this.fieldNameControl.updateValueAndValidity();
  }

  /**是否全部展开 */
  isAllExpanded = false;

  /**切换全部展开/折叠状态 */
  toggleAll() {
    this.isAllExpanded = !this.isAllExpanded;
    if (this.fieldNameControl) {
      this.fieldNameControl.controls.forEach((control: FormGroup) => {
        control.get('isOpen').patchValue(this.isAllExpanded);
      });
    }
  }
}
