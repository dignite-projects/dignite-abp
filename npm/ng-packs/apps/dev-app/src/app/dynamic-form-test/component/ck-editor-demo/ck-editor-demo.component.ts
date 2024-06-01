import { ConfigStateService } from '@abp/ng.core';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-ck-editor-demo',
  templateUrl: './ck-editor-demo.component.html',
  styleUrl: './ck-editor-demo.component.scss'
})
export class CkEditorDemoComponent {
  private config: ConfigStateService = inject(ConfigStateService)
  /**ck-Editor的值 */
  contentValue: any = '<p>aubznahsj</p>'
  /**系统语言 */
  currentCulture = this.config.getOne("localization")?.currentCulture?.name;
  /**ck-editor配置 */
  ckOptions: any = {
    language: this.currentCulture,
    // toolbar: {
    //   items: [
    //     'heading', '|',
    //     'bold', 'italic',
    //     'link', '|',
    //     'bulletedList', 'numberedList',
    //     'insertTable', '|',
    //     'uploadImage', '|',
    //     'undo', 'redo'
    //   ],
    //   viewportTopOffset: 30,
    //   shouldNotGroupWhenFull: true
    // },
    image: {
      toolbar: [
        'imageStyle:alignLeft', 'imageStyle:alignCenter', 'imageStyle:alignRight',
        '|',
        'resizeImage',
        '|',
        'imageTextAlternative'
      ],
      styles: [
        'alignLeft', 'alignCenter', 'alignRight'
      ],
      resizeOptions: [{
        name: 'resizeImage:original',
        label: 'Original',
        value: null
      },
      {
        name: 'resizeImage:25',
        label: '25%',
        value: '25'
      },
      {
        name: 'resizeImage:50',
        label: '50%',
        value: '50'
      },
      {
        name: 'resizeImage:75',
        label: '75%',
        value: '75'
      }
      ],
    },
  }
  /**富文本内容改变 */
  ckEditorChange(event) {
  }
  public onReady( editor: any ): void {
  }
}
