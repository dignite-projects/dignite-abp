import { EntityProp, ePropType } from '@abp/ng.components/extensible';



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
    },
    {
        type: ePropType.String,
        name: 'groupName',
        displayName: 'Cms::Group',
        sortable: false,
    },
    {
        type: ePropType.DateTime,
        name: 'creationTime',
        displayName: 'Cms::CreationTime',
        sortable: true,
    },
])





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
