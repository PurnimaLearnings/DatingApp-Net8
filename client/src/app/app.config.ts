import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import {provideAnimations} from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';
import { errorinterceptorInterceptor } from './_interceptors/errorinterceptor.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
     provideHttpClient(withInterceptors([errorinterceptorInterceptor])),
      provideAnimations(),
      provideToastr({
        positionClass:'toast-bottom-right'
      })
    ]
};
