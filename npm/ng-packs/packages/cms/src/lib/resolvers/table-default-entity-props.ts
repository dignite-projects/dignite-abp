import { EntityProp, ePropType } from '@abp/ng.components/extensible';
import { of } from 'rxjs';
import { FieldAbstractsService } from '../services';

/**
    [ECmsComponent.Fields]: Fields_Entity_Props,
*/
export const Fields_Entity_Props = EntityProp.createMany<any>([
  {
    type: ePropType.String,
    name: 'displayName',
    displayName: 'Cms::DisplayName',
    sortable: true,
  },
  {
    type: ePropType.String,
    name: 'name',
    displayName: 'Cms::Name',
    sortable: true,
  },
  {
    type: ePropType.String,
    name: 'formControlName',
    displayName: 'Cms::FormControlName',
    sortable: false,
    valueResolver: (data: any) => {
      const _FieldAbstractsService = data.getInjected(FieldAbstractsService);
      const fromControlList = _FieldAbstractsService.getExcludeAssignControl();
      const formControlDisplayName=fromControlList.find(el=>el.name==data.record.formControlName)?.displayName
      return of(formControlDisplayName||data.record.formControlName);
    },
  },
  {
    type: ePropType.String,
    name: 'groupName',
    displayName: 'Cms::Group',
    sortable: false,
    valueResolver: (data: any) => {
        return of(data.record.groupName||'--');
      },
  },
  {
    type: ePropType.DateTime,
    name: 'creationTime',
    displayName: 'Cms::CreationTime',
    sortable: true,
  },
]);

// PageName.Guides
// export const Guides_Entity_Props = EntityProp.createMany<any>([
//     {
//         type: ePropType.String,
//         name: 'fullName',
//         displayName: 'AbpTenantManagement::全称',
//         sortable: true,
//         // valueResolver: (data: any) => {
//         //     let sss = `<a href="/guides/${data.record.id}" class="underline-animate" title="${data.record.fullName}">${data.record.fullName}</a>`
//         //     return of(sss);
//         // },
//         action: data => {
//             //EntriesComponent对应的工具栏页面

//         },
//     },

// ]);
