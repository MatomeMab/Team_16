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
URL = 'https://localhost:44355/Api/Truck/';
  constructor(private http: HttpClient) { }

  getTrucks(): Observable<Truck[]> {
    return this.http.get<Truck[]>(`${this.URL}ALLTruckDetails`)
      .pipe(map(res => res));
  }

  
  getTruckById(Truck_ID: number): Observable<Truck[]> {
    return this.http.get<Truck[]>(`${this.URL}GetTrcukDetailsById/${Truck_ID}`)
      .pipe(map(res => res));
  }
}
