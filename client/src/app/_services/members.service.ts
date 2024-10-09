import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/photo';
import { PaginatedResults } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private httpclient=inject(HttpClient);
members= signal<Member[]>([]);
paginatedResults=signal<PaginatedResults<Member[]> | null>(null);
  baseurl=environment.apiurl;
  constructor() { }

  getMembers(pageNumber?: number, pageSize?: number){
    let params=new HttpParams();
    if(pageNumber&& pageSize){
      params=params.append('pageNumber', pageNumber);
      params=params.append('pageSize', pageSize);
    }

   return this.httpclient.get<Member[]>(this.baseurl+'users', {observe:'response', params}).subscribe({
    next:response=>
      this.paginatedResults.set({
        items: response.body as Member[],
        pagination: JSON.parse(response.headers.get('Pagination')!)
      })
   })
  }

  getMember(username:string){
  // const member= this.members().find(x=>x.userName==username);
  // if(member!=undefined){
  //   return of(member);
  // }
    return this.httpclient.get<Member>(this.baseurl+'users/'+username);
  }

  updateMember(member:Member){
    return this.httpclient.put(this.baseurl+'users', member).pipe(
      // tap(()=>{
      //     this.members.update(members=>members.map(m=>m.userName==member.userName?member:m))
      //   })
    );
  }

  setMainPhoto(photo:Photo){
    return this.httpclient.put(this.baseurl+'users/set-main-photo/'+photo.id,{}).pipe(
    //   tap(()=>{
    //   this.members.update(members=>members.map(m=>{
    //       if(m.photos.includes(photo)){
    //         m.photoUrl=photo.uRl
    //       }
    //       return m;
    //     })
    //   )
    // })
  );
  }

  deletePhoto(photo:Photo){
    return this.httpclient.delete(this.baseurl+'users/delete-photo/'+photo.id).pipe(
    //   tap(()=>{
    //   this.members.update(members=>members.map(m=>{
    //       if(m.photos.includes(photo)){
    //         m.photos=m.photos.filter(x=>x.id!==photo.id);
    //       }
    //       return m;
    //     })
    //   )
    // })
  );
  }
}
