CREATE TABLE [dbo].[SensorDeviceLog] (
    [createDateTime]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [setid]            UNIQUEIDENTIFIER NOT NULL,
    [uid]              UNIQUEIDENTIFIER NOT NULL,
    [DeviceType]       INT              DEFAULT ((0)) NULL,
    [Temp]             DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [Humidity]         DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [WetBulbTemp]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [DewPointTemp]     DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [AbsoluteHumidity] DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [Enthalpy]         DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MaxTemp]          DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MinTemp]          DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [In_Out_Flag]      BIT              DEFAULT ((0)) NULL,
    [ErrorType]        INT              DEFAULT ((0)) NULL,
    [Noodle_Number]    INT              DEFAULT ((0)) NULL,
    [ConnectionFlag]   BIT              DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([createDateTime] ASC, [setid] ASC, [uid] ASC)
);

