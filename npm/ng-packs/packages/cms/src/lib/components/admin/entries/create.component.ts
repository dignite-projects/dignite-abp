/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { DatePipe, Location } from '@angular/common';
import { Component, OnInit, inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
// import { EntryAdminService } from '../../../proxy/admin/entries';
import { ECmsComponent } from '../../../enums';
import { CreateOrUpdateEntryInputBase } from './create-or-update-entry-input-base';
import { ValidatorsService, UpdateListService } from '@dignite-ng/expand.core';
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
    this.isOther = queryParams.isOther;
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
  /**提交 */
  save() {
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.formEntity);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'Cms')) {
      this.isSubmit = false;
      return this.cultureInput.disable();
    }
    const input = this.formEntity?.value;
    // input.culture = this.cultureName;
    input.publishTime = new Date(
      new Date(input.publishTime).getTime() + 8 * 60 * 60 * 1000,
    ).toISOString();
    if (!this.formEntity.valid) return;
    this._EntryAdminService
      .create(input)
      .pipe(
        finalize(() => {
          this.isSubmit = false;
        }),
      )
      .subscribe(res => {
        this.toaster.success(this._LocalizationService.instant(`AbpUi::SavedSuccessfully`));
        this._location.back();
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
    this.submitclick?.nativeElement?.click();
  }

  /**返回上一页 */
  backTo() {
    this._location.back();
  }
}
