import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddclientComponent } from './Client/Components/addclient/addclient.component';

const routes: Routes = [
  {path:"addclient",
    component: AddclientComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
