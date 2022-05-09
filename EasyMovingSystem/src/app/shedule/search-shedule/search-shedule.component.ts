import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { ScheduleService } from 'src/app/services/schedule.service';
import { Schedule, ScheduleResponse } from 'src/app/schedule';

@Component({
  selector: 'app-search-shedule',
  templateUrl: './search-shedule.component.html',
  styleUrls: ['./search-shedule.component.css']
})
export class SearchSheduleComponent implements OnInit {
  ScheduleR: ScheduleResponse[]=[];
  Schedule: Schedule[] =[];
  constructor(private Service : ScheduleService, private route: Router ) { }
  form!: FormGroup;
  ngOnInit(): void {
    this.Service.getSchedule().subscribe(
      result=>{
        this.Schedule = result;
        
        let ref =  this.Schedule.map(s => {
          this.Service.getEmployeeTypeById(s.sheduleId).subscribe(
            result => {
              
              result.map(type => {
                this.ScheduleR.push(type);
              })
              
            },
            err=>{
               alert(err.message);
            });
        });
      },
      err=>{
         alert(err.message);
      });
  
    
  }
  Edit(ID: string)
  {
    localStorage.setItem("ProductTypeID",ID)
    this.route.navigate(['/updatetype'])
  }


}
