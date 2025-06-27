import { AuthService } from '@abp/ng.core';
import { Component, inject } from '@angular/core';

@Component({
  standalone: false,
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  protected readonly authService = inject(AuthService);

  loading = false;
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  login() {
    this.loading = true;
    this.authService.navigateToLogin({
      __tenant:"22d6e516-ee6d-e699-465f-3a1321d1a780"
    });
  }
}
