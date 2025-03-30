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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Advances] (
        [AdvanceID] int NOT NULL IDENTITY,
        [ConsumerID] nvarchar(max) NOT NULL,
        [Amount] float NOT NULL,
        [DateInserted] datetime2 NOT NULL,
        [DateUsed] datetime2 NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Advances] PRIMARY KEY ([AdvanceID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Bills] (
        [BillID] int NOT NULL IDENTITY,
        [ReadingID] nvarchar(max) NOT NULL,
        [Consumed_Amount] float NOT NULL,
        [From] date NOT NULL,
        [To] date NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [BillDate] datetime2 NOT NULL,
        [BillStatus] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Bills] PRIMARY KEY ([BillID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Consumers] (
        [ConsumerID] int NOT NULL IDENTITY,
        [UserID] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [ConnectionType] nvarchar(max) NOT NULL,
        [Connection_Date] datetime2 NOT NULL,
        [Meter_Number] nvarchar(max) NOT NULL,
        [Consumer_Status] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Consumers] PRIMARY KEY ([ConsumerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Meters] (
        [ReadingID] int NOT NULL IDENTITY,
        [Meter_Number] nvarchar(max) NOT NULL,
        [ReaderID] nvarchar(max) NOT NULL,
        [Previous_Reading] float NULL,
        [Current_Reading] float NOT NULL,
        [Usage] float NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Meters] PRIMARY KEY ([ReadingID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Payments] (
        [PaymentID] int NOT NULL IDENTITY,
        [BillID] nvarchar(max) NOT NULL,
        [CashierID] nvarchar(max) NOT NULL,
        [Amount_Paid] float NOT NULL,
        [Change] float NOT NULL,
        [PenaltyIncluded] nvarchar(max) NULL,
        [AdvanceIncluded] nvarchar(max) NULL,
        [PaymentDate] date NOT NULL,
        [PaymentMethod] nvarchar(max) NOT NULL,
        [Remarks] nvarchar(max) NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Penalties] (
        [PenaltiesID] int NOT NULL IDENTITY,
        [ConsumerID] nvarchar(max) NOT NULL,
        [PenaltyAmount] float NOT NULL,
        [DateImplemented] datetime2 NOT NULL,
        CONSTRAINT [PK_Penalties] PRIMARY KEY ([PenaltiesID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Settings] (
        [SettingID] int NOT NULL IDENTITY,
        [SystemName] nvarchar(max) NULL,
        [PenaltyAmount] float NOT NULL,
        [AmountPerCubic] float NOT NULL,
        CONSTRAINT [PK_Settings] PRIMARY KEY ([SettingID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE TABLE [Users] (
        [UserID] int NOT NULL IDENTITY,
        [Firstname] nvarchar(max) NOT NULL,
        [Middlename] nvarchar(max) NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Date_Created] datetime2 NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Token] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Advances_AdvanceID] ON [Advances] ([AdvanceID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_UserID] ON [Users] ([UserID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114093914_FirstMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250114093914_FirstMigration', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    DROP TABLE [Penalties];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Settings]') AND [c].[name] = N'PenaltyAmount');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Settings] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Settings] ALTER COLUMN [PenaltyAmount] float NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Settings]') AND [c].[name] = N'AmountPerCubic');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Settings] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Settings] ALTER COLUMN [AmountPerCubic] float NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Payments]') AND [c].[name] = N'PenaltyIncluded');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Payments] DROP CONSTRAINT [' + @var2 + '];');
    EXEC(N'UPDATE [Payments] SET [PenaltyIncluded] = 0.0E0 WHERE [PenaltyIncluded] IS NULL');
    ALTER TABLE [Payments] ALTER COLUMN [PenaltyIncluded] float NOT NULL;
    ALTER TABLE [Payments] ADD DEFAULT 0.0E0 FOR [PenaltyIncluded];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    ALTER TABLE [Meters] ADD [Reading_Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Advances]') AND [c].[name] = N'DateUsed');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Advances] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Advances] ALTER COLUMN [DateUsed] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Settings_SettingID] ON [Settings] ([SettingID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250117133258_Fresh_Migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250117133258_Fresh_Migration', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250128061245_UpdatedMigration'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Meters]') AND [c].[name] = N'Previous_Reading');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Meters] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Meters] ALTER COLUMN [Previous_Reading] float NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250128061245_UpdatedMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250128061245_UpdatedMigration', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250202235237_MonthOfAdded'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Meters]') AND [c].[name] = N'Previous_Reading');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Meters] DROP CONSTRAINT [' + @var5 + '];');
    EXEC(N'UPDATE [Meters] SET [Previous_Reading] = 0.0E0 WHERE [Previous_Reading] IS NULL');
    ALTER TABLE [Meters] ALTER COLUMN [Previous_Reading] float NOT NULL;
    ALTER TABLE [Meters] ADD DEFAULT 0.0E0 FOR [Previous_Reading];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250202235237_MonthOfAdded'
)
BEGIN
    ALTER TABLE [Meters] ADD [MonthOf] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250202235237_MonthOfAdded'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250202235237_MonthOfAdded', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250205122215_feb02'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bills]') AND [c].[name] = N'DueDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Bills] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Bills] ALTER COLUMN [DueDate] date NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250205122215_feb02'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250205122215_feb02', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250216065134_monthOfNull'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Meters]') AND [c].[name] = N'MonthOf');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Meters] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Meters] ALTER COLUMN [MonthOf] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250216065134_monthOfNull'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250216065134_monthOfNull', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250216152822_convertTostring'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Payments]') AND [c].[name] = N'BillID');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Payments] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Payments] ALTER COLUMN [BillID] int NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250216152822_convertTostring'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bills]') AND [c].[name] = N'ReadingID');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Bills] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Bills] ALTER COLUMN [ReadingID] int NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250216152822_convertTostring'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250216152822_convertTostring', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217073526_addReferenceNumberOnReading'
)
BEGIN
    ALTER TABLE [Meters] ADD [ReferenceNumber] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217073526_addReferenceNumberOnReading'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250217073526_addReferenceNumberOnReading', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217081148_changeReferenceNUmber'
)
BEGIN
    ALTER TABLE [Bills] ADD [ReferenceNumber] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217081148_changeReferenceNUmber'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250217081148_changeReferenceNUmber', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217082424_removeReferenceFromReading'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Meters]') AND [c].[name] = N'ReferenceNumber');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Meters] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Meters] DROP COLUMN [ReferenceNumber];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250217082424_removeReferenceFromReading'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250217082424_removeReferenceFromReading', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322135343_add_Payment_Status'
)
BEGIN
    ALTER TABLE [Payments] ADD [PaymentStatus] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322135343_add_Payment_Status'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250322135343_add_Payment_Status', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322141648_drop_StatusPayment'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Payments]') AND [c].[name] = N'PaymentStatus');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Payments] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Payments] DROP COLUMN [PaymentStatus];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322141648_drop_StatusPayment'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250322141648_drop_StatusPayment', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322142430_settings_add_Qr_And_Name'
)
BEGIN
    ALTER TABLE [Settings] ADD [GcashQr] varbinary(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322142430_settings_add_Qr_And_Name'
)
BEGIN
    ALTER TABLE [Settings] ADD [Gcash_Name] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322142430_settings_add_Qr_And_Name'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250322142430_settings_add_Qr_And_Name', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250323065642_paymentReference_Number'
)
BEGIN
    ALTER TABLE [Payments] ADD [PaymentReferenceNumber] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250323065642_paymentReference_Number'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250323065642_paymentReference_Number', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250327115337_userAppliedAdded'
)
BEGIN
    ALTER TABLE [Users] ADD [Applied] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250327115337_userAppliedAdded'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250327115337_userAppliedAdded', N'9.0.0');
END;

COMMIT;
GO

