import { mapEnumToOptions } from '@abp/ng.core';

export enum PaymentRequestState {
  Waiting = 0,
  Completed = 1,
  Failed = 2,
  Refunded = 3,
}

export const paymentRequestStateOptions = mapEnumToOptions(PaymentRequestState);
