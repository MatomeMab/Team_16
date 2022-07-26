import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
@Component({
  selector: 'app-quote-list',
  templateUrl: './quote-list.component.html',
  styleUrls: ['./quote-list.component.scss']
})
export class QuoteListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'client name', 'client surname','client email address', 'Phone Number','From Address','To Address','Additional Notes','Date Created','Description','Amount'];
  public dataSource = new MatTableDataSource<any>();
  constructor() { }

  ngOnInit(): void {
  }
  public myFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLowerCase();
  }
}
