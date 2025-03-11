import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SelectRoutingModule } from './select-routing.module';
import { ViewComponent } from './view/view.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [ViewComponent],
  imports: [
    CommonModule,
    SelectRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SelectModule { }
