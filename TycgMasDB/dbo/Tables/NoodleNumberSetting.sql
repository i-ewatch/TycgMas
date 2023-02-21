CREATE TABLE [dbo].[NoodleNumberSetting] (
    [NoodleNumber] INT           NOT NULL,
    [NoodleName]   NVARCHAR (50) DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([NoodleNumber] ASC)
);

