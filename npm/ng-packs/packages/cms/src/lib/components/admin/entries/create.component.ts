/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import {  Location } from '@angular/common';
import { Component, OnInit, inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
// import { EntryAdminService } from '../../../proxy/admin/entries';
import { ECmsComponent } from '../../../enums';
import {
  CreateOrUpdateEntryInputBase,
  EntriesToFormLabelMap,
} from './create-or-update-entry-input-base';
import {
  ValidatorsService,
  UpdateListService,
  LocationBackService,
  DigniteValidatorsService,
} from '@dignite-ng/expand.core';
import { finalize } from 'rxjs';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';

@Component({
  selector: 'cms-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Entries_Create,
    },
  ],
})
export class CreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private _updateListService = inject(UpdateListService);
  private toaster = inject(ToasterService);
  public _location = inject(Location);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private _EntryAdminService = inject(EntryAdminService);
  private _LocalizationService = inject(LocalizationService);
  private _ValidatorsService = inject(ValidatorsService);

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**表单验证状态
   * {
   *  title:true,
   * }
   */
  formValidation: any = '';
  /**表单实体 */
  formEntity: FormGroup | undefined;
  /**语言 */
  cultureName: string | any = '';
  /**条目类型id */
  entryTypeId: string | any = '';
  /**版块id */
  sectionId: string | any = '';
  /**条目版本id */
  entryVersionId: string | any = '';
  /**条目信息 */
  entryInfo: any = '';
  /**是否创建其他语言版本 */
  isOther: string | any = 0;

  /**是否草稿控件*/
  get draftInput() {
    return this.formEntity?.get('draft') as FormControl;
  }
  /**语言控件 */
  get cultureInput(): FormControl {
    return this.formEntity?.get('culture') as FormControl;
  }

  async ngOnInit(): Promise<void> {
    const queryParams = this.route.snapshot.queryParams;

    this.cultureName = queryParams.cultureName;
    this.entryTypeId = queryParams.entryTypeId;
    this.sectionId = queryParams.sectionId;
    this.entryVersionId = queryParams.entryVersionId;
    this.isOther = queryParams.isOther || 0;
    this.formEntity = this.fb.group(new CreateOrUpdateEntryInputBase());
    this.cultureInput.patchValue(this.cultureName);
    if (this.entryVersionId) await this.getEntryInfo();
  }
  /**获取条目信息 */
  getEntryInfo() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.get(this.entryVersionId).subscribe(res => {
        this.cultureName = res.culture;
        this.entryTypeId = res.entryTypeId;
        this.sectionId = res.sectionId;
        this.entryInfo = res;

        resolve(res);
      });
    });
  }
  /**显示条目类型信息 */
  showEntryTypeInfo: any = '';
  /**反馈子级页面信息 */
  _feedbackChildInfo(event) {
    this.showEntryTypeInfo = event?.showEntryType || '';
  }


  private _LocationBackService = inject(LocationBackService);
  private _DigniteValidatorsService = inject(DigniteValidatorsService);

  /**提交 */
  save() {
    const input = this.formEntity?.value;
    
    input.publishTime = new Date(
      new Date(input.publishTime).getTime() + 8 * 60 * 60 * 1000,
    ).toISOString();
    this.formEntity.markAllAsTouched();
    this.formValidation=true;
// console.log(input,'提交',this.formEntity)
    if (!this.formEntity.valid) {
      for (const item of this.showEntryTypeInfo.fieldTabs) {
        for (const el of item.fields) {
          const info = `${this._LocalizationService.instant(
            `Cms::The{1}FieldUnderThe{0}TAB`,
            item.name,
            el.field.displayName,
          )}`;
          EntriesToFormLabelMap[el.field.name] = info;
        }
      }

      this._DigniteValidatorsService.getErrorMessage({
        form: this.formEntity,
        map: EntriesToFormLabelMap,
      });

      this.isSubmit = false;
      if (this.isOther == 1) {
        this.slugInput.disable();
      }
      this.cultureInput.disable();

      return;
    }
    
    this._EntryAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        }),
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));

        //检测该页面上一级是否是系统页面，如果是返回上一页，如果不是，则使用页面跳转到上一级页面。
        this._LocationBackService.backTo({
          url: `/cms/admin/entries`,
          replenish: '/create',
        });

        this._updateListService.updateList();
      });
  }
  isSubmit: boolean | any = false;
  /**点击提交 */
  clickSubmit(type) {
    // if (this.isSubmit) return;
    // this.isSubmit = true;
    this.draftInput.patchValue(type);
    this.cultureInput.enable();
    if (this.isOther == 1) {
      this.slugInput.enable();
    }
    this.formEntity.updateValueAndValidity();
    setTimeout(() => {
      this.submitclick.nativeElement.click();
    }, 0);
  }
  get slugInput(): FormControl {
    return this.formEntity?.get('slug') as FormControl;
  }

  /**返回上一页 */
  backTo() {
    this._location.back();
  }
}
