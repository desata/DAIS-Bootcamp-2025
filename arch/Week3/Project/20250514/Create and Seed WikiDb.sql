Use master;
Go
create database WikiDb;
GO
use WikiDb;
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
	Tags NVARCHAR(500) NOT NULL, 
	AccessLevel INT NOT NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId)
);

CREATE TABLE DocumentVersions (
    DocumentVersionsId INT PRIMARY KEY IDENTITY NOT NULL, 
	Content NVARCHAR(MAX) NOT NULL,    	
	Version VARCHAR(12) NOT NULL,
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
    CollectionId INT NOT NULL FOREIGN KEY REFERENCES Collections(CollectionId) ,
    DocumentId INT NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId) ,    
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


-- Insert 20 Documents
INSERT INTO Documents (Title, Tags, AccessLevel, IsDeleted, CreatorId, CategoryId) VALUES
('Introduction to Agile', 'agile, scrum, project', 3, 0, 1, 1),
('Advanced C# Programming', 'csharp, dotnet, coding', 1, 0, 2, 2),
('Machine Learning Basics', 'ml, ai, data', 3, 0, 3, 3),
('Cloud Migration Guide', 'cloud, azure, migration', 2, 0, 4, 4),
('DevOps Best Practices', 'devops, ci/cd, tools', 2, 0, 5, 5),
('Cybersecurity Fundamentals', 'security, threats, prevention', 1, 0, 6, 6),
('Quality Assurance Strategy', 'qa, testing, bugs', 2, 0, 7, 7),
('Data Visualization Tips', 'data, charts, visuals', 3, 0, 8, 8),
('REST API Design', 'api, rest, http', 2, 0, 9, 9),
('UX Principles 101', 'ux, design, interface', 3, 0, 10, 10),
('HR Onboarding Process', 'hr, onboarding, process', 3, 0, 11, 11),
('Performance Reviews Guide', 'hr, feedback, evaluation', 3, 0, 12, 12),
('Technical Writing Guide', 'docs, writing, technical', 3, 0, 11, 10),
('Project Risk Management', 'risk, project, planning', 2, 0, 1, 9),
('Database Optimization', 'sql, db, performance', 3, 0, 5, 5),
('Kubernetes Essentials', 'k8s, containers, orchestration', 2, 0, 1, 6),
('Business Analysis Techniques', 'business, analysis, requirements', 3, 0, 7, 10),
('AI Model Deployment', 'ml, ai, deployment', 2, 0, 8, 1),
('Introduction to Git', 'git, version control, tools', 3, 0, 12, 9),
('Scrum Master Handbook', 'agile, scrum, team', 1, 0, 2, 10);


--Insert 20 DocumentVersions
INSERT INTO DocumentVersions (Content, Version, CreateDate, DocumentId) VALUES
('Content for Agile introduction document.', 'v1.0', GETDATE(), 1),
('Deep dive into C# advanced features.', 'v1.0', GETDATE(), 2),
('Fundamentals of machine learning concepts.', 'v1.0', GETDATE(), 3),
('Steps for migrating infrastructure to cloud.', 'v1.0', GETDATE(), 4),
('DevOps tools and best practices explained.', 'v1.0', GETDATE(), 5),
('Guide to identify and prevent cyber attacks.', 'v1.0', GETDATE(), 6),
('QA strategies and testing methodologies.', 'v1.0', GETDATE(), 7),
('How to visualize data for better insight.', 'v1.0', GETDATE(), 8),
('Best practices for REST API design.', 'v1.0', GETDATE(), 9),
('UX principles every designer should know.', 'v1.0', GETDATE(), 10),
('Effective onboarding process in HR.', 'v1.0', GETDATE(), 11),
('Performance review system guidelines.', 'v1.0', GETDATE(), 12),
('How to write great technical documents.', 'v1.0', GETDATE(), 13),
('Mitigating risks in project management.', 'v1.0', GETDATE(), 14),
('Optimizing SQL database performance.', 'v1.0', GETDATE(), 15),
('Essentials of Kubernetes orchestration.', 'v1.0', GETDATE(), 16),
('Effective business analysis tools.', 'v1.0', GETDATE(), 17),
('Deploying AI models in production.', 'v1.0', GETDATE(), 18),
('Getting started with Git version control.', 'v1.0', GETDATE(), 19),
('Complete guide for Scrum Masters.', 'v1.0', GETDATE(), 20);

--Insert Collections
INSERT INTO Collections (Name, CreatorId) VALUES 
('Emily''s Collection', 5),
('Bob Work stuff', 2);

INSERT INTO CollectionDocuments(CollectionId, DocumentId) VALUES
(1,1),
(1,2),
(1,5),
(2,5),
(2,6);