3. Full Script Export with Example Queries
A Full Script Export provides a complete SQL script that creates the entire database structure and often includes example data and queries. It's essentially an executable version of your database design that demonstrates how the system works.
Components of a Full Script Export

Database Schema Creation:

CREATE DATABASE statements
CREATE TABLE statements with constraints
Indexes, views, stored procedures, etc.


Reference Data:

INSERT statements for lookup tables and reference data
Sample data for demonstration


Example Queries:

SELECT statements showing how to retrieve specific information
INSERT, UPDATE, DELETE examples for typical operations
Complex queries demonstrating joins, aggregations, etc.


Documentation:

Comments explaining the purpose of tables and queries
Usage notes and explanations



Example Full Script Export
Here's a simplified example based on your document management system:
sql-- ============================================================
-- Database Creation and Schema Definition
-- ============================================================

-- Create the database
CREATE DATABASE DocumentManagementSystem;
GO

USE DocumentManagementSystem;
GO

-- Create tables
CREATE TABLE DocumentShortInfo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(255) NOT NULL,
    AccessLevelId INT NOT NULL,
    Tag VARCHAR(100),
    DocumentVersion INT NOT NULL DEFAULT 1,
    CategoryId INT NOT NULL,
    Creator VARCHAR(100) NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DocumentId INT NOT NULL
);

CREATE TABLE Document (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Content NVARCHAR(MAX) NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastModifiedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    HasOldVersions BIT NOT NULL DEFAULT 0
);

CREATE TABLE DocumentArchive (
    ArchiveId INT PRIMARY KEY IDENTITY(1,1),
    OriginalDocumentId INT NOT NULL,
    DocumentVersion INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    AccessLevelId INT NOT NULL,
    Tag VARCHAR(100),
    CategoryId INT NOT NULL,
    Creator VARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    ArchiveDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    OriginalCreationDate DATETIME NOT NULL
);

CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(100) NOT NULL,
    FullName VARCHAR(255) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    AccessLevelId INT NOT NULL
);

