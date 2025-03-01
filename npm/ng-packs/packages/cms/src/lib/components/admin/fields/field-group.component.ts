import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { finalize} from 'rxjs/operators';
import { FieldAbstractsService } from '../../../services/field-abstracts.service';
import { LocalizationService } from '@abp/ng.core';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { FieldGroupAdminService } from '../../../proxy/dignite/cms/admin/fields';

@Component({
  selector: 'cms-field-group',
  templateUrl: './field-group.component.html',
  styleUrls: ['./field-group.component.scss']
})
export class FieldGroupComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private _FieldGroupAdminService: FieldGroupAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    public _FieldAbstractsService: FieldAbstractsService,
    public _LocalizationService: LocalizationService,
  ) { }


  /**创建分组表单 */
  createForm: FormGroup | undefined;

  /** 编辑分组表单*/
  editGroupForm: FormGroup | undefined;

  /**表单已存在的值 */
  selected = {} as any;

  /**选择的字段分组id */
  fieldGroupId: string | undefined = ''

  /**创建分组模态框状态 */
  createGroupOpen = false

  /**用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean = false

  /**编辑分组模态框状态 */
  editGroupOpen: boolean = false

  /**点击分组回调 */
  @Output() OnGroupClickBack = new EventEmitter();

  ngOnInit(): void {
    this._FieldAbstractsService.getfieldGroupList()
  }

  /**创建字段分组 */
  createGroupBtn() {
    this.createGroupOpen = true;
    this.formValidation='';
    this.createForm = this.fb.group({
      name: ['', [Validators.required]],
    })
  }

  /**创建分组模态框状态改变 */
  createGroupVisibleChange(event) {
    if (!event) {
      return
    }
  }

  /**编辑字段分组 */
  editGroupBtn(itemName: string) {
    this.editGroupOpen = true;
    this.formValidation='';
    this.editGroupForm = this.fb.group({
      name: [itemName, [Validators.required]],
    })
  }

  /**编辑分组模态框状态改变 */
  editGroupVisibleChange(event) {
    if (!event) {
      return
    }
  }
  private _ValidatorsService = inject(ValidatorsService);
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';


  /**创建字段分组保存 */
  createSave() {
    let input = this.createForm.value;
    this.modalBusy = true;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.createForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation,'Cms')) return;
    this._FieldGroupAdminService.create(input).pipe(finalize(() => {
      this.modalBusy = false;
      this.createGroupOpen = false;
      this.formValidation='';
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
      this._FieldAbstractsService.getfieldGroupList()
    })
  }

  /**编辑字段分组保存 */
  editSave() {
    let input = this.editGroupForm.value;
    this.modalBusy = true;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.createForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation,'Cms')) return;
    this._FieldGroupAdminService.update(this.fieldGroupId, input).pipe(finalize(() => {
      this.modalBusy = false;
      this.editGroupOpen = false;
      this.formValidation='';
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
      this._FieldAbstractsService.getfieldGroupList()
    })
  }

  /**删除字段分组1 */
  deleteGroupbtn(fieldGroupitem: any) {
    this.confirmation.warn(
      fieldGroupitem.name,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._FieldGroupAdminService.delete(this.fieldGroupId).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
          this.fieldGroupId = ''
          this._FieldAbstractsService.getfieldGroupList()
        })
      }
    });
  }

  /**字段分组改变 */
  fieldGroupChange(event) {
    this.fieldGroupId = event
    this.OnGroupClickBack.emit(event)
  }

}


