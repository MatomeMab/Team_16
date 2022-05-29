import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIUrl = "https://localhost:44355/api";

  constructor( private http:HttpClient) { }

  AllEmployeeDetails():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Employee');
  }

  InsertEmployeeDetails(data:any){
    return this.http.post(this.APIUrl+'/Employee',data);
  }

  UpdateEmployeeDetails(id:number|string, data:any){
    return this.http.put(this.APIUrl+ `/Employee/${id}`, data);
  }

  DeleteEmployeeDetails(id:number|string, data:any){
    return this.http.delete(this.APIUrl+`/Employee/${id}`);
  }
}
