USE [master]
GO
/****** Object:  Database [donorbridge-iitr-database]    Script Date: 13-01-2026 23:00:17 ******/
CREATE DATABASE [donorbridge-iitr-database]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'donorbridge-iitr-database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.RAMSQL\MSSQL\DATA\donorbridge-iitr-database.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'donorbridge-iitr-database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.RAMSQL\MSSQL\DATA\donorbridge-iitr-database_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [donorbridge-iitr-database] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [donorbridge-iitr-database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [donorbridge-iitr-database] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ARITHABORT OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [donorbridge-iitr-database] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [donorbridge-iitr-database] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [donorbridge-iitr-database] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET  ENABLE_BROKER 
GO
ALTER DATABASE [donorbridge-iitr-database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [donorbridge-iitr-database] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [donorbridge-iitr-database] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [donorbridge-iitr-database] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [donorbridge-iitr-database] SET  MULTI_USER 
GO
ALTER DATABASE [donorbridge-iitr-database] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [donorbridge-iitr-database] SET DB_CHAINING OFF 
GO
ALTER DATABASE [donorbridge-iitr-database] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [donorbridge-iitr-database] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [donorbridge-iitr-database] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [donorbridge-iitr-database] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [donorbridge-iitr-database] SET QUERY_STORE = ON
GO
ALTER DATABASE [donorbridge-iitr-database] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [donorbridge-iitr-database]
GO
/****** Object:  Table [dbo].[tbl_donations]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_donations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[amount] [int] NOT NULL,
	[statusID] [int] NULL,
	[created] [date] NOT NULL,
	[updated] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_login]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [varchar](30) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[roleID] [int] NULL,
	[is_active] [bit] NOT NULL,
	[created] [date] NOT NULL,
	[updated] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_roleMaster]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_roleMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_stateMaster]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_stateMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_statusMaster]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_statusMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_transactions]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_transactions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[donationID] [int] NULL,
	[statusID] [int] NULL,
	[created] [date] NOT NULL,
	[updated] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_userRegistration]    Script Date: 13-01-2026 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_userRegistration](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginID] [int] NULL,
	[firstName] [varchar](30) NOT NULL,
	[lastName] [varchar](30) NOT NULL,
	[mobile] [varchar](30) NOT NULL,
	[stateID] [int] NULL,
	[is_verified] [bit] NOT NULL,
	[created] [date] NOT NULL,
	[updated] [date] NOT NULL,
	[email] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_login] ON 
GO
INSERT [dbo].[tbl_login] ([id], [userID], [password], [roleID], [is_active], [created], [updated]) VALUES (1, N'admin', N'Passw0rd#', 1, 1, CAST(N'2026-01-09' AS Date), CAST(N'2026-01-09' AS Date))
GO
INSERT [dbo].[tbl_login] ([id], [userID], [password], [roleID], [is_active], [created], [updated]) VALUES (2, N'testUser', N'test', 2, 1, CAST(N'2026-01-13' AS Date), CAST(N'2026-01-13' AS Date))
GO
SET IDENTITY_INSERT [dbo].[tbl_login] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_roleMaster] ON 
GO
INSERT [dbo].[tbl_roleMaster] ([id], [name]) VALUES (1, N'admin')
GO
INSERT [dbo].[tbl_roleMaster] ([id], [name]) VALUES (2, N'donor')
GO
SET IDENTITY_INSERT [dbo].[tbl_roleMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_stateMaster] ON 
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (1, N'Andhra Pradesh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (2, N'Arunachal Pradesh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (3, N'Assam')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (4, N'Bihar')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (5, N'Chhattisgarh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (6, N'Goa')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (7, N'Gujarat')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (8, N'Haryana')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (9, N'Himachal Pradesh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (10, N'Jharkhand')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (11, N'Karnataka')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (12, N'Kerala')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (13, N'Madhya Pradesh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (14, N'Maharashtra')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (15, N'Manipur')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (16, N'Meghalaya')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (17, N'Mizoram')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (18, N'Nagaland')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (19, N'Odisha')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (20, N'Punjab')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (21, N'Rajasthan')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (22, N'Sikkim')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (23, N'Tamil Nadu')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (24, N'Telangana')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (25, N'Tripura')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (26, N'Uttar Pradesh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (27, N'Uttarakhand')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (28, N'West Bengal')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (29, N'Andaman & Nicobar Islands')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (30, N'Chandigarh')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (31, N'Dadra & Nagar Haveli and Daman & Diu')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (32, N'Delhi (National Capital Territory)')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (33, N'Jammu & Kashmir (UT)')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (34, N'Ladakh (UT)')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (35, N'Lakshadweep')
GO
INSERT [dbo].[tbl_stateMaster] ([id], [name]) VALUES (36, N'Puducherry')
GO
SET IDENTITY_INSERT [dbo].[tbl_stateMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_statusMaster] ON 
GO
INSERT [dbo].[tbl_statusMaster] ([id], [name]) VALUES (1, N'completed')
GO
INSERT [dbo].[tbl_statusMaster] ([id], [name]) VALUES (2, N'pending')
GO
INSERT [dbo].[tbl_statusMaster] ([id], [name]) VALUES (3, N'failed')
GO
SET IDENTITY_INSERT [dbo].[tbl_statusMaster] OFF
GO
ALTER TABLE [dbo].[tbl_donations] ADD  DEFAULT ((2)) FOR [statusID]
GO
ALTER TABLE [dbo].[tbl_donations] ADD  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[tbl_donations] ADD  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[tbl_login] ADD  DEFAULT ((2)) FOR [roleID]
GO
ALTER TABLE [dbo].[tbl_login] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[tbl_login] ADD  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[tbl_login] ADD  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[tbl_transactions] ADD  DEFAULT ((2)) FOR [statusID]
GO
ALTER TABLE [dbo].[tbl_transactions] ADD  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[tbl_transactions] ADD  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[tbl_userRegistration] ADD  DEFAULT ((0)) FOR [is_verified]
GO
ALTER TABLE [dbo].[tbl_userRegistration] ADD  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[tbl_userRegistration] ADD  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[tbl_donations]  WITH CHECK ADD FOREIGN KEY([statusID])
REFERENCES [dbo].[tbl_statusMaster] ([id])
GO
ALTER TABLE [dbo].[tbl_donations]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[tbl_userRegistration] ([id])
GO
ALTER TABLE [dbo].[tbl_login]  WITH CHECK ADD FOREIGN KEY([roleID])
REFERENCES [dbo].[tbl_roleMaster] ([id])
GO
ALTER TABLE [dbo].[tbl_transactions]  WITH CHECK ADD FOREIGN KEY([donationID])
REFERENCES [dbo].[tbl_donations] ([id])
GO
ALTER TABLE [dbo].[tbl_transactions]  WITH CHECK ADD FOREIGN KEY([statusID])
REFERENCES [dbo].[tbl_statusMaster] ([id])
GO
ALTER TABLE [dbo].[tbl_userRegistration]  WITH CHECK ADD FOREIGN KEY([loginID])
REFERENCES [dbo].[tbl_login] ([id])
GO
ALTER TABLE [dbo].[tbl_userRegistration]  WITH CHECK ADD FOREIGN KEY([stateID])
REFERENCES [dbo].[tbl_stateMaster] ([id])
GO
USE [master]
GO
ALTER DATABASE [donorbridge-iitr-database] SET  READ_WRITE 
GO
