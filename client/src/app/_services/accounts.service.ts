import { inject, Inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  private http = inject(HttpClient);
  baseurl = environment.apiurl;
  CurrentUser=signal<User|null>(null);

  login(model: any) {

    return this.http.post<User>(this.baseurl + 'account/login', model).pipe(
      map(user=>{
        if(user){
          // localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    );
  }
  register(model: any) {

    return this.http.post<User>(this.baseurl + 'account/register', model).pipe(
      map(user=>{
        if(user){
        this.setCurrentUser(user);
        }
        return user;
      })
    );

    
  }

  setCurrentUser(user:User){
    localStorage.setItem('user', JSON.stringify(user));
    this.CurrentUser.set(user);
    
  }

  logout(){
    localStorage.removeItem('user');
    this.CurrentUser.set(null);
  }

}
