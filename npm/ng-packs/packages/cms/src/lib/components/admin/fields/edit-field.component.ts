import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, ElementRef, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FieldAbstractsService, CmsApiService } from '../../../services';
import { ECmsComponent } from '../../../enums';
import { UpdateListService } from '@dignite-ng/expand.core';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { FieldAdminService } from '../../../proxy/dignite/cms/admin/fields';

@Component({
  selector: 'cms-edit-field',
  templateUrl: './edit-field.component.html',
  styleUrls: ['./edit-field.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsEdit,
    },
  ],
})
export class EditFieldComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    public _FieldAbstractsService: FieldAbstractsService,
    public _FieldAdminService: FieldAdminService,
    private route: ActivatedRoute,
    private toaster: ToasterService,
    public _location: Location,
    public _LocalizationService: LocalizationService,
    public _CmsApiService: CmsApiService
  ) {}
  private _UpdateListService = inject(UpdateListService);
  private _ValidatorsService = inject(ValidatorsService);
  /**表单实体 */
  newEntity: FormGroup | undefined;

  /**字段id */
  fieldId: string = '';

  /**字段详情 */
  fieldDetails: any;

  isSubmit: boolean = false;
  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  async ngOnInit(): Promise<void> {
    const _fieldId = this.route.snapshot.params.id;
    if (_fieldId) {
      this.fieldId = _fieldId;
      this.newEntity = this.fb.group(new CreateOrUpdateFieldInputBase());
      await Promise.all([this._FieldAbstractsService.getFromControlList(), this.getFieldEdit()]);
      this.newEntity.patchValue({
        ...this.fieldDetails,
        formConfiguration: this.fieldDetails.formConfiguration,
      });
    }
  }

  /**获取字段详情 */
  getFieldEdit() {
    return new Promise((resolve, reject) => {
      this._FieldAdminService.get(this.fieldId).subscribe(res => {
        res.groupId = res.groupId ? res.groupId : '';
        this.fieldDetails = res;
        resolve(res);
      });
    });
  }
  

  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click();
  }

  /**保存表单 */
  save() {
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.newEntity);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')) return;
    if (this.isSubmit) return;
    this.isSubmit = true;
    let input = this.newEntity.value;

    if (!this.newEntity.valid) return;
    this._FieldAdminService
      .update(this.fieldId, input)
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
