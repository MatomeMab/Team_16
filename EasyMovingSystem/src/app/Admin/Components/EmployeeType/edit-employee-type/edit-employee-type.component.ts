import { Component, OnInit,Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import{IEmployeeType} from 'src/app/Admin/Models/iemployee-type';
import{EmployeeTypeService} from 'src/app/Admin/Services/employee-type.service';
@Component({
  selector: 'app-edit-employee-type',
  templateUrl: './edit-employee-type.component.html',
  styleUrls: ['./edit-employee-type.component.css']
})
export class EditEmployeeTypeComponent implements OnInit {
  empTypeForm!:FormGroup;
  empType!:IEmployeeType;
  dataSaved = false;  
  massage!:any;
  constructor(public empTypeService:EmployeeTypeService,public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EditEmployeeTypeComponent>,
    private notificationService: NotificationsService,
    @Inject(MAT_DIALOG_DATA)
    public data:any,) { }

  ngOnInit(): void {
    this.createForm();
    this.refreshForm();
  }
  Close(){
    this.dialog.closeAll();
  }
  
  createForm() {
      this.empTypeForm = this.formBuilder.group({  
        EmployeeTypeName: [this.data.EmployeeTypeName, [Validators.maxLength(50), Validators.minLength(5),Validators.required]],
        EmployeeDescription: [this.data.EmployeeDescription, [ Validators.maxLength(100), Validators.minLength(10),Validators.required]] 
      });  
    
  }
  OnSubmit() {
    console.log('Hello')
    if (this.empTypeForm.valid) {
      const empType: IEmployeeType = this.empTypeForm.value;
      empType.EmployeeType_ID = this.data.EmployeeType_ID;
      this.empTypeService.updateEmployeeType(empType).subscribe(res => {
        this.dataSaved = true; 
        this.massage=this.notificationService.successToaster('You have successfuly updated the service','Horaay')
        this.refreshForm();
        this.dialogRef.close('add');
      },
      err => {
        if (err.status == 401) {
          this.notificationService.failToaster('Unable to Edit Vehicle', 'Error');
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
    this.empType = {
      EmployeeType_ID: 0,
      EmployeeTypeName: '',
      EmployeeDescription: ''
      
    }
  }

}
