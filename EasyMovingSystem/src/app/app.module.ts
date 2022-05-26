import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
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
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { JobListingComponent } from './Admin/Components/JobListing/job-listing/job-listing.component';
import { MaterialModule } from './Shared/material.module';
import { HttpClientModule } from '@angular/common/http';
import { CreateServiceComponent } from './Admin/Components/Servi/create-service/create-service.component';
import { ServiceListComponent } from './Admin/Components/Servi/service-list/service-list.component';
import { StaticPageComponent } from './LandingPage/static-page/static-page.component';



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
    JobListingComponent,
    CreateServiceComponent,
    ServiceListComponent,
    StaticPageComponent
    
   //navigation
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, BrowserAnimationsModule , 
    
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        MaterialModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule
     
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas:[ CUSTOM_ELEMENTS_SCHEMA ]
 // exports:[ ]
})
export class AppModule { }
