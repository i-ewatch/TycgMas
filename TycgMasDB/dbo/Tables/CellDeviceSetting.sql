CREATE TABLE [dbo].[CellDeviceSetting] (
    [uid]      UNIQUEIDENTIFIER NOT NULL,
    [Cellid]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Cell_Type] INT DEFAULT (0) NOT NULL,
    [CellName] NVARCHAR (50)    DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC, [Cellid] ASC)
);

