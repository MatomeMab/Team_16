import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs';
import { ClientService } from '../../Service/client.service';

@Component({
  selector: 'app-viewclient',
  templateUrl: './viewclient.component.html',
  styleUrls: ['./viewclient.component.css']
})
export class ViewclientComponent implements OnInit {
  users:any;

  constructor(private clientService: ClientService) {}

  ngOnInit() {
      this.clientService.getAll()
          .pipe()
          .subscribe(users => this.users = users);
  }
}