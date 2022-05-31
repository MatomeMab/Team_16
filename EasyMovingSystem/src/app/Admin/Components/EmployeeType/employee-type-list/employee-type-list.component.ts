import { Component, OnInit } from '@angular/core';
import { IEmployeeType } from 'src/app/Admin/Models/iemployee-type';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog,MatDialogConfig,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable} from 'rxjs';
import { FormBuilder, NumberValueAccessor, Validators } from '@angular/forms'; 
import { EmployeeTypeComponent } from '../employee-type/employee-type.component';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { EditEmployeeTypeComponent } from '../edit-employee-type/edit-employee-type.component';
@Component({
  selector: 'app-employee-type-list',
  templateUrl: './employee-type-list.component.html',
  styleUrls: ['./employee-type-list.component.css']
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
  employeeType$:Observable<IEmployeeType[]>=this.empTypeService.getAllEmployeeType();
  id!: number;
  empTypes: IEmployeeType[]=[];
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private empTypeService:EmployeeTypeService,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
 //this.serviceList=this.service.getAllService();
 this.readEmployeeType();
  }
 
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog() {
    this.dialog.open(EmployeeTypeComponent, {
      width:'80%'
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
        this.massage='Cannot List Employee Types, Please Contact System Administrator','Error';
      }
    })
  }

//new delete

deleteEmployeeType(empTypeId: number){
  if (confirm("Are you sure you want to delete this ?")){
  this.empTypeService.deleteEmployeeTypeById(empTypeId).subscribe(res=>{
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
//new update
/*updateEmployeeType(employeeId: number){
  const dialog = new MatDialogConfig
  dialog.disableClose = true,
  dialog.width = '50%';
  dialog.height = 'auto';
  dialog.data = { add: 'yes' }
  const dialogReference = this.dialog.open(
    EmployeeTypeComponent,
    {
      data: employeeId
    });
  dialogReference.afterClosed().subscribe((res) => {
    if (res == 'add') {
      this.notificationService.successToaster('Employee Edited', 'Success')
      this.readEmployeeType();
    }
  });
}*/

routerEditEmpType(EmployeeType_ID: number, EmployeeTypeName: string, EmployeeDescription: string ){
  const dialog = new MatDialogConfig();
  dialog.disableClose = true;
  dialog.width = '50%';
  dialog.height = 'auto';
  dialog.data = {add: 'yes'};
  const dialogReference = this.dialog.open(
    EditEmployeeTypeComponent,
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
