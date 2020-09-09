IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Departments] (
    [DepartmentId] int NOT NULL IDENTITY(1,1),
    [DepartmentName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
);

GO

CREATE TABLE [Employees] (
    [EmployeeId] int NOT NULL IDENTITY(1,1),
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max)NOT NULL,
    [Email] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [HireDate] datetime2 NOT NULL,
    [Salary] decimal(18,2) NOT NULL,
    [DepartmentId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId]),
    CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId])
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200906051648_First', N'3.1.7');

GO

