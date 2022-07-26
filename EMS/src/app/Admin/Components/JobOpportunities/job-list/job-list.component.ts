import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreateJobComponent } from '../create-job/create-job.component';
import { IJobListing, IJobStatus } from 'src/app/Admin/Settings/Models/iadmin';
import { FormBuilder } from '@angular/forms';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { UpdateJobComponent } from '../update-job/update-job.component';
@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent implements OnInit {
  jobs$:Observable<IJobListing[]>=this.jobService.getAllJob();
  status$:Observable<IJobStatus[]>=this.jobService.getAllListingStatus();
  dataSaved = false;  
  allListingStatus!:Observable<IJobStatus[]>;
  public dataSource = new MatTableDataSource<IJobListing>();
  jobs: IJobListing[]=[];
  allJobs!: Observable<IJobListing[]>;
  jobIdUpdate =null;  
  jobForm: any;  
  displayedColumns: string[] = ['Description', 'Amount', 'DatePosted','ExpiryDate','Status','edit','delete'];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private jobService:AdminService,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
    this.loadAllJobs();
  }
  loadAllJobs() {  
    this.allJobs = this.jobService.getAllJob();  
    this.allListingStatus=this.jobService.getAllListingStatus();
   
  } 
  Createjob(job: IJobListing) {  
    if (this.jobIdUpdate == null) {  
      this.jobService.createJob(job).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.notificationService.successToaster('Record saved Successfully','');  
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
        this.notificationService.successToaster('Record Updated Successfully','');  
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
    this.notificationService.successToaster('Successfully deleted Job','Error')
  },
  (  err: { status: number; })=>{
    if(err.status != 201){
      this.notificationService.errorToaster('Cannot delete this Job, Please Contact System Administrator','Error');
    }
    
  });
}
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

public myFilter = (value: string) => {
  this.dataSource.filter = value.trim().toLowerCase();
  console.log(this.dataSource.filter)
}

routerEditJob(Job_ID: number, Description: string, Amount: string,ExpiryDate:Date,JobStatus_ID:number ){
  const dialog = new MatDialogConfig();
  dialog.disableClose = true;
  dialog.width = 'auto';
  dialog.height = 'auto';
  dialog.data = {add: 'yes'};
  const dialogReference = this.dialog.open(
    UpdateJobComponent,
    {
     data: { Job_ID: Job_ID,  Description: Description, Amount: Amount, ExpiryDate:ExpiryDate,JobStatus_ID:JobStatus_ID}
   });

 dialogReference.afterClosed().subscribe((res) => {
   if (res == 'add') {
     this.notificationService.successToaster('Service Edited', 'Hooray');
     this.loadAllJobs();
   }
 });
}

}
