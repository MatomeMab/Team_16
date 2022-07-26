import { Component, OnInit,Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { IService } from 'src/app/Admin/Settings/Models/iadmin';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';

@Component({
  selector: 'app-update-service',
  templateUrl: './update-service.component.html',
  styleUrls: ['./update-service.component.scss']
})
export class UpdateServiceComponent implements OnInit {
  serviceForm!:FormGroup;
  service!:IService;
  dataSaved = false;  
  massage!:any;
  constructor(private Service: AdminService,
    public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<UpdateServiceComponent>,
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
      this.serviceForm = this.formBuilder.group({  
        ServiceName: [this.data.ServiceName, [Validators.maxLength(20), Validators.minLength(5),Validators.required]],
        ServiceDescription: [this.data.ServiceDescription, [ Validators.maxLength(20), Validators.minLength(10),Validators.required]] 
      });  
    
  }
  OnSubmit() {
    console.log('Hello')
    if (this.serviceForm.valid) {
      const service: IService = this.serviceForm.value;
      service.Service_ID = this.data.Service_ID;
      this.Service.updateService(service).subscribe(res => {
        this.dataSaved = true; 
        this.massage=this.notificationService.successToaster('You have successfuly added the service','Horaay')
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
    this.service = {
      Service_ID: 0,
      ServiceName: '',
      ServiceDescription: ''
      
    }
  }
}
