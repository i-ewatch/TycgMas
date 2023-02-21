CREATE TABLE [dbo].[ElectricDeviceSetting] (
    [uid]          UNIQUEIDENTIFIER NOT NULL,
    [Cellid]       UNIQUEIDENTIFIER NOT NULL,
    [Electricid]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Fq_Type]      BIT DEFAULT(0) NOT NULL,
    [Electric_Type]  INT DEFAULT(0) NOT NULL,
    [ElectricName] NVARCHAR (50)    DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC, [Cellid] ASC, [Electricid] ASC)
);

