import { AfterContentInit, Component, Input } from '@angular/core';

@Component({
  selector: 'app-dynamic-field',
  templateUrl: './dynamic-field.component.html',
  styleUrl: './dynamic-field.component.scss'
})
export class DynamicFieldComponent implements AfterContentInit {
  ngAfterContentInit(): void {
    console.log(this.type,this.form);
  }

  /**字段类型 */
  @Input() type: any;
  /**表单 */
  @Input() form: any;
}
