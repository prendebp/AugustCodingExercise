CREATE TABLE [dbo].[DeductionType] (
    [DeductionTypeCode] NVARCHAR (10)   NOT NULL,
    [TypeDescription]   NCHAR (250)     NULL,
    [AppliesToEmp]      BIT             NULL,
    [AppliesToDep]      BIT             NULL,
    [AppliesToSpo]      BIT             NULL,
    [EmpAmt]            NUMERIC (18, 2) NULL,
    [DepAmt]            NUMERIC (18, 2) NULL,
    [SpoAmt]            NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_DeductionType] PRIMARY KEY CLUSTERED ([DeductionTypeCode] ASC)
);

