import { Component } from '@angular/core';
import { AuthRequest } from '../../models/auth-request';
import { AuthResponse } from '../../models/auth-response';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})



export class LoginComponent {
email: string = "Correo electrónico";
password: string = "123456";
role: string = "admin";
jwt: string = ' ';


constructor(private AuthService: AuthService){}

async submit(){
  const authData : AuthRequest = { email: this.email, password: this.password, role: this.role};
  const result = await this.AuthService.login(authData);

  if (result.success){
    this.jwt = result.data.accessToken;
  }
}
}
