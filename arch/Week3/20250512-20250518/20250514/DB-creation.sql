IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BirthdayGifts')
BEGIN
    CREATE DATABASE BirthdayGifts;
END
GO

USE BirthdayGifts;
GO

CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(256) NOT NULL, 
    FullName VARCHAR(100) NOT NULL,
    BirthDate DATE NOT NULL
)

CREATE TABLE Gifts (
    GiftId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Price DECIMAL(10,2)
)

CREATE TABLE VotingSessions (
    VotingSessionId INT IDENTITY(1,1) PRIMARY KEY,
    BirthdayPersonId INT NOT NULL,
    CreatedById INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    BirthYear INT NOT NULL,
    FOREIGN KEY (BirthdayPersonId) REFERENCES Employees(EmployeeId),
    FOREIGN KEY (CreatedById) REFERENCES Employees(EmployeeId)
)

CREATE TABLE Votes (
    VoteId INT IDENTITY(1,1) PRIMARY KEY,
    VotingSessionId INT NOT NULL,
    VoterId INT NOT NULL,
    GiftId INT NOT NULL,
    VoteDate DATETIME NOT NULL,
    FOREIGN KEY (VotingSessionId) REFERENCES VotingSessions(VotingSessionId),
    FOREIGN KEY (VoterId) REFERENCES Employees(EmployeeId),
    FOREIGN KEY (GiftId) REFERENCES Gifts(GiftId)
)

CREATE UNIQUE NONCLUSTERED INDEX IX_ActiveVotingPerPerson
ON VotingSessions (BirthdayPersonId, BirthYear)
WHERE IsActive = 1

CREATE UNIQUE NONCLUSTERED INDEX IX_OneVotePerPersonPerSession
ON Votes (VotingSessionId, VoterId)