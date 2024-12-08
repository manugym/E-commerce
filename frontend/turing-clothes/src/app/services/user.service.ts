import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { UserDto } from '../models/user-dto';
import { PassDto } from '../models/pass-dto';
import { AuthService } from './auth.service';
import { AuthResponse } from '../models/auth-response';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly TOKEN_KEY = 'token';
  decodedToken: any = null;

  constructor(private api: ApiService, private authService: AuthService) { }

  async getUserOrder(): Promise<Result<UserDto>> {
    const userOrder = await this.api.get<UserDto>('User/UserOrder');
    return userOrder;
  }

  async updatePass(passDto: PassDto): Promise<Result<AuthResponse>> {
    const result = await this.api.put<AuthResponse>('Auth/UpdatePass', passDto);
    
    if (result.success) {
      
      this.authService.handleSession(result.data.accessToken, false)
    } 
    return result;
  }

}
