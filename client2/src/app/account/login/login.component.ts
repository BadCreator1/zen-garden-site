import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public loginForm: FormGroup = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  constructor(private _accountService: AccountService
    ,private _router: Router
    ,private _snackBar: MatSnackBar) {

  }

  onSubmit() {
    console.log(this.loginForm.value);
    this._accountService.login(this.loginForm.value).subscribe(response => {
      console.log(response);
      console.log(this._accountService.currentUser$);
      this._snackBar.open("Login Successfully!", "Ok");
      
      this._router.navigateByUrl("/news").then(() => {
        location.reload();
      });
    },
    error =>{
      console.log(error);
      
    } )
  }
}