CREATE TABLE Collection (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    UserId INT NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE CollectionDocument (
    CollectionId INT NOT NULL,
    DocumentId INT NOT NULL,
    AddedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (CollectionId, DocumentId)
);

-- Add foreign key constraints
ALTER TABLE DocumentShortInfo ADD CONSTRAINT FK_DocumentShortInfo_Document
    FOREIGN KEY (DocumentId) REFERENCES Document(Id);

ALTER TABLE DocumentArchive ADD CONSTRAINT FK_DocumentArchive_Document
    FOREIGN KEY (OriginalDocumentId) REFERENCES Document(Id);

ALTER TABLE Collection ADD CONSTRAINT FK_Collection_User
    FOREIGN KEY (UserId) REFERENCES [User](UserId);

ALTER TABLE CollectionDocument ADD CONSTRAINT FK_CollectionDocument_Collection
    FOREIGN KEY (CollectionId) REFERENCES Collection(Id);

ALTER TABLE CollectionDocument ADD CONSTRAINT FK_CollectionDocument_Document
    FOREIGN KEY (DocumentId) REFERENCES Document(Id);

-- ============================================================
-- Reference Data
-- ============================================================

-- Insert reference data for AccessLevel
CREATE TABLE AccessLevel (
    Id INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

INSERT INTO AccessLevel (Id, Name) VALUES
(1, 'Public'),
(2, 'Internal'),
(3, 'Confidential'),
(4, 'Restricted');

-- Insert reference data for Category
CREATE TABLE Category (
    Id INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

INSERT INTO Category (Id, Name) VALUES
(1, 'Financial'),
(2, 'HR'),
(3, 'Legal'),
(4, 'Technical'),
(5, 'Marketing');

-- Insert reference data for Role
CREATE TABLE Role (
    Id INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

INSERT INTO Role (Id, Name) VALUES
(1, 'User'),
(2, 'Editor'),
(3, 'Admin'),
(4, 'System Admin');

-- ============================================================
-- Sample Data
-- ============================================================

-- Insert sample users
INSERT INTO [User] (Username, FullName, PasswordHash, RoleId, AccessLevelId) VALUES
('john.doe', 'John Doe', 'HASH_VALUE_1', 1, 2),
('jane.smith', 'Jane Smith', 'HASH_VALUE_2', 3, 4),
('bob.johnson', 'Bob Johnson', 'HASH_VALUE_3', 2, 3);

-- Insert sample documents
INSERT INTO Document (Content, CreationDate, LastModifiedDate, HasOldVersions) VALUES
('This is a sample HR policy document contents.', '2023-01-15', '2023-01-15', 0),
('Financial report for Q1 2023 showing revenue growth.', '2023-04-10', '2023-04-12', 1),
('Technical specification for the new product release.', '2023-03-22', '2023-03-22', 0);

-- Insert sample document info
INSERT INTO DocumentShortInfo (Title, AccessLevelId, Tag, DocumentVersion, CategoryId, Creator, IsDeleted, DocumentId) VALUES
('HR Policy Document', 2, 'Policy', 1, 2, 'john.doe', 0, 1),
('Q1 Financial Report', 3, 'Report', 2, 1, 'jane.smith', 0, 2),
('Product Specification', 2, 'Spec', 1, 4, 'bob.johnson', 0, 3);

-- Insert sample archived document
INSERT INTO DocumentArchive (OriginalDocumentId, DocumentVersion, Title, AccessLevelId, Tag, CategoryId, Creator, Content, ArchiveDate, OriginalCreationDate) VALUES
(2, 1, 'Q1 Financial Report - Draft', 3, 'Report', 1, 'jane.smith', 'Draft financial report for Q1 2023.', '2023-04-10', '2023-04-10');

-- Insert sample collections
INSERT INTO Collection (Name, UserId, CreationDate) VALUES
('HR Documents', 1, '2023-01-20'),
('Financial Reports', 2, '2023-04-15'),
('Technical Documentation', 3, '2023-03-25');

-- Add documents to collections
INSERT INTO CollectionDocument (CollectionId, DocumentId, AddedDate) VALUES
(1, 1, '2023-01-20'),
(2, 2, '2023-04-15'),
(3, 3, '2023-03-25'),
(2, 1, '2023-04-16'); -- Adding HR policy to Financial Reports collection

-- ============================================================
-- Example Queries
-- ============================================================

-- Example 1: Search for documents by title and category
SELECT 
    dsi.Id, dsi.Title, dsi.Tag, 
    c.Name AS Category, 
    al.Name AS AccessLevel,
    u.FullName AS Creator
FROM 
    DocumentShortInfo dsi
    JOIN Category c ON dsi.CategoryId = c.Id
    JOIN AccessLevel al ON dsi.AccessLevelId = al.Id
    JOIN [User] u ON dsi.Creator = u.Username
WHERE 
    dsi.Title LIKE '%Policy%'
    AND dsi.CategoryId = 2
    AND dsi.IsDeleted = 0;

-- Example 2: Get full document information
SELECT 
    dsi.Id, dsi.Title, dsi.Tag, dsi.DocumentVersion,
    c.Name AS Category, 
    al.Name AS AccessLevel,
    u.FullName AS Creator,
    d.Content, d.CreationDate, d.LastModifiedDate, d.HasOldVersions
FROM 
    DocumentShortInfo dsi
    JOIN Document d ON dsi.DocumentId = d.Id
    JOIN Category c ON dsi.CategoryId = c.Id
    JOIN AccessLevel al ON dsi.AccessLevelId = al.Id
    JOIN [User] u ON dsi.Creator = u.Username
WHERE 
    dsi.Id = 1;

-- Example 3: List all documents in a collection
SELECT 
    c.Name AS CollectionName,
    dsi.Id, dsi.Title, dsi.Tag,
    cat.Name AS Category
FROM 
    Collection c
    JOIN CollectionDocument cd ON c.Id = cd.CollectionId
    JOIN DocumentShortInfo dsi ON cd.DocumentId = dsi.DocumentId
    JOIN Category cat ON dsi.CategoryId = cat.Id
WHERE 
    c.Id = 2
ORDER BY 
    cd.AddedDate DESC;

-- Example 4: Get version history of a document
SELECT 
    dsi.Title AS CurrentTitle, dsi.DocumentVersion AS CurrentVersion,
    da.Title AS ArchivedTitle, da.DocumentVersion AS ArchivedVersion,
    da.ArchiveDate, da.Content AS ArchivedContent
FROM 
    DocumentShortInfo dsi
    JOIN Document d ON dsi.DocumentId = d.Id
    JOIN DocumentArchive da ON d.Id = da.OriginalDocumentId
WHERE 
    dsi.Id = 2
ORDER BY 
    da.DocumentVersion DESC;

-- Example 5: Create a new document
BEGIN TRANSACTION;

INSERT INTO Document (Content, CreationDate, LastModifiedDate, HasOldVersions)
VALUES ('New document content here', GETDATE(), GETDATE(), 0);

DECLARE @NewDocumentId INT = SCOPE_IDENTITY();

INSERT INTO DocumentShortInfo (Title, AccessLevelId, Tag, DocumentVersion, CategoryId, Creator, IsDeleted, DocumentId)
VALUES ('New Document Title', 2, 'Tag1', 1, 3, 'john.doe', 0, @NewDocumentId);

COMMIT;

-- Example 6: Update a document with version archiving
BEGIN TRANSACTION;

-- Get current document info
DECLARE @DocId INT = 1;
DECLARE @DocumentId INT;
DECLARE @OldVersion INT;
DECLARE @OldTitle NVARCHAR(255);
DECLARE @OldAccessLevelId INT;
DECLARE @OldTag NVARCHAR(100);
DECLARE @OldCategoryId INT;
DECLARE @OldCreator NVARCHAR(100);
DECLARE @OldContent NVARCHAR(MAX);
DECLARE @OriginalCreationDate DATETIME;

SELECT 
    @DocumentId = dsi.DocumentId,
    @OldVersion = dsi.DocumentVersion,
    @OldTitle = dsi.Title,
    @OldAccessLevelId = dsi.AccessLevelId,
    @OldTag = dsi.Tag,
    @OldCategoryId = dsi.CategoryId,
    @OldCreator = dsi.Creator,
    @OldContent = d.Content,
    @OriginalCreationDate = d.CreationDate
FROM 
    DocumentShortInfo dsi
    JOIN Document d ON dsi.DocumentId = d.Id
WHERE 
    dsi.Id = @DocId;

-- Archive old version
INSERT INTO DocumentArchive (
    OriginalDocumentId, DocumentVersion, Title, AccessLevelId, Tag, CategoryId, 
    Creator, Content, ArchiveDate, OriginalCreationDate
)
VALUES (
    @DocumentId, @OldVersion, @OldTitle, @OldAccessLevelId, @OldTag, @OldCategoryId,
    @OldCreator, @OldContent, GETDATE(), @OriginalCreationDate
);

-- Update document content
UPDATE Document
SET Content = 'Updated document content here', 
    LastModifiedDate = GETDATE(),
    HasOldVersions = 1
WHERE Id = @DocumentId;

-- Update document info
UPDATE DocumentShortInfo
SET Title = 'Updated Document Title',
    DocumentVersion = @OldVersion + 1
WHERE Id = @DocId;

COMMIT;

-- Example 7: Add document to collection
INSERT INTO CollectionDocument (CollectionId, DocumentId, AddedDate)
VALUES (1, 3, GETDATE());