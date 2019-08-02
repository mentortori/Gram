USE Gram
GO

DECLARE
     @UserId                        NVARCHAR(450)   = '01eda685-01e2-4aa2-be5b-f7636d81e5ae'
    ,@UserName                      NVARCHAR(256)   = 'joe@doe.com'
    ,@PasswordHash                  NVARCHAR(max)   = 'AQAAAAEAACcQAAAAELZgorQtbFSj1XCB932Kj3+AdwskYK6mM4IruzV38D7nvtRfF//JRG0hpZtLUg+XLQ=='
    ,@SecurityStamp                 NVARCHAR(max)   = 'RMW5C47GC3P2WXAGF3X2M5O6NAGFKH5S'
    ,@UserConcurrencyStamp          NVARCHAR(max)   = '117fb8c7-fa7a-4ba2-8545-cdb78732c638'
    ,@AdministratorRoleId           NVARCHAR(450)   = '548ea374-87ae-47d6-9cba-4542a65f0e81'
    ,@AdministratorRoleName         NVARCHAR(256)   = 'Administrator'
    ,@AdministratorConcurrencyStamp NVARCHAR(max)   = '2ff76ca0-d66d-48aa-ac4d-bcf9eb2e0441'
    ,@EmployeeRoleId                NVARCHAR(450)   = 'dc00e21b-fbf9-4bd2-b8e4-05730bb60ad9'
    ,@EmployeeRoleName              NVARCHAR(256)   = 'Employee'
    ,@EmployeeConcurrencyStamp      NVARCHAR(max)   = 'a238c8cd-f79d-4372-ae60-40e315352ccc'
    ,@NationalityId                 INT             = 1
    ,@EventStatusId                 INT             = 2
    ,@ParticipationStatusId         INT             = 3

DELETE dbo.AspNetUserRoles
DELETE dbo.AspNetUsers
DELETE dbo.AspNetRoles
DELETE Subjects.Person
DELETE General.GeneralType
DELETE Events.Event

-- Create person
SET IDENTITY_INSERT Subjects.Person ON

INSERT Subjects.Person (Id, FirstName, LastName)
SELECT 1, 'Joe', 'Doe'

SET IDENTITY_INSERT Subjects.Person OFF

-- Create employee
SET IDENTITY_INSERT Subjects.Employee ON

INSERT Subjects.Employee (Id, PersonId)
SELECT 1, 1

SET IDENTITY_INSERT Subjects.Employee OFF

-- Create general types
SET IDENTITY_INSERT General.GeneralType ON

INSERT General.GeneralType (Id, Title, IsListed, IsFixed)
VALUES
     (@NationalityId, 'Nationality', 0, 1)
    ,(@EventStatusId, 'EventStatus', 0, 1)
    ,(@ParticipationStatusId, 'ParticipationStatus', 0, 1)

