/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CmsApiService } from '../../../services';

@Component({
  selector: 'df-matrix-control',
  templateUrl: './matrix-control.component.html',
  styleUrls: ['./matrix-control.component.scss'],
})
export class MatrixControlComponent {
  constructor(private fb: FormBuilder, private _CmsApiService: CmsApiService) {}

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    if (v) {
      for (const key in v.field?.formConfiguration) {
        if (Array.isArray(v.field?.formConfiguration[key])) {
          v.field.formConfiguration[key] = this._CmsApiService.convertKeysToCamelCase(
            v.field?.formConfiguration[key]
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
      v = this._CmsApiService.convertKeysToCamelCase(v);
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
      if (this._selected) this.fieldNameControl.patchValue(this._selected);
      this._selected = '';
    }
  }

  formConfiguration: any;
  /**获取表格字段代表的控件 */
  fieldNameControl: FormArray | undefined;

  AfterInit() {
    return new Promise((resolve, rejects) => {
      let formConfiguration = this._fields.field.formConfiguration;
      let newArrayGroup = this.fb.array([]);
      this.extraProperties.setControl(this._fields.field.name, newArrayGroup);
      this.fieldNameControl = this.extraProperties.get(this._fields.field.name) as FormArray;
      if (this._selected) {
        this._selected.forEach(el => {
          this.addMatrixControl(
            formConfiguration.MatrixBlockTypes.find(item => item.name == el.matrixBlockTypeName)
          );
        });
      }
      this.MatrixBlockTypesList = formConfiguration.MatrixBlockTypes;
      resolve(true);
    });
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
      })
    );
  }
  /**删除矩阵控件 */
  deleteMatrixControl(index, item) {
    this.fieldNameControl.removeAt(index);
  }
}
