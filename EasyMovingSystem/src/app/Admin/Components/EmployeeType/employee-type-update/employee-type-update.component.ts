import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { EmployeeType } from 'src/app/interfaces/employee-type';

@Component({
  selector: 'app-employee-type-update',
  templateUrl: './employee-type-update.component.html',
  styleUrls: ['./employee-type-update.component.css']
})
export class EmployeeTypeUpdateComponent implements OnInit {
  form: FormGroup;
  employeeType:EmployeeType;

  error_messages = {
    EmployeeTypeName: [
      { type: 'minlength', message: 'Employee Type must be more than 2 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type name is required' }
    ],
    EmployeeTypeDescription: [
      { type: 'minlength', message: 'Employee Type must be more than 2 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type description is required' }
    ]
  }

  constructor(
    private employeeTypeService:EmployeeTypeService,
    private notificationService: NotificationsService,
    public dialog: MatDialog,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EmployeeTypeUpdateComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: any
  ){ }

  ngOnInit(): void {    
    this.createForm(); 
    this.refreshForm();   
  }
 
  Close(){
    this.dialog.closeAll();
  }

  createForm(){
    this.form=this.formBuilder.group({
      EmployeeTypeName: [this.data.EmployeeTypeName, [Validators.maxLength(30), Validators.minLength(2),Validators.required,]],
      EmployeeTypeDescription:[this.data.EmployeeTypeDescription, [Validators.maxLength(30), Validators.minLength(2),Validators.required]]
    });
  }

  OnSubmit(){
   if (this.form.valid){
      const employeeType:EmployeeType=this.form.value;
      employeeType.EmployeeType_ID=this.data.EmployeeType_ID;
      this.employeeTypeService.UpdateEmployeeType(employeeType).subscribe(res=>{
        this.refreshForm();
        this.dialogRef.close('add');
      },
      err => {
        if (err.status == 401) {
          this.notificationService.failToaster('Unable to Edit Employee Type', 'Error');
        }
        else if(err.status != 201){
          this.notificationService.errorToaster('Server Error, Please Contact System Administrator','Error');
        }
        else{
          console.log(err);
        }
      }
    );
  }
}

  refreshForm() {
    this.employeeType = {
      EmployeeType_ID: 0,
      EmployeeTypeName: '',
      EmployeeDescription: ''
    }
  }

}
