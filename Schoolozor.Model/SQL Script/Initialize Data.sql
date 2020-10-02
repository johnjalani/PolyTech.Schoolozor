USE TREX

SELECT * FROM AspNetUsers
--SELECT * FROM AspNetUserRoles
--SELECT * FROM AspNetRoles
--INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())
--INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Staff', 'STAFF', NEWID())
--INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Teacher', 'TEACHER', NEWID())
--INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Parent', 'PARENT', NEWID())
--INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Student', 'STUDENT', NEWID())


--DELETE FROM SchoolProfile  
--DELETE FROM SchoolYear
--DELETE FROM SchoolLevel
--DELETE FROM SchoolSection

DECLARE @SchoolId UNIQUEIDENTIFIER = '5C351E2C-07EA-4CD3-AF4D-BDA68EA96EFF'
--INSERT INTO SchoolProfile
--(Id, Name, ContactNumber, FirstName, LastName, InsertedDateTime, UpdatedDateTime, DeletedDateTime)
--VALUES
--(@SchoolId, 'Dino School', '+6321234567890', 'John', 'Bonsol', GETDATE(), GETDATE(), null)
SELECT * FROM SchoolProfile
--UPDATE AspNetUsers SET SchoolId = @SchoolId
--DECLARE @SchoolYearId UNIQUEIDENTIFIER = NEWID()
--INSERT INTO SchoolYear
--(Id, Name, Start, [End], SchoolId, InsertedDateTime, UpdatedDateTime, DeletedDateTime	)
--VALUES
--(@SchoolYearId, 'SY 2020-2021', 'June 8, 2020', 'April 2, 2021', @SchoolId, GETDATE(), GETDATE(), null)
SELECT * FROM SchoolYear
--INSERT INTO SchoolLevel (Id, Name, SchoolId, InsertedDateTime, UpdatedDateTime, DeletedDateTime) VALUES (NEWID(), 'Grade 1', @SchoolId, GETDATE(), GETDATE(), null)
--SELECT * FROM SchoolLevel
--INSERT INTO SchoolSection (Id, Name, SchoolId, InsertedDateTime, UpdatedDateTime, DeletedDateTime) VALUES (NEWID(), 'Magiting', @SchoolId, GETDATE(), GETDATE(), null)
--SELECT * FROM SchoolSection
--INSERT INTO StudentProfile
--(Id, FirstName, LastName, MiddleName, DOB, Email, Gender, CurrentAddressId, PermanentAddressId, Phone, Mobile, UserId, MasterListId, InsertedDateTime, UpdatedDateTime, DeletedDateTime	)
--VALUES
--(NEWID(), 'Josh', 'Bonsol', 'Bayot', 'August 19, 2007', 'joshemmanuelbonsol@gmail.com', 0, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), NULL)
SELECT * FROM StudentProfile
--SELECT * FROM UserAuditEvents
SELECT * FROM StudentAddress