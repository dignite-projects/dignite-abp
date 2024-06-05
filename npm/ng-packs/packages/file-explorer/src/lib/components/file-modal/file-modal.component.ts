import { Component, EventEmitter, Input, Output, SimpleChanges, inject } from '@angular/core';
import * as FileService from '../../proxy/dignite/file-explorer/files';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { PagedResultDto, ABP, ListService, Rest, RestService, LocalizationService, LIST_QUERY_DEBOUNCE_TIME } from '@abp/ng.core';
import { FileDescriptorDto, FileDescriptorService, GetFilesInput } from '../../proxy/dignite/file-explorer/files';
import { FileApiService } from '../../services/file-api.service';
import { SelectionType } from '@swimlane/ngx-datatable';
var that
@Component({
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
  ]
})
export class FileModalComponent {

  constructor(
    private _FileService: FileService.FileDescriptorService,
    private toaster: ToasterService,
    public readonly list: ListService,
    public _FileApiService: FileApiService,
    private restService: RestService,
    private confirmation: ConfirmationService,
    private _LocalizationService: LocalizationService,
  ) {
    that = this
    
  }

  private _FileDescriptorService=inject(FileDescriptorService)
  /**获取目录配置 */
  getFilesConfiguration(){
    this._FileDescriptorService.getFileContainerConfiguration(this._fileContainerName).subscribe(res=>{
      this.createDirectoryPermissionName=res.createDirectoryPermissionName
    })
  }
  /**目录的权限名称 */
  createDirectoryPermissionName:string=null

  /**图片容器 */
  _fileContainerName: string
  @Input()
  public set fileContainerName(v: string) {
    if (v) {
      this._fileContainerName = v;
      this.loadData()
     
    }
  }

  /**是否多选 */
  _multiple: boolean = false
  @Input()
  public set multiple(v: boolean) {
    this._multiple = v;
    if (v) { }
  }

  /**文件大小限制 
* @param 1mb
*/
  sizeLimit: number = 1048576
  @Input()
  public set limit(v: number) {
    this.sizeLimit = v;
  }
  /**父组件传递的模态框状态 */
  @Input()
  public set visible(v: boolean) {
    this.ModalOpen = v;
    if (v) {
      this.loadData()
    }
  }

  /**模态框状态回调 */
  @Output() visibleChange = new EventEmitter()



  /**模态框-状态-是否打开 */
  ModalOpen: boolean = false

  /**模态框-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalBusy: boolean = false



  /**模态框-状态改变回调 */
  ModalVisibleChange(event) {
    if (!event) {
      this.visibleChange.emit(event)
      return
    }
  }

  /**模态框保存 */
  modalSave() {
    let selectedTablearr = this._FileApiService.deepClone(this.selectedTable)
    this.selectFilefn.emit(selectedTablearr)

    this.ModalVisibleChange(false)
  }
  /**dignite-file-modal-tree */
  /**选择的tree节点 */
  _theSelectedTreeNode: any = ''

  /**初始化数据 */
  loadData() {
    if (this.ModalOpen && this._fileContainerName) {
      this.hookToQuery()
      this.getFilesConfiguration()
    }
  }

  /**tree-节点选择 */
  _nodeClick(event) {
    this._theSelectedTreeNode = event
    this.list.get()
  }

  /**图片上传-要上传图片的状态文件列表 */
  uploadPictureStatusList: any[] = []

  /**图片上传-获取文件信息改变 */
  async getFileChange(event) {
    let files = new Array(...event.target.files)
    this.uploadPictureStatusList = files
    for (const file of files) {
      if (file.size > this.sizeLimit) {
        this.setuploadPictureStatus(file, 2)
        continue;
      }
      await this.uploadingFile(file).then(() => {
        this.setuploadPictureStatus(file, 1)
        this.list.get()
      }).catch(() => {
        this.setuploadPictureStatus(file, 2)
      }); // 等待每个文件上传完成  
    }
    this.list.get()
    let isSubmit = !this.uploadPictureStatusList.some(el => el.status == 2);
    if (isSubmit) {
      // this.toaster.success("上传完成");
      setTimeout(() => {
        this.uploadPictureStatusList = []
      }, 6000)
    }
  }

  /**图片上传-设置uploadPictureStatusList的状态 */
  setuploadPictureStatus(file, type) {
    this.uploadPictureStatusList.map(el => {
      if (el == file) el.status = type
    })
  }


  /**图片上传-递归按顺序上传 */
  uploadingFile(file) {
    return new Promise((resolve, rejects) => {
      let formData = new FormData();
      formData.append('file', file, file.name);
      this.createFile({
        file: formData,
        containerName: this._fileContainerName,
        directoryId: this._theSelectedTreeNode?.key || '',
        entityId: ''
      }).subscribe(res => {
        resolve(true)
      }, (err) => {
        rejects(false)
      })
    })
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

  /**文件表格-选择的表格数据项 */
  selectedTable = []

  /**已选定的文件 */
  @Input() selectPickerFile: any[]

  ngOnChanges(changes: SimpleChanges): void {
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.
    this.selectedTable = this._FileApiService.deepClone(this.selectPickerFile)
  }
  /**当前选择的table项 id */
  nowSelectId: any = ''

  /**选择文件回调 */
  @Output() selectFilefn = new EventEmitter<any[]>()

  /**文件表格-获取表格数据 */
  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) => this._FileService.getList({
      ...query,
      ...this.filters,
      containerName: this._fileContainerName,
      directoryId: this._theSelectedTreeNode.key
    });
    const setData = (list: PagedResultDto<FileDescriptorDto>) => (this.data = list);
    this.list.hookToQuery(getData).subscribe(setData);
  }

  /**文件表格-查看所有分组的文件数据 */
  lookAllFile() {
    this._theSelectedTreeNode = ''
    this.list.get()
  }

  /**选择表格项 */
  onSelectTableItem({ selected }) {
    this.selectedTable = this._FileApiService.deepClone(selected)
    let selectedTablearr = this.removeDuplicatesById(this.selectedTable)
    if (selected.length > selectedTablearr.length) {
      selectedTablearr = selectedTablearr.filter(el => el.id !== this.nowSelectId)
    }
    this.selectedTable = selectedTablearr
  }

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

  /**一个布尔or函数，可用于检查是否要根据条件选择特定行。 */
  selectCheck = (row, column, value) => {
    this.nowSelectId = row.id
    return true;
  }

  /**判断row是否选中 */
  selectedcheckbox = (id) => {
    return this.selectedTable.some(el => el.id == id)
  }



  /**删除图片 */
  deleteFile(file) {

    this.confirmation
      .warn('', { key: '', defaultValue: this._LocalizationService.instant(`AbpUi::ItemWillBeDeletedMessage`) })
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._FileService.delete(file.id).subscribe(res => {
            this.toaster.success(this._LocalizationService.instant(`AbpUi::SuccessfullyDeleted`));
            this.list.get()
          })
        }
      });
  }

  /**关闭文件状态弹窗 */
  closeFileStatusModal() {
    this.uploadPictureStatusList = []
  }

  /**创建图片的接口，代理中的file类型不匹配，切换为any类型 */
  createFile = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileDescriptorDto>({
      method: 'POST',
      url: '/api/file-explorer/files',
      params: { containerName: input.containerName, cellName: input.cellName, directoryId: input.directoryId, entityId: input.entityId },
      body: input.file,
    },
      { apiName: 'FileExplorer', ...config });
}
