import { Component, OnInit ,Inject} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
import { IJobListing } from 'src/app/Admin/Settings/Models/iadmin';
@Component({
  selector: 'app-update-job',
  templateUrl: './update-job.component.html',
  styleUrls: ['./update-job.component.scss']
})
export class UpdateJobComponent implements OnInit {
  jobForm!:FormGroup;
  job!:IJobListing;
  dataSaved = false;  
  massage!:any;
  constructor(private jobService: AdminService,
    public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<UpdateJobComponent>,
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
    const job: IJobListing = this.jobForm.value;
    job.Job_ID = this.data.Job_ID;
    this.jobService.updateJob(job).subscribe(res => {
      this.dataSaved = true; 
      this.massage=this.notificationService.successToaster('You have successfuly added the job opportunity','Horaay')
      this.refreshForm();
      this.dialogRef.close('add');
    },
    err => {
      if (err.status == 401) {
        this.notificationService.failToaster('Unable to update the Job Opportunity', 'Error');
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
    //JobStatusName:'',
    JobStatus_ID:0 
  }
}
}
