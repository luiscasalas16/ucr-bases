IF DB_ID('DemoEf') IS NULL
	CREATE DATABASE DemoEf;
GO

USE DemoEf;

DROP TABLE IF EXISTS tpc.Articles;
DROP TABLE IF EXISTS tpc.Images;
DROP TABLE IF EXISTS tpc.Videos;
DROP TABLE IF EXISTS tpc.Contents;
DROP SEQUENCE IF EXISTS tpc.ContentSequence;
DROP SCHEMA IF EXISTS tpc;
GO

CREATE SCHEMA tpc;
GO

CREATE SEQUENCE tpc.ContentSequence START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
	
CREATE TABLE tpc.Articles (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR tpc.ContentSequence),
    Title nvarchar(max) NOT NULL,
    Author nvarchar(max) NOT NULL,
    PublishedDate datetime2 NOT NULL,
    Status nvarchar(max) NOT NULL,
    Content nvarchar(max) NOT NULL,
    Summary nvarchar(max) NOT NULL,
    CONSTRAINT PK_Articles PRIMARY KEY (ContentId)
);

CREATE TABLE tpc.Images (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR tpc.ContentSequence),
    Title nvarchar(max) NOT NULL,
    Author nvarchar(max) NOT NULL,
    PublishedDate datetime2 NOT NULL,
    Status nvarchar(max) NOT NULL,
    ImageUrl nvarchar(max) NOT NULL,
    Dimensions nvarchar(max) NOT NULL,
    CONSTRAINT PK_Images PRIMARY KEY (ContentId)
);

CREATE TABLE tpc.Videos (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR tpc.ContentSequence),
    Title nvarchar(max) NOT NULL,
    Author nvarchar(max) NOT NULL,
    PublishedDate datetime2 NOT NULL,
    Status nvarchar(max) NOT NULL,
    VideoUrl nvarchar(max) NOT NULL,
    Duration int NOT NULL,
    Resolution nvarchar(max) NOT NULL,
    CONSTRAINT PK_Videos PRIMARY KEY (ContentId)
);

/*
SELECT * FROM tpc.Articles;
SELECT * FROM tpc.Images;
SELECT * FROM tpc.Videos;
*/