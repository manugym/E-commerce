import { Injectable } from '@angular/core';
import { InicioRequest } from '../models/inicio-request.model';
import { InicioResponse } from '../models/inicio-response.model';

@Injectable({
  providedIn: 'root'
})
export class InicioSesionService {

 /* constructor(private api: ApiService) { }

async login(authData: InicioRequest): Promise<Result<InicioResponse>>{
  const result = await this.api.post<InicioResponse>('iniciosesion', authData);

  if(result.success){
    this.api.jwt = result.data.accessToken;
  }
  return result;
}
*/

}
