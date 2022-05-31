import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable} from 'rxjs';
import { FormBuilder, NumberValueAccessor, Validators } from '@angular/forms'; 
import { ITruck } from 'src/app/Admin/Models/itruck';
import { TruckTruck } from 'src/app/Admin/Services/truck.service';
import { TruckComponent } from '../truck/truck.component';
import{NotificationsService} from 'src/app/Admin/Services/notifications.service'
import { EditTruckComponent } from '../edit-truck/edit-truck.component';
@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.css']
})
export class TruckListComponent implements OnInit {
  empTypeList!:Observable<ITruck[]>;
  displayedColumns:string[] = ['TruckTypeName','Model','Year','Colour','RegNum','Make','TruckStatusName'];
  dataSaved = false; 
  public dataSource = new MatTableDataSource<ITruck>();
  truckForm: any;
  massage ='';
  allTrucks!: Observable<ITruck[]>;
  truckIdUpdate!:number;
  trucks$:Observable<ITruck[]>=this.truck.getAllTruck();

  empTypes: ITruck[]=[];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private truck:TruckTruck,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
 //this.serviceList=this.service.getAllService();
 this.readTruck();
  }
  /*loadTruckToEdit(truckId: number) {  
    this.truck.getTruckById(truckId).subscribe(truck=> {  
      this.massage = '';  
      this.dataSaved = false;  
      //this.TruckIdUpdate == empTypeId.Truck_ID;  
      this.truckForm.controls['TruckName'].setValue(empType.TruckName);  
      this.truckForm.controls['EmployeeDescription'].setValue(empType.EmployeeDescription);  
      
        
    });  
  
  }*/
  /*routerTruckToEdit(truckId: number){
    const dialog = new MatDialogConfig
    dialog.disableClose = true,
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = { add: 'yes' }
    const dialogReference = this.dialog.open(
      TruckComponent,
      {
        data: truckId
      });
    dialogReference.afterClosed().subscribe((res) => {
      if (res == 'add') {
        this.notificationService.successToaster('Employee Edited', 'Success')
        this.readTruck();
      }
    });
  }*/
  deleteTruck(truckId: number) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.truck.deleteTruckById(truckId).subscribe(() => {  
      this.dataSaved = true;  
      this.massage = 'Record Deleted Succefully';  
      this.truck.getAllTruck();  
      this.truckIdUpdate == null;  
      this.truckForm.reset();  
  
    });  
    
  }  
}  

updateTruck(truck:ITruck){
  truck.Truck_ID = this.truckIdUpdate;  
      this.truck.updateTruck(truck).subscribe(() => {  
        this.router.navigate(["/createService"])
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';   
        this.truckIdUpdate == null;  
        this.truckForm.reset();  
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
    this.dialog.open(TruckComponent, {
      width:'80%'
    });
  }
  onFormSubmit() {  
    this.dataSaved = false;  
    const employee = this.truckForm.value;  
    this.updateTruck(employee);  
    this.truckForm.reset();  
  } 
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
    console.log(this.dataSource.filter)
  }

  readTruck(){
    this.trucks$.subscribe((res)=>{
      this.empTypes = res;
      this.dataSource.data = this.empTypes; //for search
      console.log(res);
    },
    err=>{
      if(err.status != 201){
        this.massage='Cannot List Trucks, Please Contact System Administrator','Error';
      }
    })
  }
  routerTruckToEdit(TruckType_ID: number, Model: string, Year: string ,Colour:string,RegNum:string,Make:string,TruckStatus_ID:string){
    const dialog = new MatDialogConfig();
    dialog.disableClose = true;
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = {add: 'yes'};
    const dialogReference = this.dialog.open(
      EditTruckComponent,
      {
       data: { TruckType_ID: TruckType_ID,  Model: Model, Year: Year,Colour:Colour,RegNum:RegNum ,Make:Make,TruckStatus_ID:TruckStatus_ID}
     });

   dialogReference.afterClosed().subscribe((res) => {
     if (res == 'add') {
       this.notificationService.successToaster('Truck Succesfully Edited', 'Hooray');
       this.readTruck();
     }
   });
 }

}
