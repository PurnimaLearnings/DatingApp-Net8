import { Component,  EventEmitter,  inject,  input, OnInit, Output, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { createInjectableType } from '@angular/compiler';
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  ngOnInit(): void {
    this.initializeForm();
this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }
 private accountService=inject(AccountsService);
//  toastr=inject(ToastrService);
 private fb=inject(FormBuilder);
  // @Input() usersFromHomeComponent:any;
 // @Output() cancelRegister =new EventEmitter();
  cancelRegister=output<boolean>();
  usersFromHomeComponent=input.required<any>();
private router=inject(Router);

registerForm:FormGroup=new FormGroup({});
maxDate = new Date();
validationErrors:string[]|undefined;
initializeForm(){
  this.registerForm=this.fb.group({
    gender:['male'],
    username: ['', Validators.required],
    knownAs:['',Validators.required],
    dateOfBirth:['',Validators.required],
    city:['',Validators.required],
    country:['',Validators.required],
    password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
    confirmPassword: ['', [Validators.required, this.matchValues('password')]],
  });

  this.registerForm.controls['password'].valueChanges.subscribe({
    next: ()=>this.registerForm.controls['confirmPassword'].updateValueAndValidity()
  });
}

matchValues(matchTo:string):ValidatorFn{
return (control:AbstractControl)=>{
  return control.value===control.parent?.get(matchTo)?.value?null:{isMatching:true}
}
}

register(){
//  console.log(this.registerForm.value);
  const dob=this.getOnly(this.registerForm.get('dateOfBirth')?.value);
  this.registerForm.patchValue({dateOfBirth:dob});
    this.accountService.register(this.registerForm.value).subscribe({
    next: _=>this.router.navigateByUrl('/members'),
    error:error=>this.validationErrors=error
    
  })

}
cancel(){
  this.cancelRegister.emit(false);
  console.log("cancelled");
}

private getOnly(dob:string|undefined){
  if(!dob) return;

  return new Date(dob).toISOString().slice(0,10);
}
}
