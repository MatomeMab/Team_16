import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import{IUser} from '../Models/iuser'
@Injectable({
  providedIn: 'root'
})
export class UserService {

  userData!: IUser;
  userList!: IUser[];
  ApiURL = 'https://localhost:44355';


  constructor(private http: HttpClient) { }

//user
GetUsers() {
  // https://localhost:58326/api/Users
  return this.http.get(this.ApiURL + '/api/User').toPromise();
}

Login(Username:string, Password:string)
{
  const params = new HttpParams().set("Username", Username).set("Password", Password)
  return this.http.post(this.ApiURL + '/api/User/Login', "", {params})
}

GetUser(id:number) {
 // https://localhost:58326/api/Users/5
  return this.http.get(this.ApiURL + '/api/User/' + id) ;
}

PostUser(obj: IUser) {
  // https://localhost:58326/api/Users,Users { User_id: obj.User_id, ........ }
  return this.http.post(this.ApiURL + '/api/User', obj);
}

PutUser(obj: IUser) {
   // https://localhost:58326/api/Users/5,User { User_id: obj.User_id, ........ }
  return this.http.put(this.ApiURL + '/api/User' + obj.User_ID, obj);
}

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
  RegisterClient(obj: IUser)
  {
    console.log(obj)
    return this.http.post(this.ApiURL + '/Api/User/RegisterClient', obj)
  }

  RegisterEmployee(obj: IUser)
  {
    console.log(obj)
    return this.http.post(this.ApiURL + '/api/User/RegisterEmployee', obj)
  }
}
