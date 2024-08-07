import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {TitleCasePipe} from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
 accountService=inject(AccountsService);
 router=inject(Router);
 toastr=inject(ToastrService);
  model:any={};

  login(){
    this.accountService.login(this.model).subscribe({
      next: ()=>{
        this.router.navigateByUrl('/members');
      },
      error:(error: any)=>this.toastr.error(error.error)
      
    })
    console.log(this.model);
  }

  logout(){
    this.accountService.logout()
    this.router.navigateByUrl('/');
  }

}
