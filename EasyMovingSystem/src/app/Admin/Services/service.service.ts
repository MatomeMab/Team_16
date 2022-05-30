import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs'; 
import { map } from "rxjs/operators";
import{IService} from '../Models/iservice'

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  readonly url = 'https://localhost:44355/Api/Service'
  constructor(private http:HttpClient) { }
  
  
  getAllService(): Observable<IService[]> {  
    return this.http.get<IService[]>(this.url + '/AllServiceDetails/');  
  }  
  getServiceById(Service_ID: number): Observable<IService> {  
    return this.http.get<IService>(this.url + '/GetServiceDetailsById/' + Service_ID);  
  }  
  updateService(service: IService): Observable<IService> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<IService>(this.url + '/UpdateServiceDetails/',  
    service, httpOptions);  
  }  
  deleteServiceById(serviceId: number): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteServiceDetails?id=' +serviceId,  
 httpOptions);  
  } 
  createService(service: IService): Observable<IService> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<IService>(this.url + '/InsertServiceDetails/',  
    service, httpOptions);  
  }
}
