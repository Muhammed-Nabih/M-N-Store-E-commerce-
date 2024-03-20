import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup;
  returnUrl:string;
  constructor(private accountService:AccountService,private router:Router,private fb:FormBuilder
    ,private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
    this.createLoginForm();
  }
  createLoginForm(){
    this.loginForm = this.fb.group({
      email:['',[Validators.required,Validators.pattern('^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$')]],
      password:['',Validators.required]
    })
  }
  get _email(){
    return this.loginForm.get('email');
  }
  get _password(){
    return this.loginForm.get('password');
  }

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe(
      {
        next:()=>{this.router.navigateByUrl(this.returnUrl)},
        error:(err)=>{console.error(err)}
      }
    )
  }

}