import { Component, EventEmitter, Input, Output, SimpleChanges, inject, OnChanges } from '@angular/core';
import * as FileService from '../../proxy/dignite/file-explorer/files';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import {
  PagedResultDto,
  ABP,
  ListService,
  Rest,
  RestService,
  LocalizationService,
  LIST_QUERY_DEBOUNCE_TIME,
} from '@abp/ng.core';
import {
  FileDescriptorDto,
  FileDescriptorService,
  GetFilesInput,
} from '../../proxy/dignite/file-explorer/files';
import { FileApiService } from '../../services/file-api.service';
import { SelectionType } from '@swimlane/ngx-datatable';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'fe-file-modal',
  templateUrl: './file-modal.component.html',
  styleUrls: ['./file-modal.component.scss'],
  providers: [
    // [Required]
    ListService,
    // [Optional]
    // Provide this token if you want a different debounce time.
    // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
})
export class FileModalComponent implements OnChanges {
  constructor(
    private _FileService: FileService.FileDescriptorService,
    private toaster: ToasterService,
    public readonly list: ListService,
    public _FileApiService: FileApiService,
    private restService: RestService,
    private confirmation: ConfirmationService,
    private _LocalizationService: LocalizationService
  ) {
  }

  private _FileDescriptorService = inject(FileDescriptorService);
  /**获取目录配置 */
  getFilesConfiguration() {
    return new Promise((resolve, reject) => {
      this._FileDescriptorService
        .getFileContainerConfiguration(this._fileContainerName)
        .subscribe(res => {
          this.createDirectoryPermissionName = res?.createDirectoryPermissionName;
          resolve(res);
        });
    });
  }
  /**目录的权限名称 */
  createDirectoryPermissionName: string = null;

  /**图片容器 */
  _fileContainerName: string;
  @Input()
  public set fileContainerName(v: string) {
    if (v) {
      this._fileContainerName = v;
      // this.loadData()
    }
  }

  /**是否多选 */
  _multiple = false;
  @Input()
  public set multiple(v: boolean) {
    this._multiple = v;
  }

  /**文件大小限制
   * @param 1mb
   */
  sizeLimit = 1048576;
  @Input()
  public set limit(v: number) {
    this.sizeLimit = v;
  }
  /**父组件传递的模态框状态 */
  @Input()
  public set visible(v: boolean) {
    this.ModalOpen = v;
    if (v) {
      this.loadData();
    }
  }

  /**模态框状态回调 */
  @Output() visibleChange = new EventEmitter();

  /**模态框-状态-是否打开 */
  ModalOpen = false;

  /**模态框-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalBusy = false;

  /**模态框-状态改变回调 */
  ModalVisibleChange(event) {
    if (!event) {
      this.ModalOpen = false;
      this.ModalBusy = false;
      this.visibleChange.emit(event);
      this.createDirectoryPermissionName = '';
      this._theSelectedTreeNode = '';
      this.selectedTable = [];
      this.uploadPictureStatusList = [];
      return;
    }
  }

  /**模态框保存 */
  modalSave() {
    const selectedTablearr = this._FileApiService.deepClone(this.selectedTable);
    this.selectFilefn.emit(selectedTablearr);
    this.ModalVisibleChange(false);
  }
  /**dignite-file-modal-tree */
  /**选择的tree节点 */
  _theSelectedTreeNode: any = '';
  isCreateList = false;
  /**初始化数据 */
  loadData() {
    if (this.ModalOpen && this._fileContainerName) {
      this.list.maxResultCount = 100;
      // this.filters.skipCount = 0;
      this.getFilesConfiguration();
      if (!this.isCreateList) {
        this.hookToQuery();
        this.isCreateList = true;
      } else {
        this.list.get();
      }
    }
  }
  /** 从tree获取来的数据 */
  fileGroupList: any[] = [];
  /** 从tree获取数据 */
  treeNodeData(event) {
    this.fileGroupList = this.flattenNestedArray(event);
  }
  /**
   * 将嵌套数组扁平化
   * @param {Array} nestedArray - 包含嵌套children的数组
   * @returns {Array} - 扁平化后的数组
   */
  flattenNestedArray(nestedArray) {
    const result = [];

    function flatten(items) {
      if (!items) return;

      for (const item of items) {
        // 将当前项添加到结果数组
        result.push({ ...item });

        // 如果有children属性且是数组，递归处理
        if (item.children && Array.isArray(item.children)) {
          flatten(item.children);
        }
      }
    }

    flatten(nestedArray);
    return result;
  }

