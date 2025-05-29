Use master;

create database WikiDB;

use WikiDB;

create table Categories
(
	CategoryId int primary key identity NOT NULL,
	Name varchar(100) NOT NULL,
);

create table Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY NOT NULL,
	FullName varchar(100) NOT NULL,
	Username varchar(50) NOT NULL,
	Password varchar(256) NOT NULL,
	Picture varchar(max),
	Role int NOT NULL,
	AccessLevel int NOT NULL,
);

CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY IDENTITY NOT NULL,
    Title NVARCHAR(200) NOT NULL,
	Tags nvarchar(500) NOT NULL, 
	IsDeleted BIT NOT NULL DEFAULT 0,
	IsArchived BIT NOT NULL DEFAULT 0,
	AuthorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId)
);

CREATE TABLE DocumentInformations (
    DocumentInformationId INT PRIMARY KEY IDENTITY NOT NULL,  	
	Content NVARCHAR(MAX) NOT NULL,
    [Version] varchar(12) NOT NULL, 	
	AccessLevel INT NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedAt DATETIME,
    DocumentId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Documents(DocumentId)
);

CREATE TABLE DocumentArchives (
    ArchiveId INT PRIMARY KEY IDENTITY not null,
    Title NVARCHAR(200) NOT NULL,
    AuthorId INT NOT NULL,
    CategoryId INT NOT NULL,
    Tags NVARCHAR(500) NOT NULL,
    AccessLevel INT NOT NULL,    
    [Version] varchar(12) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
	ArchivedAt DATETIME NOT NULL DEFAULT GETDATE(),    
	OriginalDocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId)
);

CREATE TABLE Collections (
    CollectionId INT PRIMARY KEY IDENTITY NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) ,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	IsDeleted BIT NOT NULL DEFAULT 0
);

CREATE TABLE CollectionDocuments (
    CollectionId INT NOT NULL,
    DocumentId INT NOT NULL,
    PRIMARY KEY (CollectionId, DocumentId),
    FOREIGN KEY (CollectionId) REFERENCES Collections(CollectionId) ,
    FOREIGN KEY (DocumentId) REFERENCES Documents(DocumentId) 
);
--123456 8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
INSERT INTO Employees (FullName, Username, Password, Picture, Role, AccessLevel)
VALUES 
('Alice Johnson', 'alice.j', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 1, 1),    
('Bob Smith', 'bob.s', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 2, 2),          
('Carol White', 'carol.w', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 3, 3),      
('David Lee', 'david.l', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 2, 2),
('Emily Chen', 'emily.c', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 3, 1),
('Frank Moore', 'frank.m', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 2, 3),
('Grace Kim', 'grace.k', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 3, 2),
('Henry Ford', 'henry.f', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 2, 1),
('Isla Brown', 'isla.b', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 3, 2),
('Jack Davis', 'jack.d', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 1, 1),        
('Karen Black', 'karen.b', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 2, 3),
('Liam Scott', 'liam.s', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', NULL, 3, 2);

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


-- Insert 20 Documents
INSERT INTO Documents (Title, Tags, IsDeleted, IsArchived, AuthorId, CategoryId) VALUES
('Agile Development Guide', 'agile,scrum,development', 0, 0, 1, 2),
('Cloud Migration Strategy', 'cloud,aws,azure,migration', 0, 0, 2, 9),
('Data Visualization Guide', 'data,visualization,charts', 0, 0, 3, 3),
('CI/CD Pipeline Setup', 'devops,ci/cd,pipelines', 0, 0, 4, 6),
('QA Checklist', 'qa,testing,manual,automated', 0, 0, 5, 7),
('Machine Learning 101', 'ml,ai,intro,models', 0, 0, 6, 4),
('Secure Coding Best Practices', 'security,code,safe', 0, 0, 7, 8),
('Microservices in Practice', 'microservices,docker,k8s', 0, 0, 8, 1),
('UI/UX Principles', 'ux,ui,design,layout', 0, 0, 9, 5),
('Business Requirement Doc', 'requirements,analysis,business', 0, 0, 10, 11),
('HR Policy Handbook', 'hr,policies,handbook', 0, 0, 11, 10),
('Docker Deep Dive', 'docker,containers,images', 0, 0, 12, 6),
('Advanced SQL Queries', 'sql,database,queries', 0, 0, 1, 1),
('Git & GitHub Essentials', 'git,github,versioncontrol', 0, 0, 2, 1),
('Project Timeline Template', 'project,template,planning', 0, 0, 3, 2),
('Security Incident Protocol', 'incident,response,security', 0, 0, 4, 8),
('API Docs Standards', 'api,documentation,style', 0, 0, 5, 12),
('Cloud Cost Optimization', 'cloud,cost,optimization', 0, 0, 6, 9),
('User Feedback Guide', 'feedback,ux,data', 0, 0, 7, 5),
('Jenkins CI Config', 'jenkins,ci,cd,automation', 0, 0, 8, 6);


--Insert 20 DocumentInformations
INSERT INTO DocumentInformations (Content, [Version], AccessLevel, CreatedAt, UpdatedAt, DocumentId) VALUES
('Complete guide to agile methodologies.', 'v1.0', 1, GETDATE(), NULL, 1),
('Planning and executing cloud migrations.', 'v1.0', 2, GETDATE(), NULL, 2),
('Tips on effective data visualization.', 'v1.0', 3, GETDATE(), NULL, 3),
('Setting up CI/CD with Jenkins and GitHub.', 'v1.0', 1, GETDATE(), NULL, 4),
('Manual and automated testing checklist.', 'v1.0', 2, GETDATE(), NULL, 5),
('Intro to machine learning concepts.', 'v1.0', 3, GETDATE(), NULL, 6),
('How to write secure code.', 'v1.0', 1, GETDATE(), NULL, 7),
('Deploying microservices using Docker and Kubernetes.', 'v1.0', 2, GETDATE(), NULL, 8),
('UI/UX principles for product design.', 'v1.0', 3, GETDATE(), NULL, 9),
('Structure for writing business requirement docs.', 'v1.0', 1, GETDATE(), NULL, 10),
('Handbook for HR policies.', 'v1.0', 2, GETDATE(), NULL, 11),
('Deep dive into Docker features.', 'v1.0', 3, GETDATE(), NULL, 12),
('Advanced SQL performance tips.', 'v1.0', 1, GETDATE(), NULL, 13),
('How to use Git and GitHub effectively.', 'v1.0', 2, GETDATE(), NULL, 14),
('Template for managing project timelines.', 'v1.0', 3, GETDATE(), NULL, 15),
('Plan for handling security incidents.', 'v1.0', 1, GETDATE(), NULL, 16),
('Standard format for API documentation.', 'v1.0', 2, GETDATE(), NULL, 17),
('Ways to optimize your cloud costs.', 'v1.0', 3, GETDATE(), NULL, 18),
('Collecting and analyzing user feedback.', 'v1.0', 1, GETDATE(), NULL, 19),
('Configuring Jenkins for CI pipelines.', 'v1.0', 2, GETDATE(), NULL, 20);
