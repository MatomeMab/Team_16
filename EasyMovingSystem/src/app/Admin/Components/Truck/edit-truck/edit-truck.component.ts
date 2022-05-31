import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import{ITruck} from 'src/app/Admin/Models/itruck';
import{TruckTruck} from 'src/app/Admin/Services/truck.service';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-edit-truck',
  templateUrl: './edit-truck.component.html',
  styleUrls: ['./edit-truck.component.css']
})
export class EditTruckComponent implements OnInit {
  truckForm!:FormGroup;
  truck!:ITruck;
  dataSaved = false;  
  massage!:any;
  allTruckStatus!:Observable<ITruck[]>;
  allTruckTypes!:Observable<ITruck[]>;
  constructor(private Truck: TruckTruck,
    public dialog: MatDialog, 
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EditTruckComponent>,
    private notificationService: NotificationsService,
    @Inject(MAT_DIALOG_DATA)
    public data:any,) { }

  ngOnInit(): void {
    this.createForm();
  this.refreshForm();
  }
  Close(){
    this.dialog.closeAll();
  }
  
  createForm() {
      this.truckForm = this.formBuilder.group({  
        ServiceName: [this.data.ServiceName, [Validators.maxLength(20), Validators.minLength(5),Validators.required]],
        ServiceDescription: [this.data.ServiceDescription, [ Validators.maxLength(20), Validators.minLength(10),Validators.required]] ,
        TruckType_ID: [this.data.TruckType_ID, [Validators.required]],  
        Model: [this.data.Model, [Validators.maxLength(20), Validators.minLength(5),Validators.required]],  
        Year: [this.data.Year, [Validators.maxLength(4), Validators.required]],  
        Colour: [this.data.Colour, [Validators.maxLength(50), Validators.minLength(4),Validators.required]], 
        RegNum:[this.data.RegNum,[Validators.maxLength(10), Validators.minLength(5),Validators.required]],
        Make:[this.data.Make,[Validators.maxLength(20), Validators.minLength(4),Validators.required]],
        TruckStatus_ID:[this.data.TruckStatus_ID,[Validators.required]],
      });  
      this.allTruckStatus=this.Truck.getAllTruckStatus();
      this.allTruckTypes=this.Truck.getAllTruckType();
  }
  OnSubmit() {
    console.log('Hello')
    if (this.truckForm.valid) {
      const truck: ITruck = this.truckForm.value;
      truck.Truck_ID = this.data.Truck_ID;
      this.Truck.updateTruck(truck).subscribe(res => {
        this.dataSaved = true; 
        this.massage=this.notificationService.successToaster('You have successfuly added the service','Horaay')
        this.refreshForm();
        this.dialogRef.close('add');
      },
      err => {
        if (err.status == 401) {
          this.notificationService.failToaster('Unable to Edit Vehicle', 'Error');
        }
        else if(err.status != 201){
          this.notificationService.errorToaster('Server Error, Please Contact System Administrator','Error');
        }
        else{
          console.log(err);
        }
      }
    );
  }
  }
  refreshForm(){
    this.truck = {
      Truck_ID: 0,
      TruckType_ID:0,
      TruckStatus_ID:0,
      TruckStatusName:'',
      TruckTypeName:'',
      Model:'',
      Year:'',
      Colour:'',
      RegNum:'',
      Make:''
      
    }
  }
  resetForm() {  
    this.truckForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
  }
}
