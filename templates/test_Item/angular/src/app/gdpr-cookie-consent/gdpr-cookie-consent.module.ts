import { NgModule } from '@angular/core';
import { CookiePolicyComponent } from './cookie-policy.component';
import { PrivacyPolicyComponent } from './privacy-policy.component';
import { GdprCookieConsentRoutingModule } from './gdpr-cookie-consent-routing.module';

@NgModule({
  declarations: [CookiePolicyComponent, PrivacyPolicyComponent],
  imports: [GdprCookieConsentRoutingModule],
})
export class GdprCookieConsentModule {}
