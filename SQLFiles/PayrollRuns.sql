CREATE TABLE [dbo].[PayrollRuns] (
    [PayrollId]     BIGINT          NOT NULL,
    [RunId]         BIGINT          NOT NULL,
    [WeekEnding]    DATETIME        NOT NULL,
    [PersonId]      BIGINT          NOT NULL,
    [Status]        NVARCHAR (10)   NOT NULL,
    [CheckDate]     DATETIME        NULL,
    [GrossPay]      NUMERIC (18, 2) NULL,
    [Deduction]     NUMERIC (18, 2) NULL,
    [DeductionType] NVARCHAR (10)   NULL,
    [Discount]      NUMERIC (18, 2) NULL,
    [DiscountType]  NVARCHAR (10)   NULL,
    [NetPay]        NUMERIC (18, 2) NULL,
    [Year]          INT             NULL,
    CONSTRAINT [PK_PayrollRuns] PRIMARY KEY CLUSTERED ([PayrollId] ASC),
    CONSTRAINT [FK_PayrollRuns_DeductionType] FOREIGN KEY ([DeductionType]) REFERENCES [dbo].[DeductionType] ([DeductionTypeCode]) ON UPDATE CASCADE,
    CONSTRAINT [FK_PayrollRuns_DiscountType] FOREIGN KEY ([DiscountType]) REFERENCES [dbo].[DiscountType] ([DiscountTypeCode]) ON UPDATE CASCADE,
    CONSTRAINT [FK_PayrollRuns_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId]) ON UPDATE CASCADE
);




GO
CREATE TRIGGER [dbo].[TR_PayrollRuns_AUDIT]
ON dbo.PayrollRuns
FOR UPDATE
AS
           DECLARE @bit            INT,
                   @field          INT,
                   @maxfield       INT,
                   @char           INT,
                   @fieldname      VARCHAR(128),
                   @TableName      VARCHAR(128),
                   @PKCols         VARCHAR(1000),
                   @sql            VARCHAR(2000),
                   @UpdateDate     VARCHAR(21),
                   @UserName       VARCHAR(128),
                   @Type           CHAR(1),
                   @PKSelect       VARCHAR(1000)


           --You will need to change @TableName to match the table to be audited.
           -- Here we made GUESTS for your example.
           SELECT @TableName = 'PAYROLLRUNS'

           SELECT @UserName = SYSTEM_USER,
                  @UpdateDate = CONVERT(NVARCHAR(30), GETDATE(), 126)

           -- Action
           IF EXISTS (
                  SELECT *
                  FROM   INSERTED
              )
               IF EXISTS (
                      SELECT *
                      FROM   DELETED
                  )
                   SELECT @Type = 'U'
               ELSE
                   SELECT @Type = 'I'
           ELSE
               SELECT @Type = 'D'

           -- get list of columns
           SELECT * INTO #ins
           FROM   INSERTED

           SELECT * INTO #del
           FROM   DELETED

           -- Get primary key columns for full outer join
           SELECT @PKCols = COALESCE(@PKCols + ' and', ' on') 
                  + ' i.[' + c.COLUMN_NAME + '] = d.[' + c.COLUMN_NAME + ']'
           FROM   INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk,
                  INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
           WHERE  pk.TABLE_NAME = @TableName
                  AND CONSTRAINT_TYPE = 'PRIMARY KEY'
                  AND c.TABLE_NAME = pk.TABLE_NAME
                  AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME

           -- Get primary key select for insert
           SELECT @PKSelect = COALESCE(@PKSelect + '+', '') 
                  + '''<[' + COLUMN_NAME 
                  + ']=''+convert(varchar(100),
           coalesce(i.[' + COLUMN_NAME + '],d.[' + COLUMN_NAME + ']))+''>'''
           FROM   INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk,
                  INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
           WHERE  pk.TABLE_NAME = @TableName
                  AND CONSTRAINT_TYPE = 'PRIMARY KEY'
                  AND c.TABLE_NAME = pk.TABLE_NAME
                  AND c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME

           IF @PKCols IS NULL
           BEGIN
               RAISERROR('no PK on table %s', 16, -1, @TableName)

               RETURN
           END

           SELECT @field = 0,
                  -- @maxfield = MAX(COLUMN_NAME) 
                  @maxfield = -- FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName


                  MAX(
                      COLUMNPROPERTY(
                          OBJECT_ID(TABLE_SCHEMA + '.' + @TableName),
                          COLUMN_NAME,
                          'ColumnID'
                      )
                  )
           FROM   INFORMATION_SCHEMA.COLUMNS
           WHERE  TABLE_NAME = @TableName






           WHILE @field < @maxfield
           BEGIN
               SELECT @field = MIN(
                          COLUMNPROPERTY(
                              OBJECT_ID(TABLE_SCHEMA + '.' + @TableName),
                              COLUMN_NAME,
                              'ColumnID'
                          )
                      )
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = @TableName
                      AND COLUMNPROPERTY(
                              OBJECT_ID(TABLE_SCHEMA + '.' + @TableName),
                              COLUMN_NAME,
                              'ColumnID'
                          ) > @field

               SELECT @bit = (@field - 1)% 8 + 1

               SELECT @bit = POWER(2, @bit - 1)

               SELECT @char = ((@field - 1) / 8) + 1





               IF SUBSTRING(COLUMNS_UPDATED(), @char, 1) & @bit > 0
                  OR @Type IN ('I', 'D')
               BEGIN
                   SELECT @fieldname = COLUMN_NAME
                   FROM   INFORMATION_SCHEMA.COLUMNS
                   WHERE  TABLE_NAME = @TableName
                          AND COLUMNPROPERTY(
                                  OBJECT_ID(TABLE_SCHEMA + '.' + @TableName),
                                  COLUMN_NAME,
                                  'ColumnID'
                              ) = @field



                   SELECT @sql = 
                          '
           insert into Audit (    Type, 
           TableName, 
           PK, 
           FieldName, 
           OldValue, 
           NewValue, 
           UpdateDate, 
           UserName)
           select ''' + @Type + ''',''' 
                          + @TableName + ''',' + @PKSelect
                          + ',''' + @fieldname + ''''
                          + ',convert(varchar(1000),d.' + @fieldname + ')'
                          + ',convert(varchar(1000),i.' + @fieldname + ')'
                          + ',''' + @UpdateDate + ''''
                          + ',''' + @UserName + ''''
                          + ' from #ins i full outer join #del d'
                          + @PKCols
                          + ' where i.' + @fieldname + ' <> d.' + @fieldname 
                          + ' or (i.' + @fieldname + ' is null and  d.'
                          + @fieldname
                          + ' is not null)' 
                          + ' or (i.' + @fieldname + ' is not null and  d.' 
                          + @fieldname
                          + ' is null)' 



                   EXEC (@sql)
               END
           END