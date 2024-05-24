import { ABP, ConfigStateService, LIST_QUERY_DEBOUNCE_TIME, ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation, } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild, inject, } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators, } from '@angular/forms';
import { EntryTypeAdminService, GetSectionsInput, SectionAdminService, SectionDto } from '../../../proxy/admin/sections';
import { SiteAdminService } from '../../../proxy/admin/sites';
import { ColumnMode } from "@swimlane/ngx-datatable";
import { finalize } from 'rxjs/operators';
import { CreateOrUpdateSectionsInputBase } from './create-or-update-sections-input-base';
import { SectionType, sectionTypeOptions } from '../../../proxy/sections';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums';
import { CmsApiService } from '../../../services/cms-api.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { UpdateListService } from '../../../services/update-list.service';

@Component({
  selector: 'cms-sections',
  templateUrl: './sections.component.html',
  styleUrls: ['./sections.component.scss'],
  providers: [
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Sections,
    },
  ]
})
export class SectionsComponent implements OnInit {
  constructor(
    public readonly list: ListService,
    private _SectionAdminService: SectionAdminService,
    private _SiteAdminService: SiteAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private fb: FormBuilder,
    private configState: ConfigStateService,
    public _EntryTypeAdminService: EntryTypeAdminService,
    public _LocalizationService: LocalizationService,
    public _CmsApiService: CmsApiService,
    private router: Router,
  ) {

  }
  private _UpdateListService=inject(UpdateListService)

  /**跳转编辑 */
  jumpSectionsEdit(row, item) {
    this.router.navigate([`/cms/admin/sections/${row.id}/entry-types/${item.id}/edit`], {
    })
  }
  /**跳转新建 */
  jumpSectionsCreate(row) {
    this.router.navigate([`/cms/admin/sections/${row.id}/entry-types/create`], {
    })
  }

  /**版块列表 */
  siteList: any[] = []
  /*** */
  ColumnMode = ColumnMode;
  data: PagedResultDto<SectionDto> = {
    items: [],
    totalCount: 0,
  };
  /** */
  filters = {} as GetSectionsInput;
  /**获取页面列表 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) => this._SectionAdminService.getList({
      ...query,
      ...this.filters,
    });

    const setData = (list: PagedResultDto<SectionDto>) => (this.data = list);
    this.list.hookToQuery(getData).subscribe(setData);
  }

  async ngOnInit(): Promise<void> {
    await this.getSiteList()
    this.hookToQuery()
    this._UpdateListService.updateListEvent.subscribe(() => {
      this.list.get()
    });
  }

  /**站点切换 */
  siteIdChange() {
    this.list.page = 0
    this.list.get()
  }

  /**获取版块列表 */
  getSiteList() {
    return new Promise((resolve, rejects) => {
      this._SiteAdminService.getList({}).subscribe(res => {
        this.siteList = res.items
        this.filters.siteId = res.items[0]?.id || ''
        resolve(true)
        rejects(false)

      })
    })

  }

  /**创建版块模态框状态 */
  visibleOpen: boolean = false

  /**用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean = false
  /**创建版块表单 */
  createOrEditForm: FormGroup | undefined;

  /**版块给定的表单值 */
  selected: any

  _SectionType = SectionType
  _sectionTypeOptions = sectionTypeOptions

  /**表单控件模板-动态赋值表单控件 */
  @ViewChild('createOrEditModalSubmitBtn', { static: false }) createOrEditModalSubmitBtn: ElementRef;

