CREATE TABLE "Users" (
  "Id" serial NOT NULL,
  PRIMARY KEY ("Id"),
  "UserName" character varying NULL,
  "PasswordHash" bytea NULL,
  "PasswordSalt" bytea NULL
);