import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { IEmployeeType } from 'src/app/Admin/Models/iemployee-type';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent implements OnInit {

  form!:FormGroup;
  employeeType!:IEmployeeType
 
   error_messages = {
     EmployeeTypeName: [
       { type: 'minlength', message: 'Employee Type must be more than 1 character' },
       { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
       { type: 'required', message: 'Employee Type name is required' }
     ],
     EmployeeTypeDescription: [
       { type: 'minlength', message: 'Employee Type must be more than 1 character' },
       { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
       { type: 'required', message: 'Employee Type description is required' }
     ]
   }

  constructor(
    private employeeTypeService: EmployeeTypeService,
    public dialog: MatDialog,
    private formBuilder: FormBuilder, 
    private notificationService: NotificationsService,
    public dialogRef: MatDialogRef<EmployeeTypeComponent>
  ) { }

  ngOnInit(): void {
    this.refreshForm();
    this.createForm();
    console.log('Employee Types')
  }

  createForm() {
    this.form = this.formBuilder.group({
      EmployeeTypeName: new FormControl(
        this.employeeType.EmployeeTypeName,
        Validators.compose([
          Validators.maxLength(20),
          Validators.minLength(2),
          Validators.required
        ])
      ),
      EmployeeTypeDescription: new FormControl(
        this.employeeType.EmployeeDescription,
        Validators.compose([
          Validators.maxLength(60),
          Validators.minLength(2),
          Validators.required          
        ])
      ),
    });
  }
      Close(){
        this.dialog.closeAll();
      }
        
      OnSubmit() {
        console.log('Employee Type added')
        if (this.form.valid) {
          this.employeeType = this.form.value;
          this.employeeTypeService.CreateEmployeeType(this.employeeType).subscribe(res => {
            this.refreshForm();
            this.dialogRef.close('add');
          },
          err => {
            if (err.status == 401) {
              this.notificationService.failToaster('Unable to Create Employee Type', 'Error');
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

      refreshForm(){
        this.employeeType = {
          EmployeeType_ID: 0,
          EmployeeTypeName: '',
          EmployeeDescription:''
        }
      }

}
