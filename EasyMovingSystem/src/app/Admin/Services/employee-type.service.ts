import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmployeeType } from 'src/app/interfaces/employee-type';
import { catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {
  apiUrl = 'http://localhost:59591/Api/EmployeeType';
  constructor(
    private http: HttpClient
  ) { }

  getEmployeeTypes(): Observable<EmployeeType[]> {
    return this.http.get<EmployeeType[]>(`${this.apiUrl}`)
      .pipe(map(res => res));
  }
  
  getEmployeeType(id: number): Observable<EmployeeType[]> {
    return this.http.get<EmployeeType[]>(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }

  UpdateEmployeeType(employeeType: EmployeeType) {
    return this.http.put(`${this.apiUrl}/${employeeType.EmployeeType_ID}`, employeeType)
      .pipe(map(res => res));
  }

  CreateEmployeeType(employeeType: EmployeeType): Observable<any> {
    return this.http.post(`${this.apiUrl}`, employeeType)
      .pipe(map(res => res));
  }

  DeleteEmployeeType(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }

}