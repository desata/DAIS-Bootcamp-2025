Use master;
Go
create database OfficeResourcesReservationSystem;
GO
use OfficeResourcesReservationSystem;
GO

create table Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY NOT NULL,
	FullName VARCHAR(100) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(256) NOT NULL,
);


create table ResourceTypes
(
	ResourceTypeId INT PRIMARY KEY IDENTITY NOT NULL,
	Name VARCHAR(100) NOT NULL,
)

create table Resources
(
	ResourceId INT PRIMARY KEY IDENTITY NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Description	NVARCHAR(255),
	IsAvailable BIT NOT NULL DEFAULT 1,
	ResourceTypeId INT NOT NULL FOREIGN KEY REFERENCES ResourceTypes(ResourceTypeId),
);

create table ResourceCharacteristics
(
	ResourceCharacteristicId INT PRIMARY KEY IDENTITY NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Value VARCHAR(200) NOT NULL,	
	ResourceId INT NOT NULL FOREIGN KEY REFERENCES Resources(ResourceId)
)

create table Reservations
(
	ReservationId INT PRIMARY KEY IDENTITY NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	Purpose VARCHAR(100) NOT NULL,
	NumberOfParticipants INT NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId)
)

CREATE TABLE ResourceReservations
(
    PRIMARY KEY (ReservationId, ResourceId),
    ReservationId INT NOT NULL FOREIGN KEY REFERENCES Reservations(ReservationId),
    ResourceId INT NOT NULL FOREIGN KEY REFERENCES Resources(ResourceId)
);

