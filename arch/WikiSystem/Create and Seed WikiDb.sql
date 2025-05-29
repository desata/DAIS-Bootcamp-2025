Use master;
Go
create database WikiDB;
GO
use WikiDB;
GO
create table Categories
(
	CategoryId int primary key identity NOT NULL,
	Name varchar(100) NOT NULL,
);


create table Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY NOT NULL,
	FullName VARCHAR(100) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(256) NOT NULL,
	Role int NOT NULL,
	AccessLevel int NOT NULL,
);

CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY IDENTITY NOT NULL,
    Title NVARCHAR(200) NOT NULL,	
	AccessLevel INT NOT NULL,
	IsArchived BIT NOT NULL DEFAULT 0,	
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId)	
);

CREATE TABLE DocumentVersions (
    DocumentVersionsId INT PRIMARY KEY IDENTITY NOT NULL, 
	Content NVARCHAR(MAX) NOT NULL,    	
	Version VARCHAR(12) NOT NULL,
	IsArchived BIT NOT NULL DEFAULT 0,
	CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
	DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId)
);

CREATE TABLE Collections (
    CollectionId INT PRIMARY KEY IDENTITY NOT NULL,
    Name NVARCHAR(200) NOT NULL,
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
);

CREATE TABLE CollectionDocuments (
	PRIMARY KEY (CollectionId, DocumentId),
    CollectionId INT NOT NULL FOREIGN KEY REFERENCES Collections(CollectionId),
    DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId),    
);

create table Tags
(
	TagId int primary key identity NOT NULL,
	Name varchar(100) NOT NULL,
);

CREATE TABLE DocumentTags (
	PRIMARY KEY (TagId, DocumentId),    
    DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId),
	TagId INT NOT NULL FOREIGN KEY REFERENCES Tags(TagId),    
);

--123456 8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
INSERT INTO Employees (FullName, Username, PasswordHash, Role, AccessLevel)
VALUES 
('Alice Johnson', 'alice',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 1),    
('Bob Smith', 'bob',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 2),          
('Carol White', 'carol',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 3),      
('David Lee', 'david',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 2),
('Emily Chen', 'emily',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 1),
('Frank Moore', 'frank',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 3),
('Grace Kelly', 'grace',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 2),
('Henry Ford', 'henry.f',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 1),
('Isla Brown', 'isla.b',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 2),
('Jack Davis', 'jack.d',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 1),        
('Karen Black', 'karen.b',	'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 2, 3),
('Ted Scott', 'ted',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 3, 2),
('Phill Scott', 'phill',		'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, 1);

INSERT INTO Categories (Name)
VALUES 
('Software Development'),
('Project Management'),
('Data Science'),
('Machine Learning'),
('UI/UX Design'),
('DevOps'),
('Quality Assurance'),
('Cybersecurity'),
('Cloud Computing'),
('Human Resources'),
('Business Analysis'),
('Technical Writing');

INSERT INTO Tags (Name)
VALUES 
('C#'),
('SQL'),
('Agile'),
('Docker'),
('Python'),
('Testing'),
('Security'),
('Cloud'),
('UX'),
('Machine Learning');

INSERT INTO Documents (Title, AccessLevel, IsArchived, CreatorId, CategoryId)
VALUES
('Intro to C#', 1, 0, 1, 1),
('Agile Practices', 2, 0, 2, 2),
('SQL Performance Tips', 3, 0, 3, 3),
('Docker for DevOps', 1, 0, 1, 1),
('ML Basics', 2, 0, 2, 3);


INSERT INTO DocumentVersions (Content, Version, IsArchived, CreateDate, DocumentId)
VALUES
('Initial version of C# guide', 'v1.0', 0, GETDATE(), 1),
('Updated Agile doc with sprint tips', 'v1.1', 0, GETDATE(), 2),
('Initial SQL tips', 'v1.0', 0, GETDATE(), 3),
('Docker fundamentals', 'v1.0', 0, GETDATE(), 4),
('Machine learning concepts', 'v1.0', 0, GETDATE(), 5);

INSERT INTO Collections (Name, CreatorId)
VALUES
('Developer Essentials', 1),
('Project Management', 2),
('Data & AI', 3);


INSERT INTO CollectionDocuments (CollectionId, DocumentId)
VALUES
(1, 1), -- Intro to C# in Developer Essentials
(1, 4), -- Docker in Developer Essentials
(2, 2), -- Agile in PM
(3, 5), -- ML in Data & AI
(3, 3); -- SQL Tips in Data & AI


INSERT INTO DocumentTags (TagId, DocumentId)
VALUES
(1, 1), -- C# tag for Intro to C#
(3, 2), -- Agile tag for Agile Practices
(2, 3), -- SQL tag for SQL Tips
(4, 4), -- Docker tag for Docker
(10, 5); -- ML tag for ML Basics
