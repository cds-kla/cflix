CREATE TABLE "EasterEggs" (
    "Id" serial NOT NULL,
    "Description" text,
    "Hash" varchar(64),
    "Title" varchar(200),
    CONSTRAINT "PK_EasterEggs" PRIMARY KEY ("Id")
);

CREATE TABLE "UserEasterEggs" (
    "EasterEggId" int4 NOT NULL,
    "CFlixUserId" text NOT NULL,
    "CreationDate" timestamptz NOT NULL DEFAULT (current_timestamp),
    CONSTRAINT "PK_UserEasterEggs" PRIMARY KEY ("EasterEggId", "CFlixUserId"),
    CONSTRAINT "FK_UserEasterEggs_AspNetUsers_CFlixUserId" FOREIGN KEY ("CFlixUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserEasterEggs_EasterEggs_EasterEggId" FOREIGN KEY ("EasterEggId") REFERENCES "EasterEggs" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_UserEasterEggs_CFlixUserId" ON "UserEasterEggs" ("CFlixUserId");
CREATE INDEX "IX_EasterEggs_Hash" ON "EasterEggs" ("Hash");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170425143858_03-add EasterEgg', '1.1.1');

