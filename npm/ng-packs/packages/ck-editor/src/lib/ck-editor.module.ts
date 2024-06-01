import { NgModule } from '@angular/core';

import { CkEditorRoutingModule } from './ck-editor-routing.module';
import { CoreModule } from '@abp/ng.core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CkEditorConfigComponent, CkEditorControlComponent } from './dynamic-form';
import { CkEditorBaseComponent } from './components';



@NgModule({
  declarations: [
    CkEditorControlComponent,
    CkEditorConfigComponent,
    CkEditorBaseComponent
  ],
  imports: [
    FormsModule,
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
    CkEditorRoutingModule,
  ],
  exports: [
    // CkEditorControlComponent,
    // CkEditorConfigComponent,
    CkEditorBaseComponent
  ]
})
export class CkEditorModule { }
