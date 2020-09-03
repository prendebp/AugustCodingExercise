CREATE TABLE [dbo].[PersonType] (
    [PersonCode]      NVARCHAR (10) NOT NULL,
    [TypeDescription] NCHAR (250)   NULL,
    CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED ([PersonCode] ASC)
);

