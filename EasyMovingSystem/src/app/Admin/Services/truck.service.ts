import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs'; 
import{ITruck} from 'src/app/Admin/Models/itruck'
import { map } from "rxjs/operators";
@Injectable({
  providedIn: 'root'
})
export class TruckTruck {
  readonly url = 'https://localhost:44355/Api/Truck'
  readonly urlTruckType='https://localhost:44355/Api/TruckType'
  readonly urlTruckStatus='https://localhost:44355/Api/TruckStatus'
  constructor(private http:HttpClient) { }
  
  
  getAllTruck(): Observable<ITruck[]> {  
    return this.http.get<ITruck[]>(this.url + '/AllTruckDetails/');  
  }  
  getTruckById(Truck_ID: number): Observable<ITruck> {  
    return this.http.get<ITruck>(this.url + '/GetTruckDetailsById/' + Truck_ID);  
  }  
  updateTruck(Truck: ITruck): Observable<ITruck> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<ITruck>(this.url + '/UpdateTruckDetails/',  
    Truck, httpOptions);  
  }  
  deleteTruckById(TruckId: number): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteTruckDetails?id=' +TruckId,  
 httpOptions);  
  } 
  createTruck(Truck: ITruck): Observable<ITruck> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<ITruck>(this.url + '/InsertTruckDetails/',  
    Truck, httpOptions);  
  }

  //get status
  getAllTruckStatus(): Observable<ITruck[]> {  
    return this.http.get<ITruck[]>(this.urlTruckStatus + '/AllTruckStatusDetails/');  
  }
  //get truck type
  getAllTruckType(): Observable<ITruck[]> {  
    return this.http.get<ITruck[]>(this.urlTruckType + '/AllTruckTypeDetails/');  
  }
}
