/* eslint-disable @angular-eslint/component-selector */
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import {
  ABP,
  LIST_QUERY_DEBOUNCE_TIME,
  ListService,
  LocalizationService,
  PagedResultDto,
} from '@abp/ng.core';
import { Component,  OnInit,  inject } from '@angular/core';
import { ECmsComponent } from '../../../enums';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import {
  FormBuilder,
  FormGroup,
} from '@angular/forms';
import { UpdateListService } from '@dignite-ng/expand.core';

import { ColumnMode } from '@swimlane/ngx-datatable';
import { finalize } from 'rxjs';
import { CreateOrUpdateSectionsInputBase } from './create-or-update-sections-input-base';
import { EntryTypeAdminService, GetSectionsInput, SectionAdminService, SectionDto } from '../../../proxy/dignite/cms/admin/sections';
import { SectionType, sectionTypeOptions } from '../../../proxy/dignite/cms/sections';

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
  ],
})
export class SectionsComponent implements OnInit {
  constructor(
    public readonly list: ListService,
    private _SectionAdminService: SectionAdminService,
    private toaster: ToasterService,
    private confirmation: ConfirmationService,
    private fb: FormBuilder,
    public _EntryTypeAdminService: EntryTypeAdminService,
    public _LocalizationService: LocalizationService,
  ) {}
  private _UpdateListService = inject(UpdateListService);
  siteList: any[] = [];
  _SectionType = SectionType;
  _sectionTypeOptions = sectionTypeOptions;
  ColumnMode = ColumnMode;
  data: PagedResultDto<SectionDto> = { items: [], totalCount: 0 };
  filters = {} as GetSectionsInput;
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this._SectionAdminService.getList({ ...query, ...this.filters });
    const setData = (list: PagedResultDto<SectionDto>) => {
      this.data = list;
      this.scrollToTop();
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }
  scrollToTop() {
    const scrollContainer = document.getElementsByClassName('lpx-scroll-container')[0];
    (scrollContainer || window).scrollTo(0, 0);
  }
  async ngOnInit(): Promise<void> {
    this.hookToQuery();
    this._UpdateListService.updateListEvent.subscribe(() => {
      this.list.get();
    });
  }
  siteIdChange() {
    this.list.page = 0;
    this.list.get();
  }

  /**表单 */
  formEntity: FormGroup | undefined;
  /**弹窗状态 */
  isVisibleOpen: boolean|any = false;
  /**弹窗回调 */
  visibleChange(event) {
    this.isVisibleOpen = event;
    if (!event) {
      this.formEntity = undefined;
    }
  }
  /**创建 */
  createBtn() {
    this.formEntity = this.fb.group(new CreateOrUpdateSectionsInputBase());
    this.isVisibleOpen = true;
  }
  /**编辑 */
  editSectionBtn(item) {
    this.formEntity = this.fb.group(new CreateOrUpdateSectionsInputBase());
    this.formEntity.patchValue({
      ...item,
    });
    this.isVisibleOpen = true;
  }

  /**删除板块 */
  deletefield(row) {
    this.confirmation
      .warn(row.displayName, this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`))
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._SectionAdminService
            .delete(row.id)
            .pipe(finalize(() => {}))
            .subscribe(res => {
              this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
              this.list.get();
            });
        }
      });
  }
  /**删除条目类型 */
    deleteEntryType(row) {
    this.confirmation
      .warn(row.displayName, this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`))
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._EntryTypeAdminService
            .delete(row.id)
            .pipe(finalize(() => {}))
            .subscribe(res => {
              this.toaster.success(this._LocalizationService.instant(`AbpUi::DeletedSuccessfully`));
              this.list.get();
            });
        }
      });
  }

}
