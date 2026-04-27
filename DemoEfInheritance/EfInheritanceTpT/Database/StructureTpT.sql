Use Demo;

DROP TABLE IF EXISTS Articles;
DROP TABLE IF EXISTS Images;
DROP TABLE IF EXISTS Videos;
DROP TABLE IF EXISTS Contents;

CREATE TABLE Contents (
    ContentId int NOT NULL IDENTITY,
    Title varchar(max) NOT NULL,
    Author varchar(max) NOT NULL,
    PublishedDate datetime NOT NULL,
    ContentType varchar(max) NOT NULL,
    Status varchar(max) NOT NULL,
    CONSTRAINT PK_Contents PRIMARY KEY (ContentId)
);

CREATE TABLE Articles (
    ContentId int NOT NULL,
    Content varchar(max) NOT NULL,
    Summary varchar(max) NOT NULL,
    CONSTRAINT PK_Articles PRIMARY KEY (ContentId),
    CONSTRAINT FK_Articles_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES Contents (ContentId) ON DELETE CASCADE
);

CREATE TABLE Images (
    ContentId int NOT NULL,
    ImageUrl varchar(max) NOT NULL,
    Dimensions varchar(max) NOT NULL,
    CONSTRAINT PK_Images PRIMARY KEY (ContentId),
    CONSTRAINT FK_Images_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES Contents (ContentId) ON DELETE CASCADE
);

CREATE TABLE Videos (
    ContentId int NOT NULL,
    VideoUrl varchar(max) NOT NULL,
    Duration int NOT NULL,
    Resolution varchar(max) NOT NULL,
    CONSTRAINT PK_Videos PRIMARY KEY (ContentId),
    CONSTRAINT FK_Videos_Contents_ContentId FOREIGN KEY (ContentId) REFERENCES Contents (ContentId) ON DELETE CASCADE
);

/*
SELECT * FROM Contents;
SELECT * FROM Articles;
SELECT * FROM Images;
SELECT * FROM Videos;
*/