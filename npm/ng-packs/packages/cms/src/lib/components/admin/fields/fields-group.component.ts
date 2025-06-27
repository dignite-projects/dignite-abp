/* eslint-disable @typescript-eslint/no-inferrable-types */
/* eslint-disable @angular-eslint/component-selector */
import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { FieldsDataService } from '../../../services/fields-data.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { LocalizationService } from '@abp/ng.core';

@Component({
  selector: 'cms-fields-group',
  templateUrl: './fields-group.component.html',
  styleUrl: './fields-group.component.scss',
})
export class FieldsGroupComponent {
  constructor(
    private fb: FormBuilder,
    public _service: FieldsDataService,
    private confirmation: ConfirmationService,
    public _LocalizationService: LocalizationService,
    private toaster: ToasterService,
  ) {}

  /**
   * 选择的字段分组id
   */
  selectedGroupId: string = '';
  /**模态框是否开启状态 */
  modalOpen: boolean = false;
  /**模态框是否在忙碌状态 */
  modalBusy: boolean = false;
  /**存储将要修改的值 */
  selected: string = '';
  /**字段分组表单 */
  groupForm: FormGroup | undefined;

  /**表单是否触发验证 */
  formValidation: boolean = false;

  /**模态框-表单--控件模板-动态赋值表单控件 */
  @ViewChild('ModalFormSubmit', { static: false }) ModalFormSubmit: ElementRef;

  /**点击分组回调 */
  @Output() OnGroupClickBack = new EventEmitter();

  /**选择分组 */
  groupChangeBtn(groupId = '') {
    this.selectedGroupId = groupId;
    this.OnGroupClickBack.emit(groupId);
  }

  /**模态框状态改变 */
  modalVisibleChange(event) {
    if (!event) {
      this.modalOpen = event;
      this.modalBusy = false;
      this.formValidation = false;
      this.selected = '';
      this.groupForm = undefined;
      return;
    }
  }

  /**
   * 创建分组按钮
   */
  createGroupBtn() {
    this.modalOpen = true;
    this.groupForm = this.fb.group({
      name: ['', Validators.required],
    });
  }
  /**编辑分组按钮 */
  editGroupBtn(name) {
    this.modalOpen = true;
    this.selected = name;
    this.groupForm = this.fb.group({
      name: [name, Validators.required],
    });
  }

  /**表单提交 */
  save() {
    this.formValidation = true;
    if (!this.groupForm?.valid) {
      return;
    }
    this.modalBusy = true;
    if (this.selected) {
      // this.
      this._service.updateFieldGroup(this.selectedGroupId, this.groupForm.value).subscribe({
        next: () => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
          this.modalVisibleChange(false);
          this._service.getfieldGroups(true);
        },
        error: e => console.error(e),
        complete: () => console.info('complete'),
      });
      // .subscribe(() => {
      //   this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
      //   this.modalVisibleChange(false);
      //   this._service.getfieldGroups(true);
      // });
      return;
    }
    this._service.createFieldGroup(this.groupForm.value).subscribe(async res => {
      this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
      this.modalVisibleChange(false);
      await this._service.getfieldGroups(true);
      this.groupChangeBtn(res.id);
    });
  }

  /**删除分组 */
  deleteGroupBtn(fieldGroupitem: any) {
    this.confirmation
      .warn(
        fieldGroupitem.name,
        this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
      )
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._service.deleteFieldGroup(fieldGroupitem.id).subscribe(async () => {
            this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
            await this._service.getfieldGroups(true);
            this.groupChangeBtn();
          });
        }
      });
  }
}
