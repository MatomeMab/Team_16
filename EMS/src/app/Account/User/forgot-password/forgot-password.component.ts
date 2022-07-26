import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../../Settings/Services/user.service';
import { NewPasswordComponent } from '../new-password/new-password.component';
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordGroup: UntypedFormGroup = this.fb.group({
    EmailAddress: ['', Validators.compose([Validators.required, Validators.maxLength(50)])]
  });
  isLoading:boolean = false
  constructor(private fb: UntypedFormBuilder,
    private userService:UserService,
    private snack: MatSnackBar,
    private router: Router, 
    private dialog: MatDialog) { }

  ngOnInit(): void {
  }
  openResetPassword(){
    const register = this.dialog.open(NewPasswordComponent, {
      disableClose: true
    });
  }
  
  LoginUser(){
    if(this.forgotPasswordGroup.valid)
    {
      this.isLoading = true

      this.userService.Login(this.forgotPasswordGroup.value).subscribe((result: any) => {
        localStorage.setItem('User', JSON.stringify(result))
        this.forgotPasswordGroup.reset();
        this.router.navigate(['otp']).then((navigated: boolean) => {
          if(navigated) {
            this.snack.open(`The OTP has been sent to your email address`, 'X', {duration: 10000});
          }
       });
      }, (response: HttpErrorResponse) => {
        this.isLoading = false
        if (response.status === 404) {
          this.snack.open(response.error, 'X', {duration: 5000});
        }
        if (response.status === 500){
          this.snack.open(response.error, 'X', {duration: 5000});
        }
      })
    }
  }
}
