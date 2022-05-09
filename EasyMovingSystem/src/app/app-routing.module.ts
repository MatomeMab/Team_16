import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateSheduleComponent } from './shedule/create-shedule/create-shedule.component';
import { SearchSheduleComponent } from './shedule/search-shedule/search-shedule.component';
import { UpdateScheduleComponent } from './shedule/update-schedule/update-schedule.component';

const routes: Routes = [
  {
    path:"create",
    component:CreateSheduleComponent
  },
  {
    path:"view",
    component:SearchSheduleComponent
  },
  {
    path:"update",
    component: UpdateScheduleComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
