ALTER TABLE "AspNetUsers" ADD "HaveReadRules" bool NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170526115147_08-add readRules', '1.1.2');

