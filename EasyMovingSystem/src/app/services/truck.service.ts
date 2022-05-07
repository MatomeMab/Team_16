import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable,BehaviorSubject } from 'rxjs';
//import { Truck } from '../class/truck';
import { Truck } from '../Model/Trucks/truck.model';
import { catchError, tap, map } from 'rxjs/operators';
const baseURL = 'http://localhost:49243/api/Customers';
@Injectable({
  providedIn: 'root'
})
export class TruckService {
  selectedTruck!: Truck;
  TruckList!: Observable<Truck[]>;

 //Create constructor to get Http instance
 constructor(private httpClient: HttpClient) {
 }
 getTruckList(): Observable<Truck[]> {  
  this.TruckList=this.httpClient.get<Truck[]>(baseURL);  
  return this.TruckList;  
}  
getTruckById(id:any): Observable<any> {
  return this.httpClient.get(`${baseURL}/${id}`);
}
addTruck(data:any): Observable<any> {
  return this.httpClient.post(baseURL, data);
}
updateTruck(id: any, data: any): Observable<any> {
  return this.httpClient.put(`${baseURL}/${id}`, data);
}
deleteTruck(id: any): Observable<any> {
  return this.httpClient.delete(`${baseURL}/${id}`);
}

searchByCustomer(name: any): Observable<any> {
  return this.httpClient.get(`${baseURL}?name=${name}`);
}
}
