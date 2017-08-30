
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/30/2017 09:55:33
-- Generated from EDMX file: C:\LabWeb\Reserva\Reserva\Reserva\Models\ReservaBD.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ReservaBD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AlmoxarifadoOperador]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Militars_Operador] DROP CONSTRAINT [FK_AlmoxarifadoOperador];
GO
IF OBJECT_ID(N'[dbo].[FK_MilitarPatente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Militars] DROP CONSTRAINT [FK_MilitarPatente];
GO
IF OBJECT_ID(N'[dbo].[FK_AlmoxarifadoMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais] DROP CONSTRAINT [FK_AlmoxarifadoMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_CalibreMunicao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais_Municao] DROP CONSTRAINT [FK_CalibreMunicao];
GO
IF OBJECT_ID(N'[dbo].[FK_MunicaoArmamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais_Armamento] DROP CONSTRAINT [FK_MunicaoArmamento];
GO
IF OBJECT_ID(N'[dbo].[FK_AlmoxarifadoCautela]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cautelas] DROP CONSTRAINT [FK_AlmoxarifadoCautela];
GO
IF OBJECT_ID(N'[dbo].[FK_FabricanteMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais] DROP CONSTRAINT [FK_FabricanteMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_OperadorCautela]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cautelas] DROP CONSTRAINT [FK_OperadorCautela];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioCautela]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cautelas] DROP CONSTRAINT [FK_UsuarioCautela];
GO
IF OBJECT_ID(N'[dbo].[FK_CautelaOperacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operacaos] DROP CONSTRAINT [FK_CautelaOperacao];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialOperacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operacaos] DROP CONSTRAINT [FK_MaterialOperacao];
GO
IF OBJECT_ID(N'[dbo].[FK_Operador_inherits_Militar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Militars_Operador] DROP CONSTRAINT [FK_Operador_inherits_Militar];
GO
IF OBJECT_ID(N'[dbo].[FK_Municao_inherits_Material]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais_Municao] DROP CONSTRAINT [FK_Municao_inherits_Material];
GO
IF OBJECT_ID(N'[dbo].[FK_Armamento_inherits_Material]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais_Armamento] DROP CONSTRAINT [FK_Armamento_inherits_Material];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_inherits_Militar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Militars_Usuario] DROP CONSTRAINT [FK_Usuario_inherits_Militar];
GO
IF OBJECT_ID(N'[dbo].[FK_Acessorio_inherits_Material]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiais_Acessorio] DROP CONSTRAINT [FK_Acessorio_inherits_Material];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Almoxarifadoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Almoxarifadoes];
GO
IF OBJECT_ID(N'[dbo].[Militars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Militars];
GO
IF OBJECT_ID(N'[dbo].[Patentes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patentes];
GO
IF OBJECT_ID(N'[dbo].[Materiais]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materiais];
GO
IF OBJECT_ID(N'[dbo].[Fabricantes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fabricantes];
GO
IF OBJECT_ID(N'[dbo].[Calibres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calibres];
GO
IF OBJECT_ID(N'[dbo].[Cautelas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cautelas];
GO
IF OBJECT_ID(N'[dbo].[Operacaos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Operacaos];
GO
IF OBJECT_ID(N'[dbo].[Militars_Operador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Militars_Operador];
GO
IF OBJECT_ID(N'[dbo].[Materiais_Municao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materiais_Municao];
GO
IF OBJECT_ID(N'[dbo].[Materiais_Armamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materiais_Armamento];
GO
IF OBJECT_ID(N'[dbo].[Militars_Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Militars_Usuario];
GO
IF OBJECT_ID(N'[dbo].[Materiais_Acessorio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materiais_Acessorio];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Almoxarifadoes'
CREATE TABLE [dbo].[Almoxarifadoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL,
    [Sigla] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Militars'
CREATE TABLE [dbo].[Militars] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [NomeGuerra] nvarchar(max)  NOT NULL,
    [Matricula] nvarchar(max)  NOT NULL,
    [PatenteId] int  NOT NULL
);
GO

-- Creating table 'Patentes'
CREATE TABLE [dbo].[Patentes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL,
    [Sigla] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Materiais'
CREATE TABLE [dbo].[Materiais] (
    [Tombo] int IDENTITY(1,1) NOT NULL,
    [AlmoxarifadoId] int  NOT NULL,
    [Disponivel] bit  NOT NULL,
    [FabricanteId] int  NOT NULL,
    [Lote] int  NOT NULL
);
GO

-- Creating table 'Fabricantes'
CREATE TABLE [dbo].[Fabricantes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Calibres'
CREATE TABLE [dbo].[Calibres] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cautelas'
CREATE TABLE [dbo].[Cautelas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Data] datetime  NOT NULL,
    [NRegistro] nvarchar(max)  NOT NULL,
    [Almoxarifado_Id] int  NOT NULL,
    [Operador_Id] int  NOT NULL,
    [Operador_Matricula] nvarchar(max)  NOT NULL,
    [Usuario_Id] int  NOT NULL,
    [Usuario_Matricula] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Operacaos'
CREATE TABLE [dbo].[Operacaos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cautelado] bit  NOT NULL,
    [CautelaId] int  NOT NULL,
    [MaterialTombo] int  NOT NULL
);
GO

-- Creating table 'Militars_Operador'
CREATE TABLE [dbo].[Militars_Operador] (
    [Email] nvarchar(max)  NOT NULL,
    [AutenticacaoID] nvarchar(max)  NOT NULL,
    [ADM] bit  NOT NULL,
    [AlmoxarifadoId] int  NOT NULL,
    [CautelaId] int  NOT NULL,
    [Id] int  NOT NULL,
    [Matricula] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Materiais_Municao'
CREATE TABLE [dbo].[Materiais_Municao] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL,
    [CalibreId] int  NOT NULL,
    [QuantidadeBala] int  NOT NULL,
    [Tombo] int  NOT NULL
);
GO

-- Creating table 'Materiais_Armamento'
CREATE TABLE [dbo].[Materiais_Armamento] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NSerie] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL,
    [MunicaoId] int  NOT NULL,
    [Tombo] int  NOT NULL
);
GO

-- Creating table 'Militars_Usuario'
CREATE TABLE [dbo].[Militars_Usuario] (
    [Id] int  NOT NULL,
    [Matricula] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Materiais_Acessorio'
CREATE TABLE [dbo].[Materiais_Acessorio] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL,
    [Tombo] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Almoxarifadoes'
ALTER TABLE [dbo].[Almoxarifadoes]
ADD CONSTRAINT [PK_Almoxarifadoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Matricula] in table 'Militars'
ALTER TABLE [dbo].[Militars]
ADD CONSTRAINT [PK_Militars]
    PRIMARY KEY CLUSTERED ([Id], [Matricula] ASC);
GO

-- Creating primary key on [Id] in table 'Patentes'
ALTER TABLE [dbo].[Patentes]
ADD CONSTRAINT [PK_Patentes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Tombo] in table 'Materiais'
ALTER TABLE [dbo].[Materiais]
ADD CONSTRAINT [PK_Materiais]
    PRIMARY KEY CLUSTERED ([Tombo] ASC);
GO

-- Creating primary key on [Id] in table 'Fabricantes'
ALTER TABLE [dbo].[Fabricantes]
ADD CONSTRAINT [PK_Fabricantes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Calibres'
ALTER TABLE [dbo].[Calibres]
ADD CONSTRAINT [PK_Calibres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cautelas'
ALTER TABLE [dbo].[Cautelas]
ADD CONSTRAINT [PK_Cautelas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Operacaos'
ALTER TABLE [dbo].[Operacaos]
ADD CONSTRAINT [PK_Operacaos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Matricula] in table 'Militars_Operador'
ALTER TABLE [dbo].[Militars_Operador]
ADD CONSTRAINT [PK_Militars_Operador]
    PRIMARY KEY CLUSTERED ([Id], [Matricula] ASC);
GO

-- Creating primary key on [Tombo] in table 'Materiais_Municao'
ALTER TABLE [dbo].[Materiais_Municao]
ADD CONSTRAINT [PK_Materiais_Municao]
    PRIMARY KEY CLUSTERED ([Tombo] ASC);
GO

-- Creating primary key on [Tombo] in table 'Materiais_Armamento'
ALTER TABLE [dbo].[Materiais_Armamento]
ADD CONSTRAINT [PK_Materiais_Armamento]
    PRIMARY KEY CLUSTERED ([Tombo] ASC);
GO

-- Creating primary key on [Id], [Matricula] in table 'Militars_Usuario'
ALTER TABLE [dbo].[Militars_Usuario]
ADD CONSTRAINT [PK_Militars_Usuario]
    PRIMARY KEY CLUSTERED ([Id], [Matricula] ASC);
GO

-- Creating primary key on [Tombo] in table 'Materiais_Acessorio'
ALTER TABLE [dbo].[Materiais_Acessorio]
ADD CONSTRAINT [PK_Materiais_Acessorio]
    PRIMARY KEY CLUSTERED ([Tombo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AlmoxarifadoId] in table 'Militars_Operador'
ALTER TABLE [dbo].[Militars_Operador]
ADD CONSTRAINT [FK_AlmoxarifadoOperador]
    FOREIGN KEY ([AlmoxarifadoId])
    REFERENCES [dbo].[Almoxarifadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlmoxarifadoOperador'
CREATE INDEX [IX_FK_AlmoxarifadoOperador]
ON [dbo].[Militars_Operador]
    ([AlmoxarifadoId]);
GO

-- Creating foreign key on [PatenteId] in table 'Militars'
ALTER TABLE [dbo].[Militars]
ADD CONSTRAINT [FK_MilitarPatente]
    FOREIGN KEY ([PatenteId])
    REFERENCES [dbo].[Patentes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MilitarPatente'
CREATE INDEX [IX_FK_MilitarPatente]
ON [dbo].[Militars]
    ([PatenteId]);
GO

-- Creating foreign key on [AlmoxarifadoId] in table 'Materiais'
ALTER TABLE [dbo].[Materiais]
ADD CONSTRAINT [FK_AlmoxarifadoMaterial]
    FOREIGN KEY ([AlmoxarifadoId])
    REFERENCES [dbo].[Almoxarifadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlmoxarifadoMaterial'
CREATE INDEX [IX_FK_AlmoxarifadoMaterial]
ON [dbo].[Materiais]
    ([AlmoxarifadoId]);
GO

-- Creating foreign key on [CalibreId] in table 'Materiais_Municao'
ALTER TABLE [dbo].[Materiais_Municao]
ADD CONSTRAINT [FK_CalibreMunicao]
    FOREIGN KEY ([CalibreId])
    REFERENCES [dbo].[Calibres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CalibreMunicao'
CREATE INDEX [IX_FK_CalibreMunicao]
ON [dbo].[Materiais_Municao]
    ([CalibreId]);
GO

-- Creating foreign key on [MunicaoId] in table 'Materiais_Armamento'
ALTER TABLE [dbo].[Materiais_Armamento]
ADD CONSTRAINT [FK_MunicaoArmamento]
    FOREIGN KEY ([MunicaoId])
    REFERENCES [dbo].[Materiais_Municao]
        ([Tombo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MunicaoArmamento'
CREATE INDEX [IX_FK_MunicaoArmamento]
ON [dbo].[Materiais_Armamento]
    ([MunicaoId]);
GO

-- Creating foreign key on [Almoxarifado_Id] in table 'Cautelas'
ALTER TABLE [dbo].[Cautelas]
ADD CONSTRAINT [FK_AlmoxarifadoCautela]
    FOREIGN KEY ([Almoxarifado_Id])
    REFERENCES [dbo].[Almoxarifadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlmoxarifadoCautela'
CREATE INDEX [IX_FK_AlmoxarifadoCautela]
ON [dbo].[Cautelas]
    ([Almoxarifado_Id]);
GO

-- Creating foreign key on [FabricanteId] in table 'Materiais'
ALTER TABLE [dbo].[Materiais]
ADD CONSTRAINT [FK_FabricanteMaterial]
    FOREIGN KEY ([FabricanteId])
    REFERENCES [dbo].[Fabricantes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FabricanteMaterial'
CREATE INDEX [IX_FK_FabricanteMaterial]
ON [dbo].[Materiais]
    ([FabricanteId]);
GO

-- Creating foreign key on [Operador_Id], [Operador_Matricula] in table 'Cautelas'
ALTER TABLE [dbo].[Cautelas]
ADD CONSTRAINT [FK_OperadorCautela]
    FOREIGN KEY ([Operador_Id], [Operador_Matricula])
    REFERENCES [dbo].[Militars_Operador]
        ([Id], [Matricula])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OperadorCautela'
CREATE INDEX [IX_FK_OperadorCautela]
ON [dbo].[Cautelas]
    ([Operador_Id], [Operador_Matricula]);
GO

-- Creating foreign key on [Usuario_Id], [Usuario_Matricula] in table 'Cautelas'
ALTER TABLE [dbo].[Cautelas]
ADD CONSTRAINT [FK_UsuarioCautela]
    FOREIGN KEY ([Usuario_Id], [Usuario_Matricula])
    REFERENCES [dbo].[Militars_Usuario]
        ([Id], [Matricula])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioCautela'
CREATE INDEX [IX_FK_UsuarioCautela]
ON [dbo].[Cautelas]
    ([Usuario_Id], [Usuario_Matricula]);
GO

-- Creating foreign key on [CautelaId] in table 'Operacaos'
ALTER TABLE [dbo].[Operacaos]
ADD CONSTRAINT [FK_CautelaOperacao]
    FOREIGN KEY ([CautelaId])
    REFERENCES [dbo].[Cautelas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CautelaOperacao'
CREATE INDEX [IX_FK_CautelaOperacao]
ON [dbo].[Operacaos]
    ([CautelaId]);
GO

-- Creating foreign key on [MaterialTombo] in table 'Operacaos'
ALTER TABLE [dbo].[Operacaos]
ADD CONSTRAINT [FK_MaterialOperacao]
    FOREIGN KEY ([MaterialTombo])
    REFERENCES [dbo].[Materiais]
        ([Tombo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialOperacao'
CREATE INDEX [IX_FK_MaterialOperacao]
ON [dbo].[Operacaos]
    ([MaterialTombo]);
GO

-- Creating foreign key on [Id], [Matricula] in table 'Militars_Operador'
ALTER TABLE [dbo].[Militars_Operador]
ADD CONSTRAINT [FK_Operador_inherits_Militar]
    FOREIGN KEY ([Id], [Matricula])
    REFERENCES [dbo].[Militars]
        ([Id], [Matricula])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tombo] in table 'Materiais_Municao'
ALTER TABLE [dbo].[Materiais_Municao]
ADD CONSTRAINT [FK_Municao_inherits_Material]
    FOREIGN KEY ([Tombo])
    REFERENCES [dbo].[Materiais]
        ([Tombo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tombo] in table 'Materiais_Armamento'
ALTER TABLE [dbo].[Materiais_Armamento]
ADD CONSTRAINT [FK_Armamento_inherits_Material]
    FOREIGN KEY ([Tombo])
    REFERENCES [dbo].[Materiais]
        ([Tombo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id], [Matricula] in table 'Militars_Usuario'
ALTER TABLE [dbo].[Militars_Usuario]
ADD CONSTRAINT [FK_Usuario_inherits_Militar]
    FOREIGN KEY ([Id], [Matricula])
    REFERENCES [dbo].[Militars]
        ([Id], [Matricula])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tombo] in table 'Materiais_Acessorio'
ALTER TABLE [dbo].[Materiais_Acessorio]
ADD CONSTRAINT [FK_Acessorio_inherits_Material]
    FOREIGN KEY ([Tombo])
    REFERENCES [dbo].[Materiais]
        ([Tombo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------