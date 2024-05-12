import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCmsRouteNames } from '../enums/route-names';

export const CMS_ROUTE_PROVIDERS = [
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
        name: eCmsRouteNames.Cms,
        iconClass: 'fas fa fa-newspaper-o',
        layout: eLayoutType.application,
        order: 3,
      },
      {
        path: '/cms/admin/entries',
        name: eCmsRouteNames.Entries,
        parentName:eCmsRouteNames.Cms,
        iconClass: 'fas fa fa-file-text-o',
        layout: eLayoutType.application,
        order: 4,
      },
      {
        path: '',
        name: eCmsRouteNames.Settings,
        parentName:eCmsRouteNames.Cms,
        iconClass: 'fas fa fa-cog',
        layout: eLayoutType.application,
        order: 5,
      },
      {
        path: '/cms/admin/fields',
        name: eCmsRouteNames.Fields,
        parentName:eCmsRouteNames.Settings,
        iconClass: 'fas fa fa-pencil-square-o',
        layout: eLayoutType.application,
        order: 6,
      },
      {
        path: '/cms/admin/sites',
        name: eCmsRouteNames.Sites,
        parentName:eCmsRouteNames.Settings,
        iconClass: 'fas fa fa-globe ',
        layout: eLayoutType.application,
        order: 7,
      },
      {
        path: '/cms/admin/sections',
        name: eCmsRouteNames.Sections,
        parentName:eCmsRouteNames.Settings,
        iconClass: 'fas fa fa-newspaper-o',
        layout: eLayoutType.application,
        order: 8,
      },
    ]);
  };
}
