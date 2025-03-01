import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCmsKitRouteName } from '../enums/route-names';

export const CMS_KIT_ROUTE_PROVIDERS = [
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
        path: '/cms',
        name: eCmsKitRouteName.CmsKit,
        order: 10,
        iconClass: 'bi-calendar2-minus-fill',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsKit.Comments||CmsKit.Menus',
      },
      {
        path: '/cms/comments',
        name: eCmsKitRouteName.Comments,
        parentName: eCmsKitRouteName.CmsKit,
        iconClass: 'bi bi-chat-square-text-fill',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsKit.Comments',
      },
      {
        path: '/cms/menus/items',
        name: eCmsKitRouteName.MenuItem,
        parentName: eCmsKitRouteName.CmsKit,
        iconClass: 'fa fa-stream',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsKit.Menus',
      },
    ]);
  };
}
