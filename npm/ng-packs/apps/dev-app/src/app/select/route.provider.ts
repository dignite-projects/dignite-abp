import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const Select_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/select',
        name: '选择',
        iconClass: 'bi bi-check2-circle',
        order: 1,
        layout: eLayoutType.application,
      },
    ]);
  };
}
