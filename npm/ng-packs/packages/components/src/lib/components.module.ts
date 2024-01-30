import { ModuleWithProviders, NgModule, forwardRef } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import { ModalComponent, PreviewComponent, PageHeaderComponent, ToastsComponent, TableComponent, PageComponent, TabsComponent } from './components';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DDatePipe } from './pipes';
import styles from './constants/styles';
import { TreeComponent } from './components/tree/tree.component';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import {
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { DigniteInitDirective } from './directive/dignite-init.directive';
import { TreeSelectComponent } from './components/tree-select/tree-select.component';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { NzInputModule } from 'ng-zorro-antd/input';
import { AddSelectInputComponent } from './components/add-select-input/add-select-input.component';



@NgModule({
  declarations: [
    PageHeaderComponent,
    ModalComponent,
    PreviewComponent,
    ToastsComponent,
    PageComponent,
    TableComponent,
    TabsComponent,
    DDatePipe,
    TreeComponent,
    DigniteInitDirective,
    TreeSelectComponent,
    AddSelectInputComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbToastModule,
    NgTemplateOutlet,
    NzTreeModule,
    NzTreeSelectModule,
    NgxDatatableModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    ReactiveFormsModule,
    NzInputModule,
  ],
  exports: [
    PageHeaderComponent,
    ModalComponent,
    PreviewComponent,
    ToastsComponent,
    PageComponent,
    TableComponent,
    TabsComponent,
    TreeComponent,
    DigniteInitDirective,
    TreeSelectComponent,
    AddSelectInputComponent,
    // DDatePipe
  ],

  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TreeSelectComponent),
      multi: true,
    },
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AddSelectInputComponent),
      multi: true,
    }
  ]

})

export class ComponentsModule {

  static forRoot(): ModuleWithProviders<ComponentsModule> {
    const stylesElement = document.createElement('style');
    var oText = document.createTextNode(styles);
    stylesElement.appendChild(oText)
    document.body.appendChild(stylesElement);
    return {
      ngModule: ComponentsModule,
      providers: [],
    };
  }
}
