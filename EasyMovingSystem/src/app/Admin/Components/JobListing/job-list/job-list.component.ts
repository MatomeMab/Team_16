import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';  
import { Observable } from 'rxjs';
import { JobService } from 'src/app/Admin/Services/job.service';  
import { IJob } from 'src/app/Admin/Models/ijob';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css']
})
export class JobListComponent implements OnInit {
  dataSaved = false;  
  jobForm: any;  
  allJobs!: Observable<IJob[]>;  
  allListingStatus!:Observable<IJob[]>;
  allJobTypes!:Observable<IJob[]>;
  jobIdUpdate = null;  
  massage ='';  
  allJobTypesList:any=[];
  DatePosted= new Date();
  constructor(private formbulider: FormBuilder, private jobService:JobService,private router: Router) { }

  ngOnInit(): void {
    this.jobForm = this.formbulider.group({  
      Description: ['', [Validators.required]],  
      Amount: ['', [Validators.required]],  
      DatePosted: ['', [Validators.required]],  
      ExpiryDate: ['', [Validators.required]], 
      ListingStatusName:['',[Validators.required]],
    });  
  }
  Createjob(job: IJob) {  
    if (this.jobIdUpdate == null) {  
      this.jobService.createJob(job).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = 'Record saved Successfully';  
          
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
   
  resetForm() {  
    this.jobForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
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

}
