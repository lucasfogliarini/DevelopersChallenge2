CREATE TABLE [dbo].[BankTransaction] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [TransType]     INT          NOT NULL,
    [TransAmount]   DECIMAL (18, 4) NOT NULL,
    [DatePosted]    DATETIME     NOT NULL,
    [Memo]          VARCHAR(50)   NOT NULL,
    [BankAccountId] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankTransaction_BankAccount] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[BankAccount] ([Id])
);

