CREATE TABLE [dbo].[FlowDeviceLog] (
    [createDateTime] DATETIME         DEFAULT (getdate()) NOT NULL,
    [setid]          UNIQUEIDENTIFIER NOT NULL,
    [uid]            UNIQUEIDENTIFIER NOT NULL,
    [Flow]           DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [FlowTotal]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InputTemp]      DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [OutputTemp]     DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [Rang]           DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [HeatLoadRate]   DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [HeatLoad]       DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MaxInputTemp]   DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MinInputTemp]   DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MaxOutputTemp]  DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [MinOutputTemp]  DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [InErrorType]    INT              DEFAULT ((0)) NULL,
    [OutErrorType]   INT              DEFAULT ((0)) NULL,
    [ConnectionFlag] BIT              DEFAULT ((0)) NULL,
    [LFlow]          DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [FlowPercent]    DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([createDateTime] ASC, [setid] ASC, [uid] ASC)
);

