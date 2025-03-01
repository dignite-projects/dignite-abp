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
