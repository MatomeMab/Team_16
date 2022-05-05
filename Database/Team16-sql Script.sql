Use Master 
If Exists (Select * from sys.databases where name = 'EasyMovingSystem') 
DROP DATABASE EasyMovingSystem  
Go 
Create Database EasyMovingSystem 
Go

Use EasyMovingSystem 
Go

/* Create Table 1 */
CREATE TABLE [Role]  
(
Role_ID int primary key identity(1,1) not null,
UserRoleName varchar (50) not null
)

insert into [Role] values 
('Client'),
('Employee')

/* Create Table 2 */
CREATE TABLE [User]
(
[User_ID] int Identity (1,1) Primary Key Not Null,
UserName varchar (50) not null,
[Password] varchar (50) not null,
Email varchar (100) not null,
Role_ID int not null foreign key references Role(Role_ID) on delete no action
)

/* Create Table 3 */
CREATE TABLE Client  
(
Client_ID int Identity (1,1) Primary Key Not Null,
[Address] varchar (50) not null,
[User_ID] int references [User]([User_ID]),
ClientName varchar (50) not null,
ClientSurname varchar (50) not null,
PhoneNum varchar (10) not null,
DateOfBirth date not null
)

/* Create Table 4 */
CREATE TABLE EmployeeType   
(
EmployeeType_ID int Identity (1,1) Primary Key Not Null,
EmployeeTypeName varchar (50) not null,
EmployeeDescription varchar (100) not null
)

insert into EmployeeType values 
('Owner', 'the person who is in charge of the business'),
('Administrator', 'A person who is in charge in adminitration'),
('Driver', 'A person who is Responsible to deliver items'),
('Assistant', 'A person who is helping the driver with delivering items')


/* Create Table 5 */
CREATE TABLE Employee 
(
Employee_ID int Identity (1,1) Primary Key Not Null,
[User_ID] int references [User]([User_ID]),
EmployeeType_ID int references EmployeeType(EmployeeType_ID),
Name varchar (50) not null, 
Surname varchar (50) not null,
PhoneNum int not null,
IDNum int not null,
UserPassword varchar (20) not null
)

/* Create Table 6 */
CREATE TABLE Booking 
(
Booking_ID int Identity (1,1) Primary Key Not Null,
Client_ID int references Client(Client_ID),
DateMade datetime not null
)

/* Create Table 7 */
CREATE TABLE TrackingStatus   
(
TrackingStatus_ID int Identity (1,1) Primary Key Not Null,
TrackingStatusName varchar (20) not null, 
TrackingStatusDescription varchar (200) not null
)

/* Create Table 8 */
CREATE TABLE RentalStatus 
(
RentalStatus_ID int Identity (1,1) Primary Key Not Null,
RentalStatusName varchar (50) not null
)

/* Create Table 9 */
CREATE TABLE InspectionType  
(
InspectionType_ID int Identity (1,1) Primary Key Not Null,
TypeName varchar (50) not null,
TypeDescription varchar (200) not null
)

/* Create Table 10 */
CREATE TABLE [Admin]
(
Admin_ID int Identity (1,1) Primary Key Not Null,
[User_ID] int references [User]([User_ID]),
AdminName varchar (50) not null,
SurName varchar (50) not null,
PhoneNum varchar (10) not null 
)

/* Create Table 11 */
CREATE TABLE TruckType 
(
TruckType_ID int Identity (1,1) Primary Key Not Null,
TruckTypeName varchar (50) not null,
TruckTypeDescription varchar (200) not null
)

/* Create Table 12 */
CREATE TABLE TruckStatus 
(
TruckStatus_ID int Identity (1,1) Primary Key Not Null,
TruckStatusName varchar (50) not null,
)

/* Create Table 13 */
CREATE TABLE Truck 
(
Truck_ID int Identity (1,1) Primary Key Not Null,
Client_ID int references Client(Client_ID),
TruckType_ID int references TruckType(TruckType_ID),
Employee_ID int references Employee(Employee_ID),
TruckStatus_ID int references TruckStatus(TruckStatus_ID),
Admin_ID int references Admin(Admin_ID),
Model varchar (50) not null,
Year int not null,
Colour varchar (50) not null,
NumberPlate varchar (10) not null,
Make varchar (50) not null
)


