/* eslint-disable @angular-eslint/component-selector */
import { ConfigStateService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe, Location } from '@angular/common';
import { CmsApiService } from '../../../services';
import { SectionAdminService } from '../../../proxy/dignite/cms/admin/sections';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';
import { Observable } from 'rxjs';

@Component({
  selector: 'cms-create-or-edit-entries',
  templateUrl: './create-or-edit-entries.component.html',
  styleUrls: ['./create-or-edit-entries.component.scss'],
})
export class CreateOrEditEntriesComponent {
  private toaster = inject(ToasterService);
  public _location = inject(Location);
  private configState = inject(ConfigStateService);
  private _SectionAdminService = inject(SectionAdminService);
  private _EntryAdminService = inject(EntryAdminService);
  private datePipe = inject(DatePipe);
  private _LocalizationService = inject(LocalizationService);
  private router = inject(Router);
  private _CmsApiService = inject(CmsApiService);
  private cdRef = inject(ChangeDetectorRef);

  /**语言列表 */
  languagesList: any[] = [];
  /**条目列表-选择上级条目 */
  entryList: any[] = [];
  /**条目信息 */
  entryInfo: any = '';
  /**版本信息 */
  sectionInfo: any = '';
  /**需要展示的b板块下条目类型 */
  showEntryType: any = '';
  /**版本条目id */
  entryVersionId: any = '';
  /**版本列表 */
  AllVersionsList: any[] = [];

  @Input() isEdit: boolean|any = false;

  @Input() sectionId: string|any = '';
  @Input() entryTypeId: string|any = '';
  @Input() public set select(v: any) {
    this.entryVersionId = v.id;
    this.entryInfo = v;
  }

  formEntity: FormGroup | undefined;
  @Input() set entity(value: FormGroup | undefined) {
    this.formEntity = value;
    if (value) {
      const languages = this.configState.getDeep('localization.languages');
      this.languagesList = languages;
      this.loadData();
    }
  }
  /**是否加载完成 */
  isLoad: boolean|any = false;

  /**语言控件 */
  get cultureInput(): FormControl {
    return this.formEntity?.get('culture') as FormControl;
  }
  get slugInput(): FormControl {
    return this.formEntity?.get('slug') as FormControl;
  }

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  /**加载数据 */
  async loadData() {
    if (this.sectionId) {
      await this.getSectionInfo();
      await this.getEntryList();
    }
    this.cultureInput.disable();
    const repetition = await this.cultureAsyncValidator();
    if (repetition) this.cultureInput.setErrors(repetition);
    this.slugInput.addAsyncValidators(this.SlugAsyncValidator());
    

    if (this.entryInfo) {
      // this.slugInput.reset(this.entryInfo.slug);
      // this.slugInput.updateValueAndValidity();
      await this.getAllVersionsList();
      this.formEntity.patchValue({
        entryTypeId: this.entryInfo.entryTypeId,
        publishTime: this.datePipe.transform(this.entryInfo.publishTime, 'yyyy-MM-dd HH:mm:ss'),
        title: this.entryInfo.title,
        slug: this.entryInfo.slug,
        parentId: this.entryInfo.parentId,
        versionNotes: this.entryInfo.versionNotes,
        initialVersionId: this.entryInfo.id,
      });
      this.slugInput.setErrors({})
      this.slugInput.setErrors(null)
    } else {
      this.formEntity.patchValue({
        entryTypeId: this.entryTypeId,
        publishTime: this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss'),
      });
    }
    
    this.cdRef.detectChanges();
    this.isLoad = true;
    setTimeout(() => {
      this.submitclick?.nativeElement.click();
    }, 0);
  }
  // /**别名查重 */
  SlugAsyncValidator() {
    return (
      control: AbstractControl
    ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise( resolve => {
        if ( control.value) {
          if (control.value == this.entryInfo?.slug) {
            resolve(null);
            return;
          }
          this._EntryAdminService
            .slugExists({
              culture: this.cultureInput.value,
              sectionId: this.sectionId,
              slug: control.value,
            })
            .subscribe(res => {
              if (res) {
                resolve({
                  repetition: this._LocalizationService.instant(
                    `Cms::EntrySlug{0}AlreadyExist`,
                    control.value
                  ),
                });
              } else {
                resolve(null);
              }
            });
        }
      });
    };
  }

