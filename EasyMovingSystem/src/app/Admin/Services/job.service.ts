import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs'; 
import { map } from "rxjs/operators";
import { IJob } from '../Models/ijob';
@Injectable({
  providedIn: 'root'
})
export class JobService {
  url = 'https://localhost:44355/Api/JobListing'; 
  urlListingStatus='https://localhost:44355/Api/ListingStatus/AllListingStatusDetails';
  constructor(private http: HttpClient) { }
  getAllJob(): Observable<IJob[]> {  
    return this.http.get<IJob[]>(this.url + '/AllJobListingDetails/');  
  }  
  getJobById(JobListing_ID: number): Observable<IJob> {  
    return this.http.get<IJob>(this.url + '/GetJobDetailsById/' + JobListing_ID);  
  } 
  createJob(Job: IJob): Observable<IJob> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<IJob>(this.url + '/InsertJobListingDetails/',  
    Job, httpOptions); 
 
  }
  updateJob(Job: IJob): Observable<IJob> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<IJob>(this.url + '/UpdateJobListingDetails/',  Job, httpOptions);  
  }  
  deleteJobById(Jobid: number): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteJobListingDetails?id=' +Jobid,  
 httpOptions);  
  } 
  //
  getAllListingStatus(): Observable<IJob[]> {  
    return this.http.get<IJob[]>(this.urlListingStatus);  
  } 
}
