ALTER TABLE "AspNetUsers" ADD "DisplayName" text;

ALTER TABLE "AspNetUsers" ADD "EmployeeID" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170328122304_02-add user info', '1.1.1');

