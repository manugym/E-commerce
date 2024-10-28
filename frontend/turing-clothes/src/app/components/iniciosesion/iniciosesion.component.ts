import { Component } from '@angular/core';
import { InicioRequest } from '../../models/loginRequest';
import { InicioResponse } from '../../models/auth-response';
import { FormsModule } from '@angular/forms';
import { InicioSesionService } from '../../services/login.service';

@Component({
  selector: 'app-iniciosesion',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './iniciosesion.component.html',
  styleUrl: './iniciosesion.component.css'
})



export class InicioSesionComponent {/*
email: string = "Correo electrónico";
password: string = "123456";
role: string = "admin";
jwt: string = ' ';


constructor(private InicioSesionService: InicioSesionService){}

async submit(){
  const authData : InicioRequest = { email: this.email, password: this.password, role: this.role};
  const result = await this.InicioSesionService.login(authData);

  if (result.success){
    this.jwt = result.data.accessToken;
  }
}*/
}
