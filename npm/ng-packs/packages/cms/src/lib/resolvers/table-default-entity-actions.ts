import { EntityAction } from "@abp/ng.components/extensible";
import { FieldsComponent } from "../components";
import { Router } from "@angular/router";
// import { AbpTableComponent } from "../view/table/abp-table.component";

// [PageName.Area]:Area_Entity_Action,
/**
    [ECmsComponent.Fields]: Fields_Entity_Action,
 */
export const Fields_Entity_Action = EntityAction.createMany<any>([
    {
        text: 'AbpUi::Edit',
        action: data => {
            const router = data.getInjected(Router);
            router.navigate([`/cms/admin/fields/${data.record.id}/edit`])
        },
        permission: 'CmsAdmin.Field.Update',
    },
    {
        text: 'AbpUi::Delete',
        action: data => {
            const component = data.getInjected(FieldsComponent);
            // component.clickbtn({ data, type: 'delete' })
            component.deletefield(data.record)
        },
        permission: 'CmsAdmin.Field.Delete',
    },
]);

