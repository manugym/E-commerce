import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private api: ApiService) {}

  async login(authData: AuthDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('auth', authData);

    if (result.success) {
      this.api.jwt = result.data.accessToken;
      localStorage.setItem("token", this.api.jwt)
    }

    return result;
  }

  async getSecretMessage(): Promise<Result<string>> {
    const token = localStorage.getItem("token")
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`)
    const options = { headers: headers };
    return this.api.get<string>('Auth', options);
  }

  
}
