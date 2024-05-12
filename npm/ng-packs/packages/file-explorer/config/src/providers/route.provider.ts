import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eFileRouteNames } from '../enums';

export const FILE_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService],
    multi: true,
  },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/file/file-dome',
        name:eFileRouteNames.FileUploadDemo,
        iconClass: 'fas fa fa-file-archive-o',
        layout: eLayoutType.application,
        order: 9,
      },
    ]);
  };
}
