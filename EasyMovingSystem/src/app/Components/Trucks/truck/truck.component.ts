import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { TruckService } from 'src/app/services/truck.service';
import { ITruck } from 'src/app/Model/ITrucks/itruck';
import { HttpClient } from '@angular/common/http';
//import { Interface } from 'readline';


@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css']
})
export class TruckComponent implements OnInit {
  truckIdUpdate = null;  
  dataSaved = false;
  //truckForm: any;
  allTrucks: any;  
  truckForm: any;  
  //allTrucks: Observable<ITruck[]>;  
  massage = null; 
  constructor(private formBuilder: FormBuilder, private truckService: TruckService,httpClient:HttpClient) {
    this.truckService.getTruckList().subscribe(data => {
       this.allTrucks = data;
       
    });
   }

  ngOnInit(): void {
    this.truckForm = this.formBuilder.group({
       Make:["",[Validators.required]],
       Model:["",[Validators.required]],
       Year:["",[Validators.required]],
       NumberPlate:["",[Validators.required]],
       Colour:["",[Validators.required]]
    }); 
  }
  loadAllTrucks() {  
    this.allTrucks= this.truckService.getTruckList();  
  }  
  onFormSubmit(){
    this.dataSaved = false;
    const truck = this.truckForm.value;
    //this.addTruck(truck);
    this.truckForm.reset();
  }
  addTruck(truck: ITruck) {  
    if (this.truckIdUpdate == null) {  
      this.truckService.addTruck(truck).subscribe(  
        () => {  
          this.dataSaved = true;  
         // this.massage = 'Record saved Successfully';  
          this.loadAllTrucks();  
          this.truckIdUpdate = null;  
          this.truckForm.reset();  
        }  
      );  
    } else {  
      truck.Truck_ID= this.truckIdUpdate;  
      this.truckService.updateTruck(truck).subscribe(() => {  
        this.dataSaved = true;  
        //this.massage = 'Record Updated Successfully';  
        this.loadAllTrucks();  
        this.truckIdUpdate = null;  
        this.truckForm.reset();  
      });  
    }  
  }   
    
  deleteTruck(truckId: string) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.truckService.deleteTruck(truckId).subscribe(() => {  
      this.dataSaved = true;  
      //this.massage = 'Record Deleted Succefully';  
      this.loadAllTrucks();  
      this.truckIdUpdate = null;  
      this.truckForm.reset();  
  
    });  
  }  
}  

}
