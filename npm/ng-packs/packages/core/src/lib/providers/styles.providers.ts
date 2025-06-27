import { CONTENT_STRATEGY, DomInsertionService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import styles from '../constants/styles';

/**将样式添加到head中 */
export const CORE_STYLES_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureStyles,
    deps: [DomInsertionService],
    multi: true,
  },
];

export function configureStyles(domInsertion: DomInsertionService) {
  return () => {
    domInsertion.insertContent(CONTENT_STRATEGY.AppendStyleToHead(styles));
  };
}
