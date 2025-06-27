/* eslint-disable @typescript-eslint/no-inferrable-types */
/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { Component, ElementRef,  OnInit, ViewChild } from '@angular/core';
import { ECmsComponent } from '../../../enums';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {
  DigniteValidatorsService,
  LocationBackService,
  UpdateListService,
} from '@dignite-ng/expand.core';
import { FieldsFormConfig, fieldToFormLabelMap } from './fields-form-config';
import { FieldsDataService } from '../../../services/fields-data.service';

@Component({
  selector: 'cms-create-field',
  templateUrl: './create-field.component.html',
  styleUrl: './create-field.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.FieldsCreate,
    },
  ],
})
export class CreateFieldComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    public _service: FieldsDataService,
    private toaster: ToasterService,
    public _LocationBackService: LocationBackService,
    public _LocalizationService: LocalizationService,
    private route: ActivatedRoute,
    private _UpdateListService: UpdateListService,
    private _DigniteValidatorsService: DigniteValidatorsService,
  ) {}

  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    const { groupId } = this.route.snapshot.queryParams;
    this.formEntity = this.fb.group(new FieldsFormConfig());
    if (groupId) {
      this.groupIdInput.patchValue(groupId);
    }
  }

  /**表单实体 */
  formEntity: FormGroup | undefined;

  /**表单是否触发验证 */
  formValidation: boolean = false;

  /**是否提交 */
  isSubmitted: boolean = false;

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**字段分组控件 */
  get groupIdInput() {
    return this.formEntity?.get('groupId') as FormControl;
  }

  /**提交表单 */
  save() {
    console.log(this.formEntity.value, '提交表单', this.formEntity);
    this.formValidation = true;
    if (!this.formEntity.valid) {
      this._DigniteValidatorsService.getErrorMessage({
        form:this.formEntity,
        map:fieldToFormLabelMap
      });
      return;
    }
    if (this.isSubmitted) return;
    this.isSubmitted = true;
    const input = this.formEntity.value;
    this._service.createField(input).subscribe(
      () => {
        this.reset();
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        // this._LocationBackService.back();
        this._LocationBackService.backTo({
          url: `/cms/admin/fields`,
          replenish: '/create',
        });
        this._UpdateListService.updateList();
      },
      () => {
        this.reset();
      },
    );
  }
  /**重置表单 */
  reset() {
    this.isSubmitted = false;
  }
}
