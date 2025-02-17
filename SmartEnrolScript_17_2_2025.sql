USE [master]
GO
/****** Object:  Database [SmartEnrolDB]    Script Date: 2/17/2025 9:53:49 AM ******/
CREATE DATABASE [SmartEnrolDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartEnrolDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\SmartEnrolDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartEnrolDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\SmartEnrolDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SmartEnrolDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartEnrolDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartEnrolDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartEnrolDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartEnrolDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SmartEnrolDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartEnrolDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartEnrolDB] SET  MULTI_USER 
GO
ALTER DATABASE [SmartEnrolDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartEnrolDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartEnrolDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartEnrolDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartEnrolDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartEnrolDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartEnrolDB', N'ON'
GO
ALTER DATABASE [SmartEnrolDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [SmartEnrolDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SmartEnrolDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[RoleId] [int] NOT NULL,
	[AreaId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdmissionMethodOfMajor]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmissionMethodOfMajor](
	[AdmissionMethodsOfMajorId] [int] IDENTITY(1,1) NOT NULL,
	[UniMajorId] [int] NOT NULL,
	[AdmissionMethodOfUniId] [int] NOT NULL,
	[AdmissionTargets] [int] NULL,
 CONSTRAINT [PK_AdmissionMethodOfMajor] PRIMARY KEY CLUSTERED 
(
	[AdmissionMethodsOfMajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdmissionMethodOfUni]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmissionMethodOfUni](
	[AdmissionMethodOfUniId] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](max) NOT NULL,
	[UniId] [int] NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_AdmissionMethodOfUni] PRIMARY KEY CLUSTERED 
(
	[AdmissionMethodOfUniId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[AreaId] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Characteristic]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Characteristic](
	[CharacteristicId] [int] IDENTITY(1,1) NOT NULL,
	[CharacteristicName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Characteristic] PRIMARY KEY CLUSTERED 
(
	[CharacteristicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacteristicOfMajor]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacteristicOfMajor](
	[CharacteristicOfMajorId] [int] IDENTITY(1,1) NOT NULL,
	[CharacteristicId] [int] NOT NULL,
	[MajorId] [int] NOT NULL,
 CONSTRAINT [PK_CharacteristicOfMajor] PRIMARY KEY CLUSTERED 
(
	[CharacteristicOfMajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacteristicOfStudent]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacteristicOfStudent](
	[CharacteristicOfStudentId] [int] IDENTITY(1,1) NOT NULL,
	[CharacteristicId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Score] [int] NOT NULL,
 CONSTRAINT [PK_CharacteristicOfStudent] PRIMARY KEY CLUSTERED 
(
	[CharacteristicOfStudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[MajorId] [int] IDENTITY(1,1) NOT NULL,
	[MajorName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recommendation]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recommendation](
	[RecommendationId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Recommendation] PRIMARY KEY CLUSTERED 
(
	[RecommendationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecommendationDetail]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecommendationDetail](
	[RecommendationDetailId] [int] IDENTITY(1,1) NOT NULL,
	[RecommendationId] [int] NOT NULL,
	[UniMajorId] [int] NOT NULL,
	[Recommendation] [nvarchar](max) NULL,
 CONSTRAINT [PK_RecommendationDetail] PRIMARY KEY CLUSTERED 
(
	[RecommendationDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/17/2025 9:53:50 AM ******/
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
/****** Object:  Table [dbo].[UniMajor]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniMajor](
	[UniMajorId] [int] IDENTITY(1,1) NOT NULL,
	[UniId] [int] NOT NULL,
	[MajorId] [int] NULL,
 CONSTRAINT [PK_UniMajor] PRIMARY KEY CLUSTERED 
(
	[UniMajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[UniId] [int] IDENTITY(1,1) NOT NULL,
	[UniName] [nvarchar](max) NOT NULL,
	[UniCode] [nvarchar](50) NULL,
	[Location] [nvarchar](max) NULL,
	[AreaId] [int] NOT NULL,
	[Email] [nchar](255) NULL,
	[Phone] [nchar](15) NULL,
	[Website] [nchar](255) NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[UniId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishList]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishList](
	[WishListId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_WishList] PRIMARY KEY CLUSTERED 
(
	[WishListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishListItem]    Script Date: 2/17/2025 9:53:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishListItem](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[UniMajorId] [int] NULL,
	[UniId] [int] NULL,
	[WishListId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_WishListItem] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([AreaId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Area]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor]  WITH CHECK ADD  CONSTRAINT [FK_AdmissionMethodOfMajor_AdmissionMethodOfUni] FOREIGN KEY([AdmissionMethodOfUniId])
REFERENCES [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId])
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor] CHECK CONSTRAINT [FK_AdmissionMethodOfMajor_AdmissionMethodOfUni]
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor]  WITH CHECK ADD  CONSTRAINT [FK_AdmissionMethodOfMajor_UniMajor] FOREIGN KEY([UniMajorId])
REFERENCES [dbo].[UniMajor] ([UniMajorId])
GO
ALTER TABLE [dbo].[AdmissionMethodOfMajor] CHECK CONSTRAINT [FK_AdmissionMethodOfMajor_UniMajor]
GO
ALTER TABLE [dbo].[AdmissionMethodOfUni]  WITH CHECK ADD  CONSTRAINT [FK_AdmissionMethodOfUni_University] FOREIGN KEY([UniId])
REFERENCES [dbo].[University] ([UniId])
GO
ALTER TABLE [dbo].[AdmissionMethodOfUni] CHECK CONSTRAINT [FK_AdmissionMethodOfUni_University]
GO
ALTER TABLE [dbo].[CharacteristicOfMajor]  WITH CHECK ADD  CONSTRAINT [FK_CharacteristicOfMajor_Characteristic] FOREIGN KEY([CharacteristicId])
REFERENCES [dbo].[Characteristic] ([CharacteristicId])
GO
ALTER TABLE [dbo].[CharacteristicOfMajor] CHECK CONSTRAINT [FK_CharacteristicOfMajor_Characteristic]
GO
ALTER TABLE [dbo].[CharacteristicOfMajor]  WITH CHECK ADD  CONSTRAINT [FK_CharacteristicOfMajor_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([MajorId])
GO
ALTER TABLE [dbo].[CharacteristicOfMajor] CHECK CONSTRAINT [FK_CharacteristicOfMajor_Major]
GO
ALTER TABLE [dbo].[CharacteristicOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_CharacteristicOfStudent_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[CharacteristicOfStudent] CHECK CONSTRAINT [FK_CharacteristicOfStudent_Account]
GO
ALTER TABLE [dbo].[CharacteristicOfStudent]  WITH CHECK ADD  CONSTRAINT [FK_CharacteristicOfStudent_Characteristic] FOREIGN KEY([CharacteristicId])
REFERENCES [dbo].[Characteristic] ([CharacteristicId])
GO
ALTER TABLE [dbo].[CharacteristicOfStudent] CHECK CONSTRAINT [FK_CharacteristicOfStudent_Characteristic]
GO
ALTER TABLE [dbo].[Recommendation]  WITH CHECK ADD  CONSTRAINT [FK_Recommendation_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Recommendation] CHECK CONSTRAINT [FK_Recommendation_Account]
GO
ALTER TABLE [dbo].[RecommendationDetail]  WITH CHECK ADD  CONSTRAINT [FK_RecommendationDetail_Recommendation] FOREIGN KEY([RecommendationId])
REFERENCES [dbo].[Recommendation] ([RecommendationId])
GO
ALTER TABLE [dbo].[RecommendationDetail] CHECK CONSTRAINT [FK_RecommendationDetail_Recommendation]
GO
ALTER TABLE [dbo].[RecommendationDetail]  WITH CHECK ADD  CONSTRAINT [FK_RecommendationDetail_UniMajor] FOREIGN KEY([UniMajorId])
REFERENCES [dbo].[UniMajor] ([UniMajorId])
GO
ALTER TABLE [dbo].[RecommendationDetail] CHECK CONSTRAINT [FK_RecommendationDetail_UniMajor]
GO
ALTER TABLE [dbo].[UniMajor]  WITH CHECK ADD  CONSTRAINT [FK_UniMajor_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([MajorId])
GO
ALTER TABLE [dbo].[UniMajor] CHECK CONSTRAINT [FK_UniMajor_Major]
GO
ALTER TABLE [dbo].[UniMajor]  WITH CHECK ADD  CONSTRAINT [FK_UniMajor_University] FOREIGN KEY([UniId])
REFERENCES [dbo].[University] ([UniId])
GO
ALTER TABLE [dbo].[UniMajor] CHECK CONSTRAINT [FK_UniMajor_University]
GO
ALTER TABLE [dbo].[University]  WITH CHECK ADD  CONSTRAINT [FK_University_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([AreaId])
GO
ALTER TABLE [dbo].[University] CHECK CONSTRAINT [FK_University_Area]
GO
ALTER TABLE [dbo].[WishList]  WITH CHECK ADD  CONSTRAINT [FK_WishList_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[WishList] CHECK CONSTRAINT [FK_WishList_Account]
GO
ALTER TABLE [dbo].[WishListItem]  WITH CHECK ADD  CONSTRAINT [FK_WishListItem_UniMajor] FOREIGN KEY([UniMajorId])
REFERENCES [dbo].[UniMajor] ([UniMajorId])
GO
ALTER TABLE [dbo].[WishListItem] CHECK CONSTRAINT [FK_WishListItem_UniMajor]
GO
ALTER TABLE [dbo].[WishListItem]  WITH CHECK ADD  CONSTRAINT [FK_WishListItem_University] FOREIGN KEY([UniId])
REFERENCES [dbo].[University] ([UniId])
GO
ALTER TABLE [dbo].[WishListItem] CHECK CONSTRAINT [FK_WishListItem_University]
GO
ALTER TABLE [dbo].[WishListItem]  WITH CHECK ADD  CONSTRAINT [FK_WishListItem_WishList] FOREIGN KEY([WishListId])
REFERENCES [dbo].[WishList] ([WishListId])
GO
ALTER TABLE [dbo].[WishListItem] CHECK CONSTRAINT [FK_WishListItem_WishList]
GO
USE [master]
GO
ALTER DATABASE [SmartEnrolDB] SET  READ_WRITE 
GO
