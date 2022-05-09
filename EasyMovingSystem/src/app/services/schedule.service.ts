import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Schedule, ScheduleResponse, ScheduleTypeRequest } from '../schedule';
@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private apiUrl = 'http://localhost:5000/api/Product/';
  private header ;
  constructor(private http: HttpClient) { 
    this.header = new HttpHeaders().
    set('content-type','application/json').
    set('authorization','Bearer ' + localStorage.getItem('token'));

    this.header = new HttpHeaders().
    set('content-type','application/json');
  }
  getSchedule(): Observable<Schedule[]>{
    return this.http.get<Schedule[]>(this.apiUrl + "getSchedule",{headers: this.header});
  }
  getEmployeeTypeById(cid: string): Observable<ScheduleResponse[]>{
    return this.http.get<ScheduleResponse[]>(this.apiUrl + "getEmployeeTypeById" + cid,{headers: this.header});
  }
  addSchedule(req: ScheduleTypeRequest): Observable<any>{
    return this.http.post<any>(this.apiUrl + "addSchedule", req,{headers: this.header});
  }
}
