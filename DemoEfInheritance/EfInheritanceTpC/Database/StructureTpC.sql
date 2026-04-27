Use Demo;

DROP TABLE IF EXISTS Articles;
DROP TABLE IF EXISTS Images;
DROP TABLE IF EXISTS Videos;
DROP TABLE IF EXISTS Contents;

CREATE SEQUENCE ContentSequence START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
	
CREATE TABLE Articles (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR ContentSequence),
    Title nvarchar(max) NOT NULL,
    Author nvarchar(max) NOT NULL,
    PublishedDate datetime2 NOT NULL,
    Status nvarchar(max) NOT NULL,
    Content nvarchar(max) NOT NULL,
    Summary nvarchar(max) NOT NULL,
    CONSTRAINT PK_Articles PRIMARY KEY (ContentId)
);

CREATE TABLE Images (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR ContentSequence),
    Title nvarchar(max) NOT NULL,
    Author nvarchar(max) NOT NULL,
    PublishedDate datetime2 NOT NULL,
    Status nvarchar(max) NOT NULL,
    ImageUrl nvarchar(max) NOT NULL,
    Dimensions nvarchar(max) NOT NULL,
    CONSTRAINT PK_Images PRIMARY KEY (ContentId)
);

CREATE TABLE Videos (
    ContentId int NOT NULL DEFAULT (NEXT VALUE FOR ContentSequence),
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
SELECT * FROM Articles;
SELECT * FROM Images;
SELECT * FROM Videos;
*/