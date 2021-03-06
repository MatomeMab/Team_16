import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TruckComponent } from './Admin/Components/Truck/truck/truck.component';
import { EmployeeComponent } from './Admin/Components/Employee/employee/employee.component';
import { EmployeeTypeComponent } from './Admin/Components/EmployeeType/employee-type/employee-type.component';
import { EmployeeListComponent } from './Admin/Components/Employee/employee-list/employee-list.component';
import { TruckListComponent } from './Admin/Components/Truck/truck-list/truck-list.component';

const routes: Routes = [ 
  {
    path: 'Employee', 
    component: EmployeeComponent
  },
  {
    path: 'EmployeeList', 
    component: EmployeeListComponent
  },
  { 
    path: 'Truck', 
    component: TruckComponent
  },
  { 
    path: 'TruckList', 
    component: TruckListComponent
  },
  {
    path: 'EmployeeType', 
    component: EmployeeTypeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
