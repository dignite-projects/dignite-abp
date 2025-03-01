import { mapEnumToOptions } from "@abp/ng.core";

export enum CkEditorModeEnum {
    Simple,
    Classic
}

export const CkEditorModeEnumOptions = mapEnumToOptions(CkEditorModeEnum);