INSERT General.GeneralType (Id, Title, ParentId)
VALUES
     (101, 'Afghan', @NationalityId)
    ,(102, 'Albanian', @NationalityId)
    ,(103, 'Algerian', @NationalityId)
    ,(104, 'American', @NationalityId)
    ,(105, 'Andorran', @NationalityId)
    ,(106, 'Angolan', @NationalityId)
    ,(107, 'Antiguans', @NationalityId)
    ,(108, 'Argentinean', @NationalityId)
    ,(109, 'Armenian', @NationalityId)
    ,(110, 'Australian', @NationalityId)
    ,(111, 'Austrian', @NationalityId)
    ,(112, 'Azerbaijani', @NationalityId)
    ,(113, 'Bahamian', @NationalityId)
    ,(114, 'Bahraini', @NationalityId)
    ,(115, 'Bangladeshi', @NationalityId)
    ,(116, 'Barbadian', @NationalityId)
    ,(117, 'Barbudans', @NationalityId)
    ,(118, 'Batswana', @NationalityId)
    ,(119, 'Belarusian', @NationalityId)
    ,(120, 'Belgian', @NationalityId)
    ,(121, 'Belizean', @NationalityId)
    ,(122, 'Beninese', @NationalityId)
    ,(123, 'Bhutanese', @NationalityId)
    ,(124, 'Bolivian', @NationalityId)
    ,(125, 'Bosnian', @NationalityId)
    ,(126, 'Brazilian', @NationalityId)
    ,(127, 'British', @NationalityId)
    ,(128, 'Bruneian', @NationalityId)
    ,(129, 'Bulgarian', @NationalityId)
    ,(130, 'Burkinabe', @NationalityId)
    ,(131, 'Burmese', @NationalityId)
    ,(132, 'Burundian', @NationalityId)
    ,(133, 'Cambodian', @NationalityId)
    ,(134, 'Cameroonian', @NationalityId)
    ,(135, 'Canadian', @NationalityId)
    ,(136, 'Cape Verdean', @NationalityId)
    ,(137, 'Central African', @NationalityId)
    ,(138, 'Chadian', @NationalityId)
    ,(139, 'Chilean', @NationalityId)
    ,(140, 'Chinese', @NationalityId)
    ,(141, 'Colombian', @NationalityId)
    ,(142, 'Comoran', @NationalityId)
    ,(143, 'Congolese', @NationalityId)
    ,(144, 'Costa Rican', @NationalityId)
    ,(145, 'Croatian', @NationalityId)
    ,(146, 'Cuban', @NationalityId)
    ,(147, 'Cypriot', @NationalityId)
    ,(148, 'Czech', @NationalityId)
    ,(149, 'Danish', @NationalityId)
    ,(150, 'Djibouti', @NationalityId)
    ,(151, 'Dominican', @NationalityId)
    ,(152, 'Dutch', @NationalityId)
    ,(153, 'Dutchman', @NationalityId)
    ,(154, 'Dutchwoman', @NationalityId)
    ,(155, 'East Timorese', @NationalityId)
    ,(156, 'Ecuadorean', @NationalityId)
    ,(157, 'Egyptian', @NationalityId)
    ,(158, 'Emirian', @NationalityId)
    ,(159, 'Equatorial Guinean', @NationalityId)
    ,(160, 'Eritrean', @NationalityId)
    ,(161, 'Estonian', @NationalityId)
    ,(162, 'Ethiopian', @NationalityId)
    ,(163, 'Fijian', @NationalityId)
    ,(164, 'Filipino', @NationalityId)
    ,(165, 'Finnish', @NationalityId)
    ,(166, 'French', @NationalityId)
    ,(167, 'Gabonese', @NationalityId)
    ,(168, 'Gambian', @NationalityId)
    ,(169, 'Georgian', @NationalityId)
    ,(170, 'German', @NationalityId)
    ,(171, 'Ghanaian', @NationalityId)
    ,(172, 'Greek', @NationalityId)
    ,(173, 'Grenadian', @NationalityId)
    ,(174, 'Guatemalan', @NationalityId)
    ,(175, 'Guinea-Bissauan', @NationalityId)
    ,(176, 'Guinean', @NationalityId)
    ,(177, 'Guyanese', @NationalityId)
    ,(178, 'Haitian', @NationalityId)
    ,(179, 'Herzegovinian', @NationalityId)
    ,(180, 'Honduran', @NationalityId)
    ,(181, 'Hungarian', @NationalityId)
    ,(182, 'I-Kiribati', @NationalityId)
    ,(183, 'Icelander', @NationalityId)
    ,(184, 'Indian', @NationalityId)
    ,(185, 'Indonesian', @NationalityId)
    ,(186, 'Iranian', @NationalityId)
    ,(187, 'Iraqi', @NationalityId)
    ,(188, 'Irish', @NationalityId)
    ,(189, 'Israeli', @NationalityId)
    ,(190, 'Italian', @NationalityId)
    ,(191, 'Ivorian', @NationalityId)
    ,(192, 'Jamaican', @NationalityId)
    ,(193, 'Japanese', @NationalityId)
    ,(194, 'Jordanian', @NationalityId)
    ,(195, 'Kazakhstani', @NationalityId)
    ,(196, 'Kenyan', @NationalityId)
    ,(197, 'Kittian and Nevisian', @NationalityId)
    ,(198, 'Kuwaiti', @NationalityId)
    ,(199, 'Kyrgyz', @NationalityId)
    ,(200, 'Laotian', @NationalityId)
    ,(201, 'Latvian', @NationalityId)
    ,(202, 'Lebanese', @NationalityId)
    ,(203, 'Liberian', @NationalityId)
    ,(204, 'Libyan', @NationalityId)
    ,(205, 'Liechtensteiner', @NationalityId)
    ,(206, 'Lithuanian', @NationalityId)
    ,(207, 'Luxembourger', @NationalityId)
    ,(208, 'Macedonian', @NationalityId)
    ,(209, 'Malagasy', @NationalityId)
    ,(210, 'Malawian', @NationalityId)
    ,(211, 'Malaysian', @NationalityId)
    ,(212, 'Maldivan', @NationalityId)
    ,(213, 'Malian', @NationalityId)
    ,(214, 'Maltese', @NationalityId)
    ,(215, 'Marshallese', @NationalityId)
    ,(216, 'Mauritanian', @NationalityId)
    ,(217, 'Mauritian', @NationalityId)
    ,(218, 'Mexican', @NationalityId)
    ,(219, 'Micronesian', @NationalityId)
    ,(220, 'Moldovan', @NationalityId)
    ,(221, 'Monacan', @NationalityId)
    ,(222, 'Mongolian', @NationalityId)
    ,(223, 'Moroccan', @NationalityId)
    ,(224, 'Mosotho', @NationalityId)
    ,(225, 'Motswana', @NationalityId)
    ,(226, 'Mozambican', @NationalityId)
    ,(227, 'Namibian', @NationalityId)
    ,(228, 'Nauruan', @NationalityId)
    ,(229, 'Nepalese', @NationalityId)
    ,(230, 'Netherlander', @NationalityId)
    ,(231, 'New Zealander', @NationalityId)
    ,(232, 'Ni-Vanuatu', @NationalityId)
    ,(233, 'Nicaraguan', @NationalityId)
    ,(234, 'Nigerian', @NationalityId)
    ,(235, 'Nigerien', @NationalityId)
    ,(236, 'North Korean', @NationalityId)
    ,(237, 'Northern Irish', @NationalityId)
    ,(238, 'Norwegian', @NationalityId)
    ,(239, 'Omani', @NationalityId)
    ,(240, 'Pakistani', @NationalityId)
    ,(241, 'Palauan', @NationalityId)
    ,(242, 'Panamanian', @NationalityId)
    ,(243, 'Papua New Guinean', @NationalityId)
    ,(244, 'Paraguayan', @NationalityId)
    ,(245, 'Peruvian', @NationalityId)
    ,(246, 'Polish', @NationalityId)
    ,(247, 'Portuguese', @NationalityId)
    ,(248, 'Qatari', @NationalityId)
    ,(249, 'Romanian', @NationalityId)
    ,(250, 'Russian', @NationalityId)
    ,(251, 'Rwandan', @NationalityId)
    ,(252, 'Saint Lucian', @NationalityId)
    ,(253, 'Salvadoran', @NationalityId)
    ,(254, 'Samoan', @NationalityId)
    ,(255, 'San Marinese', @NationalityId)
    ,(256, 'Sao Tomean', @NationalityId)
    ,(257, 'Saudi', @NationalityId)
    ,(258, 'Scottish', @NationalityId)
    ,(259, 'Senegalese', @NationalityId)
    ,(260, 'Serbian', @NationalityId)
    ,(261, 'Seychellois', @NationalityId)
    ,(262, 'Sierra Leonean', @NationalityId)
    ,(263, 'Singaporean', @NationalityId)
    ,(264, 'Slovakian', @NationalityId)
    ,(265, 'Slovenian', @NationalityId)
    ,(266, 'Solomon Islander', @NationalityId)
    ,(267, 'Somali', @NationalityId)
    ,(268, 'South African', @NationalityId)
    ,(269, 'South Korean', @NationalityId)
    ,(270, 'Spanish', @NationalityId)
    ,(271, 'Sri Lankan', @NationalityId)
    ,(272, 'Sudanese', @NationalityId)
    ,(273, 'Surinamer', @NationalityId)
    ,(274, 'Swazi', @NationalityId)
    ,(275, 'Swedish', @NationalityId)
    ,(276, 'Swiss', @NationalityId)
    ,(277, 'Syrian', @NationalityId)
    ,(278, 'Taiwanese', @NationalityId)
    ,(279, 'Tajik', @NationalityId)
    ,(280, 'Tanzanian', @NationalityId)
    ,(281, 'Thai', @NationalityId)
    ,(282, 'Togolese', @NationalityId)
    ,(283, 'Tongan', @NationalityId)
    ,(284, 'Trinidadian or Tobagonian', @NationalityId)
    ,(285, 'Tunisian', @NationalityId)
    ,(286, 'Turkish', @NationalityId)
    ,(287, 'Tuvaluan', @NationalityId)
    ,(288, 'Ugandan', @NationalityId)
    ,(289, 'Ukrainian', @NationalityId)
    ,(290, 'Uruguayan', @NationalityId)
    ,(291, 'Uzbekistani', @NationalityId)
    ,(292, 'Venezuelan', @NationalityId)
    ,(293, 'Vietnamese', @NationalityId)
    ,(294, 'Welsh', @NationalityId)
    ,(295, 'Yemenite', @NationalityId)
    ,(296, 'Zambian', @NationalityId)
    ,(297, 'Zimbabwean', @NationalityId)
    -- EventStatus
    ,(298, 'New', @EventStatusId)
    ,(299, 'Draft', @EventStatusId)
    ,(300, 'Planned', @EventStatusId)
    ,(301, 'Completed', @EventStatusId)
    ,(302, 'Canceled', @EventStatusId)
    -- ParticipationStatus
    ,(303, 'Booked', @ParticipationStatusId)
    ,(304, 'Canceled', @ParticipationStatusId)
    ,(305, 'Paid', @ParticipationStatusId)
    ,(306, 'Paid partially', @ParticipationStatusId)


