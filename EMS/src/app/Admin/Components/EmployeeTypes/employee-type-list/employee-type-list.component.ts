import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IEmployeeType } from 'src/app/Admin/Settings/Models/iadmin';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';
import { UpdateEmployeeTypeComponent } from '../update-employee-type/update-employee-type.component';
import { FormBuilder } from '@angular/forms';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { CreateEmployeeTypeComponent } from '../create-employee-type/create-employee-type.component';

@Component({
  selector: 'app-employee-type-list',
  templateUrl: './employee-type-list.component.html',
  styleUrls: ['./employee-type-list.component.scss']
})
export class EmployeeTypeListComponent implements OnInit {
  empTypeList!:Observable<IEmployeeType[]>;
  displayedColumns: string[] = ['EmployeeTypeName', 'EmployeeDescription', 'edit','delete'];
  dataSaved = false; 
  public dataSource = new MatTableDataSource<IEmployeeType>();
  employeeTypeForm: any;
  massage ='';
  allEmployeeTypes!: Observable<IEmployeeType[]>;
  employeeTypeIdUpdate!:number;
  employeeType$:Observable<IEmployeeType[]>=this.service.getAllEmployeeType();
  id!: number;
  empTypes: IEmployeeType[]=[];
  constructor(private dialog:MatDialog,private fb: FormBuilder, private service:AdminService,private router: Router, private notificationService:NotificationsService ) { }

  ngOnInit(): void {
  this.readEmployeeType();
  console.log(this.employeeType$);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog() {
    this.dialog.open(CreateEmployeeTypeComponent, {
    });
  }
 
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
    console.log(this.dataSource.filter)
  }

  readEmployeeType(){
    this.employeeType$.subscribe((res)=>{
      this.empTypes = res;
      this.dataSource.data = this.empTypes; //for search
      console.log(res);
    },
    err=>{
      if(err.status != 201){
        this.notificationService.errorToaster('Cannot List Employee Types, Please Contact System Administrator','Error');
      }
    })
  }
  deleteEmployeeType(empTypeId: number){
    if (confirm("Are you sure you want to delete this ?")){
    this.service.deleteEmployeeTypeById(empTypeId).subscribe(res=>{
      this.readEmployeeType();
      console.log(res); 
      this.notificationService.successToaster('Successfully deleted Employee Type','Error')
    },
    (  err: { status: number; })=>{
      if(err.status != 201){
        this.notificationService.errorToaster('Cannot delete Employee Type, Please Contact System Administrator','Error');
      }
      
    }
    );
  }
  }
  
  
  routerEditEmpType(EmployeeType_ID: number, EmployeeTypeName: string, EmployeeDescription: string ){
    const dialog = new MatDialogConfig();
    dialog.disableClose = true;
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = {add: 'yes'};
    const dialogReference = this.dialog.open(
      UpdateEmployeeTypeComponent,
      {
       data: { EmployeeType_ID: EmployeeType_ID,  EmployeeTypeName: EmployeeTypeName, EmployeeDescription: EmployeeDescription }
     });
  
   dialogReference.afterClosed().subscribe((res) => {
     if (res == 'add') {
       this.notificationService.successToaster('Employee Type Edited', 'Hooray');
       this.readEmployeeType();
     }
   });
  }
  
  
  Close() {
    this.dialog.closeAll();
  }
  
  openAddDialog(){
    const dialog = new MatDialogConfig
    dialog.disableClose = true, 
    dialog.width = '50%';
    dialog.height = 'auto';
    dialog.data = { add: 'yes' }
    const dialogReference = this.dialog.open(
      EmployeeTypeListComponent,);
    dialogReference.afterClosed().subscribe((res) => {
      if (res == 'add') {
        this.notificationService.successToaster('Job Added', 'Success')
        this.readEmployeeType();
      }
    });
  }

}
