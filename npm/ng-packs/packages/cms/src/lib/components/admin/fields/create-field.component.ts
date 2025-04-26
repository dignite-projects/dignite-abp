/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Location } from '@angular/common';
import { Component, inject, ViewChild, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CmsApiService } from '../../../services';
import { ECmsComponent } from '../../../enums';
import { UpdateListService } from '@dignite-ng/expand.core';
import { CreateOrUpdateFieldInputBase } from './create-or-update-field-input-base';
import { ValidatorsService } from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { FieldAdminService } from '../../../proxy/dignite/cms/admin/fields';
import { ActivatedRoute } from '@angular/router';

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
export class CreateFieldComponent implements OnInit{
  private route = inject(ActivatedRoute);
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

  get groupIdInput(){
    return this.newEntity?.get('groupId') as FormControl;
  }

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    const {groupId} = this.route.snapshot.queryParams;
    this.newEntity = this.fb.group(new CreateOrUpdateFieldInputBase());
    if(groupId){
      this.groupIdInput.patchValue(groupId)
    }
   
  }

  /**触发提交按钮 */
  submitclickBtn() {
    this.submitclick.nativeElement.click();
  }
  isSubmit: boolean|any = false;

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
    const input = this.newEntity.value;
    
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
