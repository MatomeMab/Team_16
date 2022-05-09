import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  condition: boolean = false;
  Loggedin: boolean = false;
  Role: any;
  constructor( private route: Router) {
   }

  ngOnInit(): void {

    this.Role = false;

    if (localStorage.getItem('Role') != null && localStorage.getItem('Role')!.length > 0 ) {
      this.Role = localStorage.getItem('Role');
    }
    
    localStorage.setItem("nav", "nav nav");
    console.log(localStorage.getItem("nav"));

  }
  logout():void
  { 
    localStorage.removeItem("Role");
    //location.reload();
    this.route.navigate(['/login']);
  }

}


 