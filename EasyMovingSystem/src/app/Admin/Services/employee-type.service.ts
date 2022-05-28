import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IEmployeeType } from '../Models/iemployee-type';
@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {

  apiUrl = 'http://localhost:59591/Api/EmployeeType';
  constructor(
    private http: HttpClient
  ) { }

  getEmployeeTypes(): Observable<IEmployeeType[]> {
    return this.http.get<IEmployeeType[]>(`${this.apiUrl}`)
      .pipe(map(res => res));
  }
  
  getEmployeeType(id: number): Observable<IEmployeeType[]> {
    return this.http.get<IEmployeeType[]>(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }

  UpdateEmployeeType(employeeType: IEmployeeType) {
    return this.http.put(`${this.apiUrl}/${employeeType.EmployeeType_ID}`, employeeType)
      .pipe(map(res => res));
  }

  CreateEmployeeType(employeeType: IEmployeeType): Observable<any> {
    return this.http.post(`${this.apiUrl}`, employeeType)
      .pipe(map(res => res));
  }

  DeleteEmployeeType(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }
}
