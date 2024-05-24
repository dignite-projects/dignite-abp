import { ConfigStateService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { AfterContentInit, Component, Input, QueryList,  ViewChildren, ViewContainerRef } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import {Router } from '@angular/router';
import { EntryAdminService } from '../../../proxy/admin/entries';
import { SectionAdminService } from '../../../proxy/admin/sections';
import { Observable } from 'rxjs';
import { DatePipe, Location } from '@angular/common';
import { CmsApiService } from '../../../services';

@Component({
  selector: 'cms-create-or-edit-entries',
  templateUrl: './create-or-edit-entries.component.html',
  styleUrls: ['./create-or-edit-entries.component.scss']
})
export class CreateOrEditEntriesComponent implements AfterContentInit {


  constructor(
    private toaster: ToasterService,
    public _location: Location,
    private configState: ConfigStateService,
    private _SectionAdminService: SectionAdminService,
    private _EntryAdminService: EntryAdminService,
    private datePipe: DatePipe,
    private _LocalizationService: LocalizationService,
    private router: Router,
    private _CmsApiService: CmsApiService,
  ) { }


  /**语言 */
  cultureName: string = ''
  /**条目id */
  entryTypeId: string = ''

  /**新建版本的版本id，同条目id */
  RevisionEntryId: any = ''
  /**选择的条目项 */
  entryTypesItem: any = ''
  /**版块id */
  sectionId: string = ''
  /**版块详情 */
  SectionSelect: any = ''
  /**语言列表 */
  languagesList: any[] = []
  /**条目类型列表-用于选择上级条目 */
  entryTypesList: any[] = []
  /**版本列表 */
  AllVersionsList: any[] = []

  /**是否是编辑 */
  @Input() isEdit = false

  /**来自父组件的传值 */
  FromParentQueryParams: any = ''
  @Input()
  public set ParentQueryParams(v: any) {
    if (v) {
      this.FromParentQueryParams = v;
    }
  }

  /**表单实体 */
  _entity: FormGroup | undefined
  @Input()
  public set entity(v: any) {
    if (v) {
      this._entity = v;
    }
  }
  /**指定id的条目信息 */
  entrySelect: any = ''
  @Input()
  public set parentEntrySelect(v: any) {
    if (v) {
      let V_extraProperties = this._CmsApiService.deepClone(this.convertExtraProperties(v.extraProperties))
      v.extraProperties = V_extraProperties
      this.entrySelect = v;
    }
  }
  /**将对象中的ExtraProperties赋值到extraProperties */
  convertExtraProperties(obj) {
    for (let key in obj) {
      if (Array.isArray(obj[key])) {
        obj[key].forEach(item => {
          if (item.hasOwnProperty('ExtraProperties')) {
            item['extraProperties'] = item['ExtraProperties'];
          }
        });
      }
    }
    return obj;
  }

  /**别名表单实体 */
  get sluginput() {
    return this._entity.get('slug')
  }
  /**语言表单实体 */
  get cultureinput() {
    return this._entity.get('culture')
  }
  /**语言表单实体影子 */
  get culture_shadowInput() {
    return this._entity.get('culture_shadow')
  }

  /**extraProperties配置表单实体 */
  get extraProperties() {
    return this._entity.get('extraProperties') as FormGroup
  }
  async ngAfterContentInit(): Promise<void> {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    let queryParams = this.FromParentQueryParams
    if (this._entity && this.FromParentQueryParams) {
      this._entity.setControl('slug', new FormControl('', {
        validators: Validators.required,
        asyncValidators: this.repetitionAsyncValidator(),
        updateOn: 'blur'
      }))
      this._entity.setControl('culture_shadow', new FormControl('', {
        validators: [Validators.required],
        asyncValidators: this.cultureAsyncValidator_test(),
      }))
      this._entity.get('culture').disable();
      if (queryParams.RevisionEntryId && this.entrySelect) {
        this.RevisionEntryId = queryParams.RevisionEntryId
        this.setEntryconfig(this.entrySelect);
      } else {
        this.setEntryconfig(queryParams);
      }
      if (this.sectionId) await this.getSectionSelect();
      if (this.RevisionEntryId) await this.getAllVersionsList()

    }


  }

  /**表单控件模板-动态表单配置组件 */
  @ViewChildren('FormDynamicontrolRef', { read: ViewContainerRef }) FormDynamicontrolRef: QueryList<ViewContainerRef>;





  /**定义自定义异步验证 */
  cultureAsyncValidator_test() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subculture = this._entity?.get('culture').value
        if (subculture == this.entrySelect?.culture || this.SectionSelect.type !== 0) {
          resolve(null);
          return
        }
        this._EntryAdminService.cultureExistWithSingleSection({
          culture: subculture,
          sectionId: this.sectionId,
          entryTypeId: this.entryTypeId
        }).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::EntriesAlreadyExistEntryType`, this.entryTypesItem.displayName, this.languagesList.find(el => el.cultureName == subculture).displayName) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }


  /**  
 * 设置条目详情  
 * @param {object} source - 数据源对象  
 */
  setEntryconfig(source: any) {
    this.entryTypeId = source.entryTypeId;
    this.cultureName = source.culture || source.cultureName;
    this.sectionId = source.sectionId;
  }
  time: number = 0

  /**定义自定义异步验证 */
  repetitionAsyncValidator() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this._entity?.get('slug').value
        if (subslug == this.entrySelect?.slug) {
          resolve(null);
          return
        }
        this._EntryAdminService.slugExists({
          culture: this.cultureName,
          sectionId: this.sectionId,
          slug: subslug
        }).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::EntrySlug{0}AlreadyExist`, ctrl.value) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }



  /**获取版块详情 */
  getSectionSelect() {
    return new Promise((resolve, rejects) => {
      this._SectionAdminService.get(this.sectionId).subscribe(async (res) => {
        let languages = this.configState.getDeep('localization.languages')
        this.languagesList = languages.filter(el => res?.site?.languages.some(elr => elr.cultureName == el.cultureName))
        let entryTypesItem = res.entryTypes.find(el => el.id == this.entryTypeId)
        this.SectionSelect = res
        this.entryTypesItem = entryTypesItem
        this.entryTypesList = res.entryTypes.filter(el => el.id == this.entryTypeId)
        this._entity.patchValue({
          ...this.entrySelect,
          initialVersionId: this.entrySelect?.initialVersionId || this.RevisionEntryId || '',
          culture: this.cultureName,
          culture_shadow: this.cultureName,
          entryTypeId: this.entryTypeId,
          publishTime: this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss'),
        })
        resolve(true)
      })
    })
  }

  /**获取条目版本列表 */
  getAllVersionsList() {
    return new Promise((resolve, rejects) => {

      this._EntryAdminService.getAllVersions(this.RevisionEntryId).subscribe(res => {
        this.AllVersionsList = res.items
        resolve(res)
      })
    })
  }

  /**标题转化别名 */
  setTitleToSlugBlur(event) {
    let val = event.target.value
    let slug = this._entity.get('slug')
    let pinyinstr = ''
    if (slug.value) return
    pinyinstr = this._CmsApiService.chineseToPinyin(val)
    this._entity.patchValue({
      slug: pinyinstr || val
    })
  }
  /**使用Unicode编码范围判断是否是中文字符 */
  isChinese(str) {
    return /^[\u4e00-\u9fa5]+$/.test(str);
  }

  /**激活 */
  ActivatedVersion(VersionId) {
    this._EntryAdminService.activate(VersionId).subscribe((res) => {
      this.AllVersionsList.forEach(el => {
        el.isActivatedVersion = el.id === VersionId
      })
      return
    })
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
      this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
      this.getAllVersionsList()
    })
  }

}