/* Create Table 14 */
CREATE TABLE Inspection  
(
Inspection_ID int Identity (1,1) Primary Key Not Null,
InspectionNote varchar (200) not null,
InspectionTime datetime not null,
InspectionType_ID int references InspectionType(InspectionType_ID),
Truck_ID int references Truck(Truck_ID),
User_ID int references [User](User_ID),
Admin_ID int references Admin(Admin_ID)
)

/* Create Table 15 */
CREATE TABLE RentalAgreement   
(
Rental_ID int Identity (1,1) Primary Key Not Null,
Truck_ID int references Truck(Truck_ID),
RentalStatus_ID int references RentalStatus(RentalStatus_ID),
Client_ID int references Client(Client_ID),
StartDate date not null,
ExpiryDate date not null,
Inspection_ID int references Inspection(Inspection_ID)
)

/* Create Table 43 */
CREATE TABLE InspectionItem 
(
InspectionItem_ID int Identity (1,1) Primary Key Not Null,
ItemDescription varchar(300) not null,
)

/* Create Table 45 */
CREATE TABLE BookingStatus 
(
BookingStatus_ID int Identity (1,1) Primary Key Not Null,
BookingDescription varchar(50) not null,
)

insert into BookingStatus values
('Booked'),
('Cancelled'),
('Rescheduled')

/* Create Table 16 */
CREATE TABLE BookingInstance   
(
BookingInstance_ID int Identity (1,1) Primary Key Not Null,
TrackingStatus_ID int references TrackingStatus(TrackingStatus_ID),
InspectionItem_ID int references InspectionItem(InspectionItem_ID),
BookingStatus_ID int references BookingStatus(BookingStatus_ID),
Booking_ID int references Booking(Booking_ID),
Employee_ID int references Employee(Employee_ID),
Rental_ID int references RentalAgreement(Rental_ID),
BookingInstanceDate datetime not null,
BookingInstanceDescription varchar (200) not null,

)

/* Create Table 17 */
CREATE TABLE Feedback 
(
Feedback_ID int Identity (1,1) Primary Key Not Null,	 
BookingInstance_ID int references BookingInstance(BookingInstance_ID),
FeedbackDescription varchar (255) not null,
)

/* Create Table 18 */
CREATE TABLE [Service]
(
Service_ID int Identity (1,1) Primary Key Not Null,
ServiceName varchar (50) not null,
ServiceDescription varchar (200) not null
)

/* Create Table 19 */
CREATE TABLE PaymentType  
(
PaymentType_ID int Identity (1,1) Primary Key Not Null,
PaymentTypeName varchar (50) not null
)

insert into PaymentType values
('Cash'),
('EFT')

/* Create Table 20 */
CREATE TABLE Payment 
(
Payment_ID int Identity (1,1) Primary Key Not Null,
PaymentType_ID int references PaymentType(PaymentType_ID),
Rental_ID int references RentalAgreement(Rental_ID),
BookingInstance_ID int references BookingInstance(BookingInstance_ID),
Client_ID int references Client(Client_ID),
AmountPaid decimal (16,2) not null,
AmountDue decimal (16,2) not null,
DatePaid datetime not null 
)

/* Create Table 21 */
CREATE TABLE License 
(
License_ID int Identity (1,1) Primary Key Not Null,
Rental_ID int references RentalAgreement(Rental_ID),
Client_ID int references Client(Client_ID),
ExpiryDate date not null,
)

/* Create Table 22 */
CREATE TABLE ApplicationStatus 
(
ApplicationStatus_ID int Identity (1,1) Primary Key Not Null,
ApplicationStatusName varchar (50) not null,
)

/* Create Table 23 */
CREATE TABLE CandidateStatus   
(
CandidateStatus_ID int Identity (1,1) Primary Key Not Null,
CandidateStatusName varchar (50) not null,
)

/* Create Table 24 */
CREATE TABLE NotificationType
(
NotificationType_ID int Identity (1,1) Primary Key Not Null,
NotificationTypeName varchar (50) not null
)

insert into NotificationType values 
('SMS'),
('Email')

/* Create Table 25 */
CREATE TABLE [Notification]
(
Notification_ID int Identity (1,1) Primary Key Not Null,
NotificationType_ID int references NotificationType(NotificationType_ID),
NotificationDescription varchar (200) not null
)

/* Create Table 26 */
CREATE TABLE UserNotification
(
UserNotification_ID int Identity (1,1) Primary Key Not Null,
Notification_ID int references [Notification](Notification_ID),
User_ID int references [User](User_ID)
)

