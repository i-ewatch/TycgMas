CREATE TABLE [dbo].[SetDeviceSetting] (
    [uid]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TowerName] NVARCHAR (50)    DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC)
);

