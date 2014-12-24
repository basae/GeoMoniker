Create DataBase GeoMoniker
go
use Geomoniker
go

create table Company
(
Id bigint primary key identity,
Name varchar(MAX),
InsertDate datetime,
UpdateDate datetime,
Active bit
)
go

create table CompanyAddress
(
Id bigint primary key identity,
Street varchar(50),
Number varchar(30),
Cp varchar(15),
Neighborhood varchar(30),
State varchar(30),
Country varchar(40),
IdCompany bigint,
foreign key(IdCompany) references Company 
)
go

create table UserType
(
Id bigint primary key identity,
Name varchar(50)
)
go

create table Users
(
Id bigint primary key identity,
FirstName varchar(40),
LastName varchar(40),
Celphone varchar(30),
Email varchar(100),
UserTypeId bigint,
IdCompany bigint,
Active bit,
foreign key(UserTypeId) references UserType,
foreign key(IdCompany) references Company
)
go

create table UserAddress
(
Id bigint primary key identity,
Street varchar(50),
Number varchar(30),
Cp varchar(15),
Neighborhood varchar(30),
State varchar(30),
Country varchar(40),
IdUser bigint,
foreign key(IdUser) references Users
)
go

create table Oneness
(
Id bigint identity primary key,
Name varchar(30),
Owner varchar(50),
InsertDate datetime,
UserInsert bigint,
UpdateDate datetime,
UserUpdate bigint,
foreign key(UserInsert) references Users,
foreign key(UserUpdate) references Users
)

go
create table Route
(
Id bigint primary key identity,
Name varchar(50) not null unique,
InsertDate datetime,
UserInsert bigint,
UpdateDate datetime,
UserUpdate bigint,
IdCompany bigint,
foreign key(IdCompany) references Company ,
foreign key(UserInsert) references Users ,
foreign key(UserUpdate) references Users 
)

go
create table Point
(
Id bigint primary key identity,
Description varchar(100),
Lat decimal(10,6) not null,
Lng decimal(10,6) not null,
IdRute bigint,
IsStart bit,
IsEnd bit,
OrderRoute int,
foreign key(IdRute) references Route
)
go
create table ArriveControl
(
Id bigint identity,
IdRoute bigint not null,
IdPoint bigint not null,
IdOneness bigint not null,
NoTurn int not null,
ActualArrival datetime,
AwaitedArrival datetime,
primary key (IdRoute,IdPoint,NoTurn,IdOneness),
foreign key (IdRoute) references Route ,
foreign key (IdPoint) references Point,
foreign key (IdOneness) references Oneness

)