ALTER TABLE "UserEasterEggs" ADD "Rate" int2 NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170509214019_07-add UserRating', '1.1.1');

