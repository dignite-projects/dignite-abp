import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'digbite_cms_ng.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'digbite_cms_ng.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
