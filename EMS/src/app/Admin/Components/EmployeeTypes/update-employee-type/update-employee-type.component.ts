import { Component, OnInit ,Inject} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { IEmployeeType } from 'src/app/Admin/Settings/Models/iadmin';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
@Component({
  selector: 'app-update-employee-type',
  templateUrl: './update-employee-type.component.html',
  styleUrls: ['./update-employee-type.component.scss']
})
export class UpdateEmployeeTypeComponent implements OnInit {

  empTypeForm!:FormGroup;
  empType!:IEmployeeType;
  dataSaved = false;  
  //massage!:any;
  constructor(public empTypeService:AdminService,public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<UpdateEmployeeTypeComponent>,
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
        this.notificationService.successToaster('You have successfuly updated the employee type','Horaay')
        this.refreshForm();
        this.dialogRef.close('add');
      },
      err => {
        if (err.status == 401) {
          this.notificationService.failToaster('Unable to Employee type', 'Error');
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
