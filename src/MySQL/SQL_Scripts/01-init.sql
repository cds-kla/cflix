CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Medias` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ImageUri` longtext,
    `Title` longtext,
    `Type` smallint NOT NULL,
    CONSTRAINT `PK_Medias` PRIMARY KEY (`Id`)
);

CREATE TABLE `Reviews` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Content` longtext,
    `IsHidden` bit NOT NULL,
    `LastUpdated` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
    `MediaId` int NOT NULL,
    `UserName` longtext,
    CONSTRAINT `PK_Reviews` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Reviews_Medias_MediaId` FOREIGN KEY (`MediaId`) REFERENCES `Medias` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Reviews_MediaId` ON `Reviews` (`MediaId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20170424101714_01-ReInit', '1.1.1');

CREATE USER IF NOT EXISTS 'cflix-ro'@'%' IDENTIFIED BY 'mysql_password_ro';
GRANT SELECT, REFERENCES ON cflixdb.* TO 'cflix-ro'@'%';
