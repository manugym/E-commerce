import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { UserDto } from '../models/user-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private api: ApiService) { }

  async getUserOrder(): Promise<Result<UserDto>> {
    const userOrder = await this.api.get<UserDto>('User/UserOrder');
      return userOrder;
  }


}
