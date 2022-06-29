import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isLoggedIn$!: Observable<boolean>;
  value$: any | null;
  //loggedId; 
  constructor() { }

  ngOnInit(): void {
    this.value$ = localStorage.getItem('userRole')
    console.log(this.value$, "side nav")
  }

}
