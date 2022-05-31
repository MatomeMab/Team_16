import { Component, OnInit,Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { IJob } from 'src/app/Admin/Models/ijob';
import { JobService } from 'src/app/Admin/Services/job.service';
@Component({
  selector: 'app-edit-job',
  templateUrl: './edit-job.component.html',
  styleUrls: ['./edit-job.component.css']
})
export class EditJobComponent implements OnInit {
  jobForm!:FormGroup;
  job!:IJob;
  dataSaved = false;  
  massage!:any;
  constructor(public jobService:JobService,
    public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EditJobComponent>,
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
      this.jobForm = this.formBuilder.group({  
        Description: [this.data.Description, [Validators.maxLength(500), Validators.minLength(5),Validators.required]],
        Amount: [this.data.Amount, [ Validators.maxLength(5), Validators.minLength(2),Validators.required]],  
        ExpiryDate: [this.data.ExpiryDate,, [Validators.required]], 
        ListingStatus_ID:[this.data.ListingStatus_ID,,[Validators.required]]
      });  
    
  }
  OnSubmit() {
    console.log('Hello')
    if (this.jobForm.valid) {
      const job: IJob = this.jobForm.value;
      job.Job_ID = this.data.Job_ID;
      this.jobService.updateJob(job).subscribe(res => {
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
    this.job = {
      Job_ID: 0,
      Description: '',
      Amount:0,
      DatePosted:new Date,
      ExpiryDate: new Date,
      ListingStatusName:'',
      ListingStatus_ID:0 
    }
  }
}
