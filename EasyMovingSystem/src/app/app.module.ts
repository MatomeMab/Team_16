import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TruckComponent } from './Components/Trucks/truck/truck.component';
import { UserComponent } from './Components/Users/user/user.component';
import { AssignTruckComponent } from './Components/AssignTrucks/assign-truck/assign-truck.component';
import { CreateSheduleComponent } from './shedule/create-shedule/create-shedule.component';
import { SearchSheduleComponent } from './shedule/search-shedule/search-shedule.component';
import { UpdateScheduleComponent } from './shedule/update-schedule/update-schedule.component';

import { ScheduleModule } from '@syncfusion/ej2-angular-schedule';
import { ButtonModule } from '@syncfusion/ej2-angular-buttons';
import { DayService, WeekService, WorkWeekService, MonthService, AgendaService, MonthAgendaService} from '@syncfusion/ej2-angular-schedule';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar/navbar.component';



@NgModule({
  declarations: [
    AppComponent,
    TruckComponent,
    UserComponent,
    AssignTruckComponent,
    CreateSheduleComponent,
    SearchSheduleComponent,
    UpdateScheduleComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ScheduleModule,
        ButtonModule,
        ReactiveFormsModule,
        HttpClientModule
  ],
  providers: [DayService, 
    WeekService, 
    WorkWeekService, 
    MonthService,
    AgendaService,
    MonthAgendaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
