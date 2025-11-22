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
    this.enableSearchTypeList = await this._FormControlsService.getEnableSearchTypeList();
    await this.getSiteOfSectionList();
    await this.getSectionLanguagesList();
    this.filters.sorting = 'creationTime desc';
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
          this.SiteOfSectionList = res.items.filter(el => el.isActive);
          // this.filters.sectionId = res.items[res.items.length-1]?.id || '';
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
      sorting: 'creationTime desc',
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
  copylistItem: any[] = [];
  shouldScrollToTop = true;

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
      for (const element of list.items) {
        const sectionItem = this.SiteOfSectionList.find(el => el.id == element.sectionId);
        element.sectionType = sectionItem.type;
      }
      if (this.SiteOfSectionType == SectionType.Structure) {
        this.copylistItem = list.items;
        list.items = this.buildTree(list.items);
      }

      this.data = list;
      if (this.shouldScrollToTop) {
        this.scrollToTop();
      }
      this.shouldScrollToTop = true;
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }
  scrollToTop() {
    const scrollContainer = document.getElementsByClassName('lpx-scroll-container')[0];
    (scrollContainer || window).scrollTo(0, 0);
  }

  /**判断某个类型的条目是否存在 */
  isEntryTypeExist(entryTypeId: string) {
    return this.data?.items?.some(el => el.entryTypeId == entryTypeId);
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

  /** 当前拖拽目标节点的ID */
  dropTargetId: string | null = null;
  /** 拖拽位置：above-上方, below-下方, inside-内部 */
  dropPosition: 'above' | 'below' | 'inside' | null = null;
  /** 当前被拖拽的节点 */
  draggedItem: any = null;
  /** 是否正在拖拽 */
  isDragging: boolean = false;
  /** 是否为无效拖拽目标（拖拽到自身或子节点） */
  isInvalidDropTarget: boolean = false;

  /**
   * CDK拖拽开始事件
   */
  onDragStarted(event: any) {
    this.isDragging = true;
    this.draggedItem = event.source.data?.item;
  }

  /**
   * CDK拖拽结束事件
   */
  onDragEnded(event: any) {
    // 不立即清除状态,让drop方法先执行
    setTimeout(() => {
      this.isDragging = false;
      this.draggedItem = null;
      this.isInvalidDropTarget = false;
    }, 100);
  }

  /**
   * CDK拖拽进入事件
   */
  onDragEntered(event: any, targetItem: any) {
    if (!this.isDragging) return;
    this.dropTargetId = targetItem.id;
  }

  /**
   * 鼠标移动事件,判断位置
   */
  onMouseMove(event: MouseEvent, targetItem: any) {
    if (!this.isDragging || !this.draggedItem) return;
    
    const target = (event.currentTarget as HTMLElement).closest('tr');
    if (!target) return;
    
    this.dropTargetId = targetItem.id;
    
    const rect = target.getBoundingClientRect();
    const y = event.clientY - rect.top;
    const height = rect.height;
    
    if (y < height * 0.25) {
      this.dropPosition = 'above';
    } else if (y > height * 0.75) {
      this.dropPosition = 'below';
    } else {
      this.dropPosition = 'inside';
    }
    
    // 检查是否为无效拖拽目标
    this.isInvalidDropTarget = this.isDescendantOrSelf(this.draggedItem.id, targetItem.id);
    // console.log('拖拽节点ID:', this.draggedItem.id, '目标节点ID:', targetItem.id, '是否无效:', this.isInvalidDropTarget);
  }

  /**
   * 拖拽离开事件处理
   * 清除拖拽目标和位置标识
   */
  onDragLeave(event: DragEvent) {
    // 不立即清除状态，让drop事件能够获取到dropTargetId和dropPosition
    // 状态会在drop事件中清除
  }

  /**
   * 拖拽释放事件处理
   * 根据拖拽位置调用后端API移动节点
   * @param event CDK拖拽事件
   */
  drop(event: any) {
    // 获取被拖拽的节点数据
    const draggedData = event.item.data;
    if (!draggedData) {
      console.log('无draggedData');
      this.clearDropState();
      return;
    }
    
    const draggedItem = draggedData.item;
    
    // 验证拖拽目标和位置是否有效
    if (!this.dropTargetId || !this.dropPosition) {
      console.log('无拖拽目标或位置');
      this.clearDropState();
      return;
    }
    
    // 在原始平铺数据中查找目标节点
    const targetItem = this.findItemById(this.copylistItem, this.dropTargetId);
    
    // 验证目标节点存在且不是自己或子节点
    if (!targetItem || this.isDescendantOrSelf(draggedItem.id, targetItem.id)) {
      this.toaster.warn(this._LocalizationService.instant('Cms::CannotDragToSelfOrDescendant'));
      this.clearDropState();
      return;
    }
  
    console.log('拖拽节点:', draggedItem);
    /* 
     拖拽位置：above-上方, below-下方, inside-内部 
      dropPosition: 'above' | 'below' | 'inside' | null = null;
    */
    console.log('目标节点:', targetItem);
    console.log('位置:', this.dropPosition);
    
    // 构建移动参数
    const moveParams: any = {};
    
    // 如果是放置到内部，设置parentId为目标节点ID
    if (this.dropPosition === 'inside') {
      moveParams.parentId = targetItem.id;
    } 
    // 如果是放置在上方或下方，使用目标节点的parentId和order
    else {
      moveParams.parentId = targetItem.parentId;
      // 上方使用目标节点的order，下方使用order+1
      moveParams.order = this.dropPosition === 'above' ? targetItem.order : targetItem.order + 1;
    }

    console.log('移动参数:', moveParams);

    // 立即更新本地数据
    this.updateLocalData(draggedItem, moveParams);
    this.clearDropState();
    // 调用后端API
    this._EntryAdminService
      .move(draggedItem.id, moveParams)
      .subscribe(
        () => {
          this.toaster.success(this._LocalizationService.instant('AbpUi::SavedSuccessfully'));
          this.shouldScrollToTop = false;
          this.list.get();
        },
        () => {
          this.shouldScrollToTop = false;
          this.list.get();
        }
      );

  }

  /**
   * 清除拖拽状态
   */
  private clearDropState() {
    this.dropTargetId = null;
    this.dropPosition = null;
    this.isDragging = false;
    this.draggedItem = null;
    this.isInvalidDropTarget = false;
  }

  /**
   * 检查目标节点是否是拖拽节点的后代或自身
   */
  private isDescendantOrSelf(draggedId: string, targetId: string): boolean {
    if (draggedId === targetId) {
      // console.log('目标是自身');
      return true;
    }
    const result = this.isDescendant(draggedId, targetId);
    // console.log('目标是子节点:', result);
    return result;
  }

  /**
   * 递归检查目标节点是否是拖拽节点的子节点
   */
  private isDescendant(parentId: string, childId: string): boolean {
    const parent = this.findItemById(this.data.items, parentId);
    if (!parent) return false;
    
    const checkChildren = (node: any): boolean => {
      if (!node.children || node.children.length === 0) return false;
      for (const child of node.children) {
        if (child.id === childId) return true;
        if (checkChildren(child)) return true;
      }
      return false;
    };
    
    return checkChildren(parent);
  }

  /**
   * 在树形结构或平铺数组中递归查找指定ID的节点
   * @param items 节点数组
   * @param id 要查找的节点ID
   * @returns 找到的节点或null
   */
  findItemById(items: any[], id: string): any {
    for (const item of items) {
      if (item.id === id) return item;
      // 如果有子节点，递归查找
      if (item.children) {
        const found = this.findItemById(item.children, id);
        if (found) return found;
      }
    }
    return null;
  }

  /**
   * 更新本地数据
   */
  private updateLocalData(draggedItem: any, moveParams: any) {
    const flatItem = this.copylistItem.find(item => item.id === draggedItem.id);
    if (!flatItem) return;

    flatItem.parentId = moveParams.parentId;
    if (moveParams.order !== undefined) {
      flatItem.order = moveParams.order;
    } else {
      // 拖拽到内部时，设置为0（子节点开头）
      flatItem.order = 0;
    }

    this.data.items = this.buildTree(this.copylistItem);
  }

  isexpanded: boolean | any = false;
  /**高级筛选切换 */
  expandedChange(event) {
    this.isexpanded = event;
  }

  /**将平铺数据转换为树形结构并排序 */
  buildTree(items: any[]): any[] {
    const map = new Map();
    const roots: any[] = [];

    items.forEach(item => {
      map.set(item.id, { ...item, children: [] });
    });

    items.forEach(item => {
      const node = map.get(item.id);
      if (item.parentId === null || item.parentId === undefined) {
        roots.push(node);
      } else {
        const parent = map.get(item.parentId);
        if (parent) {
          parent.children.push(node);
        }
      }
    });

    const sortByOrder = (nodes: any[]) => {
      nodes.sort((a, b) => a.order - b.order);
      nodes.forEach(node => {
        if (node.children && node.children.length > 0) {
          sortByOrder(node.children);
        }
      });
    };

    sortByOrder(roots);
    return roots;
  }
}
