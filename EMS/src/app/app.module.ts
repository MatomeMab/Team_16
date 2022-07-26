import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import{MaterialModule} from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import{ToastrModule} from 'ngx-toastr';


//Client
import{ReqQuoteComponent} from './Client/Components/RequestQuotations/req-quote/req-quote.component';
import { ListQuoteComponent } from './Client/Components/RequestQuotations/list-quote/list-quote.component';
import { ReadQuoteComponent } from './Client/Components/RequestQuotations/read-quote/read-quote.component';

//User
import { LoginComponent } from './Account/User/login/login.component';
import { RegisterComponent } from './Account/User/register/register.component';
import { CreateScheduleComponent } from './Admin/Components/Schedules/create-schedule/create-schedule.component';
import{ForgotPasswordComponent} from './Account/User/forgot-password/forgot-password.component';
import { NewPasswordComponent } from './Account/User/new-password/new-password.component';
import { OTPComponent } from './Account/User/otp/otp.component';

//Common
import { CreateApplicationComponent } from './JobApplications/create-application/create-application.component';
import{NavigationComponent} from './Account/User/navigation/navigation.component';
import { HomeComponent } from './home/home.component';

//Admin Components
import { UpdateEmployeeComponent } from './Admin/Components/Employees/update-employee/update-employee.component';
import{CreateEmployeeComponent} from './Admin/Components/Employees/create-employee/create-employee.component';
import {EmployeeListComponent} from './Admin/Components/Employees/employee-list/employee-list.component';
import { RequestedQuoteListComponent } from './Admin/Components/Quotations/requested-quote-list/requested-quote-list.component';
import { ViewReqQuoteComponent } from './Admin/Components/Quotations/view-req-quote/view-req-quote.component';
import { QuoteListComponent } from './Admin/Components/Quotations/quote-list/quote-list.component';
import { TruckListComponent } from './Admin/Components/Trucks/truck-list/truck-list.component';
import { CreateTruckComponent } from './Admin/Components/Trucks/create-truck/create-truck.component';
import { UpdateTruckComponent } from './Admin/Components/Trucks/update-truck/update-truck.component';
import{CreateEmployeeTypeComponent} from './Admin/Components/EmployeeTypes/create-employee-type/create-employee-type.component';
import { EmployeeTypeListComponent } from './Admin/Components/EmployeeTypes/employee-type-list/employee-type-list.component';
import { CreateServiceComponent } from './Admin/Components/Services/create-service/create-service.component';
import { ServiceListComponent } from './Admin/Components/Services/service-list/service-list.component';
import { UpdateServiceComponent } from './Admin/Components/Services/update-service/update-service.component';
import { CreateJobComponent } from './Admin/Components/JobOpportunities/create-job/create-job.component';
import { UpdateJobComponent } from './Admin/Components/JobOpportunities/update-job/update-job.component';
import { JobListComponent } from './Admin/Components/JobOpportunities/job-list/job-list.component';

//for navigaation
import { FlexLayoutModule } from '@angular/flex-layout';
import { UpdateEmployeeTypeComponent } from './Admin/Components/EmployeeTypes/update-employee-type/update-employee-type.component';
@NgModule({
  declarations: [
    AppComponent,
    CreateApplicationComponent,
    NavigationComponent,
    HomeComponent,
    //Users
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    NewPasswordComponent,
    OTPComponent,

    //Admin
    UpdateEmployeeComponent,
    CreateScheduleComponent,
    CreateEmployeeComponent,
    EmployeeListComponent,
    RequestedQuoteListComponent,
    ViewReqQuoteComponent,
    QuoteListComponent,
    CreateEmployeeComponent,
    UpdateEmployeeComponent,
    EmployeeListComponent,
    CreateServiceComponent,
    UpdateServiceComponent,
    ServiceListComponent,
    EmployeeTypeListComponent,
    CreateEmployeeTypeComponent,
    UpdateEmployeeTypeComponent,
    TruckListComponent,
    CreateTruckComponent,
    UpdateTruckComponent,
    JobListComponent,
    CreateJobComponent,
    UpdateJobComponent,
    //Employee

    //Client
    ReqQuoteComponent,
    ListQuoteComponent,
    ReadQuoteComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FlexLayoutModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-full-width'
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
