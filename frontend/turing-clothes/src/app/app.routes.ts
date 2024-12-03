import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { FooterComponent } from './pages/footer/footer.component';
import { TestPageComponent } from './pages/test-page/test-page.component';
import { AboutusComponent } from './pages/aboutus/aboutus.component';
import { CatalogoComponent } from './pages/catalogo/catalogo.component';
import { CartComponent } from './pages/cart/cart.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { LocalCartComponent } from './pages/local-cart/local-cart.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';
import { BlockchainComponent } from './pages/blockchain/blockchain.component';
import { ConfirmCheckoutComponent } from './pages/confirm-checkout/confirm-checkout.component';
import { AdminComponent } from './pages/admin/admin.component';
import { UsersComponent } from './pages/admin/pages/users/users.component';
import { ChangePasswordComponent } from './pages/admin/pages/change-password/change-password.component';
import { ProductsComponent } from './pages/admin/pages/products/products.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'catalog', component: CatalogoComponent },
  {path: 'product-details/:id', component: ProductDetailsComponent},
  {path: 'local-cart', component: LocalCartComponent},
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'blockchain', component: BlockchainComponent },
  { path: 'confirm-checkout/:id', component: ConfirmCheckoutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'footer', component: FooterComponent},
  {path: 'test-page', component: TestPageComponent},
  {path: 'aboutus', component: AboutusComponent}

  ,{path: 'admin', component: AdminComponent},
  {path: 'admin/users', component: UsersComponent},
  {path: 'admin/products', component: ProductsComponent},
  {path: 'admin/change-password', component: ChangePasswordComponent}
];
