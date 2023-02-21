CREATE TABLE [dbo].[FlowDeviceSetting] (
    [uid]      UNIQUEIDENTIFIER NOT NULL,
    [Flowid]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FlowName] NVARCHAR (50)    DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC)
);

