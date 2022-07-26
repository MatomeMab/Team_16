import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first, Observable } from 'rxjs';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { IService } from 'src/app/Admin/Settings/Models/iadmin';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';

@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  styleUrls: ['./create-service.component.scss']
})
export class CreateServiceComponent implements OnInit {

  dataSaved = false;  
  serviceForm: any; 
  colorControl = new FormControl('accent'); 
  allServices!: Observable<AdminService[]>;  
  serviceIdUpdate = null;  
  massage !:any;  
  isAddMode!: boolean;
  id!:string;
  loading!:boolean;
  submitted!:boolean;
  constructor(private formbulider: FormBuilder,private service:AdminService, private router: Router, private route: ActivatedRoute,private notificationService:NotificationsService) { }

  ngOnInit(): void {
    this.serviceForm = this.formbulider.group({  
      ServiceName: ['', [Validators.required]],  
      ServiceDescription: ['', [Validators.required]],  
    });  
  }
  
  CreateService(service: IService) {  
    if (this.serviceIdUpdate == null) {  
      this.service.createService(service).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = this.notificationService.successToaster('Successfully added service','Hooray!!');  
          this.serviceForm.reset();  
        }  
      ); 
      console.log(service) 
    }
  }   
   
  resetForm() {  
    this.serviceForm.reset();  
    this.massage = '';  
    this.dataSaved = false;  
  }
  onFormSubmit() {  
    this.dataSaved = false; 
    const service=this.serviceForm.value;
    this.CreateService(service);   
    this.serviceForm.reset();  
  }

  error_messages = {
    ServiceName: [
      { type: 'minlength', message: 'Employee Type must be more than 1 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type name is required' }
    ],
    ServiceDescription: [
      { type: 'minlength', message: 'Employee Type must be more than 1 character' },
      { type: 'maxlength', message: 'Employee Type must be less than 30 characters' },
      { type: 'required', message: 'Employee Type description is required' }
    ]
  }

}
