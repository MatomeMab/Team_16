import { Component, OnInit } from '@angular/core';
import { MatDialog,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobListComponent } from '../job-list/job-list.component';
import { JobService } from 'src/app/Admin/Services/job.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { IJob } from 'src/app/Admin/Models/ijob';
@Component({
  selector: 'app-job-listing',
  templateUrl: './job-listing.component.html',
  styleUrls: ['./job-listing.component.css']
})
export class JobListingComponent implements OnInit {
  dataSaved = false;  
  jobForm: any;  
  allJobs!: Observable<IJob[]>;  
  allListingStatus!:Observable<IJob[]>;
  allJobTypes!:Observable<IJob[]>;
  jobIdUpdate = null;  
  massage ='';  
  allJobTypesList:any=[];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private jobService:JobService,private router: Router) { }

  ngOnInit(): void {
    this.loadAllJobs();
  }
  openDialog() {
    this.dialog.open(JobListComponent, {
      width:'20%'
    });
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
      this.jobForm.controls['Description'].setValue(job.Description);  
      this.jobForm.controls['Amount'].setValue(job.Amount);  
      this.jobForm.controls['ExpiryDate'].setValue(job.ExpiryDate); 
      this.jobForm.controls['ListingStatusName'].setValue(job.ListingStatusName) ;
      
        
    });  
  
  } 
  Createjob(job: IJob) {  
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

}
