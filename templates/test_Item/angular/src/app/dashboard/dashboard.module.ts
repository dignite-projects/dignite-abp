import { NgModule } from '@angular/core';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { AuditLoggingModule } from '@volo/abp.ng.audit-logging';
import { PageModule } from '@abp/ng.components/page';
import { SaasModule } from '@volo/abp.ng.saas';
import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { HostDashboardComponent } from './host-dashboard/host-dashboard.component';
import { TenantDashboardComponent } from './tenant-dashboard/tenant-dashboard.component';
import { DateRangePickerModule } from '@volo/abp.commercial.ng.ui';

@NgModule({
  declarations: [DashboardComponent, HostDashboardComponent, TenantDashboardComponent],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    NgbDatepickerModule,
    AuditLoggingModule,
    SaasModule,
    PageModule,
    DateRangePickerModule,
  ],
})
export class DashboardModule {}
