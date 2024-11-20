import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideNgxStripe } from 'ngx-stripe';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), 
    provideHttpClient(),
    provideNgxStripe('pk_test_51QJzkAAUJNbJRYZNDtyX9gUSm6fQuG6rpbNmsrtfbxhy5cglUFErIcd5dIcl5eUbRjTUV7mrdW8llw8kU0zLUmRy00n77k5Nx0')
  ]
};
