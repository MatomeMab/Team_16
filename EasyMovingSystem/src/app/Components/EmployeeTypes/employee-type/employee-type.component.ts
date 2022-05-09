import { Component, OnInit } from '@angular/core';
import {FormBuilder,Validators} from '@angular/forms';
import { EmployeeTypeService } from 'src/app/services/employee-type.service';
import { IEmployeeType } from 'src/app/Model/iemployee-type';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent implements OnInit {

  EmployeeTypeIdUpdate = null;  
  dataSaved = false;
  //EmployeeTypeForm: any;
  allEmployeeTypes: any;  
  EmployeeTypeForm: any;  
  //allTrucks: Observable<ITruck[]>;  
  massage = null; 
  constructor(private formBuilder: FormBuilder, private employeeTypeService: EmployeeTypeService,httpClient:HttpClient) {
    this.employeeTypeService.getEmployeeTypeList().subscribe(data => {
       this.allEmployeeTypes = data;
       
    });
   }

  ngOnInit(): void {
    this.EmployeeTypeForm = this.formBuilder.group({
       Name:["",[Validators.required]],
       Description:["",[Validators.required]]
    }); 
  }
  loadAllEmployeeTypes() {  
    this.allEmployeeTypes= this.employeeTypeService.getEmployeeTypeList();  
  }  
  onFormSubmit(){
    this.dataSaved = false;
    const employeeType = this.EmployeeTypeForm.value;
    //this.addTruck(truck);
    this.EmployeeTypeForm.reset();
  }
  addTruck(employeeType: IEmployeeType) {  
    if (this.EmployeeTypeIdUpdate == null) {  
      this.employeeTypeService.addTruck(employeeType).subscribe(  
        () => {  
          this.dataSaved = true;  
         // this.massage = 'Record saved Successfully';  
          this.loadAllEmployeeTypes();  
          this.EmployeeTypeIdUpdate = null;  
          this.EmployeeTypeForm.reset();  
        }  
      );  
    } else {  
      employeeType.EmployeeType_ID= this.EmployeeTypeIdUpdate;  
      this.employeeTypeService.updateTruck(employeeType).subscribe(() => {  
        this.dataSaved = true;  
        //this.massage = 'Record Updated Successfully';  
        this.loadAllEmployeeTypes();  
        this.EmployeeTypeIdUpdate = null;  
        this.EmployeeTypeForm.reset();  
      });  
    }  
  }   
    
  deleteTruck(employeeTypeid: string) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.employeeTypeService.deleteTruck(employeeTypeid).subscribe(() => {  
      this.dataSaved = true;  
      //this.massage = 'Record Deleted Succefully';  
      this.loadAllEmployeeTypes();  
      this.EmployeeTypeIdUpdate = null;  
      this.EmployeeTypeForm.reset();  
  
    });  
  }  
}  

}
