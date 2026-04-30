IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AuditLogs] (
    [LogID] int NOT NULL IDENTITY,
    [UserID] nvarchar(450) NOT NULL,
    [Action] nvarchar(200) NULL,
    [TableName] nvarchar(100) NULL,
    [RecordID] int NULL,
    [Timestamp] datetime NULL DEFAULT ((getdate())),
    [IPAddress] nvarchar(50) NULL,
    CONSTRAINT [PK__AuditLog__5E5499A813DDEF67] PRIMARY KEY ([LogID])
);
GO

CREATE TABLE [CaseTypes] (
    [CaseTypeID] int NOT NULL IDENTITY,
    [TypeName] varchar(120) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_CaseTypes] PRIMARY KEY ([CaseTypeID])
);
GO

CREATE TABLE [Clients] (
    [ClientID] int NOT NULL IDENTITY,
    [UserID] nvarchar(450) NULL,
    [RegisteredByUserId] nvarchar(450) NULL,
    [FirstName] varchar(50) NULL,
    [MiddleName] varchar(50) NULL,
    [LastName] varchar(50) NULL,
    [EmailId] varchar(250) NULL,
    [MobileNo] varchar(250) NULL,
    [Address] nvarchar(255) NULL,
    [State] int NULL,
    [District] int NULL,
    [City] varchar(50) NULL,
    [PinCode] varchar(6) NULL,
    [Notes] nvarchar(max) NULL,
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [IsActive] bit NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK__Clients__E67E1A042F9D710B] PRIMARY KEY ([ClientID])
);
GO

CREATE TABLE [CourtTypes] (
    [CourtTypeID] int NOT NULL IDENTITY,
    [TypeName] varchar(120) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_CourtTypes] PRIMARY KEY ([CourtTypeID])
);
GO

CREATE TABLE [Document_Master] (
    [ID] int NOT NULL IDENTITY,
    [Doc_ID] int NULL,
    [Doc_Name] nvarchar(1000) NULL,
    [Mandatory] char(1) NULL,
    [DigiDoc_Name] varchar(100) NULL,
    [Digi_Mandatory] char(1) NULL,
    [Sort] smallint NULL,
    [File_Extension] varchar(50) NULL,
    [File_SizeBytes] varchar(50) NULL,
    [File_Directory] varchar(150) NULL,
    [Doc_Desc] varchar(150) NULL,
    [Flag] varchar(10) NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_Document_Master] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [DocumentDtls] (
    [ID] int NOT NULL IDENTITY,
    [ApplicationNo] varchar(50) NULL,
    [Doc_ID] int NULL,
    [Doc_Path] varchar(500) NULL,
    [Is_DigiSign] bit NULL,
    [Digi_Path] varchar(500) NULL,
    [IsActive] bit NULL,
    [uploaddatetime] datetime NULL,
    [flag] varchar(25) NULL,
    CONSTRAINT [PK_DocumentDtls] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Documents] (
    [DocumentID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [Title] nvarchar(150) NULL,
    [FilePath] nvarchar(255) NOT NULL,
    [Category] nvarchar(50) NULL,
    [UploadedBy] nvarchar(450) NOT NULL,
    [UploadedAt] datetime NULL DEFAULT ((getdate())),
    [Version] int NULL DEFAULT 1,
    CONSTRAINT [PK__Document__1ABEEF6F6F67F17C] PRIMARY KEY ([DocumentID])
);
GO

CREATE TABLE [Events] (
    [EventID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [Title] nvarchar(150) NULL,
    [EventType] nvarchar(20) NULL,
    [EventDate] datetime NOT NULL,
    [Reminder] bit NULL DEFAULT CAST(0 AS bit),
    [CreatedBy] nvarchar(450) NOT NULL,
    [CreatedAt] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Events__7944C87055661586] PRIMARY KEY ([EventID])
);
GO

CREATE TABLE [InvoiceItems] (
    [ItemID] int NOT NULL IDENTITY,
    [InvoiceID] int NOT NULL,
    [Description] nvarchar(200) NULL,
    [Quantity] int NULL DEFAULT 1,
    [UnitPrice] decimal(10,2) NOT NULL,
    [Total] AS ([Quantity]*[UnitPrice]) PERSISTED,
    CONSTRAINT [PK__InvoiceI__727E83EBC97016ED] PRIMARY KEY ([ItemID])
);
GO

CREATE TABLE [Invoices] (
    [InvoiceID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [ClientID] int NOT NULL,
    [Amount] decimal(10,2) NOT NULL,
    [Status] nvarchar(20) NULL DEFAULT N'Pending',
    [IssuedAt] datetime NULL DEFAULT ((getdate())),
    [DueDate] datetime NULL,
    CONSTRAINT [PK__Invoices__D796AAD5ED8AE495] PRIMARY KEY ([InvoiceID])
);
GO

CREATE TABLE [Messages] (
    [MessageID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [FromUserID] nvarchar(450) NOT NULL,
    [ToUserID] nvarchar(450) NOT NULL,
    [Content] nvarchar(max) NULL,
    [SentAt] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Messages__C87C037C47261C2F] PRIMARY KEY ([MessageID])
);
GO

CREATE TABLE [Notes] (
    [NoteID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [UserID] nvarchar(450) NOT NULL,
    [Content] nvarchar(max) NULL,
    [IsPrivate] bit NULL DEFAULT CAST(1 AS bit),
    [CreatedAt] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Notes__EACE357FD65BA5B4] PRIMARY KEY ([NoteID])
);
GO

CREATE TABLE [OptionMst] (
    [SerialNo] int NOT NULL IDENTITY,
    [OptionName] nvarchar(50) NULL,
    [OptionCode] varchar(6) NULL,
    [OptionDesc] nvarchar(100) NULL,
    [OptionDescHindi] nvarchar(200) NULL,
    [IsActive] bit NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_OptionMst] PRIMARY KEY ([SerialNo])
);
GO

CREATE TABLE [OTPDtls] (
    [SNO] int NOT NULL IDENTITY,
    [OTP] nvarchar(250) NULL,
    [MobileNumber] varchar(250) NULL,
    [EmailId] varchar(2500) NULL,
    [ApplicationNo] varchar(50) NULL,
    [UserID] varchar(50) NULL,
    [Flag] varchar(50) NULL,
    [InstDate] datetime NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_CommanOTPtbl] PRIMARY KEY ([SNO])
);
GO

