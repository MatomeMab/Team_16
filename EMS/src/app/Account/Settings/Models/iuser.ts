import { IClient } from "src/app/Client/Settings/Models/iclient";
import { IEmployee } from "src/app/Employee/Settings/Models/iemployee";

export interface IUser {

    User_ID?:any;
    Role_ID:any;
    SessionID?:string;
    OTP:string;
    Active?:any;
    Email:string;
    Session_Expiry?:any;
    Password:string;
    ResetPassword?:string;


    Clients: [IClient];
  
   // Admins?: Admin;
  
    Employees?: [IEmployee]
}
