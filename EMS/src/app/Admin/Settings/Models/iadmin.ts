export interface IAdmin {
    Admin_ID:number;
    User_ID:number;
    AdminName:string;
    Surname:string;
    PhoneNum:number
}
export interface IService{
    Service_ID:number;
    ServiceName:string;
    ServiceDescription:string
    
}
export interface IJobListing {
    Job_ID:number
    JobStatus_ID:number
    Description:string
    Amount:number
    DatePosted:Date
    ExpiryDate:Date
    
}
export interface IJobStatus{
    JobStatus_ID:number;
    JobStatusName:string;
    
}
export interface IJobType{
    JobType_ID:number;
    JobType:string;

}
export interface ITruck {
    
    Truck_ID:number,
    TruckType_ID:number;
    TruckTypeName:string
    TruckStatus_ID:number;
    Available:boolean;
    Model:string;
    Year:string;
    Colour:string;
    RegNum:string;
    Make:string
}
export interface IEmployeeType {
    EmployeeType_ID:number ;
    EmployeeTypeName:string ;
    EmployeeDescription:string ;
}

export interface IDocumentType
{
    DocumentType_ID:number
    DocumentTypeDescription:string
}

export interface INotificationType
{
    NotificationType_ID:number
    NotificationTypeName:string
}

export interface IPaymentType
{
    PaymentType_ID:number
    PaymentTypeName:string
}

