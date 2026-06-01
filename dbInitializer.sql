DROP TABLE [dbo].[LocalizationRecords];

CREATE TABLE [dbo].[LocalizationRecords] ( -- Ha veletlen nem lenne tabla
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Key] [nvarchar](max) NOT NULL,
    [Text] [nvarchar](max) NOT NULL,
    [LocalizationCulture] [nvarchar](max) NOT NULL,
    [ResourceKey] [nvarchar](max) NOT NULL,
    [UpdatedTimestamp] [datetime2](7) NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_LocalizationRecords] PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [LocalizationRecords] ([Key], [Text], [LocalizationCulture], [ResourceKey], [UpdatedTimestamp])
VALUES 
('Greeting', 'Szia!', 'hu', 'Home', GETDATE()),
('Greeting', 'Hello in English!', 'en', 'Home', GETDATE());