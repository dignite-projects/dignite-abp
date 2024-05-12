import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'test_Item.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'test_Item.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
