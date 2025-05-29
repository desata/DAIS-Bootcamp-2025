Use master;
Go
create database WikiDB;
GO
use WikiDB;
GO

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
);

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NULL,
    RoleId INT NOT NULL,
    AccessLevelId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,    
    CONSTRAINT UQ_Users_Username UNIQUE (Username)
);

CREATE TABLE DocumentShortInformations (
    DocumentShortInformationsId INT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    AccessLevel INT NOT NULL,
    Tags NVARCHAR(500) NOT NULL, 
    DocumentVersion INT NOT NULL DEFAULT 1,
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
    IsDeleted BIT NOT NULL DEFAULT 0,
    
);

CREATE TABLE Documents (
    DocumentId INT PRIMARY KEY,
    Content NVARCHAR(MAX) NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    HasOldVersions BIT NOT NULL DEFAULT 0,
	DocumentShortInformationsId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES DocumentShortInformations(DocumentShortInformationsId),
);


CREATE TABLE DocumentsArchives (
    ArchiveId INT PRIMARY KEY,
    OriginalDocumentId INT NOT NULL,
    DocumentVersion INT NOT NULL,
    Title VARCHAR(200) NOT NULL,
    AccessLevel INT NOT NULL,
    Tags NVARCHAR(500) NOT NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
    CreatorId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
    Content NVARCHAR(MAX) NOT NULL,
    ArchiveDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    OriginalCreationDate DATETIME NOT NULL FOREIGN KEY REFERENCES Documents(DocumentId),
	IsDeleted BIT NOT NULL DEFAULT 0,
);

-- Collections table
CREATE TABLE Collection (
    Id INT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    UserId INT NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES User(UserId)
);

-- Many-to-many relationship table between Collections and Documents
CREATE TABLE CollectionDocument (
    CollectionId INT NOT NULL,
    DocumentId INT NOT NULL,
    AddedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (CollectionId, DocumentId),
    FOREIGN KEY (CollectionId) REFERENCES Collection(Id),
    FOREIGN KEY (DocumentId) REFERENCES Document(Id)
);
