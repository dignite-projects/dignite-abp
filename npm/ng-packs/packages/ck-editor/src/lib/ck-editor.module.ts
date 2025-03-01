import { NgModule } from '@angular/core';

import { CkEditorRoutingModule } from './ck-editor-routing.module';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CkEditorConfigComponent, CkEditorControlComponent } from './dynamic-form';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [
    CkEditorControlComponent,
    CkEditorConfigComponent,
  ],
  imports: [
    CoreModule,
    ThemeSharedModule,
    CkEditorRoutingModule,
    CKEditorModule,
  ],

})
export class CkEditorModule { }
