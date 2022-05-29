Use Master 
If Exists (Select * from sys.databases where name = 'EasyMovingSystem') 
DROP DATABASE EasyMovingSystem  
Go 
Create Database EasyMovingSystem 
Go

Use EasyMovingSystem 
Go


/* Create Table 1 */
CREATE TABLE Role  
(
Role_ID int Identity (1,1) Primary Key Not Null,
RoleName varchar(50) not null
)
GO

--Table 2
CREATE TABLE [User]
(
User_ID int Identity (1,1) Primary Key Not Null,
UserName varchar (50) not null,
Password varchar (50) not null,
Email varchar (100) not null
)
GO
--Table3
CREATE TABLE UserRole  
(
UserRole int Identity (1,1) Primary Key Not Null,
Role_ID int not null foreign key references Role(Role_ID) on delete no action,
User_ID int not null foreign key references [User] (User_ID) on delete cascade
)
GO

/* Create Table 4 */
CREATE TABLE Client  
(
Client_ID int Identity (1,1) Primary Key Not Null,
--User_ID int not null foreign key references [User] (User_ID) on delete cascade,
ClientName varchar (50) not null,
ClientSurname varchar (50) not null,
PhoneNum int not null,

)
GO


/* Create Table 5 */
CREATE TABLE EmployeeType   
(
EmployeeType_ID int Identity (1,1) Primary Key Not Null,
EmployeeTypeName varchar (50) not null,
EmployeeDescription varchar (100) not null
)
GO
--table6
CREATE TABLE EmployeeStatus 
(
EmployeeStatus_ID int Identity (1,1) Primary Key Not Null,
EmployeeStatusName varchar (50) not null
)
GO
--table7
CREATE TABLE Title 
(
Title_ID int Identity (1,1) Primary Key Not Null,
TitleName varchar (50) not null
)
GO
/* Create Table 8 */
CREATE TABLE Employee 
(
Employee_ID int Identity (1,1) Primary Key Not Null,
--User_ID int not null foreign key references [User](User_ID),
EmployeeType_ID int not null foreign key references EmployeeType(EmployeeType_ID) on delete no action,
EmployeeStatus_ID int not null foreign Key references EmployeeStatus(EmployeeStatus_ID) on delete no action,
Title_ID int not null foreign key references Title(Title_ID) on delete no action,
EmployeeName varchar (50) not null, 
EmployeeSurname varchar (50) not null,
DateOfBirth date not null,
DateEmployed date not null,
PhoneNum int  not null,
EmergencyName varchar (50) not null,
EmergencySurname varchar (50) not null,
EmergencyPhoneNum int not null
--Admin_ID int not null foreign key references Admin(Admin_ID)
)
GO


/* Create Table 9 */
CREATE TABLE Booking 
(
Booking_ID int Identity (1,1) Primary Key Not Null,
--Client_ID int not null foreign key references Client(Client_ID)  on delete cascade,
DateMade datetime not null,
)
GO
/* Create Table 10 */
CREATE TABLE BookingStatus 
(
BookingStatus_ID int Identity (1,1) Primary Key Not Null,
BookingDescription varchar(50) not null,
)
GO

/* Create Table 11 */
CREATE TABLE TrackingStatus   
(
TrackingStatus_ID int Identity (1,1) Primary Key Not Null,
TrackingStatusName varchar (20) not null, 
TrackingStatusDescription varchar (200) not null
)
GO

/* Create Table 12 */
CREATE TABLE RentalStatus 
(
RentalStatus_ID int Identity (1,1) Primary Key Not Null,
RentalStatusName varchar (50) not null
)
GO

/* Create Table 13 */
CREATE TABLE InspectionType  
(
InspectionType_ID int Identity (1,1) Primary Key Not Null,
TypeName varchar (50) not null,
TypeDescription varchar (200) not null
)
GO

/* Create Table 14 */
CREATE TABLE Admin
(
Admin_ID int Identity (1,1) Primary Key Not Null,
User_ID int not null foreign key references [User](User_ID) on delete cascade,
AdminName varchar (50) not null,
SurName varchar (50) not null,
PhoneNum int not null 
)
GO

/* Create Table 15 */
CREATE TABLE TruckType 
(
TruckType_ID int Identity (1,1) Primary Key Not Null,
TruckTypeName varchar (50) not null,
TruckTypeDescription varchar (200) not null
)
GO

