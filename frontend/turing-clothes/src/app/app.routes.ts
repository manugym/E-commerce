import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CatalogoComponent } from './components/catalogo/catalogo.component';
import { RegistroComponent } from './components/registro/registro.component';
import { IniciosesionComponent } from './components/iniciosesion/iniciosesion.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'catalog', component: CatalogoComponent},
  {path: 'register', component: RegistroComponent},
  {path: 'login', component: IniciosesionComponent}
];
