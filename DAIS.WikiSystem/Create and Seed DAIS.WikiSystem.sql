Use master;
Go
create database DAIS.WikiSystem;
GO
use DAIS.WikiSystem;
GO

create table Roles
(
	RoleId int primary key identity NOT NULL,
	Name varchar(20) NOT NULL,
);

create table AccessLevels
(
	AccessLevelId int primary key identity NOT NULL,
	Name varchar(20) NOT NULL,
);

create table Categories
(
	CategoryId int primary key identity NOT NULL,
	Name varchar(100) NOT NULL,
);

create table Users
(
	UserId INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	Password VARCHAR(256) NOT NULL,
	Role INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleId),
	AccessLevel INT NOT NULL FOREIGN KEY REFERENCES AccessLevels(AccessLevelId)
);

CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY IDENTITY NOT NULL,
    Title NVARCHAR(200) NOT NULL,		
	IsDeleted BIT NOT NULL DEFAULT 0,	
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
	AccessLevel INT NOT NULL FOREIGN KEY REFERENCES AccessLevels(AccessLevelId)
);

CREATE TABLE DocumentVersions (
    DocumentVersionsId INT PRIMARY KEY IDENTITY NOT NULL, 
	FilePath NVARCHAR(MAX) NOT NULL,    	
	Version VARCHAR(12) NOT NULL,
	IsArchived BIT NOT NULL DEFAULT 0,
	CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
	DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId)
);

CREATE TABLE Collections (
    CollectionId INT PRIMARY KEY IDENTITY NOT NULL,
    Name NVARCHAR(50) NOT NULL,
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
);

CREATE TABLE CollectionDocuments (
	PRIMARY KEY (CollectionId, DocumentId),
    CollectionId INT NOT NULL FOREIGN KEY REFERENCES Collections(CollectionId),
    DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId),    
);

create table Tags
(
	TagId int primary key identity NOT NULL,
	Name varchar(50) NOT NULL,
);

CREATE TABLE DocumentTags (
	PRIMARY KEY (TagId, DocumentId),    
    DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId),
	TagId INT NOT NULL FOREIGN KEY REFERENCES Tags(TagId),    
);

--123456 8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
INSERT INTO Roles (Name) VALUES 
('Admin'), ('Editor'), ('Viewer');


INSERT INTO AccessLevels (Name) VALUES 
('Public'), ('Internal'), ('Confidential');

INSERT INTO Categories (Name) VALUES 
('Development'), ('Design'), ('Marketing'), ('Finance'), ('HR'),
('IT'), ('Sales'), ('Support'), ('Legal'), ('Management'),
('Security'), ('Data'), ('UX'), ('Mobile'), ('Web'),
('Research'), ('Testing'), ('Training'), ('Infrastructure'), ('Operations');

INSERT INTO Users (FirstName, LastName, Username, Password, RoleId, AccessLevelId) VALUES
('Alice', 'Smith', 'asmith', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 1),
('Bob', 'Jones', 'bjones', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 2),
('Charlie', 'Brown', 'cbrown', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 3),
('Dess', 'White', 'dess', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 3),
('Ethan', 'Black', 'eblack', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 1),
('Fiona', 'Green', 'fgreen', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 1),
('George', 'Adams', 'gadams', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 3),
('Hannah', 'Mills', 'hmills', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 2),
('Ian', 'Scott', 'iscott', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 2),
('Jill', 'Baker', 'jbaker', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 1),
('Kevin', 'Wright', 'kwright', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 3),
('Laura', 'Hill', 'lhill', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 1),
('Mike', 'Cooper', 'mcooper', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 2),
('Nina', 'Bell', 'nbell', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 1),
('Oscar', 'Stone', 'ostone', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 3),
('Paula', 'Reed', 'preed', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 2),
('Quinn', 'Young', 'qyoung', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 3),
('Rachel', 'Gray', 'rgray', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 1),
('Steve', 'Ward', 'sward', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 2),
('Tina', 'Fox', 'tfox', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 1);

INSERT INTO Documents (Title, CreatorId, CategoryId, AccessLevelId) VALUES
('Document 1', 1, 1, 1), ('Document 2', 2, 2, 2), ('Document 3', 3, 3, 3), ('Document 4', 4, 4, 1),
('Document 5', 5, 5, 2), ('Document 6', 6, 6, 3), ('Document 7', 7, 7, 1), ('Document 8', 8, 8, 2),
('Document 9', 9, 9, 3), ('Document 10', 10, 10, 1), ('Document 11', 11, 11, 2), ('Document 12', 12, 12, 3),
('Document 13', 13, 13, 1), ('Document 14', 14, 14, 2), ('Document 15', 15, 15, 3), ('Document 16', 16, 16, 1),
('Document 17', 17, 17, 2), ('Document 18', 18, 18, 3), ('Document 19', 19, 19, 1), ('Document 20', 20, 20, 2);


INSERT INTO DocumentVersions (FilePath, Version, DocumentId) VALUES
('Content A', 'v1.0', 1), ('Content B', 'v1.0', 2), ('Content C', 'v1.0', 3), ('Content D', 'v1.0', 4),
('Content E', 'v1.0', 5), ('Content F', 'v1.0', 6), ('Content G', 'v1.0', 7), ('Content H', 'v1.0', 8),
('Content I', 'v1.0', 9), ('Content J', 'v1.0', 10), ('Content K', 'v1.0', 11), ('Content L', 'v1.0', 12),
('Content M', 'v1.0', 13), ('Content N', 'v1.0', 14), ('Content O', 'v1.0', 15), ('Content P', 'v1.0', 16),
('Content Q', 'v1.0', 17), ('Content R', 'v1.0', 18), ('Content S', 'v1.0', 19), ('Content T', 'v1.0', 20);


INSERT INTO Collections (Name, CreatorId) VALUES
('Collection 1', 1), ('Collection 2', 2), ('Collection 3', 3), ('Collection 4', 4), ('Collection 5', 5),
('Collection 6', 6), ('Collection 7', 7), ('Collection 8', 8), ('Collection 9', 9), ('Collection 10', 10),
('Collection 11', 11), ('Collection 12', 12), ('Collection 13', 13), ('Collection 14', 14), ('Collection 15', 15),
('Collection 16', 16), ('Collection 17', 17), ('Collection 18', 18), ('Collection 19', 19), ('Collection 20', 20);

INSERT INTO CollectionDocuments (CollectionId, DocumentId) VALUES
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5),
(6, 6), (7, 7), (8, 8), (9, 9), (10, 10),
(11, 11), (12, 12), (13, 13), (14, 14), (15, 15),
(16, 16), (17, 17), (18, 18), (19, 19), (20, 20);

INSERT INTO Tags (Name) VALUES
('Urgent'), ('Important'), ('Draft'), ('Final'), ('Reviewed'),
('Internal'), ('Client'), ('Public'), ('QA'), ('Bugfix'),
('Release'), ('Note'), ('Help'), ('Reference'), ('Meeting'),
('Plan'), ('Report'), ('Proposal'), ('Architecture'), ('Training');

INSERT INTO DocumentTags (DocumentId, TagId) VALUES
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5),
(6, 6), (7, 7), (8, 8), (9, 9), (10, 10),
(11, 11), (12, 12), (13, 13), (14, 14), (15, 15),
(16, 16), (17, 17), (18, 18), (19, 19), (20, 20);
