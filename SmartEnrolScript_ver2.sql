USE [master]
GO
/****** Object:  Database [SmartEnrol]    Script Date: 2/11/2025 12:28:16 PM ******/
CREATE DATABASE [SmartEnrol]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartEnrol', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\SmartEnrol.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartEnrol_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\SmartEnrol_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SmartEnrol] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartEnrol].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartEnrol] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartEnrol] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartEnrol] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartEnrol] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartEnrol] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartEnrol] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartEnrol] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartEnrol] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartEnrol] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartEnrol] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartEnrol] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartEnrol] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartEnrol] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartEnrol] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartEnrol] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SmartEnrol] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartEnrol] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartEnrol] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartEnrol] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartEnrol] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartEnrol] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartEnrol] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartEnrol] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartEnrol] SET  MULTI_USER 
GO
ALTER DATABASE [SmartEnrol] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartEnrol] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartEnrol] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartEnrol] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartEnrol] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartEnrol] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartEnrol', N'ON'
GO
ALTER DATABASE [SmartEnrol] SET QUERY_STORE = ON
GO
ALTER DATABASE [SmartEnrol] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SmartEnrol]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__Students__32C52B9905656C98] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdmissionMethodOfMajor]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmissionMethodOfMajor](
	[AdmissionMethodsOfMajorId] [int] IDENTITY(1,1) NOT NULL,
	[MajorId] [int] NOT NULL,
	[AdmissionMethodID] [int] NOT NULL,
	[AdmissionTargets] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdmissionMethodsOfMajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdmissionMethodOfUni]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmissionMethodOfUni](
	[AdmissionMethodID] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](255) NOT NULL,
	[Year] [int] NOT NULL,
	[UniId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdmissionMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Characteristic]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Characteristic](
	[CharacteristicId] [int] IDENTITY(1,1) NOT NULL,
	[CharacteristicName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CharacteristicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacteristicOfField]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacteristicOfField](
	[CharacteristicsOfFieldId] [int] IDENTITY(1,1) NOT NULL,
	[FieldID] [int] NOT NULL,
	[CharacteristicID] [int] NOT NULL,
	[MinScore] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CharacteristicsOfFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacteristicOfStudent]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacteristicOfStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CharacteristicID] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Score] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Field]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[FieldId] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldOfUni]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldOfUni](
	[FieldsOfUniId] [int] IDENTITY(1,1) NOT NULL,
	[UniversityId] [int] NOT NULL,
	[FieldId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldsOfUniId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[MajorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[FieldOfUniID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recommendation]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recommendation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[MajorID] [int] NOT NULL,
	[Recommendation] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](25) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[UniversityId] [int] IDENTITY(1,1) NOT NULL,
	[UniversityName] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[Website] [nvarchar](255) NULL,
 CONSTRAINT [PK__Universi__9F19E1BC0C5AAF4C] PRIMARY KEY CLUSTERED 
(
	[UniversityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishListItem]    Script Date: 2/11/2025 12:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishListItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[MajorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [AccountName], [Email], [Password], [CreatedDate], [RoleId], [IsActive]) VALUES (1, N'Minh Tri', N'triminh0502@gmail.com', N'83d4536114b0c317a3e63709a2ad844b1b28981f10c8fecc4dfab7299567c145', CAST(N'2025-10-02T00:00:00.000' AS DateTime), 3, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfMajor] ON 

INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [MajorId], [AdmissionMethodID], [AdmissionTargets]) VALUES (1, 1, 1, 1480)
INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [MajorId], [AdmissionMethodID], [AdmissionTargets]) VALUES (2, 1, 3, 960)
INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [MajorId], [AdmissionMethodID], [AdmissionTargets]) VALUES (3, 1, 4, 210)
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfMajor] OFF
GO
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfUni] ON 

INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1, N'Tốt nghiệp THPT', 2024, 1)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (2, N'Học bạ', 2024, 1)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (3, N'Kỳ thi đánh giá năng lực', 2024, 1)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (4, N'Tuyển thẳng', 2024, 1)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (6, N'Kết hợp', 2024, 1)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (7, N'Tốt nghiệp THPT', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (8, N'Học bạ', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (9, N'Kỳ thi đánh giá năng lực', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (10, N'Tuyển thẳng', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (11, N'Kết hợp', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (12, N'Chứng chỉ quốc tế', 2024, 2)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (13, N'Tốt nghiệp THPT', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (14, N'Học bạ', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (15, N'Kỳ thi đánh giá năng lực', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (16, N'Tuyển thẳng', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (17, N'Kết hợp', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (18, N'Chứng chỉ quốc tế', 2024, 3)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (19, N'Tốt nghiệp THPT', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (20, N'Học bạ', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (21, N'Kỳ thi đánh giá năng lực', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (22, N'Tuyển thẳng', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (23, N'Kết hợp', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (24, N'Chứng chỉ quốc tế', 2024, 4)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (25, N'Tốt nghiệp THPT', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (26, N'Học bạ', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (27, N'Kỳ thi đánh giá năng lực', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (28, N'Tuyển thẳng', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (29, N'Kết hợp', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (30, N'Chứng chỉ quốc tế', 2024, 5)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (31, N'Tốt nghiệp THPT', 2024, 7)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (32, N'Học bạ', 2024, 7)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (33, N'Kỳ thi đánh giá năng lực', 2024, 7)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (34, N'Tuyển thẳng', 2024, 7)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (35, N'Chứng chỉ quốc tế', 2024, 7)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (36, N'Tốt nghiệp THPT', 2024, 8)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (37, N'Học bạ', 2024, 8)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (38, N'Kỳ thi đánh giá năng lực', 2024, 8)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (39, N'Tuyển thẳng', 2024, 8)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (40, N'Chứng chỉ quốc tế', 2024, 8)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (41, N'Tốt nghiệp THPT', 2024, 9)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (42, N'Học bạ', 2024, 9)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (43, N'Kỳ thi đánh giá năng lực', 2024, 9)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (44, N'Tuyển thẳng', 2024, 9)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (45, N'Chứng chỉ quốc tế', 2024, 9)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (46, N'Tốt nghiệp THPT', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (47, N'Học bạ', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (48, N'Kỳ thi đánh giá năng lực', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (49, N'Tuyển thẳng', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (50, N'Kết hợp', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (51, N'Chứng chỉ quốc tế', 2024, 10)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (52, N'Tốt nghiệp THPT', 2024, 11)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (53, N'Học bạ', 2024, 11)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (54, N'Kỳ thi đánh giá năng lực', 2024, 11)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (55, N'Tuyển thẳng', 2024, 11)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (56, N'Chứng chỉ quốc tế', 2024, 11)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1002, N'Tốt nghiệp THPT', 2024, 12)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1003, N'Tuyển thẳng', 2024, 12)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1004, N'Kết hợp', 2024, 12)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1005, N'Tốt nghiệp THPT', 2024, 13)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1006, N'Học bạ', 2024, 13)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1007, N'Tuyển thẳng', 2024, 13)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1008, N'Kỳ thi đánh giá năng lực', 2024, 13)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1009, N'Chứng chỉ quốc tế', 2024, 13)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1010, N'Tốt nghiệp THPT', 2024, 14)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1011, N'Học bạ', 2024, 14)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1012, N'Tuyển thẳng', 2024, 14)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1013, N'Chứng chỉ quốc tế', 2024, 14)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID], [MethodName], [Year], [UniId]) VALUES (1014, N'Kết hợp', 2024, 14)
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfUni] OFF
GO
SET IDENTITY_INSERT [dbo].[Field] ON 

INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (1, N'Kinh tế, Quản trị và Tài chính')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (2, N'Kỹ thuật và Công nghệ')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (3, N'Khoa học tự nhiên

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (4, N'Khoa học xã hội và Nhân văn

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (5, N'Sư phạm

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (6, N'Nghệ thuật và Thiết kế

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (7, N'Y tế và Dược

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (8, N'Luật và Pháp lý

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (9, N'Nông nghiệp và Thủy sản

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (10, N'Du lịch và Khách sạn

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (11, N'Khoa học và Công nghệ Môi trường

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (12, N'Giao thông và Vận tải

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (13, N'Thể dục Thể thao

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (14, N'Ngoại ngữ

')
INSERT [dbo].[Field] ([FieldId], [FieldName]) VALUES (15, N'Khác')
SET IDENTITY_INSERT [dbo].[Field] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldOfUni] ON 

INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (1, 1, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (2, 1, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (3, 1, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (4, 1, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (5, 1, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (6, 1, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (7, 1, 7)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (8, 1, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (9, 1, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (10, 1, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (11, 2, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (12, 2, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (13, 2, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (14, 2, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (15, 2, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (16, 2, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (17, 2, 7)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (18, 2, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (19, 2, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (20, 2, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (21, 3, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (22, 3, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (23, 3, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (24, 3, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (25, 3, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (26, 4, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (27, 4, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (28, 4, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (29, 4, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (30, 4, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (31, 4, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (32, 5, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (33, 5, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (34, 5, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (35, 5, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (36, 5, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (37, 5, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (38, 5, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (39, 7, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (40, 7, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (41, 7, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (42, 7, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (43, 7, 7)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (44, 7, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (45, 7, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (46, 7, 13)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (47, 8, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (48, 8, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (49, 8, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (50, 8, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (51, 8, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (52, 8, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (53, 8, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (54, 9, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (55, 9, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (56, 9, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (57, 9, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (58, 9, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (59, 9, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (60, 9, 13)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (61, 9, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (62, 10, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (63, 10, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (64, 10, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (65, 10, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (66, 10, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (67, 10, 7)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (68, 10, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (69, 10, 9)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (70, 10, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (71, 10, 11)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (72, 10, 12)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (73, 10, 13)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (74, 10, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (75, 10, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (76, 11, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (77, 11, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (78, 11, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (79, 11, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (80, 11, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (81, 11, 7)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (82, 11, 8)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (83, 11, 9)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (84, 11, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (85, 11, 11)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (86, 11, 12)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (87, 11, 13)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (88, 11, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (89, 11, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (90, 12, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (91, 12, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (92, 12, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (93, 12, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (97, 12, 9)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (98, 12, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (99, 12, 11)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (102, 12, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (103, 12, 15)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (104, 13, 1)
GO
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (105, 14, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (106, 14, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (107, 14, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (108, 15, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (109, 15, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (110, 15, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (111, 16, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (112, 16, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (113, 16, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (114, 16, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (115, 16, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (116, 16, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (117, 17, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (118, 17, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (119, 18, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (120, 18, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (121, 18, 6)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (122, 18, 10)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (123, 18, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (124, 19, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (125, 19, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (126, 19, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (127, 19, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (128, 20, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (129, 20, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (130, 20, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (131, 21, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (132, 21, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (133, 22, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (134, 22, 14)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (135, 23, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (136, 23, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (137, 23, 5)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (138, 24, 1)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (139, 24, 2)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (140, 24, 3)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (141, 24, 4)
INSERT [dbo].[FieldOfUni] ([FieldsOfUniId], [UniversityId], [FieldId]) VALUES (142, 24, 14)
SET IDENTITY_INSERT [dbo].[FieldOfUni] OFF
GO
SET IDENTITY_INSERT [dbo].[Major] ON 

INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (1, N'Công nghệ thông tin', 2)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (2, N'Kỹ thuật máy tính', 2)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (3, N'Vật lý học', 2)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (4, N'Kỹ thuật điện tử', 2)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (5, N'Y khoa', 7)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (6, N'Công nghệ sinh học', 4)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (7, N'Kỹ thuật xét nghiệm y học', 7)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (8, N'Ngữ văn', 5)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (9, N'Lịch sử', 5)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (10, N'Địa lý', 5)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (11, N'Báo chí', 6)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (12, N'Luật', 8)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (13, N'Ngôn ngữ Anh', 14)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (14, N'Quản trị kinh doanh', 1)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (16, N'Kinh tế quốc tế', 1)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (17, N'Quản trị kinh doanh', 1)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (18, N' Tài chính - Ngân hàng', 1)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (19, N'Ngôn ngữ Nhật', 14)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (20, N'Ngôn ngữ Trung Quốc', 14)
INSERT [dbo].[Major] ([MajorId], [Name], [FieldOfUniID]) VALUES (21, N'Dược học', 7)
SET IDENTITY_INSERT [dbo].[Major] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (1, N'Admin                    ', 1)
INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (2, N'Staff                    ', 1)
INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (3, N'Student                  ', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[University] ON 

INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (1, N'ĐẠI HỌC QUỐC GIA HÀ NỘI
', N'VNU-HN', N'Hà Nội', N'media@vnu.edu.vn', N'(024) 6673 6701', N'www.vnu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (2, N'ĐẠI HỌC QUỐC GIA THÀNH PHỐ HỒ CHÍ MINH
', N'VNU-HCM', N'Hồ Chí Minh', N'info@vnuhcm.edu.vn', N'(+84-28) 3724 2181', N'https://vnuhcm.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (3, N'Đại học Bách khoa Hà Nội
', N'HUST', N'Hà Nội', N' vp@hust.edu.vn', N'(024) 3869 4242', N' http://www.hust.edu.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (4, N'Đại học Kinh tế TP. Hồ Chí Minh
', N'UEH ', N'Hồ Chí Minh', N'info@ueh.edu.vn', N'(+84) 28 38 26 75 05', N'www.ueh.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (5, N'TRƯỜNG ĐẠI HỌC TÔN ĐỨC THẮNG
', N'TDTU', N'Hồ Chí Minh', N'tdtu@tdtu.edu.vn', N'(+84) 28 37 14 31 31', N'www.tdtu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (7, N'Đại học Duy Tân
', N'DTU', N'Đà Nẵng', N'tuyensinh@duytan.edu.vn', N'(+84) 236 365 3333', N'www.dtu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (8, N'TRƯỜNG ĐẠI HỌC KHOA HỌC VÀ CÔNG NGHỆ HÀ NỘI
', N'USTH', N'Hà Nội', N'tuyensinh@hust.edu.vn', N'(+84) 24 3868 2919', N'https://usth.edu.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (9, N'Trường Đại học Sư phạm Hà Nội
', N'HNUE', N'Hà Nội', N'tuyensinh@hnue.edu.vn', N'(+84) 24 3858 2191', N'www.hnue.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (10, N'Đại học Đà Nẵng
', N'UDN', N'Đà Nẵng', N'tuyensinh@udn.vn', N'(+84) 236 382 0231', N' http://www.udn.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (11, N'Trường Đại học Cần Thơ
', N'CTU', N'Cần Thơ', N'tuyensinh@ctu.edu.vn', N' (+84) 292 383 0301', N'http://ctu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (12, N'Trường Đại học Dược Hà Nội
', N'HUP', N'Hà Nội', N'tuyensinh@hup.edu.vn', N'(+84) 24 3868 4112', N'www.hup.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (13, N'Học viện Ngân hàng
', N'BAV', N'Hà Nội', N'tuyensinh@hvnh.edu.vn', N'(+84) 24 3851 6254', N'https://www.hvnh.edu.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (14, N'Trường Đại học Kinh tế quốc dân
', N'NEU', N'Hà Nội', N'tuyensinh@neu.edu.vn', N'(+84) 24 3754 7522', N'http://neu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (15, N'TRƯỜNG ĐẠI HỌC SƯ PHẠM HÀ NỘI 2
', N'HPU2', N'Hà Nội', N'tuyensinh@hnue2.edu.vn', N'(+84) 24 3382 3385', N' www.hpu2.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (16, N'Trường Đại học Nguyễn Tất Thành
', N'NTT', N'Hồ Chí Minh', N'tuyensinh@ntt.edu.vn', N' (+84) 28 3843 0503', N'https://www.ntt.edu.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (17, N'Trường Đại học Thuỷ lợi
', N'TLU', N'Hà Nội', N'tuyensinh@tlu.edu.vn', N'(+84) 24 3852 4104', N' http://tlu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (18, N'TRƯỜNG ĐẠI HỌC VĂN LANG
', N'VLU', N'Hồ Chí Minh', N'tuyensinh@vlu.edu.vn', N'(+84) 28 7108 6818', N'www.vanlanguni.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (19, N'TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH
', N'HCMUE', N'Hồ Chí Minh', N'tuyensinh@hcmup.edu.vn', N'(+84) 28 3835 5546', N'www.hcmue.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (20, N'Trường Đại học Công nghiệp TP. Hồ Chí Minh
', N'IUH', N'Hồ Chí Minh', N'tuyensinh@hufi.edu.vn', N'(+84) 28 3835 5450', N'www.iuh.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (21, N'Trường Đại học Mỏ – Địa chất
', N'HUMG', N'Hà Nội', N'tuyensinh@humg.edu.vn', N'(+84) 24 3852 3037', N'http://humg.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (22, N'Trường Đại học Ngoại Thương
', N'FTU', N'Hà Nội', N'tuyensinh@ftu.edu.vn', N'(+84) 24 3754 7227', N' http://www.ftu.edu.vn')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (23, N'Trường Đại học Sư phạm Kỹ thuật TP. Hồ Chí Minh
', N'HCMUTE', N'Hồ Chí Minh', N'tuyensinh@hcmute.edu.vn', N'(+84) 28 3722 2452', N'http://hcmute.edu.vn/')
INSERT [dbo].[University] ([UniversityId], [UniversityName], [Code], [Location], [Email], [Phone], [Website]) VALUES (24, N'Trường Đại học Vinh
', N'VINHUNI', N'Nghệ An', N'tuyensinh@vnu.edu.vn', N'(+84) 238 855 0787', N'https://vinhuni.edu.vn')
SET IDENTITY_INSERT [dbo].[University] OFF
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Students__Create__3D5E1FD2]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Students__IsActi__3E52440B]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Recommendation] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor]  WITH CHECK ADD FOREIGN KEY([AdmissionMethodID])
REFERENCES [dbo].[AdmissionMethodOfUni] ([AdmissionMethodID])
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor]  WITH CHECK ADD FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([MajorId])
GO
ALTER TABLE [dbo].[AdmissionMethodOfUni]  WITH CHECK ADD  CONSTRAINT [FK__Admission__UniId__47DBAE45] FOREIGN KEY([UniId])
REFERENCES [dbo].[University] ([UniversityId])
GO
ALTER TABLE [dbo].[AdmissionMethodOfUni] CHECK CONSTRAINT [FK__Admission__UniId__47DBAE45]
GO
ALTER TABLE [dbo].[CharacteristicOfField]  WITH CHECK ADD FOREIGN KEY([CharacteristicID])
REFERENCES [dbo].[Characteristic] ([CharacteristicId])
GO
ALTER TABLE [dbo].[CharacteristicOfField]  WITH CHECK ADD FOREIGN KEY([FieldID])
REFERENCES [dbo].[Field] ([FieldId])
GO
ALTER TABLE [dbo].[CharacteristicOfStudent]  WITH CHECK ADD FOREIGN KEY([CharacteristicID])
REFERENCES [dbo].[Characteristic] ([CharacteristicId])
GO
ALTER TABLE [dbo].[CharacteristicOfStudent]  WITH CHECK ADD  CONSTRAINT [FK__Character__Stude__5BE2A6F2] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[CharacteristicOfStudent] CHECK CONSTRAINT [FK__Character__Stude__5BE2A6F2]
GO
ALTER TABLE [dbo].[FieldOfUni]  WITH CHECK ADD FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([FieldId])
GO
ALTER TABLE [dbo].[FieldOfUni]  WITH CHECK ADD  CONSTRAINT [FK__FieldsOfU__Unive__412EB0B6] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([UniversityId])
GO
ALTER TABLE [dbo].[FieldOfUni] CHECK CONSTRAINT [FK__FieldsOfU__Unive__412EB0B6]
GO
ALTER TABLE [dbo].[Major]  WITH CHECK ADD FOREIGN KEY([FieldOfUniID])
REFERENCES [dbo].[FieldOfUni] ([FieldsOfUniId])
GO
ALTER TABLE [dbo].[Recommendation]  WITH CHECK ADD FOREIGN KEY([MajorID])
REFERENCES [dbo].[Major] ([MajorId])
GO
ALTER TABLE [dbo].[Recommendation]  WITH CHECK ADD  CONSTRAINT [FK__Recommend__Stude__534D60F1] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Recommendation] CHECK CONSTRAINT [FK__Recommend__Stude__534D60F1]
GO
ALTER TABLE [dbo].[WishListItem]  WITH CHECK ADD FOREIGN KEY([MajorID])
REFERENCES [dbo].[Major] ([MajorId])
GO
ALTER TABLE [dbo].[WishListItem]  WITH CHECK ADD  CONSTRAINT [FK__WishListI__Stude__571DF1D5] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[WishListItem] CHECK CONSTRAINT [FK__WishListI__Stude__571DF1D5]
GO
USE [master]
GO
ALTER DATABASE [SmartEnrol] SET  READ_WRITE 
GO