/* Create Table 16 */
CREATE TABLE TruckStatus 
(
TruckStatus_ID int Identity (1,1) Primary Key Not Null,
TruckStatusName varchar (50) not null,
)
GO

/* Create Table 17 */
CREATE TABLE Truck 
(
Truck_ID int Identity (1,1) Primary Key Not Null,
TruckType_ID int not null foreign key references TruckType(TruckType_ID) on delete no action,
TruckStatus_ID int not null foreign key references TruckStatus(TruckStatus_ID) on delete no action,
--Admin_ID int not null foreign key references Admin(Admin_ID) on delete no action,
Model varchar (50) not null,
Year int not null,
Colour varchar (50) not null,
RegNum varchar (10) not null,
Make varchar (50) not null
)
GO


/* Create Table 18 */
CREATE TABLE Inspection  
(
Inspection_ID int Identity (1,1) Primary Key Not Null,
InspectionNote varchar (200) not null,
InspectionTime datetime not null,
InspectionType_ID int not null foreign key references InspectionType(InspectionType_ID),
Truck_ID int not null foreign key references Truck(Truck_ID) on delete cascade
--User_ID int not null foreign key references [User](User_ID),
--Admin_ID int not null foreign key references Admin(Admin_ID)
)
GO

/* Create Table 19 */

CREATE TABLE RentalAgreement   
(
Rental_ID int Identity (1,1) Primary Key Not Null,
Truck_ID int not null foreign key references Truck(Truck_ID),
RentalStatus_ID  int not null foreign key references RentalStatus(RentalStatus_ID) on delete no action,
Client_ID int not null foreign key references Client(Client_ID) on delete cascade,
Description varchar(1000),
StartDate date not null,
ExpiryDate date not null,
Inspection_ID int not null foreign key references Inspection(Inspection_ID)
)
GO
/* Create Table 20 */
CREATE TABLE InspectionItem 
(
InspectionItem_ID int Identity (1,1) Primary Key Not Null,
ItemDescription varchar(300) not null,
)
GO
/* Create Table 21 */
CREATE TABLE BookingInstance   
(
BookingInstance_ID int Identity (1,1) Primary Key Not Null,
TrackingStatus_ID int references TrackingStatus(TrackingStatus_ID) on delete no action,
InspectionItem_ID int not null foreign key references InspectionItem(InspectionItem_ID) on delete cascade,
BookingStatus_ID int references BookingStatus(BookingStatus_ID) on delete no action,
Booking_ID int not null foreign key references Booking(Booking_ID) on delete cascade,
Employee_ID int not null foreign key references Employee(Employee_ID) on delete no action,
Truck_ID int references Truck(Truck_ID) on delete no action,
Rental_ID int not null foreign key references RentalAgreement(Rental_ID) on delete no action,
BookingInstanceDate datetime not null,
BookingInstanceDescription varchar (200) not null,

)
GO
--Table 22
CREATE TABLE PaymentType  
(
PaymentType_ID int Identity (1,1) Primary Key Not Null,
PaymentTypeName varchar (50) not null
)
GO

/* Create Table 23 */
CREATE TABLE Payment 
(
Payment_ID int Identity (1,1) Primary Key Not Null,
PaymentType_ID int not null foreign key references PaymentType(PaymentType_ID),
Rental_ID int not null foreign key references RentalAgreement(Rental_ID) on delete no action,
BookingInstance_ID int not null foreign key references BookingInstance(BookingInstance_ID) on delete no action,
Client_ID int references Client(Client_ID),
--ProofOfPayment varbinary(max) not null,
AmountPaid decimal (16,2) not null,
AmountDue decimal (16,2) not null,
DatePaid datetime not null 
)
GO




/* Create Table 24 */
CREATE TABLE Feedback 
(
Feedback_ID int Identity (1,1) Primary Key Not Null,	 
BookingInstance_ID int references BookingInstance(BookingInstance_ID),
FeedbackDescription varchar (255) not null,
)

GO

GO
/* Create Table 25 */
CREATE TABLE Service
(
Service_ID int Identity (1,1) Primary Key Not Null,
ServiceName varchar (50) not null,
ServiceDescription varchar (200) not null
)
GO



