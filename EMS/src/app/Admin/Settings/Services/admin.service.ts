import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';    
import { Observable } from 'rxjs'; 
import { map } from "rxjs/operators";
import { IJobListing, IService, ITruck ,IEmployeeType, IJobType, IJobStatus} from '../Models/iadmin';
@Injectable({
  providedIn: 'root'
})
export class AdminService {

  readonly url = 'https://localhost:44355/Api/'
  constructor(private http:HttpClient) { }
//SERVICE
  getAllService(): Observable<IService[]> {  
    return this.http.get<IService[]>(this.url + 'Service/AllServiceDetails/');  
  } 
  getServiceById(Service_ID: number|string): Observable<IService> {  
    return this.http.get<IService>(this.url + 'Service/GetServiceDetailsById/' + Service_ID);  
  }  
  updateService(service: IService): Observable<IService> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<IService>(this.url + 'Service/UpdateServiceDetails/',  
    service, httpOptions);  
  }  
  deleteServiceById(serviceId: number): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + 'Service/DeleteServiceDetails?id=' +serviceId,  
 httpOptions);  
  } 
  createService(service: IService): Observable<IService> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<IService>(this.url + 'Service/InsertServiceDetails/',  
    service, httpOptions);  
  }

  //new update
  
  update(id: string, params: any) {
    return this.http.put(`${this.url}Service/UpdateServiceDetails`, params);
}

//JOB
getAllJob(): Observable<IJobListing[]> {  
  return this.http.get<IJobListing[]>(this.url + 'JobListing/AllJobListingDetails/');  
}  
getJobById(JobListing_ID: number): Observable<IJobListing> {  
  return this.http.get<IJobListing>(this.url + 'JobListing/GetJobDetailsById/' + JobListing_ID);  
}  
createJob(Job: IJobListing): Observable<IJobListing> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.post<IJobListing>(this.url + 'JobListing/InsertJobListingDetails/',  
  Job, httpOptions);  
}  
updateJob(Job: IJobListing): Observable<IJobListing> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.put<IJobListing>(this.url + 'JobListing/UpdateJobListingDetails/',  
  Job, httpOptions);  
}  
deleteJobById(Jobid: number): Observable<number> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.delete<number>(this.url + 'JobListing/DeleteJobListingDetails?id=' +Jobid,  
httpOptions);  
}  

//JOB STATUS
getAllListingStatus(): Observable<IJobStatus[]> {  
  return this.http.get<IJobStatus[]>(this.url + 'JobStatus/AllListingStatusDetails/');  
}  
getListingStatusById(JobStatus_ID: number): Observable<IJobStatus> {  
  return this.http.get<IJobStatus>(this.url + 'JobStatus/GetListingStatusDetailsById/' + JobStatus_ID);  
} 
//JOB TYPE
getAllJobType(): Observable<IJobType[]> {  
  return this.http.get<IJobType[]>(this.url + 'JobType/AllJobTypeDetails/');  
}  

  
getJobTypeById(JobType_ID: number): Observable<IJobType> {  
  return this.http.get<IJobType>(this.url+ 'JobType/GetJobTypeDetailsById/' + JobType_ID);  
} 

//TRUCK
getAllTruck(): Observable<ITruck[]> {  
  return this.http.get<ITruck[]>(this.url + 'Truck/AllTruckDetails/');  
}  
getTruckById(Truck_ID: number): Observable<ITruck> {  
  return this.http.get<ITruck>(this.url + 'Truck/GetTruckDetailsById/' + Truck_ID);  
}  
updateTruck(Truck: ITruck): Observable<ITruck> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.put<ITruck>(this.url + 'Truck/UpdateTruckDetails/',  
  Truck, httpOptions);  
}  
deleteTruckById(TruckId: number): Observable<number> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.delete<number>(this.url + 'Truck/DeleteTruckDetails?id=' +TruckId,  
httpOptions);  
} 
createTruck(Truck: ITruck): Observable<ITruck> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.post<ITruck>(this.url + 'Truck/InsertTruckDetails/',  
  Truck, httpOptions);  
}

//TRUCK STATUS
getAllTruckStatus(): Observable<ITruck[]> {  
  return this.http.get<ITruck[]>(this.url + 'TruckStatus/AllTruckStatusDetails/');  
}
//TRUCK TYPE
getAllTruckType(): Observable<ITruck[]> {  
  return this.http.get<ITruck[]>(this.url + 'TruckType/AllTruckTypeDetails/');  
}

//EMPLOYEE TYPE
 
getAllEmployeeType(): Observable<IEmployeeType[]> {  
  return this.http.get<IEmployeeType[]>(this.url + 'EmployeeType/AllEmployeeTypeDetails/');  
}  
getEmployeeTypeById(EmployeeType_ID:string): Observable<IEmployeeType> {  
  return this.http.get<IEmployeeType>(this.url + 'EmployeeType/GetEmployeeTypeDetailsById/' + EmployeeType_ID);  
}  
updateEmployeeType(empType: IEmployeeType): Observable<IEmployeeType> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.put<IEmployeeType>(this.url + 'EmployeeType/UpdateEmployeeTypeDetails/',  
  empType, httpOptions);  
}  

createEmployeeType(empType: IEmployeeType): Observable<IEmployeeType> {  
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
  return this.http.post<IEmployeeType>(this.url + 'EmployeeType/InsertEmployeeTypeDetails/',  
  empType, httpOptions);  
}

deleteEmployeeTypeById(id: number) {
  return this.http.delete(`${this.url+`EmployeeType/DeleteEmployeeTypeDetails?id=`}${id}`)
    .pipe(map(res => res));
}
}
