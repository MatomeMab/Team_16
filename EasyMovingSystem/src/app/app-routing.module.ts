import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './Admin/Components/Employee/employee/employee.component';
import { EmployeeTypeComponent } from './Admin/Components/EmployeeType/employee-type/employee-type.component';
import { InspectionTypeComponent } from './Admin/Components/InspectionType/inspection-type/inspection-type.component';
import { ServiceListComponent } from './Admin/Components/Servi/service-list/service-list.component';
import { CreateServiceComponent } from './Admin/Components/Servi/create-service/create-service.component';

const routes: Routes = [
  {path: 'employees', component: EmployeeComponent},
  {path: 'Service', component: CreateServiceComponent},
  {path:'ServiceList',component:ServiceListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
