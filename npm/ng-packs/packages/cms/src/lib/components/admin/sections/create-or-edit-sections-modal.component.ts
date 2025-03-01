import { LocalizationService } from '@abp/ng.core';
import {
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ValidatorsService, UpdateListService } from '@dignite-ng/expand.core';
import { finalize, Observable } from 'rxjs';
import { ToasterService } from '@abp/ng.theme.shared';
import { CmsApiService } from '../../../services';
import { SectionAdminService } from '../../../proxy/dignite/cms/admin/sections';
import { SectionType, sectionTypeOptions } from '../../../proxy/dignite/cms/sections';

@Component({
  selector: 'cms-create-or-edit-sections-modal',
  templateUrl: './create-or-edit-sections-modal.component.html',
  styleUrl: './create-or-edit-sections-modal.component.scss',
})
export class CreateOrEditSectionsModalComponent {
  private _LocalizationService = inject(LocalizationService);
  private toaster = inject(ToasterService);
  private _ValidatorsService = inject(ValidatorsService);
  private _UpdateListService = inject(UpdateListService);
  private _SectionAdminService = inject(SectionAdminService);
  private _CmsApiService = inject(CmsApiService);
  _SectionType = SectionType;
  _sectionTypeOptions = sectionTypeOptions;
  /**表单验证状态 */
  formValidation: any = '';
 
  /**模态框-状态-是否打开 */
  ModalOpen: boolean = false;
  @Input()
  public set visible(v: boolean) {
    this.ModalOpen = v;
  }

  /**模态框-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalBusy: boolean = false;
  /**初始值 */
  selected: any = '';
  /**模态框-表单 */
  ModalForm: FormGroup | undefined;
  @Input()
  public set formEntity(v: FormGroup | undefined) {
    this.ModalForm = v;
    if (v) {
      this.selected = v?.value;
      this.setAsyncValidatorsFn();
    }
  }
  get idInput() {
    return this.ModalForm?.get('id') as FormControl;
  }
  get displayNameInput() {
    return this.ModalForm.get('displayName') as FormControl;
  }
  get nameInput() {
    return this.ModalForm.get('name') as FormControl;
  }
  get routeInput() {
    return this.ModalForm.get('route') as FormControl;
  }
  get templateInput() {
    return this.ModalForm.get('template') as FormControl;
  }
  get typeInput() {
    return this.ModalForm.get('type') as FormControl;
  }
  radiochange() {
    this.routeInput.patchValue(this.routeInput.value);
  }

  /**模态框-表单--控件模板-动态赋值表单控件 */
  @ViewChild('ModalFormSubmit', { static: false }) ModalFormSubmit: ElementRef;
  @Output() visibleChange = new EventEmitter();
  /**模态框-状态改变回调 */
  ModalVisibleChange(event) {
    this.ModalOpen = event;
    this.visibleChange.emit(event);
    if (!event) {
      return;
    }
  }
  /**保存 */
  save() {
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.ModalForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')) return;

    if (this.ModalBusy) return;
    this.ModalBusy = true;
    if (!this.ModalForm.valid) return;
    let input = this.ModalForm.value;
    if (this.idInput.value) {
      this._SectionAdminService
        .update(this.idInput.value, input)
        .pipe(
          finalize(() => {
            this.reset();
          })
        )
        .subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
          this.visibleChange.emit(false);
          this.formValidation='';
        });
        return;
    }
    this._SectionAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.reset();
        })
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        this.visibleChange.emit(false);
        this.formValidation='';
      });
  }
  reset(){
    this.ModalBusy = false;
    this._UpdateListService.updateList();
  }

    disPlayNameInputBlur(event) {
    let value = event.target.value;
    let pinyin = this._CmsApiService.chineseToPinyin(value);
    let nameInput = this.nameInput;
    let routeInput = this.routeInput;
    let templateInput = this.templateInput;
    if (nameInput.value) return;
    nameInput.patchValue(pinyin);
    if (routeInput.value) return;
    routeInput.patchValue(pinyin + (this.typeInput.value === 0 ? '' : '/{slug}'));
    if (templateInput.value) return;
    templateInput.patchValue(pinyin + '/index');
  }

  setAsyncValidatorsFn() {
    this.ModalForm?.setControl(
      'name',
      new FormControl(this.nameInput.value || '', {
        validators: Validators.required,
        asyncValidators: this.nameRepetitionAsyncValidator(),
      })
    );
    this.ModalForm?.setControl(
      'route',
      new FormControl(this.routeInput.value || '', {
        validators: [Validators.required, this.forbiddenNameValidator()],
        asyncValidators: [this.routeRepetitionAsyncValidator()],
        updateOn: 'change',
      })
    );
  }
  forbiddenNameValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let inputValue = control.value.toLocaleLowerCase();
      let forbidden =
        this.typeInput.value == 0 ? false : inputValue.includes('{slug}') ? false : true;
      return forbidden
        ? {
            repetition: this._LocalizationService.instant(
              `Cms::RouteVerificationTips`,
              this._LocalizationService.instant(
                `Cms::Enum:SectionType:` + SectionType[this.typeInput.value]
              ),
              '{slug}'
            ),
          }
        : null;
    };
  }
  nameRepetitionAsyncValidator() {
    return (
      ctrl: AbstractControl
    ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.ModalForm?.get('name');

        if (subslug.value == this.selected?.name) {
          resolve(null);
          return;
        }
        this._SectionAdminService.nameExists({ name: subslug.value }).subscribe(res => {
          if (res) {
            resolve({
              repetition: this._LocalizationService.instant(
                `Cms::SectionName{0}AlreadyExist`,
                ctrl.value
              ),
            });
          } else {
            resolve(null);
          }
        });
      });
    };
  }
  routeRepetitionAsyncValidator() {
    return (
      ctrl: AbstractControl
    ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.ModalForm?.get('route').value;
        if (subslug == this.selected?.route) {
          resolve(null);
          return;
        }
        this._SectionAdminService.routeExists({ route: subslug }).subscribe(res => {
          if (res) {
            resolve({
              repetition: this._LocalizationService.instant(
                `Cms::SectionRoute{0}AlreadyExist`,
                ctrl.value
              ),
            });
          } else {
            resolve(null);
          }
        });
      });
    };
  }
}
