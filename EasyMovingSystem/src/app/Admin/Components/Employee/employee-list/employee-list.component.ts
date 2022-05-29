import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employeeList$!:Observable<any[]>

  constructor(private service:SharedService) { }
 

  ngOnInit(): void {
    this.employeeList$ = this.service.AllEmployeeDetails();
  }
  

}
