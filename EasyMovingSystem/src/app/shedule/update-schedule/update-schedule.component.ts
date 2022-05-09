import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ScheduleTypeRequest } from 'src/app/schedule';
import { ScheduleService } from 'src/app/services/schedule.service';

@Component({
  selector: 'app-update-schedule',
  templateUrl: './update-schedule.component.html',
  styleUrls: ['./update-schedule.component.css']
})
export class UpdateScheduleComponent implements OnInit {
  form!: FormGroup;
  constructor(private Service : ScheduleService, private route: Router ) { }


  ngOnInit(): void {
  }
  AddProductTypeform = new FormGroup({
    name: new FormControl('',[Validators.required, Validators.minLength(3)]),
    description: new FormControl('',[Validators.required, Validators.minLength(3)])
  });
  onSubmit() {
    const Producttype = new ScheduleTypeRequest();
    Producttype.date = this.AddProductTypeform.get('name')?.value;
    Producttype.endTime = this.AddProductTypeform.get('description')?.value;
    this.Service.addSchedule(Producttype).subscribe(
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