CREATE TABLE [dbo].[SensorSideSetting] (
    [SideIndex] INT           DEFAULT ((0)) NOT NULL,
    [SideName]  NVARCHAR (50) DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([SideIndex] ASC)
);

