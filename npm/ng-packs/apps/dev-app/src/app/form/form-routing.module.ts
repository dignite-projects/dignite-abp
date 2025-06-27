import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FieldsComponent } from './components/fields/fields.component';

const routes: Routes = [
  {
    path: 'form',
    children:[{
      path:'fields',
      component: FieldsComponent
    }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FormRoutingModule { }
