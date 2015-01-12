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
IdCompany bigint,
Lat decimal(10,6),
Lng decimal(10,6),
Imei bigint,
foreign key(UserInsert) references Users,
foreign key(UserUpdate) references Users,
foreign key(IdCompany) references Company
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
LatAreaMax decimal(10,6),
LatAreaMin decimal(10,6),
LngAreaMax decimal(10,6),
LngAreaMin decimal(10,6),
foreign key(IdRute) references Route
)

go

create table Turns
(
Id bigint identity primary key,
IdRoute bigint not null,
IdPoint bigint not null,
AwaitedArrival datetime not null,
NumberTurn int not null,
foreign key (IdRoute) references Route,
foreign key (IdPoint) references Point
)

go

create table TurnControl
(
Id bigint primary key identity,
DateControl datetime,
IdTurn bigint,
IdOneness bigint,
Arrived bit,
foreign key (IdTurn) references Turns,
foreign key (IdOneness) references Oneness
)
go
create table ArriveControl
(
Id bigint identity,
IdTurn bigint not null,
ActualArrival datetime,
primary key (IdTurn,ActualArrival),
foreign key (IdTurn) references Turncontrol
)