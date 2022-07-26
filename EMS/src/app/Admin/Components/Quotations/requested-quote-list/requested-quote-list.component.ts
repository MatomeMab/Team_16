import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
@Component({
  selector: 'app-requested-quote-list',
  templateUrl: './requested-quote-list.component.html',
  styleUrls: ['./requested-quote-list.component.scss']
})
export class RequestedQuoteListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'client name', 'client surname','client email address', 'booking status',  'view'];
  public dataSource = new MatTableDataSource<any>();
  constructor() { }

  ngOnInit(): void {
  }
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
  }

}
