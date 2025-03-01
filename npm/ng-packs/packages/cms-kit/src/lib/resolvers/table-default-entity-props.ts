import { EntityProp, ePropType } from '@abp/ng.components/extensible';
import { of } from 'rxjs';

// /**
//     [eCmsKitRouteName.Comments]:Comments_Entity_Props,
//  */
// export const Comments_Entity_Props = EntityProp.createMany<any>([
//     {
//         type: ePropType.String,
//         name: 'text',
//         displayName: 'CmsKit::Text',
//         sortable: true,
//         columnWidth:360,
        
//     },
//     {
//         type: ePropType.String,
//         name: 'userName',
//         displayName: 'CmsKit::Username',
//         sortable: true,
//         valueResolver(data: any) {
//             return of(data.record.author?.userName)
//         },
//     },
//     {
//         type: ePropType.String,
//         name: 'entityType',
//         displayName: 'CmsKit::EntityType',
//         sortable: true,
//     },
//     {
//         type: ePropType.String,
//         name: 'url',
//         displayName: 'CmsKit::URL',
//         sortable: true,
//         valueResolver(data: any) {
//             let aHtml=`<a href="${data.record.url}" class="underline-animate" > <i class="fa fa-location-arrow"></i></a>`
//             return of(aHtml)
//         },
//     },
//     {
//         type: ePropType.DateTime,
//         name: 'creationTime',
//         displayName: 'CmsKit::CreationTime',
//         sortable: true,
//     },
    

// ]);



