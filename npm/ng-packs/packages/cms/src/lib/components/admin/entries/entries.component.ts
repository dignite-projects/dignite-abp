import { ABP, ConfigStateService, LIST_QUERY_DEBOUNCE_TIME, ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EntryAdminService, EntryDto, GetEntriesInput } from '../../../proxy/admin/entries';
import { ColumnMode } from "@swimlane/ngx-datatable";
import { finalize } from 'rxjs/operators';
import { SiteAdminService } from '../../../proxy/admin/sites';
import { SectionAdminService } from '../../../proxy/admin/sections';
import { entryStatusOptions } from '../../../proxy/entries';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { ECmsComponent } from '../../../enums/ecms-component';
import { Router } from '@angular/router';
import { UpdateListService } from '../../../services/update-list.service';


@Component({
  selector: 'cms-entries',
  templateUrl: './entries.component.html',
  styleUrls: ['./entries.component.scss'],
  providers: [
    ListService,
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Entries,
    },
  ]
})
export class EntriesComponent implements OnInit {
  
  constructor(
    private _EntryAdminService: EntryAdminService,
    private _SiteAdminService: SiteAdminService,
    private _SectionAdminService: SectionAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private configState: ConfigStateService,
    private router: Router,
    private _LocalizationService: LocalizationService,
  ) {
  }

  private fb=inject(FormBuilder)
  private _UpdateListService=inject(UpdateListService)
  public readonly list=inject(ListService)


  ngOnInit(): void {
    this.getPageDate()
    this._UpdateListService.updateListEvent.subscribe(() => {
      this.list.get()
    });
  }

  /**站点列表 */
  siteList: any[any] = []
  /**选择的站点id */
  siteId: string = ''
  /**站点下的版块 */
  SiteOfSectionList: any[any] = []


  /**版块下的语言列表 */
  sectionLanguagesList: any[any] = []
  /**版块下的条目类型列表 */
  entryTypeList: any[any] = []
  /**状态编码 */
  _entryStatusOptions = entryStatusOptions

  /**获取页面数据 */
  async getPageDate() {
    await this.getsiteList()
    await this.getSiteOfSectionList()
    await this.getSectionLanguagesList()
    this.filters=this.filtersForm.value
    this.hookToQuery()
  }

  /**切换站点 */
  async siteIdChange() {
    await this.getSiteOfSectionList()
    await this.getSectionLanguagesList()
    this.resetData()
  }
  /**切换板块 */
  async sectionIdChange() {
    this.getSectionOfEntryType()
    await this.getSectionLanguagesList()
    this.resetData()
  }
  /**切换语言 */
  async cultureChange() {
    this.resetData()
  }
  /**获取站点列表 */
  getsiteList() {
    return new Promise((resolve, rejects) => {
      this._SiteAdminService.getList({}).subscribe(res => {
        this.siteList = res.items
        this.filtersForm.get('siteId').patchValue(res.items[0]?.id || '')
        resolve(res.items)
      })
    })
  }

  /**获取站点下的版块 */
  getSiteOfSectionList() {
    return new Promise((resolve, rejects) => {
      let siteId = this.filtersForm.get('siteId').value
      this._SectionAdminService.getList({
        maxResultCount: 1000,
        siteId: siteId
      }).subscribe(res => {
        this.SiteOfSectionList = res.items
        this.filtersForm.get('sectionId').patchValue(res.items[0]?.id || '')
        this.getSectionOfEntryType()
        resolve(res.items)
      })
    })
  }

  /**获取版块下的条目类型 */
  getSectionOfEntryType() {
    let sectionId = this.filtersForm.get('sectionId').value
    this.entryTypeList = this.SiteOfSectionList.find(el => el.id == sectionId)?.entryTypes || []
  }

  /**获取版块下的语言列表 */
  getSectionLanguagesList() {
    return new Promise((resolve, rejects) => {
  
      //获取所有语言 */
      let languages = this.configState.getDeep('localization.languages')
      //当前选择的板块id
      let sectionId = this.filtersForm.get('sectionId').value
      //板块下的语言
      let sectionOfLanguages = this.SiteOfSectionList.find(el => el.id == sectionId)?.site?.languages || []
      //获取系统语言 */
      let systemculture=this.configState.getDeep('localization.currentCulture.name')
      //获取当前选择的语言
      let nowculture=this.filtersForm.get('culture').value
      //当前选择的语言，是否在版块的的语言列表中
      let isexist=  sectionOfLanguages.find(el=>el.cultureName==nowculture)
      this.filtersForm.get('culture').patchValue(nowculture?isexist?nowculture:systemculture:systemculture)
      this.sectionLanguagesList = languages.filter(el => sectionOfLanguages.find(ell => ell.cultureName === el.cultureName))
      resolve(sectionOfLanguages)
    })
  }



    resetData() {
      this.filters=this.filtersForm.value
      this.list.page = 0
      this.data.items = []
      this.list.get()
    }

  /**新增-单个 */
  createEntriesBtn() {
    this.router.navigate(['/cms/admin/entries/create'], {
      queryParams: { cultureName: this.filters.culture, sectionId: this.filters.sectionId, entryTypeId: this.entryTypeList[0].id }
    })
  }

  /**列表相关 */
  ColumnMode = ColumnMode;
  data: PagedResultDto<EntryDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetEntriesInput;
  filtersForm: FormGroup = this.fb.group({
    siteId: [''],
    sectionId: [''],
    culture: [''],
    filter: [''],
  })

  hookToQuery() {
    
    const getData = (query: ABP.PageQueryParams) => this._EntryAdminService.getList({
      ...query,
      ...this.filters,
    });
    const setData = (list: PagedResultDto<EntryDto>) => {
      this.data = list
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }


  /**删除条目 */
  deletefield(row: any) {
    this.confirmation.warn(
      row.displayName,
      this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    ).subscribe((status: Confirmation.Status) => {
      if (status == 'confirm') {
        this._EntryAdminService.delete(row.id).pipe(finalize(() => {
        })).subscribe(res => {
          this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
          this.list.get()
        })
      }

    });
  }


}