SET IDENTITY_INSERT General.GeneralType OFF

-- Create admin user joe@doe.com with password Pa$$w0rd
INSERT dbo.AspNetUsers
(
	 [Id]
    ,[UserName]
    ,[NormalizedUserName]
    ,[Email]
    ,[NormalizedEmail]
    ,[EmailConfirmed]
    ,[PasswordHash]
    ,[SecurityStamp]
    ,[ConcurrencyStamp]
    ,[PhoneNumber]
    ,[PhoneNumberConfirmed]
    ,[TwoFactorEnabled]
    ,[LockoutEnd]
    ,[LockoutEnabled]
    ,[AccessFailedCount]
    ,[EmployeeId]
)
SELECT
	 @UserId
	,@UserName
	,UPPER(@UserName)
	,@UserName
	,UPPER(@UserName)
	,0
	,@PasswordHash
	,@SecurityStamp
	,@UserConcurrencyStamp
	,NULL
	,0
	,0
	,NULL
	,1
	,0
    ,1

-- Create roles
INSERT dbo.AspNetRoles([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES
     (@AdministratorRoleId, @AdministratorRoleName, UPPER(@AdministratorRoleName), @AdministratorConcurrencyStamp)
    ,(@EmployeeRoleId, @EmployeeRoleName, UPPER(@EmployeeRoleName), @EmployeeConcurrencyStamp)

-- Create user roles
INSERT dbo.AspNetUserRoles([UserId], [RoleId])
VALUES
     (@UserId, @AdministratorRoleId)
    ,(@UserId, @EmployeeRoleId)