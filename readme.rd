KUBERSAMPLE
===========

Simple web app to show how to use Azure Kubernetes Service (AKS)

* Entity framework

Create DB using:

dotnet ef migrations add teamMigration --context TeamDbContext
dotnet ef database update --context TeamDbContext

dotnet ef migrations add memeberMigration --context MemberDbContext
dotnet ef database update --context MemberDbContext


* SQL Scripts
CREATE DATABASE [Team];
IF SERVERPROPERTY('EngineEdition') <> 5
BEGIN
    ALTER DATABASE [Team] SET READ_COMMITTED_SNAPSHOT ON;
END;
CREATE TABLE [Team] (
        [TeamId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Team] PRIMARY KEY ([TeamId]))

CREATE DATABASE [Member];
IF SERVERPROPERTY('EngineEdition') <> 5
BEGIN
    ALTER DATABASE [Team] SET READ_COMMITTED_SNAPSHOT ON;
END;
CREATE TABLE [Member] (
        [MemberId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NULL,
        [TeamId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Member] PRIMARY KEY ([MemberId]))
