import { ABP, LIST_QUERY_DEBOUNCE_TIME, ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.theme.shared/extensions';
import { Component, inject } from '@angular/core';
import { DynamicComponent } from '../../enums';
import { ToasterService, ConfirmationService } from '@abp/ng.theme.shared';
import { Router } from '@angular/router';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { ApiService } from '../../services';
import { FieldDataService } from '../../services/field-data.service';

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrl: './fields.component.scss',
  providers: [
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: DynamicComponent.Fields,
    },
  ]
})
export class FieldsComponent {

  constructor(
    // public readonly list: ListService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private _LocalizationService: LocalizationService,
    // private router: Router,
    // private _ApiService: ApiService,
  ) {
    
  }
  private _FieldDataService=inject(FieldDataService)
  public readonly list=inject(ListService)
  private router=inject(Router)
  private _ApiService=inject(ApiService)
  /**表格单元格布局类型 */
  ColumnMode = ColumnMode;

  /**表格数据 */
  data: any = {
    items: [],
    totalCount: 0,
  };

  /**过滤器 */
  filters = {
    filter: '',
  };

  ngOnInit(): void {
    this.hookToQuery()
    // this._UpdateListService.updateListEvent.subscribe(() => {
    //   this.list.get()
    // });
  }
  getData(){
    this.list.get()
  }
  /**字段分组选择回调 */
  // fieldGroupChange(event) {
  //   this.filters.groupId = event
  //   this.list.page=0
  //   this.list.get()
  // }

  /**使用abp的list获取表格的字段数据列表 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) => this._FieldDataService.getFieldList({
      ...query,
      ...this.filters,
    });
    const setData = (list: PagedResultDto<any>) => {
      this.data = list
      console.log(list);
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }

  /**新建字段按钮 */
  toFieldsCreateBtn() {
    this.router.navigate(['/dynamic-form-test/fields/create'],
      {
        queryParams: {}
      }
    )
  }

  /**删除字段 */
  deletefield(row: any) {
    // this.confirmation.warn(
    //   row.displayName,
    //   this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`),
    // ).subscribe((status: Confirmation.Status) => {
    //   if (status == 'confirm') {
    //     this._FieldAdminService.delete(row.id).pipe(finalize(() => {
    //     })).subscribe(res => {
    //       this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
    //       this.list.get()
    //     })
    //   }
    // });
  }
}
