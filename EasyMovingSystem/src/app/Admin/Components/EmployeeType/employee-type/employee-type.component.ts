import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { IEmployeeType } from 'src/app/Admin/Models/iemployee-type';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

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
  constructor(private formbulider: FormBuilder,private empType:EmployeeTypeService) { }

  ngOnInit(): void {
    this.employeeTypeForm = this.formbulider.group({  
      EmployeeTypeName: ['', [Validators.required]],  
      EmployeeDescription: ['', [Validators.required]],  
    });  
  }
  CreateEmployeeType(emp: IEmployeeType) {  
    if (this.empTypeIdUpdate == null) {  
      this.empType.createEmployeeType(emp).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = 'Record saved Successfully';  
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
  
}
