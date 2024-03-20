import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmPassowrd } from 'src/app/shared/Validators/password.validator';
import { EmailValidator } from 'src/app/shared/Validators/validateEmailNotToken.validate';
import { AccountService } from '../account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  errors:string[];
  registerForm:FormGroup;
  constructor(private accountService:AccountService,private router:Router,private fb:FormBuilder,
    private emailvalidator:EmailValidator) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }
  createRegisterForm(){
    this.registerForm = this.fb.group({
      displayName:['',[Validators.required,Validators.minLength(3)]],
      email:['',[Validators.required,Validators.pattern('^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$')]
      ,[this.emailvalidator.ValidateEmailNotToken()]],
      password:['',[Validators.required]],
      confirmPassword:['',[Validators.required]]
    },{validators:[ConfirmPassowrd]})
  }
  get _displayName() {
    return this.registerForm.get('displayName');
  }
  get _email() {
    return this.registerForm.get('email');
  }
  get _password() {
    return this.registerForm.get('password');
  }
  get _confirmpassword() {
    return this.registerForm.get('confirmPassword');
  }
  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe({
      next:(()=>{this.router.navigateByUrl('/shop')}),
      error:((err)=>{this.errors = err.errors})
    })
  }

}