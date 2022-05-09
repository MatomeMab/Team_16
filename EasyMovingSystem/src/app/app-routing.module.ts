import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
<<<<<<< Updated upstream
=======
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';
import { TruckListComponent } from './Components/TrucksList/truck-list/truck-list.component';
import { EmployeeTypeComponent } from './Components/EmployeeTypes/employee-type/employee-type.component';
import { EmployeeTypelistComponent } from './Components/EmployeeTypeList/employee-typelist/employee-typelist.component';
const routes: Routes = [
  { path: '', component: TruckComponent },
  { path: 'AssignTruck', component: AssignTruckComponent },
  { path: 'TruckList', component: TruckListComponent },
  { path: 'EmployeeType', component: EmployeeTypeComponent },
  { path: 'EmployeeTypelist', component: EmployeeTypelistComponent }
>>>>>>> Stashed changes

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