  /**tree-节点选择 */
  _nodeClick(event) {
    this.filters.skipCount = 0;
    this._theSelectedTreeNode = event;
    this.list.get();
  }

  /**图片上传-要上传图片的状态文件列表 */
  uploadPictureStatusList: any[] = [];

  /**图片上传-获取文件信息改变 */
  async getFileChange(event) {
    const files = new Array(...event.target.files);
    this.uploadPictureStatusList = files;
    for (const file of files) {
      if (file.size > this.sizeLimit) {
        this.setuploadPictureStatus(file, 2);
        continue;
      }
      await this.uploadingFile(file)
        .then(res => {
          this.selectedTable.push(res);
          this.setuploadPictureStatus(file, 1);
          // this.list.get()
        })
        .catch(() => {
          this.setuploadPictureStatus(file, 2);
        }); // 等待每个文件上传完成
    }
    this.list.get();
    const isSubmit = !this.uploadPictureStatusList.some(el => el.status == 2);
    if (isSubmit) {
      // this.toaster.success("上传完成");
      setTimeout(() => {
        this.uploadPictureStatusList = [];
      }, 4000);
    }
  }

  /**图片上传-设置uploadPictureStatusList的状态 */
  setuploadPictureStatus(file, type) {
    this.uploadPictureStatusList.map(el => {
      if (el == file) el.status = type;
    });
  }

  /**图片上传-递归按顺序上传 */
  uploadingFile(file) {
    return new Promise((resolve, rejects) => {
      const formData = new FormData();
      formData.append('file', file, file.name);
      this.createFile({
        file: formData,
        containerName: this._fileContainerName,
        directoryId: this._theSelectedTreeNode?.key || '',
        entityId: '',
      }).subscribe(
        res => {
          resolve(res);
        },
        err => {
          rejects(false);
        }
      );
    });
  }

  /**文件表格-数据*/
  data: PagedResultDto<FileDescriptorDto> = {
    items: [],
    totalCount: 0,
  };

  /**文件表格-条件*/
  filters = {} as GetFilesInput;

  /**文件表格-表格自带选择类型 */
  SelectionType = SelectionType;

  /**选择文件回调 */
  @Output() selectFilefn = new EventEmitter<any[]>();

  /**文件表格-获取表格数据 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this._FileService.getList({
        ...query,
        ...this.filters,
        containerName: this._fileContainerName,
        directoryId: this._theSelectedTreeNode.key,
      });
    const setData = (list: PagedResultDto<FileDescriptorDto>) => {
      this.data = list;
      this.onPageChange(list.items);
    };
    this.list.hookToQuery(getData).subscribe(setData);
  }

  /**文件表格-查看所有分组的文件数据 */
  lookAllFile() {
    this.filters.skipCount = 0;
    this._theSelectedTreeNode = '';
    this.list.get();
  }

  /**删除图片 */
  deleteFile(file) {
    this._FileService.delete(file.id).subscribe(res => {
      this.toaster.success(this._LocalizationService.instant(`FileExplorer::DeletedSuccessfully`));
      this.list.get();
    });
  }

  /**删除所有选中图片 */
  onDeleteAllSelectFile() {
    this.confirmation
      .warn('', {
        key: '',
        defaultValue: this._LocalizationService.instant(`FileExplorer::AreYouSure`),
      })
      .subscribe(async (status: Confirmation.Status) => {
        if (status == 'confirm') {
          const selectedTable = this.selectedTable;
          try {
            const result = await this.batchDeleteItems(selectedTable);
            if (result.success) {
              this.toaster.success(result.message);
              this.list.get();
              // 可能需要刷新表格或更新UI
            } else {
              //删除失败的项
              this.list.get();
              // 可以选择展示失败项或重试
            }
          } catch (error) {
            //批量删除过程中发生错误
          }
        }
      });
  }

  /**
   * 批量删除表格项
   * @param selectedTable 需要删除的表格项数组
   * @returns 包含成功状态和失败项的结果对象
   */
  async batchDeleteItems(selectedTable: any[]) {
    // 存储所有删除请求的Promise
    const deletePromises = selectedTable.map(item => {
      return new Promise((resolve, reject) => {
        this._FileService.delete(item.id).subscribe(
          () => {
            resolve(null);
          },
          () => {
            reject(item);
          }
        );
      });
    });

    // 等待所有请求完成
    const results = await Promise.allSettled(deletePromises);
    // 收集失败的项
    const failedItems: any[] = [];
    results.forEach(result => {
      if (result.status === 'rejected') {
        failedItems.push(result.reason);
      }
    });

    return {
      success: failedItems.length === 0,
      failedItems,
      message:
        failedItems.length === 0
          ? this._LocalizationService.instant(`FileExplorer::DeletedSuccessfully`)
          : `${failedItems.length}个项删除失败`,
    };
  }

