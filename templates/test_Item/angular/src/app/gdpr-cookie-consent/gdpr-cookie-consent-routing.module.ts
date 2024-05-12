import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrivacyPolicyComponent } from './privacy-policy.component';
import { CookiePolicyComponent } from './cookie-policy.component';

const routes: Routes = [
  { path: 'privacy', component: PrivacyPolicyComponent },
  { path: 'cookie', component: CookiePolicyComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GdprCookieConsentRoutingModule {}
