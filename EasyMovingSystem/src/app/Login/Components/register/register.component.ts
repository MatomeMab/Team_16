import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroupDirective, NgForm, Validators, FormGroup, FormControl } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { IUser } from 'src/app/Shared/Models/iuser';
import { UserService } from 'src/app/Shared/Services/user.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerClient!: FormGroup;
  newUser!: IUser;
  constructor(private formBuilder: FormBuilder,public dialog: MatDialog, private userService:UserService) { }
  
  ngOnInit(): void {
    this.registerClient = this.formBuilder.group({
      clientName: ['', [Validators.required]],
      clientSurname: ['', [Validators.required]],
      phoneNum: ['', [Validators.required,Validators.pattern("[0-9 ]{10}")]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      password_confirm: ['', [Validators.required]],
    });
  }

 // convenience getter for easy access to form fields
 get f() {
  return this.registerClient.controls;
}
  public checkError = (controlName: string, errorName: string) => {
    return this.registerClient.controls[controlName].hasError(errorName);
  };
  omit_special_char(event: any) {
    var k;
    k = event.charCode;
    return (
      (k > 64 && k < 91) ||
      (k > 96 && k < 123) ||
      k == 8 ||
      k == 32 ||
      (k >= 48 && k <= 57)
    );
  }
  checkPasswords(group: FormGroup): boolean {
    let pw = group?.get('password')?.value;
    let confirmpw = group.get('password_confirm')?.value;

    if (pw == confirmpw) { return true; }
    else {
      return false
    }
  }
  RegisterClient() {

    let validPasswords = this.checkPasswords(this.registerClient);

    if (validPasswords == false) {
      alert("Passwords do not match")
      return;
    }

    if (this.registerClient.valid) {
      this.mapValues();
      this.userService.RegisterClient(this.newUser).subscribe((res: any) => {

        if (res.Result == "success") {
          
          let UserID = res.UserID;
          const dialogConfig = new MatDialogConfig();

          dialogConfig.width = '500px';
          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = true;
          dialogConfig.data = UserID;
          dialogConfig.backdropClass = "OTP"

          const dialogRef = this.dialog.open(OTPDialog, dialogConfig);
        }
        else {
          //console.log(res.Message)
        }

      })
    }

  }
  mapValues() {
    this.newUser = {
      User_ID: 0,
      Email: this.registerClient.value.email,
      Role_ID: 3,
      SessionID: "",
      Session_Expiry: null,
      OTP: "",


      Passwords:
        [{
          Password_ID: 0,
          User_ID: 0,
          Password1: this.registerClient.value.password
        }],

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


@Component({
  selector: 'OTP-dialog',
  templateUrl: './OTP.html',
  styleUrls: ['./register.component.css']
})
export class OTPDialog implements OnInit {
  constructor(public dialogRef: MatDialogRef<OTPDialog>, public userService: UserService, @Inject(MAT_DIALOG_DATA) public data: any, private route: Router) { };

  OTP = new FormControl('', Validators.required);
  UserID: any;

  ngOnInit() {
    this.UserID = this.data
  }

  VerifyOTP() {
    let OTP = this.OTP.value
    this.userService.VerifyOTP(OTP, this.UserID).subscribe((res: any) => {
      if (res.Result = "success") {
        this.dialogRef.close();
        this.route.navigate(['Login'])
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

}
