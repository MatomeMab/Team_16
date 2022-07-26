import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ClientService } from 'src/app/Client/Settings/Services/client.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { FormBuilder } from '@angular/forms';
import { IQuotationRequest } from 'src/app/Client/Settings/Models/iclient';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-list-quote',
  templateUrl: './list-quote.component.html',
  styleUrls: ['./list-quote.component.scss']
})
export class ListQuoteComponent implements OnInit {
  quoteReqList$:Observable<IQuotationRequest[]>=this.service.getRequestedQuotes();
  quoteReqList: IQuotationRequest[]=[];
  displayedColumns: string[] = ['id', 'From Address', 'To Address','Additional Notes'];
  public dataSource = new MatTableDataSource<any>();
  massage!: string;
  constructor(private dialog:MatDialog,private formbulider: FormBuilder, private service:ClientService,private router: Router, private notificationService:NotificationsService) { }

  ngOnInit(): void {
    this.readService();
  }
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
  }
  readService(){
    this.quoteReqList$.subscribe((res)=>{
      this.quoteReqList = res;
      this.dataSource.data = this.quoteReqList; //for search
      console.log(res);
    },
    err=>{
      if(err.status != 201){
        this.massage='Cannot List Services, Please Contact System Administrator','Ouch!!';
      }
    })
  }
}
