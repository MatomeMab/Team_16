import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable,BehaviorSubject } from 'rxjs';
//import { Truck } from '../class/truck';
import { ITruck } from '../Model/ITrucks/itruck';
import { catchError, tap, map } from 'rxjs/operators';
const baseURL = 'http://localhost:44372/api/truck';
@Injectable({
  providedIn: 'root'
})
export class TruckService {
  selectedTruck!: ITruck;
  TruckList!: Observable<ITruck[]>;

 //Create constructor to get Http instance
 constructor(private httpClient: HttpClient) {
 }
 getTruckList(): Observable<ITruck[]> {  
  this.TruckList=this.httpClient.get<ITruck[]>(baseURL+'/gettrucks');  
  return this.TruckList;  
}  
getTruckById(id:any): Observable<any> {
  return this.httpClient.get(`${baseURL}/${id}`);
}
addTruck(truck:ITruck): Observable<ITruck> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.post<ITruck>(baseURL+ '/createtrucks',truck,httpOptions);
}
updateTruck(truck:ITruck): Observable<ITruck> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.put<ITruck>(baseURL+'/updatetrucks',truck, httpOptions);
}
deleteTruck(id: string): Observable<number> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.delete<number>(baseURL+'/deletetrucks?Id='+id,httpOptions);
}

searchByTruck(name: any): Observable<any> {
  return this.httpClient.get(`${baseURL}?name=${name}`);
}
}
