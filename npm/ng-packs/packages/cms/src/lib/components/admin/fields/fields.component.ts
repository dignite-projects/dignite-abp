/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { Component, OnInit} from '@angular/core';
import { ECmsComponent } from '../../../enums';
import { FieldsDataService } from '../../../services/fields-data.service';
import {
  ListService,
  LIST_QUERY_DEBOUNCE_TIME,
  LocalizationService,
  PagedResultDto,
  ABP,
} from '@abp/ng.core';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Router } from '@angular/router';
import { UpdateListService } from '@dignite-ng/expand.core';
import { FieldDto, GetFieldsInput } from '../../../proxy/dignite/cms/admin/fields';

@Component({
  selector: 'cms-fields',
  templateUrl: './fields.component.html',
  styleUrl: './fields.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: ECmsComponent.Fields,
    },
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 50 },
  ],
})
export class FieldsComponent implements OnInit {
  constructor(
    public readonly list: ListService,
    private _service: FieldsDataService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private _LocalizationService: LocalizationService,
    private router: Router,
    private _UpdateListService: UpdateListService,
  ) {}
  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    await Promise.all([this._service.getfieldGroups(), this._service.getControlsfieldTypes()]);
    this.hookToQuery();
   console.log(this.list,'1111');

    this._UpdateListService.updateListEvent.subscribe(() => {
      this.getData();
    });
  }


  /**表格数据 */
  data: PagedResultDto<FieldDto> = {
    items: [],
    totalCount: 0,
  };

  /**过滤器 */
  filters = {} as GetFieldsInput;

  /**使用abp的list获取表格的字段数据列表 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this._service.getFieldsList({
        ...query,
        ...this.filters,
      });
    const setData = (list: PagedResultDto<FieldDto>) => {
      this.data = list;
      this.scrollToTop();
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }
  /**获取表格数据 */
  getData() {
    this.data.items = [];
    this.data.totalCount = 0;
    // this.list.page=0;
    this.list.get();
  }
  /**重置 */
  reset() {
    this.filters = {} as GetFieldsInput;
    this.getData();
  }
 

  /**回到页面顶部 */
  scrollToTop() {
    const scrollContainer = document.getElementsByClassName('lpx-scroll-container')[0];
    (scrollContainer || window).scrollTo(0, 0);
  }

  /**点击字段分组回调 */
  onGroupClickBack(groupId: string) {
    this.filters.groupId = groupId;
    this.getData();
  }

  /**删除字段 */
  deletefield(row: any) {
    this.confirmation
      .warn(row.displayName, this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`))
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._service.deleteField(row.id).subscribe(() => {
            this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
            this.getData();
          });
        }
      });
  }

  /**新建字段按钮 */
  toFieldsCreateBtn() {
    this.router.navigate(['/cms/admin/fields/create'], {
      queryParams: {
        groupId: this.filters.groupId,
      },
    });
  }
  /**编辑字段按钮 */
  editfieldBtn(row: any) {
    this.router.navigate(['/cms/admin/fields/' + row.id + '/edit'], {});
  }
}
