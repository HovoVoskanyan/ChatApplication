CREATE TABLE [dbo].[Messages] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Text]       NVARCHAR (MAX) NOT NULL,
    [UserIdFrom] INT            NOT NULL,
    [UserIdTo]   INT            NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_users] FOREIGN KEY ([UserIdFrom]) REFERENCES [dbo].[users] ([id]),
    CONSTRAINT [FK_Messages_users1] FOREIGN KEY ([UserIdTo]) REFERENCES [dbo].[users] ([id])
);

