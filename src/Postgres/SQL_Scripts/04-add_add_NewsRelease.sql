CREATE TABLE "NewsReleases" (
    "Id" serial NOT NULL,
    "Content" text,
    "CreationDate" timestamptz NOT NULL DEFAULT (current_timestamp),
    "NewsReleaseType" int2 NOT NULL,
    "UserName" text,
    CONSTRAINT "PK_NewsReleases" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170429234912_04-add NewsRelease', '1.1.1');

