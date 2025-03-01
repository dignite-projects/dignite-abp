/* eslint-disable @angular-eslint/component-selector */
import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { MenuItemAdminService } from '../../proxy/volo/cms-kit/admin/menus';
import { finalize } from 'rxjs';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { UpdateListService } from '@dignite-ng/expand.core';

@Component({
  selector: 'cms-create-or-editmenu-item-modal',
  templateUrl: './create-or-editmenu-item-modal.component.html',
  styleUrl: './create-or-editmenu-item-modal.component.scss',
})
export class CreateOrEditmenuItemModalComponent {
  private _UpdateListService = inject(UpdateListService);
  private _ValidatorsService = inject(ValidatorsService);
  private _MenuItemAdminService = inject(MenuItemAdminService);
  private toaster = inject(ToasterService);
  private _LocalizationService = inject(LocalizationService);
  @Input()
  public set form(v: FormGroup | undefined) {
    this.ModalForm = v;
  }

  @Input()
  public set visible(v: boolean) {
    this.ModalOpen = v;
  }
  @Output() visibleChange = new EventEmitter();

  /**模态框-状态-是否打开 */
  ModalOpen: boolean = false;

  /**模态框-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalBusy: boolean = false;
  /**模态框-表单 */
  ModalForm: FormGroup | undefined;

  get idInput() {
    return this.ModalForm?.get('id');
  }

  /**模态框-表单--控件模板-动态赋值表单控件 */
  @ViewChild('ModalFormSubmit', { static: false }) ModalFormSubmit: ElementRef;

  /**模态框-状态改变回调 !event模态框关闭时执行*/
  ModalVisibleChange(event) {
    this.visibleChange.emit(event);
    if (!event) {
      this.formValidation = '';
      return;
    }
  }
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';
  /**保存 */
  createOrEditSave() {
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.ModalForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'CmsKit')) return;
    if (this.ModalBusy) return;
    this.ModalBusy = true;
    let input = this.ModalForm.value;

    if (!this.ModalForm.valid) return;

    if (this.idInput.value) {
      this._MenuItemAdminService
        .update(this.idInput.value, input)
        .pipe(
          finalize(() => {
            this.reset();
          })
        )
        .subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
          this.ModalVisibleChange(false);
          this._UpdateListService.updateList();
        });
      return;
    }
    this._MenuItemAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.reset();
        })
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
        this.ModalVisibleChange(false);
        this._UpdateListService.updateList();
      });
  }
  reset() {
    this.ModalBusy = false;
    this.formValidation = '';
  }
}
