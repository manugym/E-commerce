import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { RegisterDto } from '../models/register-dto';
import { LoginComponent } from '../pages/login/login.component';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  logueado: boolean = false;
  constructor(private api: ApiService) {}

  async login(
    authData: AuthDto,
    remember: boolean
  ): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Login', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;
      console.log(this.api.jwt);

      if (remember) {
        localStorage.setItem('token', this.api.jwt);
      } else {
        /**
         * AQUÍ HE PENSADO EN GUARDARLO EN SESSION STORAGE
         */
      }
    }

    return result;
  }

  async register(authData: RegisterDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Register', authData);
    if (result.success) {
      this.api.jwt = result.data.accessToken;

      localStorage.setItem('token', this.api.jwt);
    }

    return result;
  }

  isLoggedIn(): boolean {
    if (this.api.jwt !== '' || localStorage.getItem('token') !== null) {
      console.log('true');
      return true;
    }
    console.log('false');
    return false;
  }

  logout() {
    this.api.jwt = '';
    console.log(`Jwt: ${this.api.jwt}`);
    localStorage.removeItem('token');
  }

  async getSecretMessage(): Promise<Result<string>> {
    const result = await this.api.get<string>('Auth');

    return result;
  }
}
