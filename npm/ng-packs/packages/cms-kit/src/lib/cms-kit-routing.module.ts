import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuItemComponent } from './components/menu-item/menu-item.component';
import { Extensions_Props_Action_Token_Resolver } from './resolvers/extensions-props-action-token.resolver';
import { authGuard, permissionGuard } from '@abp/ng.core';
import { CommentsInfoComponent } from './components/comments/comments-info.component';
import { CommentsComponent } from './components/comments/comments.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [authGuard, permissionGuard],
    resolve: [Extensions_Props_Action_Token_Resolver],
    children: [
      {
        path: 'menus/items',
        children: [
          {
            path: '',
            component: MenuItemComponent,
          },
        ],
      },
      {
        path: 'comments',
        children: [
          {
            path: '',
            component: CommentsComponent,
            data: { keep: true },
          },
          {
            path: ':id',
            component: CommentsInfoComponent,
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
export class CmsKitRoutingModule {}
