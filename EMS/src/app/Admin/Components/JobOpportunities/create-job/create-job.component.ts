import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first, Observable } from 'rxjs';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { IJobListing } from 'src/app/Admin/Settings/Models/iadmin';
import { IJobStatus } from 'src/app/Admin/Settings/Models/iadmin';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
@Component({
  selector: 'app-create-job',
  templateUrl: './create-job.component.html',
  styleUrls: ['./create-job.component.scss']
})
export class CreateJobComponent implements OnInit {

  dataSaved = false;  
  jobForm: any;  
  allJobs!: Observable<IJobListing[]>;  
  allListingStatus!:Observable<IJobStatus[]>;
  allJobTypes!:Observable<IJobListing[]>;
  jobIdUpdate = null;  
  massage ='';  
  allJobTypesList:any=[];
  
  constructor(private formbulider: FormBuilder, private jobService:AdminService,private router: Router) { }

  ngOnInit(): void {
    this.jobForm = this.formbulider.group({  
      Description: ['', [Validators.required]],  
      Amount: ['', [Validators.required]],  
      DatePosted: ['', [Validators.required]],  
      ExpiryDate: ['', [Validators.required]], 
      ListingStatusName:['',[Validators.required]],
    });  
    this.loadAllJobs();
    //this.refreshJobTypesMap();
  }
  loadAllJobs() {  
    this.allJobs = this.jobService.getAllJob();  
    this.allListingStatus=this.jobService.getAllListingStatus();
   
  }  
  onFormSubmit() {  
    this.dataSaved = false;  
    const job = this.jobForm.value;  
    this.Createjob(job);  
    this.jobForm.reset();  
  }  
  loadjobToEdit(jobId: number) {  
    this.jobService.getJobById(jobId).subscribe(job=> {  
      this.massage = '';  
      this.dataSaved = false;  
      //this.jobIdUpdate = job.Job_ID;  
      this.jobForm.controls['Discription'].setValue(job.Description);  
      this.jobForm.controls['Amount'].setValue(job.Amount);  
      this.jobForm.controls['ExpiryDate'].setValue(job.ExpiryDate); 
      this.jobForm.controls['ListingStatusName'].setValue(job.JobStatus_ID) ;

        
    });  
  
  }  
  Createjob(job: IJobListing) {  
    if (this.jobIdUpdate == null) {  
      this.jobService.createJob(job).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = 'Record saved Successfully';  
          this.loadAllJobs();  
          this.jobIdUpdate = null;  
          this.jobForm.reset();  
        }  
      ); 
      console.log(job) 
    } else {  
      job.Job_ID = this.jobIdUpdate;  
      this.jobService.updateJob(job).subscribe(() => {  
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';  
        this.loadAllJobs();  
        this.jobIdUpdate = null;  
        this.jobForm.reset();  
      });  
      console.log(job)
    }  
  }   
  deletejob(jobId: number) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.jobService.deleteJobById(jobId).subscribe(() => {  
      this.dataSaved = true;  
      this.massage = 'Record Deleted Succefully';  
      this.loadAllJobs();  
      this.jobIdUpdate = null;  
      this.jobForm.reset();  
  
    });  
    
  }  
}  
  resetForm() {  
    this.jobForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
  }  

}
