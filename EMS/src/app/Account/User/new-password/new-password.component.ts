import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '../../Settings/Services/user.service';
@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent implements OnInit {
  newPasswordGroup: UntypedFormGroup = this.fb.group({

    NewPassword: ['', Validators.required],
    ConfirmNewPassword: ['', Validators.required]
  });
  constructor(
    private fb: UntypedFormBuilder, 
    private service:UserService, 
    private snack: MatSnackBar,
    private dialogRef: MatDialogRef<NewPasswordComponent>,
  ) { }

  ngOnInit(): void {
  }

}
