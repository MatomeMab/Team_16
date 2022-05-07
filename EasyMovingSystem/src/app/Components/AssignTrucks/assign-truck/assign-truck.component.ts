import { Component, OnInit } from '@angular/core';
import { TruckService } from 'src/app/services/truck.service';
;
@Component({
  selector: 'app-assign-truck',
  templateUrl: './assign-truck.component.html',
  styleUrls: ['./assign-truck.component.css']
})
export class AssignTruckComponent implements OnInit {

bookingList:any;
employeeList:any;
truckList:any;
//remove viarables below and import EmployeeService,BookingService
bookingService: any;
employeeService: any;
truckService: any;
//remove vairables above
changeBooking(e:any){
  console.log(e.target.value)
}
changeEmployee(e:any){
  console.log(e.target.value)
}
changeTruck(e:any){
  console.log(e.target.value)
}
  constructor() { }

  ngOnInit(): void {
    this.bookingService.getBookingList().subscribe((data:any)=>{
      this.bookingList=data
    })
    this.employeeService.getEmployeeList().subscribe((data:any)=>{
      this.employeeList=data
    })
    this.truckService.getTruckList().subscribe((data:any)=>{
      this.truckList=data
    })
  }
}
