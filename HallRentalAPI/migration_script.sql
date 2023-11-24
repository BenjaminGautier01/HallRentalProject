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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE TABLE [Customers] (
        [CustomerID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE TABLE [Halls] (
        [HallID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [Capacity] int NOT NULL,
        [Amenities] nvarchar(max) NULL,
        CONSTRAINT [PK_Halls] PRIMARY KEY ([HallID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE TABLE [Bookings] (
        [BookingID] int NOT NULL IDENTITY,
        [CustomerID] int NOT NULL,
        [HallID] int NOT NULL,
        [BookingDate] datetime2 NOT NULL,
        [RentalDate] datetime2 NOT NULL,
        [Duration] int NOT NULL,
        [TotalCost] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Bookings] PRIMARY KEY ([BookingID]),
        CONSTRAINT [FK_Bookings_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Bookings_Halls_HallID] FOREIGN KEY ([HallID]) REFERENCES [Halls] ([HallID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE TABLE [Payments] (
        [PaymentID] int NOT NULL IDENTITY,
        [BookingID] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [PaymentDate] datetime2 NOT NULL,
        [PaymentMethod] nvarchar(max) NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentID]),
        CONSTRAINT [FK_Payments_Bookings_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [Bookings] ([BookingID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE INDEX [IX_Bookings_CustomerID] ON [Bookings] ([CustomerID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE INDEX [IX_Bookings_HallID] ON [Bookings] ([HallID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Payments_BookingID] ON [Payments] ([BookingID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231119093920_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231119093920_InitialCreate', N'6.0.23');
END;
GO

COMMIT;
GO

