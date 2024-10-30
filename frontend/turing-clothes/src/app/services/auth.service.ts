import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { RegisterDto } from '../models/register-dto';
import { routes } from '../app.routes';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private api: ApiService, private router: Router) {}

  async login(
    authData: AuthDto,
    remember: boolean
  ): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Login', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;

      if (remember) {
        localStorage.setItem('token', this.api.jwt);
      } else {
        sessionStorage.setItem('token', this.api.jwt);
      }
    }

    return result;
  }

  async register(authData: RegisterDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Register', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;

      sessionStorage.setItem('token', this.api.jwt);

      alert('Usuario registrado correctamente.');

      this.router.navigate(['/home']);
    } else {
      alert('Ha habido un problema al registrar el usuario.');
    }

    return result;
  }

  isLoggedIn(): boolean {
    if (
      sessionStorage.getItem('token') !== null ||
      localStorage.getItem('token') !== null
    ) {
      return true;
    }
    return false;
  }

  logout() {
    this.api.jwt = '';
    localStorage.removeItem('token');
    sessionStorage.removeItem('token');
  }

  async getSecretMessage(): Promise<Result<string>> {
    const result = await this.api.get<string>('Auth');

    return result;
  }
}
