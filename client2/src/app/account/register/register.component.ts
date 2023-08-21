import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'src/app/shared/models/IUser';
import { AccountService } from '../account.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  croppedImage: any = '';
  registerForm: FormGroup = new FormGroup({
    displayName: new FormControl('', [Validators.required, Validators.pattern('^(?=\\D*)\\S{6,}$')]),
    email: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}$')]),
    password: new FormControl('', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{7,}$')]),
  })

  constructor(private accountService: AccountService,
    private _router: Router,
    private _snackBar: MatSnackBar
  ) {

  }

  onImageCropped(croppedImage: any) {
    this.croppedImage = croppedImage;
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      var user = Object.assign({}, this.registerForm.value) as IUser;
      user.image = this.croppedImage;
      console.log(user);
      this.accountService.register(user).subscribe(response => {
        console.log(response);
        this._snackBar.open("Registered Successfully!", "Ok");
        this._router.navigateByUrl("/news").then(() => {
          location.reload();
        });
      },
        error => {
          this._snackBar.open(error.error.message, "Ok");
        })
    }
  }

}
