import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { UserComponent } from './Components/Users/user/user.component';
import { HttpClientModule} from '@angular/common/http';
import { TruckService } from './services/truck.service';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';
import { TruckListComponent } from './Components/TrucksList/truck-list/truck-list.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    TruckComponent,
    UserComponent,
    AssignTruckComponent,
    TruckListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, ReactiveFormsModule, HttpClientModule,  ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
