import {  NgModule} from '@angular/core';
import { Routes, RouterModule} from '@angular/router';
import {
  CreateComponent,
  CreateFieldComponent,
  CreateOrEditComponent,
  EditComponent,
  EditFieldComponent,
  EntriesComponent,
  FieldsComponent,
  SectionsComponent,
} from './components';
import {
  AuthGuard,
  PermissionGuard,
} from '@abp/ng.core';
// import { appentStyle } from './services/appent-content';
import { Extensions_Props_Action_Token_Resolver } from './resolvers/extensions-props-action-token.resolver';

const routes: Routes = [
  {
    path: 'admin',
    canActivate: [AuthGuard, PermissionGuard],
    resolve: [Extensions_Props_Action_Token_Resolver],
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
