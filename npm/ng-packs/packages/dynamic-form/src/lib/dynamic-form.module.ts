import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
// import { NzTreeModule } from 'ng-zorro-antd/tree';
import {
  TextEditConfigComponent,
  TextEditComponent,
  SwitchConfigComponent,
  SwitchControlComponent,
  NumbericEditConfigComponent,
  NumbericEditControlComponent,
  DateEditConfigComponent,
  DateEditControlComponent,
  SelectConfigComponent,
  SelectControlComponent,
  AddFieldControlGroup,
  DynamicComponent,
} from './components';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { DynamicConfigComponent } from './components/dynamic/dynamic-config.component';
import { DynamicViewComponent } from './components/dynamic/dynamic-view.component';
import { DynamicControlComponent } from './components/dynamic/dynamic-control.component';
import { SelectViewComponent } from './components/form/select/select-view.component';
import { SwitchViewComponent } from './components/form/switch/switch-view.component';
import { TextEditViewComponent } from './components/form/text-edit/text-edit-view.component';
import { DateEditViewComponent } from './components/form/date-edit/date-edit-view.component';
import { NumericEditViewComponent } from './components/form/numeric-edit/numeric-edit-view.component';



@NgModule({
  declarations: [
    TextEditConfigComponent,
    TextEditComponent,
    SwitchConfigComponent,
    SwitchControlComponent,
    NumbericEditConfigComponent,
    NumbericEditControlComponent,
    DateEditConfigComponent,
    DateEditControlComponent,
    SelectConfigComponent,
    SelectControlComponent,
    DynamicComponent,
    DynamicConfigComponent,
    DynamicViewComponent,
    DynamicControlComponent,
    SelectViewComponent,
    SwitchViewComponent,
    TextEditViewComponent,
    DateEditViewComponent,
    NumericEditViewComponent,

  ],
  imports: [
    FormsModule,
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
    NgbDropdownModule,
      NzSelectModule,
    // NzTreeModule,
  ],
  exports: [
    TextEditConfigComponent,
    TextEditComponent,
    SwitchConfigComponent,
    SwitchControlComponent,
    NumbericEditConfigComponent,
    NumbericEditControlComponent,
    DateEditConfigComponent,
    DateEditControlComponent,
    SelectConfigComponent,
    SelectControlComponent,
    DynamicComponent,
    DynamicConfigComponent,
    DynamicViewComponent,
    DynamicControlComponent,
  ],
  providers: [
  ],
})
export class DynamicFormModule {
  static forRoot(config?: any): ModuleWithProviders<DynamicFormModule> {
    return {
      ngModule: DynamicFormModule,
      providers: [
      ]
    };
  }

}
