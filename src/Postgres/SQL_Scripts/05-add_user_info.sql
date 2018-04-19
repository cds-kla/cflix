ALTER TABLE "AspNetUsers" ADD "AccountType" int4 NOT NULL DEFAULT 0;

ALTER TABLE "AspNetUsers" ADD "AvatarType" int2 NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170503133105_05-add User Info', '1.1.1');

