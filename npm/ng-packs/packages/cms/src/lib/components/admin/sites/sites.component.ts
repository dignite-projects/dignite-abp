import { ABP, ConfigStateService, LIST_QUERY_DEBOUNCE_TIME, ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, ViewChild, OnInit,  ElementRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { GetSitesInput, SiteDto, SiteAdminService } from '../../../proxy/admin/sites';
import { ColumnMode } from "@swimlane/ngx-datatable";
import { finalize } from 'rxjs/operators';
import { CreateOrUpdateSitesInputBase } from './create-or-update-sites-input-base';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums';
import { CmsApiService } from '../../../services/cms-api.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'cms-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.scss'],
  providers: [
      ListService,
      { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Sites,
    },
  ]
})
export class SitesComponent implements OnInit {
  constructor(
    public readonly list: ListService,
    private _SiteAdminService: SiteAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private fb: FormBuilder,
    private configState: ConfigStateService,
    private _LocalizationService: LocalizationService,
    private _CmsApiService: CmsApiService,
  ) {}
  /**创建站点模态框状态 */
  createSitesOpen: boolean = false
  /**用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean = false
  /**创建站点表单 */
  createOrEditForm: FormGroup | undefined;
  get languagesInput() {
    return this.createOrEditForm.get('languages').value
  }
  /**语言列表 */
  languages: any[]
  /**站点给定的表单值 */
  selected: any
  /**表单控件模板-动态赋值表单控件 */
  @ViewChild('createModalSubmit', { static: false }) createModalSubmit: ElementRef;
  ColumnMode = ColumnMode;
  data: PagedResultDto<SiteDto> = {
    items: [],
    totalCount: 0,
  };
  filters = {} as GetSitesInput;
  ngOnInit(): void {
    this.hookToQuery()
  }
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) => this._SiteAdminService.getList({
      ...query,
      ...this.filters,
    });
    const setData = (list: PagedResultDto<SiteDto>) => (this.data = list);
    this.list.hookToQuery(getData).subscribe(setData);
  }
  /**删除字段 */
  deletefield(row: any) {
    this.confirmation.warn(
      row.displayName,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._SiteAdminService.delete(row.id).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
          this.list.get()
        })
      }
    });
  }
  /**创建站点模态框状态改变 */
  createGroupVisibleChange(event) {
    if (!event) {
      this.selected = ''
      return
    }
  }
  /**选择语言改变 */
  languagesChange(event) {
    let checked = event.target.checked
    let value = event.target.value
    this.languages.forEach(el => {
      el.ischecked = el.cultureName === value ? checked : el.ischecked;
    })
    this.setFromLangguages()
  }
  /**设置默认语言 */
  setDefault(items) {
    this.languages.forEach(el => {
      el.isDefault = el.cultureName === items.cultureName;
    })
    this.setFromLangguages()
  }
  /**设置表单中的语言 */
  setFromLangguages() {
    let languagesSelect = this.languages.filter(el => el.ischecked == true)
    this.createOrEditForm.patchValue({
      languages: languagesSelect
    })
    if (languagesSelect.length == 0) {
      this.languages.forEach(el => {
        el.isDefault = false
      })
    }
  }
  /**表单保存提交 */
  createOrEditSave() {
    if (!this.createOrEditForm.valid) return
    if (this.selected) {
      return this.EditSave()
    }
    this.createSave()
  }
  /**创建站点保存 */
  createSave() {
    let input = this.createOrEditForm.value
    this.modalBusy = true
    if (!this.createOrEditForm.valid) return
    this._SiteAdminService.create(input).pipe(finalize(() => {
      this.modalBusy = false
      this.createSitesOpen = false
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.list.get()
    })
  }
  /**编辑站点保存 */
  EditSave() {
    let input = this.createOrEditForm.value
    this.modalBusy = true
    this._SiteAdminService.update(this.selected.id, input).pipe(finalize(() => {
      this.modalBusy = false
      this.createSitesOpen = false
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.list.get()
    })
  }
  /**创建站点按钮 */
  createSitesBtn() {
    this.createSitesOpen = true
    this.createOrEditForm = this.fb.group(new CreateOrUpdateSitesInputBase())
    this.setAsyncValidatorsFn()
    this.languages = this.configState.getDeep('localization.languages')
    this.languages.forEach((el, index) => {
      el.isDefault = index === 0
      el.ischecked = index === 0
    })
    this.setFromLangguages()
  }
  /**编辑站点按钮 */
  EditSitesBtn(rows) {
    this.createSitesOpen = true
    this.languages = this.configState.getDeep('localization.languages')
    this._SiteAdminService.get(rows.id).subscribe(res => {
      this.selected = rows
      this.createOrEditForm = this.fb.group(new CreateOrUpdateSitesInputBase())
      this.createOrEditForm.patchValue(rows)
      this.setAsyncValidatorsFn()
      // 更新语言和默认状态  
      this.updateLanguages(res.languages);
      // 设置表单语言  
      this.setFromLangguages()
    })
  }
  /**设置字段控件异步验证 */
  setAsyncValidatorsFn() {
    this.createOrEditForm.setControl('name', new FormControl(this.nameInput.value || '', {
      validators: Validators.required,
      asyncValidators: this.nameRepetitionAsyncValidator(),
      updateOn: 'blur'
    }))
  }
  /**更新语言和默认状态 */
  updateLanguages(siteLanguages) {
    this.languages.forEach(language => {
      const siteLanguage = siteLanguages.find(sl => sl.cultureName === language.cultureName);
      if (siteLanguage) {
        language.ischecked = true;
        language.isDefault = siteLanguage.isDefault;
      }
    });
  }
  get nameInput() {
    return this.createOrEditForm.get('name')
  }
  get hostInput() {
    return this.createOrEditForm.get('host')
  }
  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    let value = event.target.value
    let pinyin = this._CmsApiService.chineseToPinyin(value)
    let nameInput = this.nameInput
    if (nameInput.value) return
    nameInput.patchValue(pinyin)
  }
  /**name定义自定义异步验证 */
  nameRepetitionAsyncValidator() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.createOrEditForm?.get('name').value
        if (subslug == this.selected?.name) {
          resolve(null);
          return
        }
        this._SiteAdminService.nameExists(subslug).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::SiteName{0}AlreadyExist`, ctrl.value) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }
  /**host定义自定义异步验证 */
  hostRepetitionAsyncValidator() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.createOrEditForm?.get('host').value
        if (subslug == this.selected?.host || subslug == 'https://') {
          resolve(null);
          return
        }
        this._SiteAdminService.hostExists(subslug).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::SiteHost{0}AlreadyExist`, ctrl.value) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }
}
