import { HttpClient, HttpHeaders ,HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../Models/iuser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userData!: IUser;
  userList!: IUser[];
  ApiURL = 'https://localhost:44355';

  httpOptions ={
    headers: new HttpHeaders({
      ContentType: 'application/json'
    })
  }

  Login(loginUser: IUser){
    return this.http.post<IUser>(`${this.ApiURL}Authentication/Login`, loginUser, this.httpOptions)
  }
 

  constructor(private http: HttpClient) { }


DeleteUser(id: number) {
  return this.http.delete(this.ApiURL + '/api/User' + id);
}
VerifyOTP(OTP: string, UserID: any)
  {
    const params = new HttpParams().set("OTP", OTP).set("UserID", UserID)
    return this.http.post(this.ApiURL + '/api/User/VerifyOTP',"", {params} )
  }

ResendOTP(UserID: any)
  {
    const params = new HttpParams().set("UserID", UserID)
    return this.http.post(this.ApiURL + '/api/User/ResendOTP',"", {params} )
  }
 /* RegisterClient(obj: IUser)
  {
    console.log(obj)
    return this.http.post(this.ApiURL + '/Api/Client/registeraccount', obj)
  }*/
  RegisterClient(obj: IUser)
  {
    console.log(obj)
    return this.http.post(this.ApiURL + '/Api/Client/RegisterClient', obj)
  }

  RegisterEmployee(obj: IUser)
  {
    console.log(obj)
    return this.http.post(this.ApiURL + '/api/User/RegisterEmployee', obj)
  }
}
