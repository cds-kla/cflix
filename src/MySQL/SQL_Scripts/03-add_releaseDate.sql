ALTER TABLE `Medias` ADD `ReleaseDate` TIMESTAMP NOT NULL DEFAULT current_timestamp;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20170505183739_03-add ReleaseDate', '1.1.1');