/* Create Table 26 */
CREATE TABLE License 
(
License_ID int Identity (1,1) Primary Key Not Null,
Rental_ID int not null foreign key references RentalAgreement(Rental_ID) on delete no action,
Client_ID int not null foreign key references Client(Client_ID) on delete cascade,
ExpiryDate date not null,

)
GO

/* Create Table 27 */
CREATE TABLE ApplicationStatus 
(
ApplicationStatus_ID int Identity (1,1) Primary Key Not Null,
ApplicationStatusName varchar (50) not null,
)
GO




/* Create Table 28 */
CREATE TABLE NotificationType
(
NotificationType_ID int Identity (1,1) Primary Key Not Null,
NotificationTypeName varchar (50) not null
)
GO

/* Create Table 29 */
CREATE TABLE Notification
(
Notification_ID int Identity (1,1) Primary Key Not Null,
NotificationType_ID int references NotificationType(NotificationType_ID),
NotificationDescription varchar (200) not null
)
GO

/* Create Table 30 */
CREATE TABLE UserNotification
(
UserNotification_ID int Identity (1,1) Primary Key Not Null,
Notification_ID int not null foreign key references Notification(Notification_ID) on delete cascade,
User_ID int not null foreign key references [User](User_ID) on delete no action,
)
GO

/* Create Table 31 */
CREATE TABLE Quotation
(
Quotation_ID int Identity (1,1) Primary Key Not Null,
--Admin_ID int not null foreign key references Admin(Admin_ID) on delete cascade,
QuotationDescription varchar(300),
QuotationDate datetime not null,
Amount decimal(16,2) not null
)
GO


/* Create Table 32 */
CREATE TABLE QuotationRequest
(
QuotationRequest_ID int Identity (1,1) Primary Key Not Null,
--Client_ID int not null foreign key references Client(Client_ID) on delete cascade,
QuotationReqDate datetime not null,
QuotationReqDescription varchar (200) not null,
FromAddress varchar(200) not null,
ToAddress varchar(200) not null
--From and To Address
)
GO

/* Create Table 33 */
CREATE TABLE QuotationLine
(
QuotationLine_ID int Identity (1,1) Primary Key Not Null,
QuotationRequest_ID int not null foreign key references QuotationRequest(QuotationRequest_ID) ,
QuotationLineDescription varchar (200) not null,
Quotation_ID int not null foreign key references Quotation(Quotation_ID),
Service_ID int not null foreign key references Service(Service_ID) 
)
GO

/* Create Table 34 */
CREATE TABLE BackgroundCheckStatus 
(
BackgroundCheckStatus_ID int Identity (1,1) Primary Key Not Null,
BackgroundCheckStatusName varchar (50) not null,
--Admin_ID int not null foreign key references Admin(Admin_ID),
)
GO

/* Create Table 35 */
CREATE TABLE DocumentType 
(
DocumentType_ID int Identity (1,1) Primary Key Not Null,
DocumentTypeDescription varchar(200) not null,
)
GO

/* Create Table 36 */
CREATE TABLE Document 
(
Document_ID int Identity (1,1) Primary Key Not Null,
DocumentName varchar (50) not null,
DocumentType_ID int not null foreign key references DocumentType(DocumentType_ID) on delete no action,
)
GO

/* Create Table 37 */
CREATE TABLE ListingStatus 
(
ListingStatus_ID int Identity (1,1) Primary Key Not Null,
ListingStatusName varchar (50) not null,
)
GO

/*Create Table 38*/
CREATE TABLE JobType 
(
JobType_ID int Identity (1,1) Primary Key Not Null,
JobType varchar(50) not null,
)
GO

/* Create Table 39 */
CREATE TABLE JobListing
(
Job_ID int Identity (1,1) Primary Key Not Null,
Description varchar (200) not null,
Amount decimal(16,2) not null,
ListingStatus_ID int not null foreign key references ListingStatus(ListingStatus_ID) on delete no action,
--Admin_ID int not null foreign key references Admin(Admin_ID) on delete cascade,
DatePosted datetime not null,
ExpiryDate datetime not null,
)
GO

/* Create Table 40 */
CREATE TABLE Candidate  
(
Candidate_ID int Identity (1,1) Primary Key Not Null,
CandidateSurname varchar (50) not null,
CandidateName varchar (50) not null,
CandidateEmailAddress varchar(50) not null,
CandidatePhonNum int not null

)
GO



