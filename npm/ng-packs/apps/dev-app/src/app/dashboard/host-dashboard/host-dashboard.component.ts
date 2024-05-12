import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import {
  AverageExecutionDurationWidgetComponent,
  ErrorRateWidgetComponent,
} from '@volo/abp.ng.audit-logging';
import { EditionsUsageWidgetComponent, LatestTenantsWidgetComponent } from '@volo/abp.ng.saas';
import { FormBuilder } from '@angular/forms';
import { NgbDateAdapter, NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';

const now = new Date();
const oneMonthAgo = new Date(now.getFullYear(), now.getMonth() - 1, now.getDate());

@Component({
  selector: 'app-host-dashboard',
  templateUrl: './host-dashboard.component.html',
  styleUrls: ['./host-dashboard.component.scss'],
  providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class HostDashboardComponent implements AfterViewInit {
  fb = inject(FormBuilder);

  @ViewChild('errorRateWidget', { static: false })
  errorRateWidget: ErrorRateWidgetComponent;

  @ViewChild('averageExecutionDurationWidget', { static: false })
  averageExecutionDurationWidget: AverageExecutionDurationWidgetComponent;

  @ViewChild('editionsUsageWidget', { static: false })
  editionsUsageWidget: EditionsUsageWidgetComponent;

  @ViewChild('latestTenantsWidget', { static: false })
  latestTenantsWidget: LatestTenantsWidgetComponent;

  toDate = now
  fromDate = oneMonthAgo;

  formFilters = this.fb.group({
    times: [
      {
        fromDate: this.fromDate,
        toDate: this.toDate,
      },
    ],
  });

  ngAfterViewInit() {
    this.refresh();
  }

  refresh() {
    const {fromDate,toDate} = {
      ...this.formFilters.value.times,
    };
  
    const startDate = this.convertToString(fromDate);
    const endDate = this.convertToString(toDate);
    this.errorRateWidget?.draw({ startDate, endDate });
    this.averageExecutionDurationWidget?.draw({ startDate, endDate });
    this.editionsUsageWidget?.draw();
    this.latestTenantsWidget?.draw();
  }
  private convertToString(value: Date): string {
    return value.toLocalISOString();
  }
}