  /**创建版块模态框状态改变 */
  VisibleChange(event) {
    if (!event) {
      this.selected = ''
      return
    }
  }
  /**创建版块，打开模态框 */
  createSectionBtn() {
    this.visibleOpen = true
    this.createOrEditForm = this.fb.group(new CreateOrUpdateSectionsInputBase())
    this.setAsyncValidatorsFn()
  }
  /**编辑版块，打开模态框 */
  editSectionBtn(row) {
    this.visibleOpen = true
    this.createOrEditForm = this.fb.group(new CreateOrUpdateSectionsInputBase())
    this.setAsyncValidatorsFn()
    this._SectionAdminService.get(row.id).subscribe(res => {
      this.createOrEditForm.patchValue(res)
      this.selected = res
    })
  }
  /**删除某个条目类型 */
  deleteEntryType(row) {
    this.confirmation.warn(
      row.displayName,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._EntryTypeAdminService.delete(row.id).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
          this.list.get()
        })
      }
    });

  }

  /**表单保存提交 */
  createOrEditSave() {
    if (this.selected) {
      return this.EditSave()
    }
    this.createSave()
  }

  /** */
  createSave() {
    let input = this.createOrEditForm.value
    input.siteId = this.filters.siteId
    if (!this.createOrEditForm.valid) return
    this.modalBusy = true
    this._SectionAdminService.create(input).pipe(finalize(() => {
      this.modalBusy = false
      this.visibleOpen = false
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.list.get()
    })
  }
  /**创建版块保存 */
  EditSave() {
    let input = this.createOrEditForm.value
    input.siteId = this.filters.siteId
    input.concurrencyStamp = this.selected.concurrencyStamp
    if (!this.createOrEditForm.valid) return
    this.modalBusy = true
    this._SectionAdminService.update(this.selected.id, input).pipe(finalize(() => {
      this.modalBusy = false
      this.visibleOpen = false
    })).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
      this.list.get()
    })
  }
  /**删除版块 */
  deletefield(row) {
    this.confirmation.warn(
      row.displayName,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._SectionAdminService.delete(row.id).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
          this.list.get()
        })
      }
    });
  }

  /**name表单控件 */
  get displayNameInput() {
    return this.createOrEditForm.get('displayName')
  }
  /**name表单控件 */
  get nameInput() {
    return this.createOrEditForm.get('name')
  }
  /**route表单控件 */
  get routeInput() {
    return this.createOrEditForm.get('route')
  }
  /**route表单控件 */
  get templateInput() {
    return this.createOrEditForm.get('template')
  }
  /**route表单控件 */
  get typeInput() {
    return this.createOrEditForm.get('type')
  }

  radiochange() {
    this.routeInput.patchValue(this.routeInput.value)
  }


  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    let value = event.target.value
    let pinyin = this._CmsApiService.chineseToPinyin(value)
    let nameInput = this.nameInput
    let routeInput = this.routeInput
    let templateInput = this.templateInput
    if (nameInput.value) return
    nameInput.patchValue(pinyin)
    if (routeInput.value) return
    routeInput.patchValue(pinyin + (this.typeInput.value === 0 ? '' : '/{slug}'))
    if (templateInput.value) return
    templateInput.patchValue(pinyin + '/index')
  }



  /**设置字段控件异步验证 */
  setAsyncValidatorsFn() {
    this.createOrEditForm.setControl('name', new FormControl(this.nameInput.value || '', {
      validators: Validators.required,
      asyncValidators: this.nameRepetitionAsyncValidator(),
    }))
    this.createOrEditForm.setControl('route', new FormControl(this.routeInput.value || '', {
      validators: [Validators.required, this.forbiddenNameValidator()],
      asyncValidators: [this.routeRepetitionAsyncValidator()],
    }))
  }
  forbiddenNameValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let inputValue = control.value.toLocaleLowerCase()
      let forbidden = this.typeInput.value == 0 ? false : inputValue.includes('{slug}') ? false : true
      return forbidden ? { repetition: this._LocalizationService.instant(`Cms::RouteVerificationTips`, this._LocalizationService.instant(`Cms::Enum:SectionType:` + SectionType[this.typeInput.value]), '{slug}') } : null;
    };
  }

  /**定义异步验证方法 */
  nameRepetitionAsyncValidator() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.createOrEditForm?.get('name')
        if (subslug.value == this.selected?.name) {
          resolve(null);
          return
        }
        this._SectionAdminService.nameExists({
          siteId: this.filters.siteId,
          name: subslug.value
        }).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::SectionName{0}AlreadyExist`, ctrl.value) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }
  /**定义异步验证方法 */
  routeRepetitionAsyncValidator() {
    return (ctrl: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise(resolve => {
        let subslug = this.createOrEditForm?.get('route').value
        if (subslug == this.selected?.route) {
          resolve(null);
          return
        }
        this._SectionAdminService.routeExists({
          siteId: this.filters.siteId,
          route: subslug
        }).subscribe(res => {
          if (res) {
            resolve({ repetition: this._LocalizationService.instant(`Cms::SectionRoute{0}AlreadyExist`, ctrl.value) });
          } else {
            resolve(null);
          }
        })
      });
    };
  }


}
