import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountsService } from '../_services/accounts.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountsService);

  if (accountService.CurrentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountService.CurrentUser()?.token}`
      }
    })
  }

  return next(req);
};