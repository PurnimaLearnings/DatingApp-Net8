import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import {provideAnimations} from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';
import { errorinterceptorInterceptor } from './_interceptors/errorinterceptor.interceptor';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { loadingInterceptor } from './_interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
     provideHttpClient(withInterceptors([errorinterceptorInterceptor, jwtInterceptor, loadingInterceptor])),
      provideAnimations(),
      provideToastr({
        positionClass:'toast-bottom-right'
      }),
      importProvidersFrom(NgxSpinnerModule)
    ]
};
