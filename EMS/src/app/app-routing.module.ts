import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
//User
import { ForgotPasswordComponent } from './Account/User/forgot-password/forgot-password.component';
import { OTPComponent } from './Account/User/otp/otp.component';
import{LoginComponent} from './Account/User/login/login.component';

//Admin Components
import { UpdateEmployeeComponent } from './Admin/Components/Employees/update-employee/update-employee.component';
import{CreateEmployeeComponent} from './Admin/Components/Employees/create-employee/create-employee.component';
import {EmployeeListComponent} from './Admin/Components/Employees/employee-list/employee-list.component';
import { CreateScheduleComponent } from './Admin/Components/Schedules/create-schedule/create-schedule.component';
import { QuoteListComponent } from './Admin/Components/Quotations/quote-list/quote-list.component';
import { TruckListComponent } from './Admin/Components/Trucks/truck-list/truck-list.component';
import { CreateTruckComponent } from './Admin/Components/Trucks/create-truck/create-truck.component';
import { UpdateTruckComponent } from './Admin/Components/Trucks/update-truck/update-truck.component';
import{CreateEmployeeTypeComponent} from './Admin/Components/EmployeeTypes/create-employee-type/create-employee-type.component';
import { EmployeeTypeListComponent } from './Admin/Components/EmployeeTypes/employee-type-list/employee-type-list.component';
import { CreateServiceComponent } from './Admin/Components/Services/create-service/create-service.component';
import { ServiceListComponent } from './Admin/Components/Services/service-list/service-list.component';
import { UpdateServiceComponent } from './Admin/Components/Services/update-service/update-service.component';
//Client
import{ReqQuoteComponent} from './Client/Components/RequestQuotations/req-quote/req-quote.component';
import { ListQuoteComponent } from './Client/Components/RequestQuotations/list-quote/list-quote.component';
import { ReadQuoteComponent } from './Client/Components/RequestQuotations/read-quote/read-quote.component';
import { UpdateEmployeeTypeComponent } from './Admin/Components/EmployeeTypes/update-employee-type/update-employee-type.component';
import { CreateJobComponent } from './Admin/Components/JobOpportunities/create-job/create-job.component';
import { JobListComponent } from './Admin/Components/JobOpportunities/job-list/job-list.component';
import { UpdateJobComponent } from './Admin/Components/JobOpportunities/update-job/update-job.component';

//Employee
const routes: Routes = [
  {path:'',redirectTo:'home',pathMatch:'full'},
  {path:'home',component:HomeComponent},
   //User
  {path:'forgotPassword',component:ForgotPasswordComponent},
  {path:'otp',component:OTPComponent},
  {path:'login',component:LoginComponent},
  //Admin
  {path:'schedule',component:CreateScheduleComponent},
  {path:'registerEmployee',component:CreateEmployeeComponent},
  {path:'employeeList',component:EmployeeListComponent},
  //{path:'updateEmployee',component:UpdateEmployeeComponent},
  {path:'quoteList',component:QuoteListComponent},
  {path:'truckList',component:TruckListComponent},
  {path:'registerTruck',component:CreateTruckComponent},
  //{path:'updateTruck',component:UpdateTruckComponent},
  {path:'addEmployeeType',component:CreateEmployeeTypeComponent},
  //{path:'updateEmployeeType',component:UpdateEmployeeTypeComponent},
  {path:'employeeTypeList',component:EmployeeTypeListComponent},
  {path:'addService',component:CreateServiceComponent},
  //{path:'updateService',component:UpdateServiceComponent},
  {path:'serviceList',component:ServiceListComponent},
  {path:'addJob',component:CreateJobComponent},
  {path:'jobList',component:JobListComponent},
 // {path:'updateJob',component:UpdateJobComponent},
  //Employee

  //Client
  {path:'requestQuotation',component:ReqQuoteComponent},
  {path:'readQuotation',component:ReadQuoteComponent},
  {path:'quotationList',component:ListQuoteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
