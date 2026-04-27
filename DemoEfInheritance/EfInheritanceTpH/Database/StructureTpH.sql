IF DB_ID('DemoEf') IS NULL
	CREATE DATABASE DemoEf;
GO

USE DemoEf;

DROP TABLE IF EXISTS tph.Articles;
DROP TABLE IF EXISTS tph.Images;
DROP TABLE IF EXISTS tph.Videos;
DROP TABLE IF EXISTS tph.Contents;
DROP SCHEMA IF EXISTS tph;
GO

CREATE SCHEMA tph;
GO

CREATE TABLE tph.Contents (
    ContentId int NOT NULL IDENTITY,
    Title varchar(max) NOT NULL,
    Author varchar(max) NOT NULL,
    PublishedDate datetime NOT NULL,
    Status varchar(max) NOT NULL,
    ContentType int NOT NULL,
    Content varchar(max) NULL,
    Summary varchar(max) NULL,
    ImageUrl varchar(max) NULL,
    Dimensions varchar(max) NULL,
    VideoUrl varchar(max) NULL,
    Duration int NULL,
    Resolution varchar(max) NULL,
    CONSTRAINT PK_Contents PRIMARY KEY (ContentId)
);

/*
SELECT * FROM tph.Contents;
*/