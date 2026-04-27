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
SELECT * FROM Contents;
*/