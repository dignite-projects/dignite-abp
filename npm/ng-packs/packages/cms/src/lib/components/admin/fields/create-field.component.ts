import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Location } from '@angular/common';
import { Component, inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CmsApiService } from '../../../services';
import { ECmsComponent } from '../../../enums';
import { UpdateListService } from '@dignite-ng/expand.core';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { FieldAdminService } from '../../../proxy/dignite/cms/admin/fields';

@Component({
  selector: 'cms-create-field',
  templateUrl: './create-field.component.html',
  styleUrls: ['./create-field.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsCreate,
    },
  ],
})
export class CreateFieldComponent {
  constructor(
    private fb: FormBuilder,
    public _FieldAdminService: FieldAdminService,
    private toaster: ToasterService,
    public _location: Location,
    public _LocalizationService: LocalizationService,
    public _CmsApiService: CmsApiService
  ) {}
  private _UpdateListService = inject(UpdateListService);

  /**表单实体 */
  newEntity: FormGroup | undefined;

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.newEntity = this.fb.group(new CreateOrUpdateFieldInputBase());
  }

  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click();
  }
  isSubmit: boolean = false;

  private _ValidatorsService = inject(ValidatorsService);
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';

  /**保存表单 */
  save() {
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.newEntity);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')) return;
    let input = this.newEntity.value;
    if (this.isSubmit) return;
    this.isSubmit = true;
    if (!this.newEntity.valid) return;
    this._FieldAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        })
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        this._location.back();
        this._UpdateListService.updateList();
      });
  }

}
