import {  ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import { FileExplorerModule } from '@dignite-ng/expand.file-explorer';
import {
  TextEditConfigComponent,
  TextEditComponent,
  SwitchConfigComponent,
  SwitchControlComponent,
  NumbericEditConfigComponent,
  NumbericEditControlComponent,
  DateEditConfigComponent,
  DateEditControlComponent,
  FileExplorerConfigComponent,
  FileExplorerControlComponent,
  CkEditorConfigComponent,
  SelectConfigComponent,
  SelectControlComponent,
  AddFieldControlGroup,
  DynamicComponent,
} from './components';



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
    FileExplorerConfigComponent,
    FileExplorerControlComponent,
    CkEditorConfigComponent,
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
    NzTreeModule,
    FileExplorerModule,
    // DynamicComponent
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
    FileExplorerConfigComponent,
    FileExplorerControlComponent,
    CkEditorConfigComponent,
    SelectConfigComponent,
    SelectControlComponent,
    DynamicComponent,
  ],
})
export class DynamicFormModule {
  static forRoot(config?: any): ModuleWithProviders<DynamicFormModule> {
    AddFieldControlGroup(config.cmsFieldControlGroup)
    return {
      ngModule: DynamicFormModule,
      providers: []
    };
  }
}
