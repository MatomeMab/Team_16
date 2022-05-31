import { Component, OnInit } from '@angular/core';
import { first, Observable } from 'rxjs';
import { ServiceService } from 'src/app/Admin/Services/service.service';
import { FormBuilder, Validators, FormControl } from '@angular/forms'; 
import { IService } from 'src/app/Admin/Models/iservice';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'src/app/Admin/Services/notifications.service';
@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  styleUrls: ['./create-service.component.css']
})
export class CreateServiceComponent implements OnInit {
  dataSaved = false;  
  serviceForm: any; 
  colorControl = new FormControl('accent'); 
  allServices!: Observable<ServiceService[]>;  
  serviceIdUpdate = null;  
  massage !:any;  
  isAddMode!: boolean;
  id!:string;
  loading!:boolean;
  submitted!:boolean;
  constructor(private formbulider: FormBuilder,private service:ServiceService, private router: Router, private route: ActivatedRoute,private notificationService:NotificationsService) { }

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