--Seed
-- Seed Employees
INSERT INTO Employees (FullName, Username, PasswordHash)
VALUES 
('Alice Johnson', 'alice',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),    
('Bob Smith', 'bob',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),          
('Carol White', 'carol',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),      
('David Lee', 'david',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Emily Chen', 'emily',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Frank Moore', 'frank',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Grace Kelly', 'grace',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Henry Ford', 'henry.f',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Isla Brown', 'isla.b',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Jack Davis', 'jack.d',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),        
('Karen Black', 'karen.b',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Ted Scott', 'ted',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Phill Scott', 'phill',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Nina Williams', 'nina',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Oscar Wilde', 'oscar',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Pam Beesly', 'pam',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Quentin Cole', 'quentin', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Rachel Green', 'rachel',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Steve Rogers', 'steve',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'),
('Tina Cohen-Cag', 'tina',  '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92');

-- Seed ResourceTypes
INSERT INTO ResourceTypes (Name)
VALUES 
('Meeting Room'),
('Laptop'),
('Projector'),
('Conference Phone'),
('Video Camera'),
('Printer'),
('Scanner'),
('Whiteboard');

-- Seed Resources
INSERT INTO Resources (Name, Description, IsAvailable, ResourceTypeId)
VALUES 
('Room A', 'Large meeting room with TV', 1, 1),
('Room B', 'Medium meeting room', 1, 1),
('Room C', 'Small quiet room', 1, 1),
('Laptop 1', 'Dell XPS 13', 1, 2),
('Laptop 2', 'HP EliteBook', 1, 2),
('Laptop 3', 'MacBook Pro', 1, 2),
('Laptop 4', 'Lenovo ThinkPad', 1, 2),
('Projector 1', 'HD Projector', 1, 3),
('Projector 2', '4K Projector', 1, 3),
('Projector 3', 'Portable Projector', 1, 3),
('Whiteboard 1', 'Magnetic whiteboard', 1, 4),
('Whiteboard 2', 'Glass whiteboard', 1, 4),
('Room D', 'Executive conference room', 1, 1),
('Laptop 5', 'Surface Laptop', 1, 2),
('Projector 4', 'Laser projector', 1, 3),
('Whiteboard 3', 'Classic whiteboard', 1, 4),
('Room E', 'Brainstorming room', 1, 1),
('Laptop 6', 'Gaming laptop', 1, 2),
('Room F', 'Interview room', 1, 1),
('Projector 5', 'Short-throw projector', 1, 3);

-- Seed ResourceCharacteristics

-- MEETING ROOMS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Capacity', '10', 1),
('Has TV', 'Yes', 1),
('Whiteboard', 'Yes', 1),
('Capacity', '6', 2),
('Has TV', 'No', 2),
('Whiteboard', 'Yes', 2);

-- LAPTOPS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Processor', 'Intel i7', 4),
('RAM', '16GB', 4),
('Storage', '512GB SSD', 4),
('Processor', 'AMD Ryzen 5', 5),
('RAM', '8GB', 5),
('Storage', '256GB SSD', 5);

-- PROJECTORS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Resolution', '1920x1080', 8),
('Lumens', '3500', 8),
('Throw Ratio', 'Short-throw', 8),
('Resolution', '4K', 9),
('Lumens', '4000', 9);

-- CONFERENCE PHONES
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Microphone Range', '5 meters', 10),
('Bluetooth', 'Yes', 10),
('Model', 'Polycom Trio 8500', 10);

-- VIDEO CAMERAS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Resolution', '1080p', 11),
('Zoom', '10x Optical', 11),
('Field of View', '90°', 11);

-- PRINTERS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Type', 'Laser', 12),
('Color Printing', 'Yes', 12),
('Print Speed', '35 ppm', 12);

-- SCANNERS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Scan Resolution', '600 DPI', 13),
('Duplex', 'Yes', 13),
('ADF Capacity', '50 pages', 13);

-- WHITEBOARDS
INSERT INTO ResourceCharacteristics (Name, Value, ResourceId)
VALUES
('Type', 'Magnetic', 11),
('Size', '6ft x 4ft', 11),
('Mounted', 'Yes', 11);


-- Seed Reservations (all in the future)
INSERT INTO Reservations (StartDate, EndDate, Purpose, NumberOfParticipants, IsActive, CreatorId)
VALUES 
(GETDATE() + 1, GETDATE() + 1.05, 'Team meeting', 5, 1, 1),
(GETDATE() + 2, GETDATE() + 2.03, 'Client call', 2, 1, 2),
(GETDATE() + 3, GETDATE() + 3.1, 'Workshop', 10, 1, 3),
(GETDATE() + 1, GETDATE() + 1.02, 'Presentation', 4, 1, 4),
(GETDATE() + 2, GETDATE() + 2.01, 'Planning session', 6, 1, 5),
(GETDATE() + 1, GETDATE() + 1.04, 'Training', 8, 1, 6),
(GETDATE() + 3, GETDATE() + 3.02, 'Demo', 3, 1, 7),
(GETDATE() + 4, GETDATE() + 4.01, 'Interview', 2, 1, 8),
(GETDATE() + 2, GETDATE() + 2.05, 'Design Sprint', 6, 1, 9),
(GETDATE() + 5, GETDATE() + 5.03, 'Retrospective', 7, 1, 10),
(GETDATE() + 6, GETDATE() + 6.01, 'Project kickoff', 12, 1, 11),
(GETDATE() + 7, GETDATE() + 7.02, 'Townhall', 20, 1, 12),
(GETDATE() + 8, GETDATE() + 8.01, 'Scrum meeting', 5, 1, 13),
(GETDATE() + 2, GETDATE() + 2.02, 'One-on-one', 2, 1, 14),
(GETDATE() + 3, GETDATE() + 3.01, 'Demo day', 15, 1, 15),
(GETDATE() + 1, GETDATE() + 1.03, 'Review', 6, 1, 16),
(GETDATE() + 4, GETDATE() + 4.02, 'Product testing', 5, 1, 17),
(GETDATE() + 5, GETDATE() + 5.01, 'Technical talk', 10, 1, 18),
(GETDATE() + 6, GETDATE() + 6.03, 'Training', 8, 1, 19),
(GETDATE() + 7, GETDATE() + 7.01, 'Customer feedback', 4, 1, 20);

-- Seed ResourceReservations (random links between reservations and resources)
INSERT INTO ResourceReservations (ReservationId, ResourceId)
VALUES
(1, 1), (1, 4),
(2, 2), (2, 8),
(3, 13), (3, 9), (3, 11),
(4, 3), (4, 5),
(5, 1), (5, 6),
(6, 17), (6, 7), (6, 9),
(7, 18), (7, 8),
(8, 19), (8, 10),
(9, 13), (9, 15), (9, 12),
(10, 17), (10, 5),
(11, 13), (11, 6),
(12, 1), (12, 8), (12, 11),
(13, 2), (13, 7),
(14, 3), (14, 4),
(15, 13), (15, 15),
(16, 1), (16, 12),
(17, 17), (17, 14),
(18, 2), (18, 9),
(19, 3), (19, 18),
(20, 13), (20, 10);