CREATE TABLE [States] (
    [StateID] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_States] PRIMARY KEY ([StateID])
);
GO

CREATE TABLE [Tasks] (
    [TaskID] int NOT NULL IDENTITY,
    [CaseID] int NOT NULL,
    [AssignedTo] nvarchar(450) NOT NULL,
    [Title] nvarchar(150) NULL,
    [Description] nvarchar(max) NULL,
    [Status] nvarchar(20) NULL DEFAULT N'To-Do',
    [DueDate] datetime NULL,
    [CreatedAt] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Tasks__7C6949D14186CB16] PRIMARY KEY ([TaskID])
);
GO

CREATE TABLE [Districts] (
    [DistrictID] int NOT NULL IDENTITY,
    [StateID] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY ([DistrictID]),
    CONSTRAINT [FK_Districts_States_StateID] FOREIGN KEY ([StateID]) REFERENCES [States] ([StateID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cities] (
    [CityID] int NOT NULL IDENTITY,
    [DistrictID] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([CityID]),
    CONSTRAINT [FK_Cities_Districts_DistrictID] FOREIGN KEY ([DistrictID]) REFERENCES [Districts] ([DistrictID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Courts] (
    [CourtID] int NOT NULL IDENTITY,
    [CourtTypeID] int NOT NULL,
    [CityID] int NOT NULL,
    [Name] varchar(200) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Courts] PRIMARY KEY ([CourtID]),
    CONSTRAINT [FK_Courts_Cities_CityID] FOREIGN KEY ([CityID]) REFERENCES [Cities] ([CityID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courts_CourtTypes_CourtTypeID] FOREIGN KEY ([CourtTypeID]) REFERENCES [CourtTypes] ([CourtTypeID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Lawyers] (
    [LawyerID] int NOT NULL IDENTITY,
    [UserID] nvarchar(450) NOT NULL,
    [FirstName] varchar(50) NULL,
    [MiddleName] varchar(50) NULL,
    [LastName] varchar(50) NULL,
    [DateOfBirth] datetime2 NULL,
    [EmailId] varchar(250) NULL,
    [MobileNo] varchar(20) NULL,
    [Address] nvarchar(255) NULL,
    [StateId] int NULL,
    [CityId] int NULL,
    [ProfileImagePath] nvarchar(400) NULL,
    [BarLicenseNumber] varchar(80) NULL,
    [YearsOfExperience] int NULL,
    [CourtDetails] nvarchar(400) NULL,
    [RegisteredByUserId] nvarchar(450) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Lawyers] PRIMARY KEY ([LawyerID]),
    CONSTRAINT [FK_Lawyers_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([CityID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lawyers_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [States] ([StateID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [LawyerRequests] (
    [LawyerRequestID] int NOT NULL IDENTITY,
    [ClientID] int NOT NULL,
    [LawyerID] int NOT NULL,
    [CaseTypeID] int NOT NULL,
    [StateID] int NOT NULL,
    [DistrictID] int NOT NULL,
    [CityID] int NOT NULL,
    [CaseDescription] nvarchar(2000) NOT NULL,
    [Status] int NOT NULL,
    [LawyerRemark] nvarchar(2000) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_LawyerRequests] PRIMARY KEY ([LawyerRequestID]),
    CONSTRAINT [FK_LawyerRequests_CaseTypes_CaseTypeID] FOREIGN KEY ([CaseTypeID]) REFERENCES [CaseTypes] ([CaseTypeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LawyerRequests_Cities_CityID] FOREIGN KEY ([CityID]) REFERENCES [Cities] ([CityID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LawyerRequests_Clients_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [Clients] ([ClientID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LawyerRequests_Districts_DistrictID] FOREIGN KEY ([DistrictID]) REFERENCES [Districts] ([DistrictID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LawyerRequests_Lawyers_LawyerID] FOREIGN KEY ([LawyerID]) REFERENCES [Lawyers] ([LawyerID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LawyerRequests_States_StateID] FOREIGN KEY ([StateID]) REFERENCES [States] ([StateID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cases] (
    [CaseID] int NOT NULL IDENTITY,
    [LawyerRequestID] int NULL,
    [ClientID] int NOT NULL,
    [Title] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CaseType] nvarchar(100) NULL,
    [CourtName] nvarchar(150) NULL,
    [CaseNumber] nvarchar(100) NULL,
    [Purpose] nvarchar(250) NULL,
    [FilingDate] datetime NULL,
    [Opponent] nvarchar(150) NULL,
    [Status] nvarchar(20) NOT NULL DEFAULT N'Open',
    [Stage] nvarchar(20) NOT NULL DEFAULT N'Filed',
    [AssignedTo] nvarchar(450) NULL,
    [CreatedAt] datetime NULL DEFAULT ((getdate())),
    [UpdatedAt] datetime NULL,
    CONSTRAINT [PK__Cases__6CAE526C7FBA9E16] PRIMARY KEY ([CaseID]),
    CONSTRAINT [FK_Cases_LawyerRequests_LawyerRequestID] FOREIGN KEY ([LawyerRequestID]) REFERENCES [LawyerRequests] ([LawyerRequestID]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Cases_LawyerRequestID] ON [Cases] ([LawyerRequestID]);
GO

CREATE INDEX [IX_Cities_DistrictID] ON [Cities] ([DistrictID]);
GO

CREATE INDEX [IX_Courts_CityID] ON [Courts] ([CityID]);
GO

CREATE INDEX [IX_Courts_CourtTypeID] ON [Courts] ([CourtTypeID]);
GO

CREATE INDEX [IX_Districts_StateID] ON [Districts] ([StateID]);
GO

CREATE INDEX [IX_LawyerRequests_CaseTypeID] ON [LawyerRequests] ([CaseTypeID]);
GO

CREATE INDEX [IX_LawyerRequests_CityID] ON [LawyerRequests] ([CityID]);
GO

CREATE INDEX [IX_LawyerRequests_ClientID] ON [LawyerRequests] ([ClientID]);
GO

CREATE INDEX [IX_LawyerRequests_DistrictID] ON [LawyerRequests] ([DistrictID]);
GO

CREATE INDEX [IX_LawyerRequests_LawyerID] ON [LawyerRequests] ([LawyerID]);
GO

CREATE INDEX [IX_LawyerRequests_StateID] ON [LawyerRequests] ([StateID]);
GO

CREATE INDEX [IX_Lawyers_CityId] ON [Lawyers] ([CityId]);
GO

CREATE INDEX [IX_Lawyers_StateId] ON [Lawyers] ([StateId]);
GO

CREATE UNIQUE INDEX [IX_Lawyers_UserID] ON [Lawyers] ([UserID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260317163228_InitialCMS', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Staffs] (
    [StaffID] int NOT NULL IDENTITY,
    [UserID] nvarchar(450) NOT NULL,
    [LawyerID] int NOT NULL,
    [RegisteredByUserId] nvarchar(450) NULL,
    [FirstName] varchar(50) NULL,
    [MiddleName] varchar(50) NULL,
    [LastName] varchar(50) NULL,
    [DateOfBirth] datetime2 NULL,
    [EmailId] varchar(250) NULL,
    [MobileNo] varchar(20) NULL,
    [Address] nvarchar(255) NULL,
    [ProfileImagePath] nvarchar(400) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(450) NULL,
    [CreatedDate] datetime NULL DEFAULT ((getdate())),
    [UpdatedBy] nvarchar(450) NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY ([StaffID]),
    CONSTRAINT [FK_Staffs_Lawyers_LawyerID] FOREIGN KEY ([LawyerID]) REFERENCES [Lawyers] ([LawyerID]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Staffs_LawyerID] ON [Staffs] ([LawyerID]);
GO

CREATE UNIQUE INDEX [IX_Staffs_UserID] ON [Staffs] ([UserID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260329082031_AddStaff', N'8.0.0');
GO

COMMIT;
GO

