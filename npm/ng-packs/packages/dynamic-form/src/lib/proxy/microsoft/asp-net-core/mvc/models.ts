import type { EntityTagHeaderValue } from '../../net/http/headers/models';

export interface ActionResult {
}

export interface FileResult extends ActionResult {
  contentType?: string;
  fileDownloadName?: string;
  lastModified?: string;
  entityTag: EntityTagHeaderValue;
  enableRangeProcessing: boolean;
}
