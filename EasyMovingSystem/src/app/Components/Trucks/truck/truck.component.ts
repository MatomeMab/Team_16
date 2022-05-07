import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { TruckService } from 'src/app/services/truck.service';
import { Truck } from 'src/app/class/truck';
//import { Interface } from 'readline';


@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css']
})
export class TruckComponent implements OnInit {
  truckIdUpdate = null;  
  dataSaved = false;
  truckForm: any;
  //allTrucks: Observable<Truck[]>;

  constructor(private formBuilder: FormBuilder, private truckService: TruckService) {
    //this.truckService.getAllTrucks().subscribe(data => {
       //this.allTrucks = data;
       
    //});
   }

  ngOnInit(): void {
    this.truckForm = this.formBuilder.group({
       Make:["",[Validators.required]],
       Model:["",[Validators.required]],
       Year:["",[Validators.required]],
       RegNum:["",[Validators.required]],
       Colour:["",[Validators.required]]
    }); 
  }

  onFormSubmit(){
    this.dataSaved = false;
    const truck = this.truckForm.value;
    //this.CreateTruck(truck);
    this.truckForm.reset();
  }

    
  

}
