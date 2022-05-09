import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployeeType } from '../Model/iemployee-type';

const apiUrl = 'http://localhost:65003/Api/EmployeeType';
@Injectable({
  providedIn: 'root'
})
export class EmployeeTypeService {
  selectedEmployeeType!: IEmployeeType;
  EmployeeTypeList!: Observable<IEmployeeType[]>;
  constructor(private http:HttpClient) { }
  getEmployeeTypeList(): Observable<IEmployeeType[]> {  
    this.EmployeeTypeList=this.http.get<IEmployeeType[]>(apiUrl+'/getEmployeeType');  
    return this.EmployeeTypeList;  
  }  
  getEmployeeTypeById(id:any): Observable<any> {
    return this.http.get(`${apiUrl}/${id}`);
  }
  addTruck(truck:IEmployeeType): Observable<IEmployeeType> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
    return this.http.post<IEmployeeType>(apiUrl+ '/createtrucks',truck,httpOptions);
  }
  updateTruck(truck:IEmployeeType): Observable<IEmployeeType> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
    return this.http.put<IEmployeeType>(apiUrl+'/updatetrucks',truck, httpOptions);
  }
  deleteTruck(id: string): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
    return this.http.delete<number>(apiUrl+'/deletetrucks?Id='+id,httpOptions);
  }
  
  searchByTruck(id: any): Observable<any> {
    return this.http.get(`${apiUrl}?id=${id}`);
  }

}
