import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IQuotationRequest,IQuotationLine,IQuotationStatus,IQuotation, IQuotationRequestLine } from '../Models/iclient';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
   readonly apiUrl='https://localhost:44355/Api/'
  constructor(private http:HttpClient) { }

  //Quotation Request
  getRequestedQuotes(): Observable<IQuotationRequest[]> {
    return this.http.get<IQuotationRequest[]>(`${this.apiUrl}QuotationRequest/GetQuoteRequestDetails`)
   .pipe(map(res => res));
  }

  getRequestedQuote(id: number): Observable<IQuotationRequest[]> {
    return this.http.get<IQuotationRequest[]>(`${this.apiUrl}/${id}`)
    .pipe(map(res => res));
  }


  CreateRequestedQuote(requestedQuote: IQuotationRequest): Observable<any>{
    return this.http.post(`${this.apiUrl}QuotationRequest/InsertQuotationRequestDetails`, requestedQuote)
    .pipe(map(res => res));
  }
  //Quotation Line
  getQuoteLines(): Observable<IQuotationLine[]> {
    return this.http.get<IQuotationLine[]>(`${this.apiUrl}`)
      .pipe(map(res => res))
  }

  getQuoteLine(id: number): Observable<IQuotationLine[]> {
    return this.http.get<IQuotationLine[]>(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }

  UpdateQuoteLine(quoteLine: IQuotationLine) {
    return this.http.put(`${this.apiUrl}/${quoteLine.Quotation_ID}`, quoteLine) //changed from quotelineId to quoteId
      .pipe(map(res => res));
  }

  CreateQuoteLine(quoteLine: IQuotationLine): Observable<any> {
    return this.http.post(`${this.apiUrl}`, quoteLine)
      .pipe(map(res => res));
  }

  DeleteQuote(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`)
      .pipe(map(res => res));
  }
  //Quotation Status
  getQuoteStatuses(): Observable<IQuotationStatus[]> {
    return this.http.get<IQuotationStatus[]>(`${this.apiUrl}`)
    .pipe(map(res => res));
  }

  getQuoteStatus(id: number): Observable<IQuotationStatus[]> {
    return this.http.get<IQuotationStatus[]>(`${this.apiUrl}/${id}`)
    .pipe(map(res => res));
  }


  //QUOTATION
  getQuotes(): Observable<IQuotation[]> {
    return this.http.get<IQuotation[]>(`${this.apiUrl}`)
   .pipe(map(res => res));
  }

  getQuote(id: number): Observable<IQuotation[]> {
    return this.http.get<IQuotation[]>(`${this.apiUrl}/${id}`)
    .pipe(map(res => res));
  }

  CreateQuote(quote:any): Observable<any>{
    return this.http.post(`${this.apiUrl}`, quote)
    .pipe(map(res => res));
  }


  //QUOTATION REQUEST LINE
  getRequestedQuoteLines(): Observable<IQuotationRequestLine[]> {
    return this.http.get<IQuotationRequestLine[]>(`${this.apiUrl}`)
      .pipe(map(res => res));
  }

  CreateRequestedQuoteLine(requestedQuoteLine: IQuotationRequestLine): Observable<any>{
    return this.http.post(`${this.apiUrl}`, requestedQuoteLine)
    .pipe(map(res=>res));
  }
//SERVICES

}
