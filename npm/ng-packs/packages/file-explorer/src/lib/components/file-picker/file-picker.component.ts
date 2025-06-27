/* eslint-disable @angular-eslint/component-selector */
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'fe-file-picker',
  templateUrl: './file-picker.component.html',
  styleUrls: ['./file-picker.component.scss']
})
export class FilePickerComponent implements OnChanges {

  /**是否多选 */
  _multiple = false
  @Input()
  public set multiple(v: boolean) {
    this._multiple = v;
  }

  /**图片容器 */
  _fileContainerName = 'Images'
  @Input()
  public set fileContainerName(v: string) {
    this._fileContainerName = v;
  }

  /**已选定的文件 */
  @Input() selectFormFile: any[]

  ngOnChanges(changes: SimpleChanges): void {
    const selectFormFilengOnChanges = changes.selectFormFile['currentValue']
    if (selectFormFilengOnChanges.length > 0) {
      this._fileShowTable = selectFormFilengOnChanges
    }
  }

  @Output() selectedFileChange = new EventEmitter()

  _fileShowTable: any[] = []

  /**表格选择的文件回调 */
  _selectFilefn(event: any[]) {
    const _fileShowTable = structuredClone(event)
    this._fileShowTable = _fileShowTable
    this.selectFormFile = _fileShowTable
    this.selectedFileChange.emit(_fileShowTable)
  }

  /**模态框-状态-是否打开 */
  ModalOpen= false

  /**删除文件表格项 */
  deleteFileTableItem(i, file) {
    this._fileShowTable.splice(i, 1)
    this.selectFormFile = this._fileShowTable
    this.selectedFileChange.emit([])
    this.selectedFileChange.emit(this.selectFormFile)
  }
  /**调整表格位置 */
    drop(event: any) {
      moveItemInArray(
        this._fileShowTable,
        event.previousIndex,
        event.currentIndex
      );
      this.selectFormFile = this._fileShowTable
      this.selectedFileChange.emit(this.selectFormFile)
      // this._fileShowTable.updateValueAndValidity()
    }
}
