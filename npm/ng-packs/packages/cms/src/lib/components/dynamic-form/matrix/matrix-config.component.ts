/* eslint-disable no-async-promise-executor */
/* eslint-disable @angular-eslint/component-selector */
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  inject,
  Input,
  ViewChild,
} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatrixConfig, MatrixItemConfig } from './matrix-config';
import { KeysConvertToLowercaseService, FieldsDataService } from '../../../services';
import { CdkDrag,  moveItemInArray } from '@angular/cdk/drag-drop';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { ToPinyinService } from '@dignite-ng/expand.core'
@Component({
  selector: 'df-matrix-config',
  templateUrl: './matrix-config.component.html',
  styleUrls: ['./matrix-config.component.scss'],
})
export class MatrixConfigComponent {
  constructor(
    private fb: FormBuilder,
    private _KeysConvertToLowercaseService: KeysConvertToLowercaseService,
    private _FieldsDataService: FieldsDataService,
    private toPinyinService:ToPinyinService
  ) {}

  /**表单控件组 */
  _FieldControlGroup: any[]=[];
  /**表单控件类型 */
  _type: any;
  @Input()
  public set type(v: any) {
    this._type = v;
  }

  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    if (v) {
      for (const key in v.formConfiguration) {
        if (Array.isArray(v.formConfiguration[key])) {
          v.formConfiguration[key] = this._KeysConvertToLowercaseService.get(v.formConfiguration[key]);
        }
      }
      this._selected = v;
    }
  }
  /**表单实体 */
  _Entity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this._Entity = v;
    this.dataLoaded();
  }

  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._Entity && this._type) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      // this.submitclick?.nativeElement?.click();
    }
  }

  AfterInit() {
    return new Promise(async (resolve) => {
      this._FieldControlGroup = await this._FieldsDataService.getControlsfieldTypes();
      this._Entity.setControl('formConfiguration', this.fb.group(new MatrixConfig()));
      await this.setSelectValue();
      this.formConfiguration.patchValue(this._selected.formConfiguration);
      
      resolve(true);
    });
  }
  /**
   * 设置选择值
   * @returns void
   */
  setSelectValue() {
    return new Promise((resolve) => {
      if (this._selected && this._selected.formControlName == this._type) {
        this._selected.formConfiguration['MatrixBlockTypes'].forEach((el, index) => {
          this.addMatrixBlockTypeItem(el);
          el.fields.forEach((elf) => {
            this.addMatrixFieldItem(elf, index);
          });
        });
        resolve(true);
      }
    });
  }

  /**获取表单配置 */
  get formConfiguration() {
    return this._Entity.get('formConfiguration') as FormGroup;
  }

  /**获取表单配置下的矩阵块表单数组 */
  get MatrixBlockTypes() {
    return this.formConfiguration.controls['MatrixBlockTypes'] as FormArray;
  }

  /**模态框-状态 */
  matrixModalOpen: boolean | any = false;
  /**模态框-是否正在编辑 */
  isMatrixModalEdit: any = false;

  /**模态框-用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean | any = false;

  /**模态框-表单 */
  matrixModalForm: FormGroup | undefined;

  /**表单控件模板-用于在form外提交submit */
  @ViewChild('matrixModalModalSubmit', { static: false }) matrixModalModalSubmit: ElementRef;

  /**矩阵块-选择的下标 */
  selectMatrixBlockIndex: any = 0;

  /**矩阵块-选择的矩阵下字段的下标 */
  selectMatrixFieldIndex: any = 0;

  /**模态框-状态改变 */
  matrixModalVisibleChange(event) {
    if (!event) {
      this.isMatrixModalEdit = false;
      this.matrixModalForm = undefined;
      return;
    }
  }

  /**矩阵块--新增-打开模态框 */
  addMatrixBlockType() {
    this.matrixModalForm = this.fb.group(new MatrixItemConfig());
    this.matrixModalOpen = true;
  }
  formValidation: any;
  private _ValidatorsService = inject(ValidatorsService);
  /**模态框--矩阵表单保存提交 */
  createOrEditSave() {
    const input = this.matrixModalForm.value;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.matrixModalForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')) return;
    if (!this.matrixModalForm.valid) return;
    if (this.isMatrixModalEdit) {
      const MatrixBlockTypesItem = this.MatrixBlockTypes.at(
        this.selectMatrixBlockIndex
      ) as FormGroup;
      MatrixBlockTypesItem.patchValue({
        ...input,
      });
    } else {
      this.addMatrixBlockTypeItem(input);
    }
    this.matrixModalOpen = false;
    this.matrixModalVisibleChange(false);
  }
  /**新增矩阵块-向数组表单中增加项 */
  addMatrixBlockTypeItem(input) {
    this.MatrixBlockTypes.push(
      new FormGroup({
        displayName: new FormControl(input.displayName, Validators.required),
        name: new FormControl(input.name, Validators.required),
        fields: new FormArray([]),
      })
    );
  }
  /**编辑矩阵块 */
  EditMatrixBlock(input) {
    this.matrixModalForm = this.fb.group(new MatrixItemConfig());
    this.matrixModalForm.patchValue({
      ...input,
    });
    this.matrixModalOpen = true;
    this.isMatrixModalEdit = true;
   
  }

  /**删除矩阵块 */
  DeleteMatrixBlock(index) {
    this.MatrixBlockTypes.removeAt(index);
  }

  /**矩阵块-选择 */
  selectMatrixBlockChange(index) {
    this.selectMatrixBlockIndex = index;
  }

  /**矩阵字段-新增 */
  addMatrixField() {
    this.addMatrixFieldItem('', this.selectMatrixBlockIndex);
    const MatrixBlockTypesItemForm = this.MatrixBlockTypes.at(
      this.selectMatrixBlockIndex
    ) as FormGroup;
    const MatrixFieldItemForm = MatrixBlockTypesItemForm.get('fields') as FormArray;
    this.selectMatrixFieldChange(0, MatrixFieldItemForm.length - 1);
  }
  /**矩阵字段-新增矩阵字段项 */
  addMatrixFieldItem(input: any = '', selectMatrixBlockIndex) {
    /**矩阵下标的矩阵项Form */
    const MatrixBlockTypesItemForm = this.MatrixBlockTypes.at(selectMatrixBlockIndex) as FormGroup;
    const MatrixFieldItemForm = MatrixBlockTypesItemForm.get('fields') as FormArray;
    MatrixFieldItemForm.push(
      this.fb.group({
        /**字段名称 Display name of this field */
        displayName: [input.displayName, [Validators.required]],
        /**字段唯一名称 Unique Name*/
        name: [input.name, [Validators.required]],
        /**描述 说明 */
        description: [input.description, []],
        /**FieldType字段类型 表单控件名称 */
        formControlName: [input.formControlName || 'TextEdit', [Validators.required]],
        //动态表单配置
        formConfiguration: new FormGroup({}),
      })
    );
    //
  }

  /**删除矩阵字段项 */
  deleteMatrixField(MatrixFieldForm, index) {
    MatrixFieldForm.removeAt(index);
    this.selectMatrixFieldIndex = 0;
  }

  /**矩阵字段-选择 */
  selectMatrixFieldChange(MatrixBlockIndex, MatrixFieldIndex) {
    this.selectMatrixFieldIndex = MatrixFieldIndex;
  }

  get nameInput() {
    return this.matrixModalForm?.get('name');
  }
  get FieldnameInput() {
    const MatrixBlockTypesItemForm = this.MatrixBlockTypes.at(
      this.selectMatrixBlockIndex
    ) as FormGroup;
    const MatrixFieldItemForm = MatrixBlockTypesItemForm.get('fields') as FormArray;
    return MatrixFieldItemForm.at(this.selectMatrixFieldIndex).get('name');
  }

  /**矩阵displayNameInput字段失去焦点 */
  displayNameInputBlur(event) {
    const value = event.target.value;
    const pinyin = this.toPinyinService.get(value);
    const nameInput = this.nameInput;
    if (nameInput.value) return;
    nameInput.patchValue(pinyin);
  }
  /**矩阵displayNameInput字段失去焦点 */
  MatrixFieldDisplayNameInputBlur(event) {
    const value = event.target.value;
    const pinyin = this.toPinyinService.get(value);
    const FieldnameInput = this.FieldnameInput;
    if (FieldnameInput.value) return;
    FieldnameInput.patchValue(pinyin);
  }

  dropMatrix(event) {
    if (event.container.data === event.previousContainer.data) {
      moveItemInArray(this.MatrixBlockTypes.controls, event.previousIndex, event.currentIndex);
      this.MatrixBlockTypes.updateValueAndValidity();
      this.selectMatrixBlockIndex = event.currentIndex;
    } else {
      const object1 = this.MatrixBlockTypes.controls[this.selectMatrixBlockIndex].get(
        'fields'
      ) as FormArray;
      const controlsItem = object1.at(event.previousIndex);
      object1.removeAt(event.previousIndex);
      event.container.data[event.currentIndex-1].controls['fields'].push(controlsItem);
    }
    this.fieldStartDrag=false;
  }
  dropFields(event) {
    const object1 = this.MatrixBlockTypes.controls[this.selectMatrixBlockIndex].get(
      'fields'
    ) as FormArray;
    moveItemInArray(object1.controls, event.previousIndex, event.currentIndex);
    this.MatrixBlockTypes.updateValueAndValidity();
    this.selectMatrixFieldIndex = event.currentIndex;
    this.fieldStartDrag=false;
  }
  sortPredicate(index: number, item: CdkDrag<number>) {
    return index!==0;
  }
  sortPredicateTrue(){
    return true;
  }
  /**字段开始拖拽 */
  fieldStartDrag:any=false;
  CdkDragStart(event) {
    this.fieldStartDrag=true;
  }
}