/* Create Table 27 */
CREATE TABLE Quotation
(
Quotation_ID int Identity (1,1) Primary Key Not Null,
Admin_ID int references Admin(Admin_ID),
QuotationDate datetime not null,
Amount decimal(16,2) not null
)


/* Create Table 28 */
CREATE TABLE QuotationRequest
(
QuotationRequest_ID int Identity (1,1) Primary Key Not Null,
Client_ID int references Client(Client_ID),
QuotationReqDate datetime not null,
QuotationReqDescription varchar (200) not null,
)

/* Create Table 29 */
CREATE TABLE QuotationLine
(
QuotationLine_ID int Identity (1,1) Primary Key Not Null,
QuotationRequest_ID int references QuotationRequest(QuotationRequest_ID),
QuotationLineDescription varchar (200) not null,
Quotation_ID int references Quotation(Quotation_ID),
Service_ID int references Service(Service_ID)
)

/* Create Table 30 */
CREATE TABLE BackgroundCheckStatusType 
(
BackgroundCheckStatusType_ID int Identity (1,1) Primary Key Not Null,
BackgroundCheckStatusName varchar (50) not null,
Admin_ID int references Admin(Admin_ID),
)

/* Create Table 31 */
CREATE TABLE DocumentType 
(
DocumentType_ID int Identity (1,1) Primary Key Not Null,
DocumentTypeDescription varchar(200) not null,
)


/* Create Table 32 */
CREATE TABLE Document 
(
Document_ID int Identity (1,1) Primary Key Not Null,
DocumentName varchar (50) not null,
DocumentType_ID int references DocumentType(DocumentType_ID )
)

/* Create Table 33 */
CREATE TABLE ListingStatus 
(
ListingStatus_ID int Identity (1,1) Primary Key Not Null,
ListingStatusName varchar (50) not null,
)

/* Create Table 34 */
CREATE TABLE JobType 
(
JobType_ID int Identity (1,1) Primary Key Not Null,
JobType varchar(50) not null,
)

/* Create Table 35 */
CREATE TABLE JobListing
(
Job_ID int Identity (1,1) Primary Key Not Null,
JobType_ID int references JobType(JobType_ID),
Discription varchar (200) not null,
Amount decimal(16,2) not null,
HoursOrWeek decimal not null,
Document_ID int references Document(Document_ID),
ListingStatus_ID int references ListingStatus(ListingStatus_ID),
Admin_ID int references Admin(Admin_ID),
)

/* Create Table 36 */
CREATE TABLE Candidate  
(
Candidate_ID int Identity (1,1) Primary Key Not Null,
CandidateStatus_ID int references CandidateStatus(CandidateStatus_ID),
CandidateSurname varchar (50) not null,
CandidateNumber varchar (10) not null,
CandidateName varchar (50) not null
)

/* Create Table 37 */
CREATE TABLE [Application] 
(
Application_ID int Identity (1,1) Primary Key Not Null,
Candidate_ID int not null foreign key references Candidate(Candidate_ID) on delete cascade,
Job_ID int not null foreign key references JobListing(Job_ID) on delete cascade,
ApplicationStatus_ID int not null foreign key references ApplicationStatus(ApplicationStatus_ID) on delete no action,
ApplicationDate date not null
)

/* Create Table 38 */
CREATE TABLE TruckPhoto
(
Photo_ID int Identity (1,1) Primary Key Not Null,
Photo varbinary(max) not null,
Truck_ID int references Truck(Truck_ID)
)

CREATE TABLE [Date]
(
Date_ID int Identity (1,1) Primary Key Not Null,
Date datetime not null
)

/* Create Table 41 */
CREATE TABLE TimeSlot
(
TimeSlot_ID int Identity (1,1) Primary Key Not Null,
StartTime datetime not null,
EndTime datetime not null,
)

/* Create Table 42 */
CREATE TABLE DateOrTimeSlot
(
DateorTimeslot_ID int identity (1,1) primary key not null,
Date_ID int references Date(Date_ID),
TimeSlot_ID int references TimeSlot(TimeSlot_ID)
)

/* Create Table 39 */
CREATE TABLE DateOrTimeSlotOrDriver
(
Employee_ID int references Employee(Employee_ID),
DateorTimeslotDirive_ID int identity(1,1) primary key not null,
DateorTimeslot_ID int references DateorTimeslot(DateorTimeslot_ID),
Description varchar(200) not null,
BookingInstance_ID int references BookingInstance(BookingInstance_ID)
)

/* Create Table 40 */










