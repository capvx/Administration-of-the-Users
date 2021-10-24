USE [ADMIN_1]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 5/14/2019 12:21:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsAdmin] [int] NULL
) ON [PRIMARY]
GO

