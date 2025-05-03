create database ResourcePlanerDB;

create table Users
(
Id bigint primary key identity,
FullName nvarchar(100) not null,
Unit nvarchar(100) not null
);

create table ResourceCharacteristics
(
Id int primary key identity,
Description nvarchar(100) not null,
)

create table ResourceTypes
(
Id int primary key identity,
TypeName nvarchar(100) not null,
Description nvarchar(100) not null,
)

create table ResourceCharacteristicsTypes
(
Id int primary key identity,
ResourceTypesId INT FOREIGN KEY REFERENCES ResourceTypes(Id) NOT NULL,
ResourceCharacteristicsId INT FOREIGN KEY REFERENCES ResourceCharacteristics(Id) NOT NULL,
)

create table Resources
(
Id int primary key identity,
Name nvarchar(100) not null,
Available  BIT NOT NULL DEFAULT 1,
ResourceTypesId INT FOREIGN KEY REFERENCES ResourceTypes(Id) NOT NULL,
)

create table Reservations
(
Id int primary key identity,
UsersCount int not null,
StartTime datetime2 not null,
EndTime datetime2 not null,
Purpose nvarchar(100) not null,
CreatorId BIGINT FOREIGN KEY REFERENCES Users(Id) NOT NULL,
ResourceId INT FOREIGN KEY REFERENCES Resources(Id) NOT NULL,
)