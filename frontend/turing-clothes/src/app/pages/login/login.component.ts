import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AuthDto } from '../../models/auth-dto';
import { Router } from '@angular/router';
import  Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  jwt: string = '';
  remember: boolean = false;
  registerHints: Boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  async submit() {
    const authData: AuthDto = { Email: this.email, Password: this.password };
    const result = await this.authService.login(authData, this.remember);

    if (result.success) {
      this.jwt = result.data.accessToken;
      this.router.navigate(['/home']);
      Swal.fire({
        icon: 'success',
        text: 'Login Correcto',
        showConfirmButton: true
        });
    
  }}

  logout() {
    this.authService.logout();
  }
}
