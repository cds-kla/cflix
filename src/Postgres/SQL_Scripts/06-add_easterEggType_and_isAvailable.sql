ALTER TABLE "EasterEggs" ADD "EasterEggType" int2 NOT NULL DEFAULT 0;

ALTER TABLE "EasterEggs" ADD "IsAvailable" bool NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170505225932_06-add EasterEggType and IsAvailable', '1.1.1');

