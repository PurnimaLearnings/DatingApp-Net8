import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private httpclient=inject(HttpClient);
  baseurl=environment.apiurl;
  constructor() { }

  getMembers(){
   return this.httpclient.get<Member[]>(this.baseurl+'users');
  }

  getMember(username:string){
    return this.httpclient.get<Member>(this.baseurl+'users/'+username);
  }

}
