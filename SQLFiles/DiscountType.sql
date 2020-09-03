CREATE TABLE [dbo].[DiscountType] (
    [DiscountTypeCode] NVARCHAR (10)  NOT NULL,
    [TypeDescription]  NCHAR (250)    NULL,
    [AppliesToEmp]     BIT            NULL,
    [AppliesToDep]     BIT            NULL,
    [AppliesToSpo]     BIT            NULL,
    [EmpPercent]       NUMERIC (9, 5) NULL,
    [DepPercent]       NUMERIC (9, 5) NULL,
    [SpoPercent]       NUMERIC (9, 5) NULL,
    CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED ([DiscountTypeCode] ASC)
);

