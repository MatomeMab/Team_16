import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceService } from 'src/app/Admin/Services/service.service';
import { FormBuilder, Validators } from '@angular/forms'; 
import { IService } from 'src/app/Admin/Models/iservice';
@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  styleUrls: ['./create-service.component.css']
})
export class CreateServiceComponent implements OnInit {
  dataSaved = false;  
  serviceForm: any;  
  allServices!: Observable<ServiceService[]>;  
  serviceIdUpdate = null;  
  massage ='';  
  isAddMode!: boolean;
  constructor(private formbulider: FormBuilder,private service:ServiceService) { }

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
          this.massage = 'Record saved Successfully';  
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
}
