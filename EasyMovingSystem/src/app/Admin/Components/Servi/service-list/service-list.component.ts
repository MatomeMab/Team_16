import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, NumberValueAccessor, Validators } from '@angular/forms';  
import { Observable } from 'rxjs';  
import { ServiceService } from 'src/app/Admin/Services/service.service';
import { IService } from 'src/app/Admin/Models/iservice';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreateServiceComponent } from '../create-service/create-service.component';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { _SnackBarContainer } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { EditServiceComponent } from '../edit-service/edit-service.component';
@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.css']
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
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private service:ServiceService,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
 //this.serviceList=this.service.getAllService();
 this.readService();
 console.log(this.readService);
  }
 
//delete service
deleteService(serviceId: number){
  if (confirm("Are you sure you want to delete this ?")){
  this.service.deleteServiceById(serviceId).subscribe(res=>{
    this.readService();
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
        this.massage='Cannot List Services, Please Contact System Administrator','Ouch!!';
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


  //router to edit
  routerEditService(Service_ID: number, ServiceName: string, ServiceDescription: string ){
    const dialog = new MatDialogConfig();
    dialog.disableClose = true;
    dialog.width = 'auto';
    dialog.height = 'auto';
    dialog.data = {add: 'yes'};
    const dialogReference = this.dialog.open(
      EditServiceComponent,
      {
       data: { Service_ID: Service_ID,  ServiceName: ServiceName, ServiceDescription: ServiceDescription }
     });

   dialogReference.afterClosed().subscribe((res) => {
     if (res == 'add') {
       this.notificationService.successToaster('Service Edited', 'Hooray');
       this.readService();
     }
   });
 }


  Close() {
    this.dialog.closeAll();
  }

}
