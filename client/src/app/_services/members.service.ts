import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private httpclient=inject(HttpClient);
members= signal<Member[]>([]);

  baseurl=environment.apiurl;
  constructor() { }

  getMembers(){
   return this.httpclient.get<Member[]>(this.baseurl+'users').subscribe({
    next:memebers=>
      this.members.set(memebers)
   })
  }

  getMember(username:string){
  const member= this.members().find(x=>x.userName==username);
  if(member!=undefined){
    return of(member);
  }
    return this.httpclient.get<Member>(this.baseurl+'users/'+username);
  }

  updateMember(member:Member){
    return this.httpclient.put(this.baseurl+'users', member).pipe(
      tap(()=>{
          this.members.update(members=>members.map(m=>m.userName==member.userName?member:m))
        })
    );
  }
}
