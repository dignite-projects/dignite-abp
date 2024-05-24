import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const dynamic_form_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];
function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: 'dynamic-form-test/fields',
        name: '字段列表',
        iconClass: 'fas fa fa-pencil-square-o',
        order: 3,
        layout: eLayoutType.application,
      },
    //   {
    //     path: '/dashboard',
    //     name: '::Menu:Dashboard',
    //     iconClass: 'fas fa-chart-line',
    //     order: 2,
    //     layout: eLayoutType.application,
    //     requiredPolicy: 'digbite_cms_ng.Dashboard.Host  || digbite_cms_ng.Dashboard.Tenant',
    //   },
    
    ]);
  };
}
