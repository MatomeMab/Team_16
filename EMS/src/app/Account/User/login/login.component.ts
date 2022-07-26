import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../../Settings/Services/user.service';
import { RegisterComponent } from '../register/register.component';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginGroup: FormGroup = this.fb.group({
    EmailAddress: ['', Validators.compose([Validators.required, Validators.maxLength(50)])],
    Password: ['', Validators.compose([Validators.required, Validators.maxLength(50)])],
  });
 
  

  constructor(private fb: FormBuilder,
    private service:UserService,
    private snack: MatSnackBar,
    private router: Router, 
    private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  Login(): void {
    this.service.Login(this.loginGroup.value).subscribe((res: any) => {
      // route to home
      localStorage.setItem('user', JSON.stringify(res));
      this.router.navigateByUrl('report');
    }, (error: HttpErrorResponse) => {

      if (error.status === 404) {
        this.snack.open('Invalid credentials.', 'OK', {
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          duration: 3000
        });
        this.loginGroup.reset();
        return;
      }
      this.snack.open('An error occured on our servers. Try again later.', 'OK', {
        verticalPosition: 'bottom',
        horizontalPosition: 'center',
        duration: 3000
      });
      this.loginGroup.reset();
    });
  }

  openRegister() {
    const register = this.dialog.open(RegisterComponent, {
      disableClose: true
    });


  }
}
