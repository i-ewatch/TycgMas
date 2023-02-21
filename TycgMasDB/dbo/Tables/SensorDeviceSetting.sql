CREATE TABLE [dbo].[SensorDeviceSetting] (
    [uid]           UNIQUEIDENTIFIER NOT NULL,
    [Cellid]        UNIQUEIDENTIFIER NOT NULL,
    [Sensorid]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SensorName]    NVARCHAR (50)    DEFAULT ('') NULL,
    [Noodle_Number] INT              DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC, [Cellid] ASC, [Sensorid] ASC)
);

