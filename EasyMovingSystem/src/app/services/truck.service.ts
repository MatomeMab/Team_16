import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable,BehaviorSubject } from 'rxjs';
//import { Truck } from '../class/truck';
import { ITruck } from '../Model/ITrucks/itruck';
import { catchError, tap, map } from 'rxjs/operators';
const baseURL = 'https://localhost:44355/Api/Truck';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};
@Injectable({
  providedIn: 'root'
})
export class TruckService {
  selectedTruck!: ITruck;
  TruckList:any=[]
  //TruckList!: Observable<ITruck[]>;
  //TruckList = new BehaviorSubject<any>([]);
 //Create constructor to get Http instance
 constructor(private httpClient: HttpClient) {
 }

 getTruckList(): Observable<ITruck[]> {  
   
  return this.TruckList=this.httpClient.get<ITruck[]>(baseURL+'/AllTruckDetails',httpOptions); 
  
}  

getTruckById(id:any): Observable<any> {
  return this.httpClient.get(`${baseURL}/${id}`);
}
addTruck(truck:ITruck): Observable<ITruck> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.post<ITruck>(baseURL+ 'Truck/InsertTruckDetails',truck,httpOptions);
}
updateTruck(truck:ITruck): Observable<ITruck> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.put<ITruck>(baseURL+'/UpdateTruckDetails',truck, httpOptions);
}
deleteTruck(id: string): Observable<number> {
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
  return this.httpClient.delete<number>(baseURL+'DeleteTruckDetails?id={id}'+id,httpOptions);
}

searchByTruck(name: any): Observable<any> {
  return this.httpClient.get(`${baseURL}?name=${name}`);
}
}
