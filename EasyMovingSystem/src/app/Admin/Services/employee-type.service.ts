import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IEmployeeType } from '../Models/iemployee-type';
import { HttpHeaders } from '@angular/common/http';  

@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {
  readonly url = 'https://localhost:44355/Api/EmployeeType'
  constructor(private http:HttpClient) { }
  
  
  getAllEmployeeType(): Observable<IEmployeeType[]> {  
    return this.http.get<IEmployeeType[]>(this.url + '/AllEmployeeTypeDetails/');  
  }  
  getEmployeeTypeById(EmployeeType_ID:string): Observable<IEmployeeType> {  
    return this.http.get<IEmployeeType>(this.url + '/GetEmployeeTypeDetailsById/' + EmployeeType_ID);  
  }  
  updateEmployeeType(empType: IEmployeeType): Observable<IEmployeeType> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<IEmployeeType>(this.url + '/UpdateEmployeeTypeDetails/',  
    empType, httpOptions);  
  }  
  deleteEmployeeTypeById(EmployeeType_ID:number) : Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteEmployeeTypeDetails?id=' + EmployeeType_ID,  
 httpOptions);  
  } 
  createEmployeeType(empType: IEmployeeType): Observable<IEmployeeType> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<IEmployeeType>(this.url + '/InsertEmployeeTypeDetails/',  
    empType, httpOptions);  
  }

  //new delete 
  deleteEmployeeType(id: number) {
    return this.http.delete(`${this.url+`DeleteEmployeeTypeDetails?id=`}/${id}`)
      .pipe(map(res => res));
  }
}
