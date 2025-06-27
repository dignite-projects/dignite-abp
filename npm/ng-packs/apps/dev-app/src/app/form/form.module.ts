import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormRoutingModule } from './form-routing.module';
import { FieldsComponent } from './components/fields/fields.component';
import { SharedModule } from '../shared/shared.module';
import { DynamicFieldComponent } from './components/fields/dynamic-field.component';


@NgModule({
  declarations: [
    FieldsComponent,
    DynamicFieldComponent
  ],
  imports: [
    CommonModule,
    FormRoutingModule,
    SharedModule
  ]
})
export class FormModule { }
