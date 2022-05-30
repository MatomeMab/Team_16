import { Component, OnInit } from '@angular/core';
import { TruckTruck } from 'src/app/Admin/Services/truck.service';
import { ITruck } from 'src/app/Admin/Models/itruck';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable } from 'rxjs'; 
import { Router } from '@angular/router';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';

@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css']
})
export class TruckComponent implements OnInit {
  dataSaved = false;  
  truckForm: any;  
  allTrucks!: Observable<ITruck[]>;  
  allTruckStatus!:Observable<ITruck[]>;
  allTruckTypes!:Observable<ITruck[]>;
  truckIdUpdate = null;  
  massage ='';  
  alltruckTypesList:any=[];
  DatePosted= new Date();
  selectedStatus!: number;
  constructor(private formbulider: FormBuilder, private truckService:TruckTruck,private router: Router,private notificationsService:NotificationsService) { }

  ngOnInit(): void {
    this.truckForm = this.formbulider.group({  
      TruckType_ID: ['', [Validators.required]],  
      Model: ['', [Validators.required]],  
      Year: ['', [Validators.required]],  
      Colour: ['', [Validators.required]], 
      RegNum:['',[Validators.required]],
      Make:['',[Validators.required]],
      TruckStatus_ID:['',[Validators.required]],
    }); 
    this.allTruckStatus=this.truckService.getAllTruckStatus(); 
    this.allTruckTypes=this.truckService.getAllTruckType();
  }
  Createtruck(truck: ITruck) {  
    if (this.truckIdUpdate == null) {  
      this.truckService.createTruck(truck).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.notificationsService.successToaster('Registration Successful', 'Success');  
          
          this.truckIdUpdate = null;  
          this.truckForm.reset();  
        }  
      ); 
      console.log(truck) 
    } else {  
      truck.Truck_ID = this.truckIdUpdate;  
      this.truckService.updateTruck(truck).subscribe(() => {  
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';  
        this.loadAlltrucks();  
        
        this.truckIdUpdate = null;  
        this.truckForm.reset();  
      });  
      console.log(truck)
    }  
  }   
   
  resetForm() {  
    this.truckForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
  }
  loadAlltrucks() {  
    this.allTrucks = this.truckService.getAllTruck();  
    this.allTruckStatus=this.truckService.getAllTruckStatus();
  }  
  onFormSubmit() {  
    this.dataSaved = false;  
    const truck = this.truckForm.value;  
    this.Createtruck(truck);  
    this.truckForm.reset();  
  } 
  
  
 
}
