import { Component, OnInit } from '@angular/core';
import { FormBuilder, NumberValueAccessor, Validators } from '@angular/forms';  
import { Observable } from 'rxjs';  
import { ServiceService } from 'src/app/Admin/Services/service.service';
import { IService } from 'src/app/Admin/Models/iservice';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreateServiceComponent } from '../create-service/create-service.component';

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
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private service:ServiceService,private router: Router) { }

  ngOnInit(): void {
 //this.serviceList=this.service.getAllService();
 this.readService();
 console.log(this.readService);
  }
  loadServiceToEdit(serviceId: number) {  
    this.service.getServiceById(serviceId).subscribe(service=> {  
      this.massage = '';  
      this.dataSaved = false;  
      //this.serviceIdUpdate == serviceId.Service_ID;  
      this.serviceForm.controls['ServiceName'].setValue(service.ServiceName);  
      this.serviceForm.controls['ServiceDescription'].setValue(service.ServiceDescription);  
      
        
    });  
  
  }

  deleteService(serviceId: number) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.service.deleteServiceById(serviceId).subscribe(() => {  
      this.dataSaved = true;  
      this.massage = 'Record Deleted Succefully';  
      this.service.getAllService();  
      this.serviceIdUpdate == null;  
      this.serviceForm.reset();  
  
    });  
    
  }  
}  

updateService(service:IService){
  service.Service_ID = this.serviceIdUpdate;  
      this.service.updateService(service).subscribe(() => {  
        this.router.navigate(["/createService"])
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';   
        this.serviceIdUpdate == null;  
        this.serviceForm.reset();  
      });  
}
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog() {
    this.dialog.open(CreateServiceComponent, {
      width:'20%'
    });
  }
  onFormSubmit() {  
    this.dataSaved = false;  
    const service = this.serviceForm.value;  
    this.updateService(service);  
    this.serviceForm.reset();  
  } 
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
    console.log(this.dataSource.filter)
  }

  readService(){
    this.services$.subscribe((res)=>{
      this.services = res;
      this.dataSource.data = this.services; //for search
      console.log(res);
    },
    err=>{
      if(err.status != 201){
        this.massage='Cannot List Employees, Please Contact System Administrator','Error';
      }
    })
  }
}
