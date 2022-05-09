import { Component, OnInit } from '@angular/core';
import { Observable,BehaviorSubject } from 'rxjs';
import { ITruck } from 'src/app/Model/ITrucks/itruck';
import { TruckService } from 'src/app/services/truck.service';
import { ToastrService } from 'ngx-toastr';
import{FormBuilder,Validators} from '@angular/forms'

@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.css']
})
export class TruckListComponent implements OnInit {
  //allTrucks!: Observable<ITruck[]>;  
  trucks!:ITruck[];
  allTrucks:any=[];
  dataSaved=false;
  truckForm:any;
  truckIdUpdate=null;
  constructor(public truckService: TruckService,private toastr : ToastrService) { }
  ngOnInit() {
    this.loadAllTrucks();  
  }
  loadAllTrucks() {  
    this.allTrucks = this.truckService.getTruckList(); 
    console.log(this.allTrucks) 
  }  
  getTruckList() {  
   this.truckService.getTruckList();  
  }  
  showForEdit(truck: ITruck) {
    this.truckService.updateTruck(truck).subscribe(()=>{
      this.dataSaved=true;
      this.toastr.success("record deleted successfully");
      this.loadAllTrucks();
      this.truckIdUpdate=null;
      this.truckForm.reset();
    })
  }
  onDelete(id: string) {
    if (confirm('Are you sure to delete this record ?') == true) {
      this.truckService.deleteTruck(id)
      .subscribe(x => {
        this.dataSaved=true;
        this.toastr.warning("Deleted Successfully","Truck Register");
        this.truckService.getTruckList();
        this.loadAllTrucks();
      })
    }
    
  }

  editTruck(truck:ITruck) { 
      
    this.truckService.updateTruck(truck).subscribe(() => {  
      this.dataSaved = true;  
      //this.massage = 'Record Updated Successfully';  
      this.loadAllTrucks();  
      this.truckIdUpdate = null;  
      this.truckForm.reset();  
    }); 
  } 
}

