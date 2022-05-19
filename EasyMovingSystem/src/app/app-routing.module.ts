import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TruckComponent } from './Admin/Components/Truck/truck/truck.component';
import { EmployeeComponent } from './Admin/Components/Employee/employee/employee.component';

const routes: Routes = [ 
  {
    path: 'Employee', 
    component: EmployeeComponent
  },
  {path: '', component: TruckComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
