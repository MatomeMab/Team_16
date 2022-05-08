import { Component, OnInit } from '@angular/core';
import { Observable,BehaviorSubject } from 'rxjs';
import { ITruck } from 'src/app/Model/ITrucks/itruck';
import { TruckService } from 'src/app/services/truck.service';
import { ToastrService } from 'ngx-toastr'
@Component({
  selector: 'app-truck-list',
  templateUrl: './truck-list.component.html',
  styleUrls: ['./truck-list.component.css']
})
export class TruckListComponent implements OnInit {
  allTrucks!: Observable<ITruck[]>;  
  constructor(public truckService: TruckService,private toastr : ToastrService) { }
  ngOnInit() {
    this.loadAllTrucks();  
  }
  loadAllTrucks() {  
    this.allTrucks = this.truckService.getTruckList();  
  }  
  getTruckList() {  
   this.truckService.getTruckList();  
  }  
  showForEdit(truck: ITruck) {
    this.truckService.selectedTruck = Object.assign({}, truck);;
  }
  onDelete(id: string) {
    if (confirm('Are you sure to delete this record ?') == true) {
      this.truckService.deleteTruck(id)
      .subscribe(x => {
        this.truckService.getTruckList();
        this.toastr.warning("Deleted Successfully","Truck Register");
      })
    }
  }

}
