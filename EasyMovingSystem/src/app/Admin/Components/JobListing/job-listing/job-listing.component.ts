import { Component, OnInit } from '@angular/core';
import { MatDialog,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobListComponent } from '../job-list/job-list.component';
@Component({
  selector: 'app-job-listing',
  templateUrl: './job-listing.component.html',
  styleUrls: ['./job-listing.component.css']
})
export class JobListingComponent implements OnInit {

  constructor(private dialog:MatDialog) { }

  ngOnInit(): void {
  }
  openDialog() {
    this.dialog.open(JobListComponent, {
      width:'30%'
    });
  }
}
