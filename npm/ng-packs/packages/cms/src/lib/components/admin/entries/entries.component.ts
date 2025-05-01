/* eslint-disable no-constant-condition */
/* eslint-disable no-async-promise-executor */
/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import {
  ListService,
  LIST_QUERY_DEBOUNCE_TIME,
  ConfigStateService,
  LocalizationService,
  PagedResultDto,
  ABP,
} from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { finalize } from 'rxjs';
import { ECmsComponent } from '../../../enums';
import { UpdateListService } from '@dignite-ng/expand.core';
import {
  EntryAdminService,
  EntryDto,
  GetEntriesInput,
} from '../../../proxy/dignite/cms/admin/entries';
import { SectionAdminService } from '../../../proxy/dignite/cms/admin/sections';
import { entryStatusOptions } from '../../../proxy/dignite/cms/entries';
import { RegionalizationService } from '../../../proxy/dignite/abp/regionalization-management';
import { SectionType } from '../../../proxy/dignite/cms/sections';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { FormAdminService } from '../../../proxy/dignite/cms/admin/dynamic-forms';
import { FieldAdminService } from '../../../proxy/dignite/cms/admin/fields';
import { FormControlsService } from '../../../services/form-controls.service';

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
  ],
})
export class EntriesComponent implements OnInit {
  constructor(
    private _EntryAdminService: EntryAdminService,
    private _SectionAdminService: SectionAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private configState: ConfigStateService,
    private router: Router,
    private _LocalizationService: LocalizationService,
    private _FormAdminService: FormAdminService,
    private _FieldAdminService: FieldAdminService,
    private cdRef: ChangeDetectorRef,
    private _FormControlsService: FormControlsService,
  ) {}

  private fb = inject(FormBuilder);
  private _UpdateListService = inject(UpdateListService);
  public readonly list = inject(ListService<GetEntriesInput>);

  /**板块类型 */
  SectionType = SectionType;

  ngOnInit(): void {
    this.getPageDate();
    this._UpdateListService.updateListEvent.subscribe(() => {
      this.data.items = [];
      this.data.totalCount = 0;
      this.list.get();
    });
  }

  /**站点下的版块 */
  SiteOfSectionList: any[any] = [];

  /**版块下的语言列表 */
  sectionLanguagesList: any[any] = [];
  /**版块下的条目类型列表 */
  entryTypeList: any[any] = [];
  /**状态编码 */
  _entryStatusOptions = entryStatusOptions;

  /**获取页面数据 */
  async getPageDate() {
    // await this.getDynamicFormType();
    this.enableSearchTypeList = await this._FormControlsService.getEnableSearchTypeList();
    //  this.disableshowinTypeList= this._FormControlsService.getdisableshowinTypeList();
    await this.getSiteOfSectionList();
    await this.getSectionLanguagesList();
    this.hookToQuery();
  }
  /**需要查询的动态表单类型 */
  enableSearchTypeList: any[] = [];
  /**需要查询的动态表单字段 */
  enableSearchFieldList: any[] = [];
  /**不需要展示的动态表单类型 */
  disableshowinTypeList: any[] = [];
  /**需要展示的动态列表字段 */
  showinFieldList: any[] = [];
  /**获取动态表单类型 */
  // getDynamicFormType() {
  //   return new Promise((resolve, rejects) => {
  //     this._FormAdminService.getFormControls().subscribe((res:any) => {
  //       // this.dynamicFormTypeList = res.items;
  //       this.enableSearchTypeList = res.items.filter(el => el.enableSearch).map(el => el.name);
  //       resolve(res.items);
  //     });
  //   });
  // }

  /**切换板块 */
  async sectionIdChange() {
    this.getSectionOfEntryType();
    await this.getSectionLanguagesList();
    this.resetData();
  }
  /**切换语言 */
  async cultureChange() {
    this.resetData();
  }

  /**获取站点下的版块 */
  getSiteOfSectionList() {
    return new Promise((resolve, rejects) => {
      this._SectionAdminService
        .getList({
          maxResultCount: 1000,
        })
        .subscribe(async (res: any) => {

          this.SiteOfSectionList = res.items.filter(el => el.isActive );
          this.filters.sectionId = res.items[0]?.id || '';
          await this.getSectionOfEntryType();
          resolve(res.items);
        });
    });
  }
  /**表单 */
  enablegearchFormEntity: FormGroup | undefined = this.fb.group({
    extraProperties: this.fb.group({}),
  });

