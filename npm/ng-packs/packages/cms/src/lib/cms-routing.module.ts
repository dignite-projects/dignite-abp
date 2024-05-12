import { Injectable, NgModule, inject } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  CreateComponent,
  CreateFieldComponent,
  CreateOrEditComponent,
  EditComponent,
  EditFieldComponent,
  EntriesComponent,
  FieldsComponent,
  SectionsComponent,
  SitesComponent,
} from './components';
import {
  AuthGuard,
  PermissionGuard,
} from '@abp/ng.core';
import { CmsExtensionsResolver } from './toolbar';
import { appentStyle } from './services/appent-content';


const routes: Routes = [
  {
    path: 'admin',
    canActivate: [AuthGuard, PermissionGuard],
    resolve: [CmsExtensionsResolver, appentStyle],
    children: [
      {
        path: 'entries',

        children: [
          {
            path: '',
            component: EntriesComponent,
            data: { keep: true }
          },
          {
            path: 'create',
            component: CreateComponent,
          },
          {
            path: ':entrieId/edit',
            component: EditComponent,
          },
        ],
      },
      {
        path: '',
        children: [
          {
            path: 'fields',
            children: [
              {
                path: '',
                component: FieldsComponent,
                data: { keep: true }
              },
              {
                path: 'create',
                component: CreateFieldComponent,
              },
              {
                path: ':id/edit',
                component: EditFieldComponent,
              },
            ],
          },
          {
            path: 'sites',
            component: SitesComponent,
            data: { keep: true }
          },
          {
            path: 'sections',
            children: [
              {
                path: '',
                data: { keep: true },
                component: SectionsComponent,
              },
              {
                path: ':sectionsId/entry-types',
                children: [
                  {
                    path: 'create',
                    component: CreateOrEditComponent,
                  },
                  {
                    path: ':entryTypesId/edit',
                    component: CreateOrEditComponent,
                  },
                ],
              },
            ],
          },
        ],
      },
    ],
  },
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CmsRoutingModule {

}
