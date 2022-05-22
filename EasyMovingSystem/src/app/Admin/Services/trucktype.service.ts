import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { TruckType } from '../Components/Interfaces/interface';

@Injectable({
  providedIn: 'root'
})
export class TrucktypeService {
 URL = 'https://localhost:44355/Api/TruckType/';
  constructor( private http: HttpClient) { }

  getTruckTypes(): Observable<TruckType[]> {
    return this.http.get<TruckType[]>(`${this.URL}ALLTruckDetails`)
      .pipe(map(res => res));
  }
  
  getTruckType(TruckType_ID: number): Observable<TruckType[]> {
    return this.http.get<TruckType[]>(`${this.URL}GetTruckTypeDetailsById/${TruckType_ID}`)
      .pipe(map(res => res));
  }
}
