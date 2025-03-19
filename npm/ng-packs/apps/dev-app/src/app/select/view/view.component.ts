import { LazyLoadService, SubscriptionService, LOADING_STRATEGY } from '@abp/ng.core';
import { ChangeDetectorRef, Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Bold, ClassicEditor, Essentials, Italic, Paragraph } from 'ckeditor5';
import { concat } from 'rxjs';

declare let $: any;

const selectData1 = [
  { text: 'Alabama', id: 'AL' },
  { text: 'Alaska', id: 'AK' },
  { text: 'Arizona', id: 'AZ' },
  { text: 'Arkansas', id: 'AR' },
  { text: 'California', id: 'CA' },
  { text: 'Colorado', id: 'CO' },
];

const selectData2 = [
  { text: 'Apple', id: 'apple' },
  { text: 'Banana', id: 'banana' },
  { text: 'Cherry', id: 'cherry' },
  { text: 'Date', id: 'date' },
  { text: 'Elderberry', id: 'elderberry' },
  { text: 'Fig', id: 'fig' },
  { text: 'Grape', id: 'grape' },
];

/**无法使用表单事件，无法使用ngmodal,无法使用formControlName */
@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
})
export class ViewComponent {
  private lazyLoadService = inject(LazyLoadService);
  private cdr = inject(ChangeDetectorRef);
  /**需要加载的 */
  scriptsLoaded$: any = '';
  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.

    this.scriptsLoaded$ = concat(
      this.lazyLoadService.load(LOADING_STRATEGY.AppendAnonymousScriptToHead('ng-jQuery.min.js')),
      this.lazyLoadService.load(
        // LOADING_STRATEGY.PrependAnonymousStyleToHead('ng-select2.min.css'),
        LOADING_STRATEGY.AppendAnonymousStyleToHead('ng-select2.min.css'),
      ),
      this.lazyLoadService.load(LOADING_STRATEGY.AppendAnonymousScriptToHead('ng-select2.min.js')),
    );
    this.scriptsLoaded$.subscribe(res => {
      this.cdr.detectChanges();
      this.loadselect2();
    });
  }
  /**表单 */
  formentry: FormGroup | any = new FormGroup({
    select1: new FormControl(''),
    select2: new FormControl(''),
  });
  get select1Input() {
    return this.formentry.get('select1') as FormControl;
  }
  get select2Input() {
    return this.formentry.get('select2') as FormControl;
  }
  selected: any = {
    select1: ['AR'],
    select2: [],
  };
  /**数据 */
  _select1Data1 = selectData1;
  _select1Data2 = selectData2;

  @ViewChild('mySelect1', { static: false }) mySelect1: ElementRef;
  @ViewChild('mySelect2') mySelect2: ElementRef;

  loadselect2(): void {
    const $mySelect1 = $(this.mySelect1?.nativeElement);
    const $mySelect2 = $(this.mySelect2?.nativeElement);
    if ($mySelect1?.select2) {
      $mySelect1.select2({
        placeholder: '--选择单选--',
        allowClear: true,
        data: this._select1Data1,
      });
      $mySelect1.on('change.select1', () => {
        console.log('change.select1', $mySelect1.val());
        this.select1Input.setValue([$mySelect1.val()]);
      });
    }
    if ($mySelect2?.select2) {
      $mySelect2.select2({
        placeholder: '--选择多选--',
        allowClear: true,
      });
      $mySelect2.on('change.select2', () => {
        console.log('change.select2', $mySelect2.val());
        this.select2Input.setValue([...$mySelect2.val()]);
      });
    }
    this.setValues();
  }

  save() {
    console.log('event', this.formentry.value);
  }

  setValues() {
    if (this.selected) {
      this.formentry.patchValue(this.selected);
      if (this.selected.select1.length > 0) {
        const valueString = this.selected.select1[0];
        $(this.mySelect1?.nativeElement).val(valueString).trigger('change');
      }
      if (this.selected.select2.length > 0) {
        $(this.mySelect2?.nativeElement).val(this.selected.select2).trigger('change');
      }
    }
  }
  ngOnDestroy(): void {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.scriptsLoaded$ = null;
  }

  newckeditorForm: FormGroup | any = new FormGroup({
    ckeditor: new FormControl('1111111111'),
  });

 
  save111(){
    console.log('event',this.newckeditorForm.value);
    
  }
}
