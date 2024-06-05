import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateFieldComponent, EditFieldComponent, FieldViewComponent, FieldsComponent } from './component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { DynamicExtensionsResolver } from './toolbar';

const routes: Routes = [
  {
    path: 'fields',
    canActivate: [AuthGuard, PermissionGuard],
    resolve: [DynamicExtensionsResolver],
    children: [
      {
        path: '',
        component: FieldsComponent
      }, {
        path: 'create',
        component: CreateFieldComponent
      }, {
        path: ':id/edit',
        component: EditFieldComponent
      
      }, {
        path: ':id/view',
        component: FieldViewComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DynamicFormTestRoutingModule { }
