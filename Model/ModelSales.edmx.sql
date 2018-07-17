
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/15/2018 11:47:18
-- Generated from EDMX file: D:\Sales\Model\ModelSales.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBSales];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SaleInfoSet'
CREATE TABLE [dbo].[SaleInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [ProductId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [FileId] int  NOT NULL,
    [Client_Id] int  NOT NULL,
    [Product_Id] int  NOT NULL,
    [FileInformation_Id] int  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FileInformationSet'
CREATE TABLE [dbo].[FileInformationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [ManagerId] int  NOT NULL,
    [Manager_Id] int  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [PK_SaleInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileInformationSet'
ALTER TABLE [dbo].[FileInformationSet]
ADD CONSTRAINT [PK_FileInformationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Client_Id] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ClientSaleInfo]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSaleInfo'
CREATE INDEX [IX_FK_ClientSaleInfo]
ON [dbo].[SaleInfoSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ProductSaleInfo]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSaleInfo'
CREATE INDEX [IX_FK_ProductSaleInfo]
ON [dbo].[SaleInfoSet]
    ([Product_Id]);
GO

-- Creating foreign key on [FileInformation_Id] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_FileInformationSaleInfo]
    FOREIGN KEY ([FileInformation_Id])
    REFERENCES [dbo].[FileInformationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileInformationSaleInfo'
CREATE INDEX [IX_FK_FileInformationSaleInfo]
ON [dbo].[SaleInfoSet]
    ([FileInformation_Id]);
GO

-- Creating foreign key on [Manager_Id] in table 'FileInformationSet'
ALTER TABLE [dbo].[FileInformationSet]
ADD CONSTRAINT [FK_ManagerFileInformation]
    FOREIGN KEY ([Manager_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerFileInformation'
CREATE INDEX [IX_FK_ManagerFileInformation]
ON [dbo].[FileInformationSet]
    ([Manager_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------