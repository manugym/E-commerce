import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  jsonDecodedName: string
  constructor(private authService: AuthService) {
  }

  isLoguedIn(): boolean {
    return this.authService.isLoggedIn;
  }

  getLogedUsername(){
    return this.authService.decodedToken.unique_name
  }

  logout() {
    this.authService.logout()
  }
}
