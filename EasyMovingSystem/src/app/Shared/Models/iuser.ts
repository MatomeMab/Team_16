import { IEmployee } from "./iemployee";
import { IClient } from "./iclient";
export interface IUser {
    User_ID?:any;
    Role_ID:any;
    SessionID?:string;
    OTP?:string;
    Active?:any;
    Email:string;
    Session_Expiry?:any;
    
   
  
    Passwords?: 
    [{
      Password_ID: any;
      User_ID:any;
      Password1: string;
    }]
  
    Clients?: [IClient];
  
   // Admins?: Admin;
  
    Employees?: [IEmployee]
}