  /**定义自定义异步验证 */
  cultureAsyncValidator() {
    return new Promise(resolve => {
      const culture = this.cultureInput.value;
      if (culture == this.entryInfo?.culture || this.sectionInfo.type !== 0) return resolve(null);
      this._EntryAdminService
        .cultureExistWithSingleSection({
          culture: culture,
          sectionId: this.sectionId,
          entryTypeId: this.entryTypeId,
        })
        .subscribe(res => {
          if (res) {
            resolve({
              repetition: this._LocalizationService.instant(
                `Cms::EntriesAlreadyExistEntryType`,
                '',
                this.languagesList.find(el => el.cultureName == culture).displayName
              ),
            });
          } else {
            resolve(null);
          }
        });
    });
  }

  /**获取板块信息 */
  getSectionInfo() {
    return new Promise((resolve, reject) => {
      this._SectionAdminService.get(this.sectionId).subscribe(res => {
        this.showEntryType = res.entryTypes.find(el => el.id == this.entryTypeId);
        this.sectionInfo = res;
        resolve(res);
      });
    });
  }
  /**获取板块下所有条目 */
  getEntryList() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService
        .getList({
          sectionId: this.sectionId,
          maxResultCount: 1000,
          culture: this.cultureInput.value,
        })
        .subscribe((res: any) => {
          const entryList = res.items.filter(el => el.id !== this.entryInfo?.id);
          const parentList = entryList.filter(el => !el.parentId);
          parentList.forEach(el => {
            const layer: number|any = 0;
            el.layer = new Array(layer);
            el.children = this.groupByParentId(entryList, el.id, layer + 1);
          });
          this.entryList = parentList;
          resolve(res);
        });
    });
  }
  /**对数组按照父子关系进行分组 */
  groupByParentId(arr, id = '', layer) {
    let result = [];
    result = arr.filter(el => el.parentId == id);
    result.forEach(el => {
      el.layer = new Array(layer);
      el.children = this.groupByParentId(arr, el.id, layer + 1);
    });
    return result;
  }
  /**标题转化别名 */
  setTitleToSlugBlur(event) {
    const val = event.target.value;
    const slug = this.formEntity.get('slug');
    let pinyinstr = '';
    if (slug.value) return;
    pinyinstr = this._CmsApiService.chineseToPinyin(val);
    this.slugInput.patchValue(pinyinstr || val);
    this._EntryAdminService
      .slugExists({
        culture: this.cultureInput.value,
        sectionId: this.sectionId,
        slug: this.slugInput.value,
      })
      .subscribe(res => {
        if (res) {
          this.slugInput.setErrors(
            {
              repetition: this._LocalizationService.instant(
                `Cms::EntrySlug{0}AlreadyExist`,
                this.slugInput.value
              ),
            }
          )
        } else {
          this.slugInput.setErrors(null)
        }
      });
  }

  /**获取条目版本列表 */
  getAllVersionsList() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService.getAllVersions(this.entryInfo.id).subscribe(res => {
        this.AllVersionsList = res.items;
        resolve(res);
      });
    });
  }

  /**激活 */
  ActivatedVersion(VersionId) {
    this._EntryAdminService.activate(VersionId).subscribe(res => {
      this.AllVersionsList.forEach(el => {
        el.isActivatedVersion = el.id === VersionId;
      });
      return;
    });
  }
  /**编辑版本 */
  toEditUrl(id) {
    this.router.navigateByUrl('', { skipLocationChange: true }).then(() => {
      this.router.navigate([`/cms/admin/entries/${id}/edit`]);
    });
  }
  /**删除版本 */
  delectVersion(vid) {
    this._EntryAdminService.delete(vid).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
      this.getAllVersionsList();
    });
  }
}