/* Create Table 41 */
CREATE TABLE Application 
(
Application_ID int Identity (1,1) Primary Key Not Null,
Candidate_ID int not null foreign key references Candidate(Candidate_ID) on delete cascade,
Job_ID int not null foreign key references JobListing(Job_ID) on delete cascade,
Document_ID int not null foreign key references Document(Document_ID),
ApplicationStatus_ID int not null foreign key references ApplicationStatus(ApplicationStatus_ID),
ApplicationDate date not null
)
GO


/* Create Table 42 */
CREATE TABLE TruckPhoto
(
Photo_ID int Identity (1,1) Primary Key Not Null,
Photo varbinary(max) not null,
Truck_ID int not null foreign key references Truck(Truck_ID) on delete cascade,
)
GO

--Table43
CREATE TABLE Date
(
Date_ID int Identity (1,1) Primary Key Not Null,
Date datetime not null
)
GO

/* Create Table 44 */
CREATE TABLE TimeSlot
(
TimeSlot_ID int Identity (1,1) Primary Key Not Null,
StartTime datetime not null,
EndTime datetime not null,
)
GO
/* Create Table 45 */
CREATE TABLE DateOrTimeSlot
(
DateorTimeslot_ID int identity (1,1) primary key not null,
Date_ID int not null foreign key references Date(Date_ID) on delete cascade,
TimeSlot_ID int not null foreign key references TimeSlot(TimeSlot_ID) on delete cascade,
)
GO
/* Create Table 46 */
CREATE TABLE DateOrTimeSlotOrDriver
(
Employee_ID int not null foreign key references Employee(Employee_ID) on delete no action,
DateorTimeslotDirive_ID int identity(1,1) primary key not null,
DateorTimeslot_ID int not null foreign key references DateorTimeslot(DateorTimeslot_ID) on delete no action,
Description varchar(200) not null,
BookingInstance_ID int not null foreign key references BookingInstance(BookingInstance_ID) on delete no action,
)
GO

insert into InspectionType values
('Pre Inspection','Inspection done before the vehicle is rented out or used'),
('Post Inspection','Inspection done after the vehicle is returned')

insert into Title values
('Mr'),
('Ms'),
('Mrs')

insert into EmployeeStatus values
('On leave'),
('Available'),
('Resigned'),
('Fired')

insert into TruckStatus values 
('Booked for service'),
('Available')
insert into BookingStatus values
('Booked'),
('Cancelled')

insert into EmployeeType values 
('Owner', 'the person who is in charge of the business'),
('Administrator', 'A person who is in charge in adminitration'),
('Driver', 'A person who is Responsible to deliver items'),
('Assistant', 'A person who is helping the driver with delivering items')

insert into NotificationType values 
('SMS'),
('Email')

insert into PaymentType values
('Cash'),
('EFT')

insert into BookingStatus values
('Booked'),
('Cancelled')

insert into Role values 
('Client'),
('Employee'),
('Admin')

insert into TrackingStatus values
('Driver Assigned','Driver '),
('En route to collect','Driver is coming to collect Items'),
('Collected','Items collected from the client'),
('En route to deliver','Items on the way to be delivered'),
('Delivered','Items delivered')

insert into ApplicationStatus values
('In Process'),
('Interview offer'),
('Decline')

insert into ListingStatus values 
('Expired'),
('Vacany filled'),
('Active')

insert into JobType values
('Parker'),
('Cleaner')

insert into TruckType values
('Flatbed','A flatbed truck is a type of truck which can be either articulated or rigid. As the name suggests, its bodywork is just an entirely flat, level "bed" with no sides or roof.')

insert into [User] values
('felicityfanelo@gmail.com','12345678F','felicityfanelo@gmail.com'),
('percy@gmail.com','12345678P','percy@gmail.com'),
('Admin@gmail.com','12345678A','Admin@gmail.com')
insert into Admin values 
(3,'Billy','VanVick',0845631279)
insert into Client values
('Fanelo','Nghonyama',0784569874)
insert into Employee values
(2,1,1,'Percy','Chabalala','1998-01-04','2020-01-04',0782598641,'Cecil','Mpongose',0785962112)
insert into Truck values
(1,2,'Altima',2022,'White','YYD89GP','Nissan')
insert into JobListing values
('For testing purposes',500,1,'2020-01-04','2020-08-04')