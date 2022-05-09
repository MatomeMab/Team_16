import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployeeType } from 'src/app/Model/iemployee-type';
import { EmployeeTypeService } from 'src/app/services/employee-type.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-typelist',
  templateUrl: './employee-typelist.component.html',
  styleUrls: ['./employee-typelist.component.css']
})
export class EmployeeTypelistComponent implements OnInit {
  allEmployeeTypes!: Observable<IEmployeeType[]>; 
  constructor(public employeeTypeService: EmployeeTypeService,private toastr : ToastrService) { }
  ngOnInit() {
    this.loadAllTrucks();  
  }
  loadAllTrucks() {  
    this.allEmployeeTypes = this.employeeTypeService.getEmployeeTypeList();  
  }  
  getTruckList() {  
   this.employeeTypeService.getEmployeeTypeList();  
  }  
  showForEdit(employeeType: IEmployeeType) {
    this.employeeTypeService.selectedEmployeeType = Object.assign({}, employeeType);;
  }
  onDelete(id: string) {
    if (confirm('Are you sure to delete this record ?') == true) {
      this.employeeTypeService.deleteTruck(id)
      .subscribe(x => {
        this.employeeTypeService.getEmployeeTypeList();
        this.toastr.warning("Deleted Successfully","Employee Type Register");
      })
    }
  }

}

