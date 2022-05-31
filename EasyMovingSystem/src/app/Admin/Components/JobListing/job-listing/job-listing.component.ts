import { Component, OnInit } from '@angular/core';
import { MatDialog,MatDialogConfig,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobListComponent } from '../job-list/job-list.component';
import { JobService } from 'src/app/Admin/Services/job.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { IJob } from 'src/app/Admin/Models/ijob';
import { MatTableDataSource } from '@angular/material/table';
import { EditJobComponent } from '../edit-job/edit-job.component';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
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
  jobIdUpdate =null;  
  massage ='';  
  allJobTypesList:any=[];
  public dataSource = new MatTableDataSource<IJob>();
  jobs$:Observable<IJob[]>=this.jobService.getAllJob();
  status$:Observable<IJob[]>=this.jobService.getAllListingStatus();
  jobs: IJob[]=[];
  displayedColumns: string[] = ['Description', 'Amount', 'DatePosted','ExpiryDate','ListingStatus_ID','edit','delete'];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private jobService:JobService,private router: Router, private notificationService:NotificationsService) { }

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
 
deletejob(jobId: number){
  if (confirm("Are you sure you want to delete this ?")){
  this.jobService.deleteJobById(jobId).subscribe(res=>{
    this.loadAllJobs();
    console.log(res); 
    this.notificationService.successToaster('Successfully delected Service','Error')
  },
  (  err: { status: number; })=>{
    if(err.status != 201){
      this.notificationService.errorToaster('Cannot delete Service, Please Contact System Administrator','Error');
    }
    
  }
  );
}
}

//read jobs
/*readJobs(){
  this.jobs$.subscribe((res)=>{
    this.jobs = res;
    this.dataSource.data = this.jobs; //for searc 
    console.log(res);
  },
  err=>{
    if(err.status != 201){
      this.massage='Cannot List Services, Please Contact System Administrator','Ouch!!';
    }
  })
}*/
public myFilter = (value: string) => {
  this.dataSource.filter = value.trim().toLowerCase();
  console.log(this.dataSource.filter)
}

routerEditJob(Job_ID: number, Description: string, Amount: string,ExpiryDate:Date,ListingStatus_ID:number ){
  const dialog = new MatDialogConfig();
  dialog.disableClose = true;
  dialog.width = 'auto';
  dialog.height = 'auto';
  dialog.data = {add: 'yes'};
  const dialogReference = this.dialog.open(
    EditJobComponent,
    {
     data: { Job_ID: Job_ID,  Description: Description, Amount: Amount, ExpiryDate:ExpiryDate,ListingStatus_ID:ListingStatus_ID}
   });

 dialogReference.afterClosed().subscribe((res) => {
   if (res == 'add') {
     this.notificationService.successToaster('Service Edited', 'Hooray');
     this.loadAllJobs();
   }
 });
}


Close() {
  this.dialog.closeAll();
}
openAddDialog(){
  const dialog = new MatDialogConfig
  dialog.disableClose = true, 
  dialog.width = '50%';
  dialog.height = 'auto';
  dialog.data = { add: 'yes' }
  const dialogReference = this.dialog.open(
    JobListComponent,);
  dialogReference.afterClosed().subscribe((res) => {
    if (res == 'add') {
      this.notificationService.successToaster('Job Added', 'Success')
      this.loadAllJobs();
    }
  });
}
}
