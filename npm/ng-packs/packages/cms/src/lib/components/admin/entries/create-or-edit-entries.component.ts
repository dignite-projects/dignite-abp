/* eslint-disable @typescript-eslint/no-empty-function */
/* eslint-disable @angular-eslint/component-selector */
import { ConfigStateService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe, Location } from '@angular/common';
import { SectionAdminService } from '../../../proxy/dignite/cms/admin/sections';
import { EntryAdminService } from '../../../proxy/dignite/cms/admin/entries';
import { Observable } from 'rxjs';
import { RegionalizationService } from '../../../proxy/dignite/abp/regionalization-management';
import { ToPinyinService } from '@dignite-ng/expand.core';

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
  private cdRef = inject(ChangeDetectorRef);
  private _RegionalizationService = inject(RegionalizationService);
  constructor(private toPinyinService: ToPinyinService) {}

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
  //tab切换下标
  tabActive = 0;

  @Input() isEdit: boolean | any = false;
  //是否正在创建版本
  @Input() isCreateVersion: boolean | any = false;

  @Input() sectionId: string | any = '';
  @Input() entryTypeId: string | any = '';
  @Input() public set select(v: any) {
    this.entryVersionId = v.id;
    this.entryInfo = v;
  }
  /**向父级反馈信息 */
  @Output() feedbackChildInfo = new EventEmitter();

  formEntity: FormGroup | undefined;
  @Input() set entity(value: FormGroup | undefined) {
    this.formEntity = value;
    if (value) {
      this.loadData();
    }
  }
  /**是否加载完成 */
  isLoad: boolean | any = false;
  /**是否创建其他语言本 */
  @Input() isOther: any = 0;

  /**系统默认语言 */
  DefaultLanguage: string | any = '';

  /**语言控件 */
  get cultureInput(): FormControl {
    return this.formEntity?.get('culture') as FormControl;
  }
  get slugInput(): FormControl {
    return this.formEntity?.get('slug') as FormControl;
  }
  get initialVersionIdInput(): FormControl {
    return this.formEntity?.get('initialVersionId') as FormControl;
  }

  /**获取提交按钮替身，用于真实触发表单提交 */
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;
  /**站点设置语言 */
  SiteSettingsAdminLanguages: any[] = [];
  /**站点设置的默认语言 */
  defaultCultureName: any = '';
  /**
   * 获取站点设置语言
   */
  getSiteSettingsLanguages() {
    return new Promise((resolve, rejects) => {
      this._RegionalizationService.get().subscribe(res => {
        this.SiteSettingsAdminLanguages = res.availableCultureNames;
        this.defaultCultureName = res.defaultCultureName;
        resolve(res);
      });
    });
  }

  /**加载数据 */
  async loadData() {
    await this.getSiteSettingsLanguages();
    //获取语言列表
    const languagesSystem = this.configState.getDeep('localization.languages');
    //获取系统默认语言 */
    this.DefaultLanguage = this.defaultCultureName;
    // this.DefaultLanguage = this.configState.getSetting('Abp.Regionalization.DefaultCultureName');
    //选中languagesSystem中的cultureName在 languagesSystem中存在的数组项

    this.languagesList = languagesSystem.filter(el =>
      this.SiteSettingsAdminLanguages.includes(el.cultureName),
    );
    if (this.sectionId) {
      await this.getSectionInfo();
      await this.getEntryList();
    }
    if (!(this.isOther == 1)) {
      this.cultureInput.disable();
    }
    const repetition = await this.cultureAsyncValidator();
    if (repetition) this.cultureInput.setErrors(repetition);
    this.slugInput.addValidators(this.SlugRegExValidator());
    this.slugInput.addAsyncValidators(this.SlugAsyncValidator());
    if (this.entryInfo) {
      await this.getAllVersionsList();
      this.formEntity.patchValue({
        entryTypeId: this.entryInfo.entryTypeId,
        publishTime: this.datePipe.transform(this.entryInfo.publishTime, 'yyyy-MM-dd HH:mm:ss'),
        // title: this.entryInfo.title,
        slug: this.entryInfo.slug,
        parentId: this.entryInfo.parentId,
        versionNotes: this.entryInfo.versionNotes,
        initialVersionId: this.entryInfo.id,
      });
      this.slugInput.setErrors({});
      this.slugInput.setErrors(null);
    } else {
      this.formEntity.patchValue({
        entryTypeId: this.entryTypeId,
        publishTime: this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss'),
      });
    }

    this.cdRef.detectChanges();

    this.cdRef.detectChanges();
    this.feedbackChildInfo.emit({
      showEntryType: this.showEntryType,
    });
    this.isLoad = true;
    if (this.isOther == 1) {
      this.initialVersionIdInput.patchValue('');
      this.slugInput.disable();
      await this.getLocalizedEntriesBySlug();
    }

    setTimeout(() => {
      // this.submitclick?.nativeElement.click();
    }, 0);
  }

  /**获取别名下其他的语言版本 */
  getLocalizedEntriesBySlug() {
    return new Promise((resolve, rejects) => {
      this._EntryAdminService
        .getLocalizedEntriesBySlug(this.sectionId, this.slugInput.value)
        .subscribe(
          res => {
            //  console.log(res,'获取别名下其他的语言版本',this.slugInput.value);
            this.languagesList = this.languagesList.filter(
              el => !res.items.find(el2 => el2.culture === el.cultureName),
            );
            this.cultureInput.patchValue(this.languagesList[0].cultureName);
            resolve(res);
          },
          err => {
            resolve(null);
          },
        );
    });
  }
  SlugRegExValidator() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[a-zA-Z0-9_-]+$/;
      if (control.value && !regex.test(control.value)) {
        return { repetition: this._LocalizationService.instant(`Cms::SlugValidatorsText`) };
      }

      return null;
    };
  }

  // /**别名查重 */
  SlugAsyncValidator() {
    return (
      control: AbstractControl,
    ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        if (control.value) {
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
                    control.value,
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
                this.languagesList.find(el => el.cultureName == culture).displayName,
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
            const layer: number | any = 0;
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
  /**别名转化 */
  slugChange(event: any) {
    console.log(event, '别名转化');
    const val = event.target.value;
    //将val字段中的特殊字符替换为-，允许输入字母，数字，下划线，中划线，小数点
    const newVal = val.replace(/[^\-.\w\s]/g, '-');
    
    //将newVal字段中的空格替换为-
    const newVal2 = newVal.replace(/\s+/g, '-');
    //将newVal2字段中的多个-替换为单个-
    const newVal3 = newVal2.replace(/-+/g, '-');
    //如果-在开头或者结尾，则删除
    const newVal4 = newVal3.replace(/^-|-$/g, '');

    this.slugInput.patchValue(newVal4);
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
