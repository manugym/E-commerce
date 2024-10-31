import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { RegisterDto } from '../models/register-dto';
import { routes } from '../app.routes';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { NavbarComponent } from '../pages/navbar/navbar.component';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  decodedJson: any = null;
  constructor(private api: ApiService, private router: Router) { }

  async login(
    authData: AuthDto,
    remember: boolean
  ): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Login', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;
      this.decodedJson = jwtDecode(this.api.jwt);

      if (remember) {
        localStorage.setItem('token', this.api.jwt);
      }
    } else {
      alert("El usuario o la contraseña son incorrectos.")
    }
    

    return result;
  }

  async register(authData: RegisterDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Register', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;
      this.decodedJson = jwtDecode(this.api.jwt);
            
      return result;
      //this.router.navigate(['/home']);
    }

    alert('Ha habido un problema al registrar el usuario.');
    return result;
  }

  isLoggedIn(): boolean {
    return this.decodedJson != null;
  }

  logout() {
    this.api.jwt = '';
    this.decodedJson = null;
  }


  async getSecretMessage(): Promise<Result<string>> {
    const result = await this.api.get<string>('Auth');

    return result;
  }
}
