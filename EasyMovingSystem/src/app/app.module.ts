import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Form,ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
<<<<<<< Updated upstream
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
=======
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
import {ToastrModule} from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {MatStepperModule} from '@angular/material/stepper';
import { MatPaginatorModule } from '@angular/material/paginator';
import { EmployeeTypeUpdateComponent } from './Admin/Components/EmployeeType/employee-type-update/employee-type-update.component';
//Navingation 
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
<<<<<<< Updated upstream
    EmployeeTypelistComponent
>>>>>>> Stashed changes
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
=======
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
    EmployeeTypeUpdateComponent
    
    
   //navigation
   
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule, BrowserAnimationsModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatSidenavModule,
    MatDividerModule,
    MatListModule,
    MatTableModule,
    MatFormFieldModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatIconModule,
    MatTabsModule, 
    MatCheckboxModule,
    MatStepperModule,
    MatCheckboxModule,
    ToastrModule.forRoot(),
    HttpClient
    
>>>>>>> Stashed changes
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
