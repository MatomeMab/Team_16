import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ScheduleService } from 'src/app/services/schedule.service';
import { Schedule, ScheduleTypeRequest } from 'src/app/schedule';

@Component({
  selector: 'app-create-shedule',
  templateUrl: './create-shedule.component.html',
  styleUrls: ['./create-shedule.component.css']
})
export class CreateSheduleComponent implements OnInit {
  form!: FormGroup;
  ScheduleC!: Schedule[];
  constructor(private Service : ScheduleService, private route: Router ) { }


  ngOnInit(): void {
    this.Service.getSchedule().
    subscribe(
      data=>{
        this.ScheduleC = data;
      },
      err=>{
        console.log(err);
      }
    );
  }

  AddScheduleform = new FormGroup({
    name: new FormControl('',[Validators.required, Validators.minLength(3)]),
    date: new FormControl('',[Validators.required]),
    etime: new FormControl('',[Validators.required]),
    time: new FormControl('',[Validators.required])
  });

  onSubmit() {
    const Scheduletype = new ScheduleTypeRequest();
    Scheduletype.date = this.AddScheduleform.get('name')?.value;
    Scheduletype.endTime = this.AddScheduleform.get('description')?.value;
    this.Service.addSchedule(Scheduletype).subscribe(
      result=>{
        if(result.status == 200)
        {
          this.route.navigate(['viewtype']);
        }
      },
      err=>{
        alert(err.message);
      });
  }
  Close(){
    this.route.navigate(['viewtype']);
  }
}
