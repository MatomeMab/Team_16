import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Truck, TruckType } from 'src/app/Admin/Components/Interfaces/interface';
@Injectable({
  providedIn: 'root'
})
export class TruckService {
  createTruck(truck: Truck) {
    throw new Error('Method not implemented.');
  }
URL = '';
  constructor(private http: HttpClient) { }

  
  getTruckTypes(): Observable<TruckType[]> {
    return this.http.get<TruckType[]>(`${this.URL}`)
      .pipe(map(res => res));
  }

  getTruckType(id: number): Observable<TruckType[]> {
    return this.http.get<TruckType[]>(`${this.URL}/${id}`)
      .pipe(map(res => res));
  }
}
