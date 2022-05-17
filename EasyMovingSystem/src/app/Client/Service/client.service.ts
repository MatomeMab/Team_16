import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Client } from '../client';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private userSubject: BehaviorSubject<Client>;
  public user: Observable<Client>;
  private apiUrl =""

  constructor(
      private router: Router,
      private http: HttpClient
  ) {
      this.userSubject = new BehaviorSubject<Client>(JSON.parse(localStorage.getItem('user')!));
      this.user = this.userSubject.asObservable();
  }
  public get userValue(): Client {
    return this.userSubject.value;
 }

getAll() {
        return this.http.get<Client[]>(`${this.apiUrl}/users`);
    }

   
}