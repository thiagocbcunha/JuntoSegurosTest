USE [master]
GO
/****** Object:  Database [JuntoSegurosOnboarding]    Script Date: 07/06/2024 18:05:37 ******/
CREATE DATABASE [JuntoSegurosOnboarding]

GO
USE [JuntoSegurosOnboarding]

GO
CREATE TABLE [dbo].[Gender](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [uniqueidentifier] DEFAULT (NEWID()) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[DocumentNumber] [varchar](12) NOT NULL,
	[BirthDate] [date] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonAccess]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonAccess](
	[PersonId] [uniqueidentifier] NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PersonAccess] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonAccessEvent]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonAccessEvent](
	[PersonId] [uniqueidentifier] NOT NULL,
	[VersionNum] [int] NOT NULL,
	[Actived] [bit] NOT NULL,
	[EncryptedPass] [varchar](1000) NOT NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
 CONSTRAINT [PK_PersonAccessEvent] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[VersionNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonAccessRegister]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonAccessRegister](
	[Id] [bigint] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[SystemEntry] [int] NOT NULL,
	[DateEntry] [datetime] NOT NULL,
 CONSTRAINT [PK_PersonAccessRegister] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonEvent]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonEvent](
	[PersonId] [uniqueidentifier] NOT NULL,
	[VersionNum] [int] NOT NULL,
	[GenderId] [int] NOT NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[CreateDate] [date] NULL,
 CONSTRAINT [PK_PersonEvent] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[VersionNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone](
	[Id] [int] NOT NULL,
	[DDD] [int] NOT NULL,
	[Number] [bigint] NOT NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhonePerson]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhonePerson](
	[Id] [uniqueidentifier] DEFAULT (NEWID()) NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[PhoneId] [int] NOT NULL,
 CONSTRAINT [PK_PhonePerson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhonePersonEvent]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhonePersonEvent](
	[PhonePersonId] [uniqueidentifier] NOT NULL,
	[VersionNumber] [int] NOT NULL,
	[PhoneTypeId] [int] NOT NULL,
	[Actived] [bit] NOT NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PhonePersonEvent] PRIMARY KEY CLUSTERED 
(
	[PhonePersonId] ASC,
	[VersionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneType]    Script Date: 07/06/2024 18:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[Active] [bit] NOT NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PersonAccess]  WITH CHECK ADD  CONSTRAINT [FK_PersonAccess_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonAccess] CHECK CONSTRAINT [FK_PersonAccess_Person]
GO
ALTER TABLE [dbo].[PersonAccessEvent]  WITH CHECK ADD  CONSTRAINT [FK_PersonAccessEvent_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonAccessEvent] CHECK CONSTRAINT [FK_PersonAccessEvent_Person]
GO
ALTER TABLE [dbo].[PersonAccessRegister]  WITH CHECK ADD  CONSTRAINT [FK_PersonAccessRegister_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonAccessRegister] CHECK CONSTRAINT [FK_PersonAccessRegister_Person]
GO
ALTER TABLE [dbo].[PersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PersonEvent_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[PersonEvent] CHECK CONSTRAINT [FK_PersonEvent_Gender]
GO
ALTER TABLE [dbo].[PersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PersonEvent_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonEvent] CHECK CONSTRAINT [FK_PersonEvent_Person]
GO
ALTER TABLE [dbo].[PhonePerson]  WITH CHECK ADD  CONSTRAINT [FK_PhonePerson_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PhonePerson] CHECK CONSTRAINT [FK_PhonePerson_Person]
GO
ALTER TABLE [dbo].[PhonePerson]  WITH CHECK ADD  CONSTRAINT [FK_PhonePerson_Phone] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phone] ([Id])
GO
ALTER TABLE [dbo].[PhonePerson] CHECK CONSTRAINT [FK_PhonePerson_Phone]
GO
ALTER TABLE [dbo].[PhonePersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PhonePersonEvent_PhonePerson] FOREIGN KEY([PhonePersonId])
REFERENCES [dbo].[PhonePerson] ([Id])
GO
ALTER TABLE [dbo].[PhonePersonEvent] CHECK CONSTRAINT [FK_PhonePersonEvent_PhonePerson]
GO
ALTER TABLE [dbo].[PhonePersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PhonePersonEvent_PhoneType] FOREIGN KEY([PhoneTypeId])
REFERENCES [dbo].[PhoneType] ([Id])
GO
ALTER TABLE [dbo].[PhonePersonEvent] CHECK CONSTRAINT [FK_PhonePersonEvent_PhoneType]
GO
USE [master]
GO
ALTER DATABASE [JuntoSegurosOnboarding] SET  READ_WRITE 
GO