  get extraPropertiesInput() {
    return this.enablegearchFormEntity?.get('extraProperties') as FormGroup;
  }

  /**设置筛选条件 */
  setfiltersValue() {
    return new Promise((resolve, rejects) => {
      const extraProperties = this.extraPropertiesInput.value;
      const inputs: any[] = [];
      for (const key in extraProperties) {
        const element = extraProperties[key];
        if (Array.isArray(element) ? (element.length > 0 ? element : null) : element) {
          inputs.push({
            name: key,
            value: element === true ? 'True' : Array.isArray(element) ? element.join(',') : element,
          });
        }
        if (element === false) {
          inputs.push({
            name: key,
            value: 'False',
          });
        }
      }
      this.filters.queryingByFieldsJson = inputs.length > 0 ? JSON.stringify(inputs) : '';
      resolve(true);
    });
  }

  async listget() {
    await this.setfiltersValue();
    this.list.get();
    // this.filters.
  }

  /**站点下板块的类型 */
  SiteOfSectionType: any = SectionType.Single;

  /**获取版块下的条目类型 */
  async getSectionOfEntryType() {
    // let sectionId = this.filtersForm.get('sectionId').value;
    const sectionId = this.filters.sectionId;
    const SectionInfo: any = await this.getSectionInfo(sectionId);
    const _entryTypeList = SectionInfo?.entryTypes || [];

    // let _entryTypeList = this.SiteOfSectionList.find(el => el.id == sectionId)?.entryTypes || [];
    this.entryTypeList = _entryTypeList;
    //选择板块的类型SectionType
    this.SiteOfSectionType = this.SiteOfSectionList.find(el => el.id == sectionId)?.type;
    if (this.SiteOfSectionType == SectionType.Structure) {
      this.maxResultCount = 1000;
    } else {
      this.maxResultCount = 10;
    }
    // 初始化数组
    this.enableSearchFieldList = [];
    this.showinFieldList = [];
    // _entryTypeList.length === 1
    if (true) {
      // this.filters.entryTypeId = this.entryTypeList[0].id;

      // 使用for...of替代forEach来处理异步
      for (const el of _entryTypeList) {
        for (const el1 of el.fieldTabs) {
          for (const el2 of el1.fields) {
            // el2.field = await this.getDynamicFormEntity(el2.fieldId);
            if (el2.enableSearch && this.enableSearchTypeList.includes(el2.field.formControlName)) {
              this.enableSearchFieldList.push({
                ...el2,
                field: {
                  formControlName: el2.field.formControlName,
                  formConfiguration: el2.field.formConfiguration,
                  name: el2.field.name,
                },
              });
            }
            if (el2.showInList && !this.disableshowinTypeList.includes(el2.field.formControlName)) {
              this.showinFieldList.push(el2);
            }
          }
        }
      }
    }
  }
  async abpInitss() {
    await this.setfiltersValue();
  }
  /**获取板块详情 */
  getSectionInfo(ids) {
    return new Promise((resolve, rejects) => {
      this._SectionAdminService.get(ids).subscribe(res => {
        resolve(res);
      });
    });
  }
  /**获取字段详情 */
  getDynamicFormEntity(fieldId: string) {
    return new Promise((resolve, rejects) => {
      this._FieldAdminService.get(fieldId).subscribe(res => {
        resolve(res);
      });
    });
  }

