import { Component, inject } from '@angular/core';
import { FileApiService } from '../../services/file-api.service';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'fe-file-dome',
  templateUrl: './file-dome.component.html',
  styleUrls: ['./file-dome.component.scss']
})
export class FileDomeComponent {

  /**跟随表单提交--已提交的数据，或选择的数据源 */
  fileSubmittedData: any[] = []

  /**跟随表单提交--待提交的数据
   * 
   * @param 待上传的文件们
   * @param 待删除已上传的文件们
   */
  fileDataToBeSubmitted: any

private FileApiService=inject(FileApiService)
  /**跟随表单提交--数据发生改变回调 */
  fileDataChange(event) {
    this.fileDataToBeSubmitted = event
  }

  /**选择文件-弹窗的-已选定的文件 */
  selectedFileGroup:any[]=[]

  /**_selectedFile改变回调 */
  _selectedFileChange(event) {
    this.selectedFileGroup = event
  }
}
