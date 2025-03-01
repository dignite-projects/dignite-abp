import type { StringSegment } from '../../../extensions/primitives/models';

export interface EntityTagHeaderValue {
  any: EntityTagHeaderValue;
  tag: StringSegment;
  isWeak: boolean;
}
