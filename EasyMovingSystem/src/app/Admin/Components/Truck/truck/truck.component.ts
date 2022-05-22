import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule, Routes} from '@angular/router';
import { TruckService } from 'src/app/Admin/Services/truck.service';
import { TruckType, Truck } from '../../Interfaces/interface';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css']
})
export class TruckComponent implements OnInit {

  truckform!: FormGroup;
  truckTypeList: TruckType[] = [];
  truckType!: TruckType;
  TruckList!: Truck[];
  truck!: Truck;
  

  

  constructor(private truckService: TruckService, 
              private formBuilder: FormBuilder,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.createTruck();
    this.readTruckTypes();
  }

  createTruck(){
    this.truckform = this.formBuilder.group({
     NumberPlate: ['',[Validators.required]],
     Make: ['', [Validators.required]],
     Model: ['', [Validators.required]],
     Colour: ['',[Validators.required]],
     Year: ['', [Validators.required]],
    });
  }

  readTruckTypes() {
    this.truckService.getTruckTypes().subscribe(res => {
      this.truckTypeList = res as TruckType[];
    },
      err => {
        if (err.status != 201) {
          this.notificationService.errorToaster('Cannot Retrieve Truck Types, Try again', 'Error');
        }
      }
    );
  }
  OnSubmit(){
    if (this.truckform.valid) {
      this.truck = this.truckform.value;
      console.log(this.truck);
      /*this.truckService.createTruck(this.truck).subscribe((res) => {
        this.truck = res as Truck;
      },*/(err: HttpErrorResponse) => {
        if (err.status == 406) {
          this.notificationService.failToaster('Registration failed', 'Error');
        }
        else if (err.status == 200) {
          this.notificationService.successToaster('Registration Successful', 'Success')
          this.Close();
        }
      }
    }
  }
  Close() {
    throw new Error('Method not implemented.');
  }

}
