import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CmsRoutingModule } from './cms-routing.module';
import {
  CreateComponent,
  CreateFieldComponent,
  CreateOrEditComponent,
  CreateOrEditEntriesComponent,
  CreateOrEditFieldComponent,
  EditComponent,
  EditFieldComponent,
  EntriesComponent,
  FieldGroupComponent,
  FieldsComponent,
  SectionsComponent,
  MatrixConfigComponent,
  MatrixControlComponent,
  TableConfigComponent,
  TableControlComponent,
  EntryConfigComponent,
  EntryControlComponent,
  cmsFieldControlGroup
} from './components';
import {
  NgbDropdownModule,
  NgbNavModule,
  NgbAccordionModule,
} from '@ng-bootstrap/ng-bootstrap';
import { PageModule } from '@abp/ng.components/page';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
import { Toolbar_Action_Contributors, Entity_Action_Contributors, Entity_Props_Contributors } from './resolvers/extensions-props-action-token.resolver';
import { ExtensibleModule } from '@abp/ng.components/extensible';
import { CreateOrEditSectionsModalComponent } from './components/admin/sections/create-or-edit-sections-modal.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { EntryViewComponent } from './components/dynamic-form/entry/entry-view.component';
import { MatrixViewComponent } from './components/dynamic-form/matrix/matrix-view.component';
import { TableViewComponent } from './components/dynamic-form/table/table-view.component';
import { EntrySearchComponent } from './components/dynamic-form/entry/entry-search.component';
// import { AdvancedEntityFiltersModule, CommercialUiModule } from '@volo/abp.commercial.ng.ui';

@NgModule({
  declarations: [
    EntriesComponent,
    FieldsComponent,
    SectionsComponent,
    FieldGroupComponent,
    CreateFieldComponent,
    EditFieldComponent,
    CreateOrEditFieldComponent,
    CreateOrEditComponent,
    CreateComponent,
    EditComponent,
    CreateOrEditEntriesComponent,
    TableConfigComponent,
    TableControlComponent,
    MatrixConfigComponent,
    MatrixControlComponent,
    EntryConfigComponent,
    EntryControlComponent,
    EntryViewComponent,
    CreateOrEditSectionsModalComponent,
    MatrixViewComponent,
    TableViewComponent,
    EntrySearchComponent,
    
  ],
  imports: [
    CoreModule,
    ThemeSharedModule,
    CmsRoutingModule,
    NgbNavModule,
    NgbAccordionModule,
    NgbDropdownModule,
    PageModule,
    NzSelectModule,
    DynamicFormModule,
    ExtensibleModule,
    DragDropModule,
    NgxDatatableModule,
    // CommercialUiModule,
  ],
  exports: [
    TableConfigComponent,
    TableControlComponent,
    MatrixConfigComponent,
    MatrixControlComponent,
    EntryConfigComponent,
    EntryControlComponent,
    EntryViewComponent,
    MatrixViewComponent,
    TableViewComponent,
    EntrySearchComponent,
  ],
  providers: [
    
  ],

})
export class CmsModule {

  static forChild(options: any = {}): ModuleWithProviders<CmsModule> {
    return {
      ngModule: CmsModule,
      providers: [
        {
          provide: Toolbar_Action_Contributors,
          useValue: options.toolbarActionContributors,
        },
        {
          provide: Entity_Action_Contributors,
          useValue: options.entityActionContributors,
        },
        {
          provide: Entity_Props_Contributors,
          useValue: options.entityPropContributors,
        },
      ],
    };
  }
  static forLazy(options: any = {}): NgModuleFactory<CmsModule> {
    return new LazyModuleFactory(CmsModule.forChild(options));
  }
}
