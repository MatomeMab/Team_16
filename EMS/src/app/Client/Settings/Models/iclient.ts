import { IUser } from "src/app/Account/Settings/Models/iuser";

export interface IClient {
    Client_ID:number;
    User_ID:number;
    ClientName:string;
    ClientSurname:string;
    PhoneNum:string;
    
   // Email?:string;
    User?:IUser 
}
export interface IQuotationLine{
    QuotationLine_ID:number
      QuotationRequest_ID:number
      Quotation_ID:number
      Service_ID:number
      QuotationLineDescription:number
}
export interface IQuotationRequest{
    
    QuotationRequest_ID:number
    Client_ID:number
    Service_ID:number
    QuotationReqDate:Date | null
    QuotationReqDescription:string
    FromAddress:string
    ToAddress:string
    QuotationRequestLine: IQuotationRequestLine[];
}
export interface IQuotationRequestLine{
    QuotationRequestLine_ID:number
      QuotationRequest_ID:number
      Service_ID:number
      QuotationReqLineDescription:string
}
export interface IQuotationStatus{
    QuoteStatus_ID:number
    QuoteStatusName:string
}
export interface IQuotation{
    Quotation_ID:number
    Client_ID:number
    QuoteStatus_ID:number
    QuotationDescription:string
    QuotationDate:Date
    Amount:number
}
export interface IService{
    Service_ID:number
    ServiceName:string
    Description:string
}