import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreateServiceComponent } from '../create-service/create-service.component';
import { IService } from 'src/app/Admin/Settings/Models/iadmin';
import { FormBuilder } from '@angular/forms';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { UpdateServiceComponent } from '../update-service/update-service.component';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss']
})
export class ServiceListComponent implements OnInit {
  serviceList!:Observable<IService[]>;
  displayedColumns: string[] = ['ServiceName', 'ServiceDescription','edit','delete'];
  dataSaved = false; 
  public dataSource = new MatTableDataSource<IService>();
  serviceForm: any;
  massage ='';
  allServices!: Observable<IService[]>;
  serviceIdUpdate!:number;
  services$:Observable<IService[]>=this.service.getAllService();
  services: IService[]=[];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private service:AdminService,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
    this.readService();
 console.log(this.readService);
  }
//delete service
deleteService(serviceId: number){
  if (confirm("Are you sure you want to delete this ?")){
  this.service.deleteServiceById(serviceId).subscribe(res=>{
    this.readService();
    console.log(res); 
    this.notificationService.successToaster('Successfully deleted Service','Error')
  },
  (  err: { status: number; })=>{
    if(err.status != 201){
      this.notificationService.errorToaster('Cannot delete Service, Please Contact System Administrator','Error');
    }
    
  }
  );
}
}

//Filtering
applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
//Dialog

  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
    console.log(this.dataSource.filter)
  }

//read service
  readService(){
    this.services$.subscribe((res)=>{
      this.services = res;
      this.dataSource.data = this.services; //for search
      console.log(res);
    },
    err=>{
      if(err.status != 201){
        this.notificationService.errorToaster('Cannot List Services, Please Contact System Administrator','Ouch!!');
      }
    })
  }

  //open dialog
  openAddDialog(){
    const dialog = new MatDialogConfig
    dialog.disableClose = true, 
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = { add: 'yes' }
    const dialogReference = this.dialog.open(
      CreateServiceComponent,);
    dialogReference.afterClosed().subscribe((res) => {
      if (res == 'add') {
        this.notificationService.successToaster('Service Added', 'Success')
        this.readService();
      }
    });
  }

  EditDialog(){
    const dialog = new MatDialogConfig
    dialog.disableClose = true, 
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = { add: 'yes' }
    const dialogReference = this.dialog.open(
      CreateServiceComponent,);
    dialogReference.afterClosed().subscribe((res) => {
      if (res == 'add') {
        this.readService();
      }
    });
  }
  //router to edit
  routerEditService(Service_ID: number, ServiceName: string, ServiceDescription: string ){
    const dialog = new MatDialogConfig();
    dialog.disableClose = true;
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = {add: 'yes'};
    const dialogReference = this.dialog.open(
      UpdateServiceComponent,
      {
       data: { Service_ID: Service_ID,  ServiceName: ServiceName, ServiceDescription: ServiceDescription }
     });

   dialogReference.afterClosed().subscribe((res) => {
     if (res == 'add') {
       this.notificationService.successToaster('Service Succesfully Edited', 'Hooray');
       this.readService();
     }
   });
 }


  Close() {
    this.dialog.closeAll();
  }
}
