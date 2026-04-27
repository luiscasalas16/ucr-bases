IF DB_ID('DemoEf') IS NULL
	CREATE DATABASE DemoEf;
GO

USE DemoEf;

DROP TABLE IF EXISTS tpt.Articles;
DROP TABLE IF EXISTS tpt.Images;
DROP TABLE IF EXISTS tpt.Videos;
DROP TABLE IF EXISTS tpt.Contents;
DROP SCHEMA IF EXISTS tpt;
GO

CREATE SCHEMA tpt;
GO

CREATE TABLE tpt.Contents (
    ContentId int NOT NULL IDENTITY,
    Title varchar(max) NOT NULL,
    Author varchar(max) NOT NULL,
    PublishedDate datetime NOT NULL,
    ContentType varchar(max) NOT NULL,
    Status varchar(max) NOT NULL,
    CONSTRAINT PK_Contents PRIMARY KEY (ContentId)
);

CREATE TABLE tpt.Articles (
    ContentId int NOT NULL,
    Content varchar(max) NOT NULL,
    Summary varchar(max) NOT NULL,
    CONSTRAINT PK_Articles PRIMARY KEY (ContentId),
    CONSTRAINT FK_Articles_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES tpt.Contents (ContentId) ON DELETE CASCADE
);

CREATE TABLE tpt.Images (
    ContentId int NOT NULL,
    ImageUrl varchar(max) NOT NULL,
    Dimensions varchar(max) NOT NULL,
    CONSTRAINT PK_Images PRIMARY KEY (ContentId),
    CONSTRAINT FK_Images_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES tpt.Contents (ContentId) ON DELETE CASCADE
);

CREATE TABLE tpt.Videos (
    ContentId int NOT NULL,
    VideoUrl varchar(max) NOT NULL,
    Duration int NOT NULL,
    Resolution varchar(max) NOT NULL,
    CONSTRAINT PK_Videos PRIMARY KEY (ContentId),
    CONSTRAINT FK_Videos_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES tpt.Contents (ContentId) ON DELETE CASCADE
);

/*
SELECT * FROM tpt.Contents;
SELECT * FROM tpt.Articles;
SELECT * FROM tpt.Images;
SELECT * FROM tpt.Videos;
*/