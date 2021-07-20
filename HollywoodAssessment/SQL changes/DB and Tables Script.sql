USE [master]
GO
/****** Object:  Database [HollywoodAssessmentDb]    Script Date: 2019/02/04 2:03:55 PM ******/
CREATE DATABASE [HollywoodAssessmentDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HollywoodAssessmentDb', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\HollywoodAssessmentDb.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HollywoodAssessmentDb_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\HollywoodAssessmentDb_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HollywoodAssessmentDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HollywoodAssessmentDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET  MULTI_USER 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HollywoodAssessmentDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [HollywoodAssessmentDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [HollywoodAssessmentDb]
GO
/****** Object:  User [TournamentUser]    Script Date: 2019/02/04 2:03:56 PM ******/
CREATE USER [TournamentUser] FOR LOGIN [TournamentUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [TournamentUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [TournamentUser]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 2019/02/04 2:03:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventID] [bigint] IDENTITY(2000000,1) NOT NULL,
	[TournamentID] [bigint] NULL,
	[EventName] [varchar](100) NULL,
	[EventNumber] [smallint] NULL,
	[EventDateTime] [datetime] NULL,
	[EventEndDateTime] [datetime] NULL,
	[AutoClose] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventDetail]    Script Date: 2019/02/04 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventDetail](
	[EventDetailID] [bigint] IDENTITY(5000000,1) NOT NULL,
	[EventID] [bigint] NULL,
	[EventDetailStatusID] [smallint] NULL,
	[EventDetailName] [varchar](50) NULL,
	[EventDetailNumber] [smallint] NULL,
	[EventDetailOdd] [decimal](18, 7) NULL,
	[FinishingPosition] [smallint] NULL,
	[FirstTimer] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventDetailStatus]    Script Date: 2019/02/04 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventDetailStatus](
	[EventDetailStatusID] [smallint] IDENTITY(1,1) NOT NULL,
	[EventDetailStatusName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventDetailStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournament]    Script Date: 2019/02/04 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournament](
	[TournamentID] [bigint] IDENTITY(1000000,1) NOT NULL,
	[TournamentName] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[TournamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2019/02/04 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1000,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](30) NULL,
	[Username] [varchar](30) NULL,
	[PasswordHash] [binary](64) NULL,
	[PasswordSalt] [binary](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournament] ([TournamentID])
GO
ALTER TABLE [dbo].[EventDetail]  WITH CHECK ADD FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[EventDetail]  WITH CHECK ADD FOREIGN KEY([EventDetailStatusID])
REFERENCES [dbo].[EventDetailStatus] ([EventDetailStatusID])
GO
/****** Object:  StoredProcedure [dbo].[Event.ChangeOdds]    Script Date: 2019/02/04 2:03:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Event.ChangeOdds] @EventId INT,  @NewOdd DECIMAL (18,2)
AS
UPDATE [EventDetail]
SET EventDetailOdd = @NewOdd
WHERE EventId = @EventId

GO
USE [master]
GO
ALTER DATABASE [HollywoodAssessmentDb] SET  READ_WRITE 
GO
