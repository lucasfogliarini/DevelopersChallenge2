CREATE TABLE [dbo].[BankAccount] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [BankID]      NCHAR (10) NOT NULL,
    [AccountID]   NCHAR (10) NOT NULL,
    [AccountType] NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

