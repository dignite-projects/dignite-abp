import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  loading = false;
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  constructor(private authService: AuthService) {}
  login() {
    // this.loading = true;
    // this.loading = true;
    console.log('login',this.authService);
    this.authService.navigateToLogin();
  }
}
