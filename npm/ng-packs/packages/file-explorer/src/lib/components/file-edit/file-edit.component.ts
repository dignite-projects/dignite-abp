import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FileApiService } from '../../services/file-api.service';

@Component({
  selector: 'fe-file-edit',
  templateUrl: './file-edit.component.html',
  styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent {
  constructor(
    public _FileApiService: FileApiService,
  ) { }

  /**是否多选 */
  _multiple: boolean = true
  @Input()
  public set multiple(v: boolean) {
    this._multiple = v;
    if (v) { }
  }
  /**文件数据--已上传的数据 */
  _fileData: any[] = []
  @Input()
  public set fileData(v: any[]) {
    this._fileData = v;
    if (v.length > 0) {
      this.getFileChange({ target: { files: v } })
    }
  }
  /** 跟随表单提交--已提交的数据，或选择的数据源--回调*/
  @Output() fileDataChange = new EventEmitter()

  /**文件大小限制 
   * @param 1mb
  */
  sizeLimit: number = 1048576
  @Input()
  public set limit(v: number) {
    this.sizeLimit = v;
  }

  /**文件表格数据 */
  filesTableData: any[] = []
  /** 待删除已上传的文件们*/
  deleteTheUploadedFiles: any[] = []

  /**获取文件选择框的元素 */
  @ViewChild('fileEdit', { static: true }) fileEdit: ElementRef;


  /**获取文件信息改变 */
  async getFileChange(event) {
    let files = new Array(...event.target.files)
    /**需要等待setfileSizeUnits执行完后在执行其他方法--需要完善 */
    await this.waitFileToAddTable(files)
    this.fileHandling()
  }

  /**等待将文件数据加入到文件表格数据中 */
  waitFileToAddTable(files) {
    return new Promise(async (resolve, rejects) => {
      this.filesTableData.push(...await this.setfileSizeUnits(files))
      resolve(true)
    })
  }

  /**删除文件表格的项 */
  deleteFileTableItem(index, item) {
    this.filesTableData.splice(index, 1)
    if (item.id) {
      this.deleteTheUploadedFiles.push(item)
    }
    this.fileHandling()
  }

  /**文件处理-调用回调函数 */
  fileHandling() {
    let theFilesToBeUploaded = this.filesTableData.filter(el => !el.id)
    //判断图片大小是否超过限制-用于判断表单是否允许提交
    let isSubmit = !this.filesTableData.some(el => el.size > this.sizeLimit);
    this.fileDataChange.emit({ theFilesToBeUploaded, deleteTheUploadedFiles: this.deleteTheUploadedFiles, isSubmit })
  }

  /**设置值文件大小单位/ */
  async setfileSizeUnits(files: File[] | any[]): Promise<any> {
    return new Promise((resolve, rejects) => {
      let formattedFiles = []
      files.forEach(async (file) => {
        file.fileSize = this._FileApiService.formatFileSize(file.size)
        formattedFiles.push(file)
        //设置选择图片的本地url
        if (!file.src) file.src = await this._FileApiService.getImageLacolBase64Url(file);
      });
      resolve(files)
    })
  }

}
