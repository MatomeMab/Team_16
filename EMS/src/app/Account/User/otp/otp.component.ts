import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserService } from '../../Settings/Services/user.service';
import { NewPasswordComponent } from '../new-password/new-password.component';
@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.scss']
})
export class OTPComponent implements OnInit {
  OTP = new FormGroup('', Validators.required);
  UserID: any;
  constructor(private router: Router,  private fb: FormGroup,private formBuilder:FormBuilder, private snack: MatSnackBar,private userService:UserService,private dialog: MatDialog) { }

  ngOnInit(): void {
  }
  VerifyOTP() {
    let OTP = this.OTP.value
    this.userService.VerifyOTP(OTP,this.UserID).subscribe((res: any) => {
      if (res.Result = "success") {
        this.snack.open('Successful registration', 'OK()', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
        this.router.navigate(['Login'])
      }
      else {
        this.Invalid = true;
      }
    })
  }

  ReEnter = false;
  Invalid = false;

  ResendOTP() {
    this.OTP.reset();
    this.userService.ResendOTP(this.UserID).subscribe((res: any) => {
      if (res.Result = "success") {
        this.OTP.reset();
        this.ReEnter = true;
      }
      else {
        alert("One time password does not match, re-enter the password")
      }
    })
  }

openResetPassword(){
  const register = this.dialog.open(NewPasswordComponent, {
    disableClose: true
  });
}

}
