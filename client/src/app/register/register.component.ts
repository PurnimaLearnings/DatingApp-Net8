import { Component,  EventEmitter,  inject,  input, Output, output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
 private accountService=inject(AccountsService);
  // @Input() usersFromHomeComponent:any;
 // @Output() cancelRegister =new EventEmitter();
  cancelRegister=output<boolean>();
  usersFromHomeComponent=input.required<any>();


  model:any={}
register(){
  this.accountService.register(this.model).subscribe({
    next: response=>{
      console.log(response);
      this.cancel();
    },
    error:error=>console.log(error)
    
  })
  console.log(this.model);

}
cancel(){
  this.cancelRegister.emit(false);
  console.log("cancelled");
}
}
