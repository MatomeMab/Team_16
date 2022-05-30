import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { IEmployeeType } from 'src/app/Admin/Models/iemployee-type';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent implements OnInit {
  dataSaved = false;  
  employeeTypeForm: any; 
  colorControl = new FormControl('accent'); 
  allEmployeeTypes!: Observable<IEmployeeType[]>;  
  empTypeIdUpdate = null;  
  massage ='';  
  isAddMode!: boolean;
  service!:IEmployeeType;
  constructor(private formbulider: FormBuilder,private empTypeService:EmployeeTypeService, private notificationService:NotificationsService) { }

  ngOnInit(): void {
    this.employeeTypeForm = this.formbulider.group({  
      EmployeeTypeName: ['', [Validators.required]],  
      EmployeeDescription: ['', [Validators.required]],  
    }); 
   
  }
  CreateEmployeeType(emp: IEmployeeType) {  
    if (this.empTypeIdUpdate == null) {  
      this.empTypeService.createEmployeeType(emp).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.notificationService.successToaster('Registration Successful', 'Success') 
          this.employeeTypeForm.reset();  
        }  
      ); 
      console.log(emp) 
    }
  }   
 
  resetForm() {  
    this.employeeTypeForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
  }
  onFormSubmit() {  
    this.dataSaved = false; 
    const emp=this.employeeTypeForm.value;
    this.CreateEmployeeType(emp);   
    this.employeeTypeForm.reset();  
  }

  error_messages = {
    EmployeeTypeName: [
      { type: 'minlength', message: 'Employee Type must be more than 1 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type name is required' }
    ],
    EmpoyeeDescription: [
      { type: 'minlength', message: 'Employee Type must be more than 1 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type description is required' }
    ]
  }
  /*
  onSubmit() {
    if (this.employeeTypeForm.valid) {
      this.empTypeService = this.employeeTypeForm.value;
      this.empTypeService.createEmployeeType(this.service).subscribe((res) => {
        this.resetObject();
        this.service = res as IEmployeeType;
      }, (err: HttpErrorResponse) => {
        if (err.status == 406) {
          this.notificationService.failToaster('This username or email is already being used!', 'Error');
        }
        else if (err.status == 200) {
          this.notificationService.successToaster('Registration Successful', 'Success')
          this.Close();
        }
        else {
          this.notificationService.errorToaster('Server Error, Please Contact System Administrator', 'Error')
        }
      }

      )
    }
  }*/
  Close() {
    throw new Error('Method not implemented.');
  }
  resetObject() {
    this.service = {
      EmployeeType_ID: 0,
      EmployeeTypeName: '',
      EmployeeDescription: '',
     
    }
  }

}
