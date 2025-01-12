import { NgModule } from '@angular/core';
import { RouterOutletComponent } from '@abp/ng.core';
import { Routes, RouterModule } from '@angular/router';
import { TenantDomainsComponent } from './components/multi-tenancy-domains.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        component: TenantDomainsComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TenantDomainsRoutingModule {}
