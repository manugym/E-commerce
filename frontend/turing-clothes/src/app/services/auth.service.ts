import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { RegisterDto } from '../models/register-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private api: ApiService) {}

  async login(authData: AuthDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Login', authData);

    if (result.success) {
      this.api.jwt = result.data.accessToken;
      localStorage.setItem("token", this.api.jwt)
    }

    return result;
  }

  async register(authData: RegisterDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Register', authData);
    
    if (result.success) {
      this.api.jwt = result.data.accessToken;
      localStorage.setItem("token", this.api.jwt)
    }

    return result;
  }

  async getSecretMessage(): Promise<Result<string>> {
    const result = await this.api.get<string>('Auth');

    return result;
  }

  
}
