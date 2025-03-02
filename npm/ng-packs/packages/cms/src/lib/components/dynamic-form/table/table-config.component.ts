/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { TableConfig, TableFormControl } from './table-config';
import { CmsApiService, FieldAbstractsService } from '../../../services';

@Component({
  selector: 'df-table-config',
  templateUrl: './table-config.component.html',
  styleUrls: ['./table-config.component.scss']
})
export class TableConfigComponent {
  constructor(
    private fb: FormBuilder,
    private _CmsApiService: CmsApiService,
    private _FieldAbstractsService: FieldAbstractsService,
  ) { }
  /**表单实体 */
  _Entity: FormGroup | undefined
  @Input()
  public set Entity(v: FormGroup) {
    this._Entity = v;
    this.dataLoaded()
  }
  /**选择的表单信息 */
  _selected: any
  @Input()
  public set selected(v: any) {
    if (v) {
      for (const key in v.formConfiguration) {
        if(Array.isArray(v.formConfiguration[key])){
          v.formConfiguration[key]=this._CmsApiService.convertKeysToCamelCase(v.formConfiguration[key])
        }
      }
      this._selected = v
      this.dataLoaded()
    }
  }
  /**表单控件组 */
  _fieldControlGroup: any[]

  /**表单控件类型 */
  _type: any
  @Input()
  public set type(v: any) {
    this._type = v
    this.dataLoaded()
  }
  get formConfiguration() {
    return this._Entity?.get('formConfiguration') as FormGroup
  }
  get TableColumns() {
    return this.formConfiguration.controls['TableColumns'] as FormArray
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this._Entity && this._type) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }
  /**增加选项 */
  addTableColumns() {
    this.TableColumns.push(this.fb.group({
      ...new TableFormControl(),
      formConfiguration: [{}]
    }))
  }
  /**删除某个选项 */
  deleteTableColumns(index) {
    this.TableColumns.removeAt(index)
  }

  AfterInit() {
    return new Promise((resolve, rejects) => {
      this._Entity?.setControl('formConfiguration', this.fb.group(new TableConfig()))
      this._fieldControlGroup = this._FieldAbstractsService.getExcludeAssignControl(this._type)
      if (this._selected && this._selected.formControlName == this._type) {
        this._selected.formConfiguration['TableColumns'].forEach(el => {
          this.addTableColumns()
        });
        this.formConfiguration.patchValue(this._selected.formConfiguration)
      } else {
        this.addTableColumns()
      }
      resolve(true)
    })
  }
  itemForm: any
  /**选择表格的表单控件 */
  selectTableControl(event, i, item) {
    this.CurrentSelectionTableControlName = event.target.value
    this.tableSelectOpen = true
    this.tableSelectForm = this.fb.group(new TableFormControl())
    this.TableColumnsIndex = i
    this.itemForm = item
  }

  CurrentSelectionTableControlName: any

  /**正在创建或编辑的表格项下标 */
  TableColumnsIndex: any
  /**创建站点模态框状态 */
  tableSelectOpen: boolean = false

  /**用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean = false
  /**创建站点表单 */
  tableSelectForm: FormGroup | undefined;

  /**表格已选择数据 */
  _tableSelected: any


  /**表单控件模板-动态赋值表单控件 */
  @ViewChild('tableSelectModalSubmit', { static: false }) tableSelectModalSubmit: ElementRef;


  /**创建站点模态框状态改变 */
  tableSelectVisibleChange(event) {
    if (!event) {
      this._tableSelected = undefined
      this.tableSelectForm = undefined
      this.CurrentSelectionTableControlName = undefined
      this.TableColumnsIndex = undefined
      return
    }
  }
  /**表单保存提交 */
  createOrEditSave() {
    const formGroup = this.TableColumns.at(this.TableColumnsIndex) as FormGroup;
    let formConfigurationgroup = formGroup.get('formConfiguration') as FormGroup

    formConfigurationgroup.setValue({
      ...this.tableSelectForm?.value['formConfiguration'],
    });
    this.tableSelectOpen = false
  }

  /**编辑站点按钮 */
  EditSitesBtn(rows, i) {

    this.CurrentSelectionTableControlName = rows.get('formControlName').value
    this.tableSelectForm = this.fb.group(new TableFormControl())
    this.tableSelectForm.patchValue({
      ...rows.value
    })
    this.tableSelectForm.get('formConfiguration')?.patchValue({
      ...rows.value['formConfiguration']
    })
    this._tableSelected = rows.value
    this.TableColumnsIndex = i
    this.tableSelectOpen = true
  }


  /**调整表格位置 */
  TableArrowUpOrDown(type, index) {
    let controlAt = this.TableColumns.at(index)
    this.TableColumns.removeAt(index)
    let lastindex = type == 'up' ? index - 1 : index + 1
    this.TableColumns.insert(lastindex, controlAt)
  }


  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event, item) {
    let value = event.target.value
    let pinyin = this._CmsApiService.chineseToPinyin(value)
    let nameInput = item.get('name')
    if (nameInput.value) return
    nameInput.patchValue(pinyin)
  }
}
