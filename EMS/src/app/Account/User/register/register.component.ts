import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {  FormGroup, FormBuilder,Validators } from '@angular/forms';
import { MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { IUser } from '../../Settings/Models/iuser';
import { UserService } from '../../Settings/Services/user.service';
import { OTPComponent } from '../otp/otp.component';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

 // registerClient!: FormGroup;
 registerClient:FormGroup = this.fb.group({
  clientName: ['', [Validators.required]],
  clientSurname: ['', [Validators.required]],
  phoneNum: ['', [Validators.required,Validators.pattern("[0-9 ]{10}")]],
  email: ['', [Validators.required, Validators.email]],
  password: ['', [Validators.required]],
  password_confirm: ['', [Validators.required]],
});

  newUser!: IUser;
  constructor( private fb: FormBuilder, 
              private service:UserService,
              private router:Router,
              
              private dialogRef: MatDialogRef<RegisterComponent>, 
              private snack: MatSnackBar
  ) { }

  ngOnInit(): void {
    
  }
  Register() {
    this.service.RegisterClient(this.registerClient.value).subscribe(() => {
      this.mapValues();
      this.dialogRef.close();
      this.snack.open('Successful registration', 'OK', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
        duration: 3000
      });
    }, (error: HttpErrorResponse) => {
      if (error.status === 403) {
        this.snack.open('This user has already been registered.', 'OK', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 3000
        });
      }
      this.snack.open('An error occurred on our servers, try again', 'OK', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
        duration: 3000
      });
      this.dialogRef.close();
    })
  }
  RegisterClient() {

    let validPasswords = this.checkPasswords(this.registerClient);

    if (validPasswords == false) {
      alert("Passwords do not match")
      return;
    }

    if (this.registerClient.valid) {
      this.mapValues();
      this.service.RegisterClient(this.newUser).subscribe((res: any) => {

        if (res.Result == "success") {
          this.router.navigate(['otp'])
        }
        else {
          //console.log(res.Message)
        }

      })
    }

  }

  checkPasswords(group: FormGroup): boolean {
    let pw = group?.get('password')?.value;
    let confirmpw = group.get('password_confirm')?.value;

    if (pw == confirmpw) { return true; }
    else {
      return false
    }
  }

  mapValues() {
    this.newUser = {
      User_ID: 0,
      Email: this.registerClient.value.email,
      Password: this.registerClient.value.password,
      Role_ID: 3,
      SessionID: "",
      Session_Expiry: null,
      OTP: "",


      Clients:
        [{
          Client_ID: 0,
          ClientName: this.registerClient.value.clientName,
          ClientSurname: this.registerClient.value.clientSurname,
          PhoneNum: this.registerClient.value.phoneNum,
          User_ID: 0
          
        }]
    }
  }
}