  // 使用示例
  async handleBatchDelete() {}

  /**关闭文件状态弹窗 */
  closeFileStatusModal() {
    this.uploadPictureStatusList = [];
  }

  /**创建图片的接口，代理中的file类型不匹配，切换为any类型 */
  createFile = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/file-explorer/files',
        params: {
          containerName: input.containerName,
          cellName: input.cellName,
          directoryId: input.directoryId,
          entityId: input.entityId,
        },
        body: input.file,
      },
      { apiName: 'FileExplorer', ...config }
    );

  /**文件表格-选择的表格数据项 */
  selectedTable = [];
  /**当前选择的table项 id */
  nowSelectId: any = '';
  /**是否全选 */
  isAllSelected = false;

  /**已选定的文件 */
  @Input() selectPickerFile: any[];

  ngOnChanges(changes: SimpleChanges): void {
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.
    this.selectedTable = this._FileApiService.deepClone(this.selectPickerFile);
  }
  /**表格分页切换 */
  onPageChange(newArray) {
    this.isAllSelected = this.isAllSelectedFn(newArray, this.selectedTable);
  }
  /**行选择框改变 */
  onCheckboxChangeFn(event, row, array: any[]) {
    const { checked } = event.target;
    let selectedTableArray = [...this.selectedTable];
    if (this._multiple) {
      if (checked) {
        selectedTableArray.push(row);
      } else {
        selectedTableArray = selectedTableArray.filter(el => el.id != row.id);
      }
      this.isAllSelected = this.isAllSelectedFn(array, selectedTableArray);
    } else {
      selectedTableArray.length = 0;
      selectedTableArray = checked ? [row] : [];
    }
    this.selectedTable = this.removeDuplicatesById(selectedTableArray);
  }
  /**如果selectedTableArray不含array中的所有项，则将isAllSelected设为true,否则设为false */
  isAllSelectedFn(tolalArray: any[], selectedArray: any[] = []) {
    if(tolalArray.length == 0) return false;
    return tolalArray.every(item => selectedArray.some(el => el.id === item.id));
  }
  /**选择当前页全部 */
  onSelectAllFn(event: any, array: any[]) {
    let selectedTableArray = this.selectedTable;
    if (event.target.checked) {
      selectedTableArray = this.removeDuplicatesById([...selectedTableArray, ...array]);
    } else {
      selectedTableArray = selectedTableArray.filter(el => !array.some(item => item.id === el.id));
    }
    this.isAllSelected = event.target.checked;
    this.selectedTable = selectedTableArray;
  }

  /**判断row是否选中 */
  selectedcheckbox = id => {
    return this.selectedTable.some(el => el.id === id);
  };
  /**删除数组中重复的项 */
  removeDuplicatesById(array) {
    const seenIds = {};
    return array.filter(item => {
      if (!seenIds[item.id]) {
        seenIds[item.id] = true;
        return true;
      }
      return false;
    });
  }
  /**用于编辑的表单，同时只能显示编辑一个 */
  FileNameForm: FormGroup | any;
  /**当前编辑的row */
  newEditRow: any = '';
  /**是否正在加载 */
  isloading = false;
  /**提交FileName编辑 */
  onSubmitFileName(event) {
    const input = this.FileNameForm.value;
    if (!this.FileNameForm.valid) return;
    if (this.isloading) return;
    this.isloading = true;
    this._FileService
      .update(input.id, {
        name:input.fileName
      })
      .pipe(
        finalize(() => {
          this.isloading = false;
        })
      )
      .subscribe(res => {
        //通过当前newEditRow的id,修改data.items中对应项的name
        for (const element of this.data.items) {
          if (element.id == this.newEditRow.id) {
            element.name = input.fileName;
            break;
          }
        }

        this.FileNameForm = undefined;
        this.newEditRow = '';
        this.toaster.success(this._LocalizationService.instant(`FileExplorer::SavedSuccessfully`));
        // this.list.get();
      });
  }
  /**打开编辑 */
  onEditFileName(row) {
    this.FileNameForm = new FormGroup({
      fileName: new FormControl('', [Validators.required]),
      id: new FormControl('', [Validators.required]),
    });
    this.FileNameForm.patchValue({
      fileName: row.name,
      id: row.id,
    });
    this.newEditRow = row;
  }
  /**关闭编辑 */
  onCancelFileName(row) {
    this.newEditRow = '';
    this.FileNameForm = undefined;
  }
}
