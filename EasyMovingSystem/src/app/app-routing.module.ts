import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';
import { TruckListComponent } from './Components/TrucksList/truck-list/truck-list.component';
const routes: Routes = [
  { path: '', component: TruckComponent},
  {path: 'AssignTruck', component:AssignTruckComponent},
  {path:'TruckList',component:TruckListComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
