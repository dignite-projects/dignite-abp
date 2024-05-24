import { NgModule } from '@angular/core';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { FileExplorerRoutingModule } from './file-explorer-routing.module';
import { FileEditComponent, FilePickerComponent, FileModalComponent, FileModalTreeComponent, FileDomeComponent } from './components';
import { FilePreviewComponent } from './previews/file-preview.component';
import { FileExplorerConfigComponent } from './components/dynamic-form/file-explorer/file-explorer-config.component';
import { FileExplorerControlComponent } from './components/dynamic-form/file-explorer/file-explorer-control.component';
@NgModule({
  declarations: [
    FileEditComponent,
    FilePickerComponent,
    FileModalComponent,
    FileModalTreeComponent,
    FileDomeComponent,
    FilePreviewComponent,
    FileExplorerConfigComponent,
    FileExplorerControlComponent,
   
  ],
  imports: [
    FormsModule,
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
    NgbDropdownModule,
    NzTreeModule,
    NgxValidateCoreModule,
    FileExplorerRoutingModule,
  ],
  exports: [
    FileEditComponent,
    FilePickerComponent,
    FileModalComponent,
    FileModalTreeComponent,
    FileDomeComponent,
    FilePreviewComponent,
    // FileExplorerConfigComponent,
    // FileExplorerControlComponent,
  ],
  providers: [
    // // [Required]
    // ListService,
    // // [Optional]
    // // Provide this token if you want a different debounce time.
    // // Default is 300. Cannot be 0. Any value below 100 is not recommended.
    // { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
})
export class FileExplorerModule {

}
