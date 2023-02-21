CREATE TABLE [dbo].[CellDeviceLog] (
    [createDateTime]      DATETIME         DEFAULT (getdate()) NOT NULL,
    [setid]               UNIQUEIDENTIFIER NOT NULL,
    [uid]                 UNIQUEIDENTIFIER NOT NULL,
    [InputTemp]           DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutputTemp]          DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InWetBulbTemp]       DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutWetBulbTemp]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InDewPointTemp]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutDewPointTemp]     DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InAbsoluteHumidity]  DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutAbsoluteHumidity] DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InEnthalpy]          DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutEnthalpy]         DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [ActionFlag]          BIT              DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([createDateTime] ASC, [setid] ASC, [uid] ASC)
);

