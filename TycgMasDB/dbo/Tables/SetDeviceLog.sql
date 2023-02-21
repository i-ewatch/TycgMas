CREATE TABLE [dbo].[SetDeviceLog] (
    [createDateTime]      DATETIME         CONSTRAINT [DF_SetDeviceLog_createDateTime] DEFAULT (getdate()) NOT NULL,
    [uid]                 UNIQUEIDENTIFIER NOT NULL,
    [InputTemp]           DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_InputTemp] DEFAULT ((0)) NULL,
    [OutputTemp]          DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_OutputTemp] DEFAULT ((0)) NULL,
    [InWetBulbTemp]       DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_InWetBulbTemp] DEFAULT ((0)) NULL,
    [OutWetBulbTemp]      DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_OutWetBulbTemp] DEFAULT ((0)) NULL,
    [InDewPointTemp]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutDewPointTemp]     DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InRelativeHumidity]  DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutRelativeHumidity] DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InAbsoluteHumidity]  DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_InAbsoluteHumidity] DEFAULT ((0)) NULL,
    [OutAbsoluteHumidity] DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_OutAbsoluteHumidity] DEFAULT ((0)) NULL,
    [InEnthalpy]          DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_InEnthalpy] DEFAULT ((0)) NULL,
    [OutEnthalpy]         DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_OutEnthalpy] DEFAULT ((0)) NULL,
    [HeatLoadRate]        DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_HeatLoadRate] DEFAULT ((0)) NULL,
    [ElectricLoadRate]    DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_ElectricLoadRate] DEFAULT ((0)) NULL,
    [Appr]                DECIMAL (18, 2)  CONSTRAINT [DF_SetDeviceLog_Appr] DEFAULT ((0)) NULL,
    [RangeTemp]           DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [RangeEnthalpy]       DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    CONSTRAINT [PK_SetDeviceLog] PRIMARY KEY CLUSTERED ([createDateTime] ASC, [uid] ASC)
);

