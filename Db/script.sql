/****** Object:  Table [dbo].[AdminUser]    Script Date: 20/07/2018 14:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Photo] [varchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](200) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [varchar](200) NULL,
 CONSTRAINT [PK_AdminUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuAccess]    Script Date: 20/07/2018 14:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuAccess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NULL,
	[Menu] [varchar](100) NULL,
	[Allowed] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](200) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [varchar](200) NULL,
 CONSTRAINT [PK_MenuAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [unique_access] UNIQUE NONCLUSTERED 
(
	[ProfileId] ASC,
	[Menu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 20/07/2018 14:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Enabled] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](200) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [varchar](200) NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdminUser]  WITH CHECK ADD  CONSTRAINT [FK_AdminUser_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO
ALTER TABLE [dbo].[AdminUser] CHECK CONSTRAINT [FK_AdminUser_Profile]
GO
ALTER TABLE [dbo].[MenuAccess]  WITH CHECK ADD  CONSTRAINT [FK_MenuAccess_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO
ALTER TABLE [dbo].[MenuAccess] CHECK CONSTRAINT [FK_MenuAccess_Profile]
GO

--- Adding first profile and user

INSERT INTO [dbo].[Profile] ([Name], [Enabled], CreatedAt, CreatedBy, ModifiedAt, ModifiedBy)
VALUES ('Administrador', 1, GETDATE(), 'SCRIPT', GETDATE(), 'SCRIPT')
GO

INSERT INTO [dbo].[AdminUser] ([Name], ProfileId, Email, [Password], [Enabled], CreatedAt, CreatedBy, ModifiedAt, ModifiedBy)
VALUES ('Administrador', (SELECT MIN(Id) from dbo.[Profile]), 'admin@admin.com', '033b83d92431548e13424903c235a9922af56dd34d53c9b72b37cf158489213e', 
        1, GETDATE(), 'SCRIPT', GETDATE(), 'SCRIPT') -- senha: abc123!
GO