  private config = inject(ConfigStateService);
  private _RegionalizationService = inject(RegionalizationService);
  /**获取版块下的语言列表 */
  getSectionLanguagesList() {
    return new Promise(async (resolve, rejects) => {
      //获取所有语言 */
      const languagesSystem = this.configState.getDeep('localization.languages');
      //获取系统默认语言 */
      let DefaultLanguage = this.config.getSetting('Abp.Regionalization.DefaultCultureName');
      const configCmsSiteLanguages = this.config.getSetting('Cms.Site.Languages');
      if (!configCmsSiteLanguages) {
        await this.getSiteSettingsLanguages();
      }
      if (this.defaultCultureName) {
        DefaultLanguage = this.defaultCultureName;
      }
      const LanguagesSite =
        (configCmsSiteLanguages ? configCmsSiteLanguages.split(',') : '') ||
        this.SiteSettingsAdminLanguages;
      const LanguagesArray = languagesSystem.filter(el => LanguagesSite.includes(el.cultureName));
      //获取当前选择的语言
      const nowculture = this.filters.culture;
      // let nowculture = this.filtersForm.get('culture').value;
      //当前选择的语言，是否在版块的的语言列表中
      const isexist = LanguagesArray.find(el => el.cultureName == nowculture);
      this.filters.culture = nowculture
        ? isexist
          ? nowculture
          : DefaultLanguage
        : DefaultLanguage;
      this.sectionLanguagesList = LanguagesArray;
      resolve(LanguagesArray);
    });
  }

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

  resetData() {
    this.filters = {
      sectionId: this.filters.sectionId,
      culture: this.filters.culture,
    } as GetEntriesInput;
    this.list.filter = '';
    this.list.maxResultCount = this.maxResultCount;
    this.list.page = 0;
    this.data.items = [];
    this.list.get();
  }

  /**新增-单个 */
  createEntriesBtn() {
    this.router.navigate(['/cms/admin/entries/create'], {
      queryParams: {
        cultureName: this.filters.culture,
        sectionId: this.filters.sectionId,
        entryTypeId: this.entryTypeList[0].id,
      },
    });
  }
  createCopyEntriesBtn() {
    this.router.navigate(['/cms/admin/entries/create-copy'], {
      queryParams: {
        cultureName: this.filters.culture,
        sectionId: this.filters.sectionId,
        entryTypeId: this.entryTypeList[0].id,
      },
    });
  }

  /**列表相关 */
  ColumnMode = ColumnMode;
  data: PagedResultDto<EntryDto> | any = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetEntriesInput;

  maxResultCount = 10;

  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this._EntryAdminService.getList({
        ...query,
        ...this.filters,
        maxResultCount: this.maxResultCount,
      });
    const setData = (list: PagedResultDto<EntryDto> | any) => {
      this.data.items = [];
      this.data.totalCount = 0;
      if (this.SiteOfSectionType == SectionType.Structure) {
        list.items = list.items.sort((a, b) => {
          return a.order - b.order;
        });
      }
      for (const element of list.items) {
        const sectionItem=this.SiteOfSectionList.find(el=>el.id==element.sectionId);
        element.sectionType=sectionItem.type;
      }
      this.data = list;
      this.scrollToTop();
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }
  scrollToTop() {
    const scrollContainer = document.getElementsByClassName('lpx-scroll-container')[0];
    (scrollContainer || window).scrollTo(0, 0);
  }

  /**删除条目 */
  deletefield(row: any) {
    this.confirmation
      .warn(row.displayName, this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`))
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._EntryAdminService
            .delete(row.id)
            .pipe(finalize(() => {}))
            .subscribe(res => {
              this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
              this.list.get();
            });
        }
      });
  }

  drop(event: any) {
    const previousId: any = this.data.items[event.previousIndex].id;
    const previousIndexOrder = this.data.items[event.previousIndex].order;
    const currentIndexOrder = this.data.items[event.currentIndex].order;
    let moveorder = currentIndexOrder;
    if (event.previousIndex == event.currentIndex) return
    if (previousIndexOrder < currentIndexOrder) {
      moveorder = currentIndexOrder + 1;
    }
    moveItemInArray(this.data.items, event.previousIndex, event.currentIndex);
    this._EntryAdminService
      .move(previousId, {
        order: moveorder,
        // order: event.currentIndex,
      })
      .pipe(
        finalize(() => {
          this.list.get();
        }),
      )
      .subscribe(
        res => {},
        err => {},
      );
    // moveItemInArray(this.rows, event.previousIndex, event.currentIndex);
    // this.rows = [...this.rows];
  }

  isexpanded: boolean | any = false;
  /**高级筛选切换 */
  expandedChange(event) {
    this.isexpanded = event;
  }
}
