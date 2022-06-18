import { IUser } from "./iuser";
export interface IClient {
    Client_ID:number;
    User_ID:number;
    ClientName:string;
    ClientSurname:string;
    PhoneNum:string;
    
    Email?:string;
    User?:IUser  
}
