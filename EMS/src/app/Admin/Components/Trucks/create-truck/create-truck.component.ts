import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ITruck } from 'src/app/Admin/Settings/Models/iadmin';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
@Component({
  selector: 'app-create-truck',
  templateUrl: './create-truck.component.html',
  styleUrls: ['./create-truck.component.scss']
})
export class CreateTruckComponent implements OnInit {
  dataSaved = false;  
  truckForm: any;  
  allTrucks!: Observable<ITruck[]>;  
  allTruckStatus!:Observable<ITruck[]>;
  allTruckTypes!:Observable<ITruck[]>;
  truckIdUpdate = null;  
  alltruckTypesList:any=[];
  DatePosted= new Date();
  selectedStatus!: number;
 
  constructor(private formbulider: FormBuilder, private truckService:AdminService,private router: Router,private notificationsService:NotificationsService) { }

  ngOnInit(): void {
    this.truckForm = this.formbulider.group({  
      TruckType_ID: ['', [Validators.required]],  
      Model: ['', [Validators.required]],  
      Year: ['', [Validators.required]],  
      Colour: ['', [Validators.required]], 
      RegNum:['',[Validators.required]],
      Make:['',[Validators.required]],
      
      
    }); 
    this.allTruckStatus=this.truckService.getAllTruckStatus(); 
    this.allTruckTypes=this.truckService.getAllTruckType();
  }
  Createtruck(truck: ITruck) {  
    if (this.truckIdUpdate == null) {  
      this.truckService.createTruck(truck).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.notificationsService.successToaster('Truck added Successfully', 'Success');  
          
          this.truckIdUpdate = null;  
          this.truckForm.reset();  
        }  
      ); 
      console.log(truck) 
    } else {  
      truck.Truck_ID = this.truckIdUpdate;  
      this.truckService.updateTruck(truck).subscribe(() => {  
        this.dataSaved = true;  
        this.notificationsService.successToaster('Record Updated Successfully','');  
        this.loadAlltrucks();  
        
        this.truckIdUpdate = null;  
        this.truckForm.reset();  
      });  
      console.log(truck)
    }  
  }   
   
  resetForm() {  
    this.truckForm.reset();  
    this.dataSaved = false;  
  }
  loadAlltrucks() {  
    this.allTrucks = this.truckService.getAllTruck();  
    this.allTruckStatus=this.truckService.getAllTruckStatus();
    this.allTruckTypes=this.truckService.getAllTruckType();
  }  
  onFormSubmit() {  
    this.dataSaved = false;  
    const truck = this.truckForm.value;  
    this.Createtruck(truck);  
    this.truckForm.reset();  
  } 
  
  
 
}
