import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCkEditorRouteNames } from '../enums';

export const ck_editor_ROUTE_PROVIDERS = [
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
        path: '/ck-editor/dome',
        name:eCkEditorRouteNames.CkEditorDemo,
        iconClass: 'fas fa fa-file-archive-o',
        layout: eLayoutType.application,
        order: 9,
      },
    ]);
  };
}
