import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TruckComponent } from './Components/Trucks/truck/truck.component';

const routes: Routes = [
  { path: '', component: TruckComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
