import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { CatalogoComponent } from './pages/catalogo/catalogo.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { LoginComponent } from './pages/login/login.component';
import { FooterComponent } from './pages/footer/footer.component';
import { TestPageComponent } from './pages/test-page/test-page.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'catalog', component: CatalogoComponent },
  { path: 'register', component: RegistroComponent },
  { path: 'login', component: LoginComponent },
  { path: 'footer', component: FooterComponent},
  {path: 'test-page', component: TestPageComponent}
];
