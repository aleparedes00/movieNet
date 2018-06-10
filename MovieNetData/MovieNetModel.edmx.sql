
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/10/2018 16:42:09
-- Generated from EDMX file: C:\Users\student\Desktop\MovieNet\MovieNetData\MovieNetModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MovieNetDataDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_FilmComment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FilmSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilmSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FilmSet'
CREATE TABLE [dbo].[FilmSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Genres] nvarchar(max)  NOT NULL,
    [Synopsis] nvarchar(max)  NULL,
    [Score] float  NULL,
    [Year] smallint  NULL,
    [Director] nvarchar(max)  NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Score] float  NULL,
    [Text] nvarchar(max)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [ModificationDate] datetime  NOT NULL,
    [User_Id] int  NOT NULL,
    [Film_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'FilmSet'
ALTER TABLE [dbo].[FilmSet]
ADD CONSTRAINT [PK_FilmSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[CommentSet]
    ([User_Id]);
GO

-- Creating foreign key on [Film_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_FilmComment]
    FOREIGN KEY ([Film_Id])
    REFERENCES [dbo].[FilmSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmComment'
CREATE INDEX [IX_FK_FilmComment]
ON [dbo].[CommentSet]
    ([Film_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------