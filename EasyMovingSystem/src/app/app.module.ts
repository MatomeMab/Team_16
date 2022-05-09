import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { UserComponent } from './Components/Users/user/user.component';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';
<<<<<<< Updated upstream
=======
import { TruckListComponent } from './Components/TrucksList/truck-list/truck-list.component';
import { ToastrModule } from 'ngx-toastr';
import { EmployeeTypeComponent } from './Components/EmployeeTypes/employee-type/employee-type.component';
import { EmployeeTypelistComponent } from './Components/EmployeeTypeList/employee-typelist/employee-typelist.component';
>>>>>>> Stashed changes

@NgModule({
  declarations: [
    AppComponent,
    TruckComponent,
    UserComponent,
<<<<<<< Updated upstream
    AssignTruckComponent
=======
    AssignTruckComponent,
    TruckListComponent,
    EmployeeTypeComponent,
    EmployeeTypelistComponent
>>>>>>> Stashed changes
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
