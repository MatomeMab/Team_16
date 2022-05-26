import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
import { EmployeeTypeUpdateComponent } from '../employee-type-update/employee-type-update.component';
import { EmployeeTypeComponent } from '../employee-type/employee-type.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmployeeTypeService } from 'src/app/Admin/Services/employee-type.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Observable } from 'rxjs';
import { EmployeeType } from 'src/app/interfaces/employee-type';

@Component({
  selector: 'app-employee-type-list',
  templateUrl: './employee-type-list.component.html',
  styleUrls: ['./employee-type-list.component.css']
})
export class EmployeeTypeListComponent implements OnInit {

  employeeTypeList: EmployeeType[] = [];
  employeeTypes$: Observable<EmployeeType[]> = this.employeeTypeService.getEmployeeTypes();
  employeeType: EmployeeType

  displayedColumns: string[] = ['EmployeeTypeName', 'EmployeeTypeDescription', 'edit', 'delete'];
  public dataSource = new MatTableDataSource<EmployeeType>();

  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
    console.log(this.dataSource.filter)
  }

  constructor(
    public dialog: MatDialog,
    private employeeTypeService: EmployeeTypeService,
    private notificationsService: NotificationsService,) { }


    ngOnInit(): void {
      this.GetEmployeeTypes();
      this.refreshForm();
    }

    refreshForm() {
      this.employeeType = {
        EmployeeType_ID: 0,
        EmployeeTypeName: '',
        EmployeeDescription:''
      }
    }

    Close() {
      this.dialog.closeAll();
    }

    GetEmployeeTypes(){
      this.employeeTypes$.subscribe(res=>{
        if(res){
          this.employeeTypeList = res;
          this.dataSource.data = this.employeeTypeList;
          console.log(res);
        }
      });
    }

    DeleteEmployeeType(id:number){
      console.log(id);
      this.employeeTypeService.DeleteEmployeeType(id).subscribe((res)=>{
          this.notificationsService.successToaster('Employee Type Deleted', 'Success');
          this.GetEmployeeTypes();
      });

    }

    routerEditEmployeeTypes(EmployeeType_ID:number, EmployeeTypeName:string, EmployeeTypeDescription:string){
      const dialog=new MatDialogConfig();
      dialog.disableClose=true;
      dialog.width='auto';
      dialog.height='auto';
        dialog.data={add:'yes'}
      const dialogReference=this.dialog.open(
        EmployeeTypeUpdateComponent,
        {
          data:{EmployeeType_ID:EmployeeType_ID, EmployeeTypeName:EmployeeTypeName, EmployeeTypeDescription:EmployeeTypeDescription}
        }
        );
        dialogReference.afterClosed().subscribe((res)=>{
          if (res == 'add'){
            this.notificationsService.successToaster('Employee Type Edited', 'Success');
            this.GetEmployeeTypes();
          }
        });
      }



  routerAddEmployeeTypes(){
    const dialog = new MatDialogConfig();
    dialog.disableClose = true;
    dialog.width = 'auto';
    dialog.height = 'auto',
    dialog.data = { add: 'yes' }
    const dialogReference = this.dialog.open(
      EmployeeTypeComponent,
      dialog
    );
  dialogReference.afterClosed().subscribe((res)=>{
    if(res == 'add'){
      this.notificationsService.successToaster('Employee Type Added', 'Success');
      this.GetEmployeeTypes();
    }
  });

  }

}
