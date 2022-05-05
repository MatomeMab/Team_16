import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { UserComponent } from './Components/Users/user/user.component';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';

@NgModule({
  declarations: [
    AppComponent,
    TruckComponent,
    UserComponent,
    AssignTruckComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
