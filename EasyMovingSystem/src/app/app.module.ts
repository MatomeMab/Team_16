import { SharedService } from './shared.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InspectionTypeListComponent } from './Admin/Components/InspectionType/inspection-type-list/inspection-type-list.component';
import { EmployeeTypeListComponent } from './Admin/Components/EmployeeType/employee-type-list/employee-type-list.component';
import { TruckListComponent } from './Admin/Components/Truck/truck-list/truck-list.component';
import { EmployeeListComponent } from './Admin/Components/Employee/employee-list/employee-list.component';
import { EmployeeComponent } from './Admin/Components/Employee/employee/employee.component';
import { InspectionTypeComponent } from './Admin/Components/InspectionType/inspection-type/inspection-type.component';
import { TruckComponent } from './Admin/Components/Truck/truck/truck.component';
import { EmployeeTypeComponent } from './Admin/Components/EmployeeType/employee-type/employee-type.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JobListComponent } from './Admin/Components/JobListing/job-list/job-list.component';
import { QoutationComponent } from './Admin/Components/qoutation/qoutation.component';
import { RequestQouteListComponent } from './Admin/Components/qoutation/request-qoute-list/request-qoute-list.component';
import { SearchBookingComponent } from './Admin/Components/Booking/search-booking/search-booking.component';
import { UpdateBackgroundStatusComponent } from './Admin/Components/update-background-status/update-background-status.component';
import { CapturePaymentComponent } from './Admin/Components/Payment/capture-payment/capture-payment.component';
import { LoginComponent } from './Login/Components/login/login.component';
import { RegisterComponent } from './Login/Components/register/register.component';
import { ForgotPasswordComponent } from './Login/Components/forgot-password/forgot-password.component';
import { NavBarComponent } from './Admin/Components/nav-bar/nav-bar.component';
import { Router, RouterModule} from '@angular/router';
import { MatButtonModule} from '@angular/material/button';
import { MatToolbarModule} from '@angular/material/toolbar';
import { MatSidenavModule} from '@angular/material/sidenav';
import { MatIconModule} from '@angular/material/icon';
import { MatDividerModule} from '@angular/material/divider';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatSelectModule} from '@angular/material/select';
import {MatTableModule} from '@angular/material/table';

import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    InspectionTypeListComponent,
    EmployeeTypeListComponent,
    TruckListComponent,
    EmployeeListComponent,
    EmployeeComponent,
    TruckComponent,
    InspectionTypeComponent,
    EmployeeTypeComponent,
    JobListComponent,
    QoutationComponent,
    RequestQouteListComponent,
    SearchBookingComponent,
    UpdateBackgroundStatusComponent,
    CapturePaymentComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    NavBarComponent,
    
    
   //navigation
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, BrowserAnimationsModule,
    MatButtonModule, MatToolbarModule,
    MatDividerModule,MatSidenavModule,
    MatIconModule, MatCardModule,
    MatFormFieldModule, MatInputModule,
    MatDatepickerModule, MatSelectModule,
    MatTableModule, HttpClientModule,
    FormsModule, ReactiveFormsModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }