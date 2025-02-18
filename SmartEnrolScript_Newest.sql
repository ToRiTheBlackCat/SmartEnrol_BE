USE [master]
GO
/****** Object:  Database [SmartEnrolDB]    Script Date: 2/17/2025 9:11:34 PM ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[AdmissionMethodOfMajor]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[AdmissionMethodOfUni]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[Area]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[Characteristic]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[CharacteristicOfMajor]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[CharacteristicOfStudent]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[Major]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[Recommendation]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[RecommendationDetail]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[UniMajor]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[University]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[WishList]    Script Date: 2/17/2025 9:11:35 PM ******/
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
/****** Object:  Table [dbo].[WishListItem]    Script Date: 2/17/2025 9:11:35 PM ******/
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
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [AccountName], [Email], [Password], [RoleId], [AreaId], [CreatedDate], [IsActive]) VALUES (2, N'Minh Trí', N'triminh0502@gmail.com', N'83d4536114b0c317a3e63709a2ad844b1b28981f10c8fecc4dfab7299567c145', 1, 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfMajor] ON 

INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [UniMajorId], [AdmissionMethodOfUniId], [AdmissionTargets]) VALUES (1, 1, 1, 1480)
INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [UniMajorId], [AdmissionMethodOfUniId], [AdmissionTargets]) VALUES (2, 2, 3, 960)
INSERT [dbo].[AdmissionMethodOfMajor] ([AdmissionMethodsOfMajorId], [UniMajorId], [AdmissionMethodOfUniId], [AdmissionTargets]) VALUES (3, 3, 4, 210)
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfMajor] OFF
GO
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfUni] ON 

INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (1, N'Tốt nghiệp THPT', 1, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (2, N'Học bạ', 1, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (3, N'Kỳ thi đánh giá năng lực', 1, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (4, N'Tuyển thẳng', 1, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (6, N'Kết hợp', 1, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (13, N'Tốt nghiệp THPT', 3, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (14, N'Học bạ', 3, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (15, N'Kỳ thi đánh giá năng lực', 3, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (16, N'Tuyển thẳng', 3, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (17, N'Kết hợp', 3, 2024)
INSERT [dbo].[AdmissionMethodOfUni] ([AdmissionMethodOfUniId], [MethodName], [UniId], [Year]) VALUES (18, N'Chứng chỉ quốc tế', 3, 2024)
SET IDENTITY_INSERT [dbo].[AdmissionMethodOfUni] OFF
GO
SET IDENTITY_INSERT [dbo].[Area] ON 

INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (1, N'Hồ Chí Minh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (2, N'Bà Rịa - Vũng Tàu')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (3, N'Bắc Giang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (4, N'Bắc Kạn')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (5, N'Bạc Liêu')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (6, N'Bắc Ninh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (7, N'Bến Tre')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (8, N'Bình Định')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (9, N'Bình Dương')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (10, N'Bình Phước')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (11, N'Bình Thuận')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (12, N'Cà Mau')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (13, N'Cần Thơ')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (14, N'Cao Bằng')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (15, N'Đà Nẵng')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (16, N'Đắk Lắk')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (17, N'Đắk Nông')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (18, N'Điện Biên')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (19, N'Đồng Nai')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (20, N'Đồng Tháp')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (21, N'Gia Lai')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (22, N'Hà Giang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (23, N'Hà Nam')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (24, N'Hà Nội')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (25, N'Hà Tĩnh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (26, N'Hải Dương')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (27, N'Hải Phòng')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (28, N'Hậu Giang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (29, N'Hòa Bình')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (30, N'Hưng Yên')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (31, N'Khánh Hòa')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (32, N'Kiên Giang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (33, N'Kon Tum')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (34, N'Lai Châu')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (35, N'Lâm Đồng')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (36, N'Lạng Sơn')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (37, N'Lào Cai')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (38, N'Long An')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (39, N'Nam Định')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (40, N'Nghệ An')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (41, N'Ninh Bình')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (42, N'Ninh Thuận')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (43, N'Phú Thọ')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (44, N'Phú Yên')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (45, N'Quảng Bình')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (46, N'Quảng Nam')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (47, N'Quảng Ngãi')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (48, N'Quảng Ninh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (49, N'Quảng Trị')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (50, N'Sóc Trăng')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (51, N'Sơn La')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (52, N'Tây Ninh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (53, N'Thái Bình')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (54, N'Thái Nguyên')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (55, N'Thanh Hóa')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (56, N'Thừa Thiên Huế')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (57, N'Tiền Giang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (58, N'TP. Hồ Chí Minh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (59, N'Trà Vinh')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (60, N'Tuyên Quang')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (61, N'Vĩnh Long')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (62, N'Vĩnh Phúc')
INSERT [dbo].[Area] ([AreaId], [AreaName]) VALUES (63, N'Yên Bái')
SET IDENTITY_INSERT [dbo].[Area] OFF
GO
SET IDENTITY_INSERT [dbo].[Major] ON 

INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (2, N'Kỹ thuật máy tính')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (3, N'Vật lý học')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (4, N'Kỹ thuật điện tử')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (5, N'Y khoa')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (6, N'Công nghệ sinh học')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (7, N'Kỹ thuật xét nghiệm y học')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (8, N'Ngữ văn')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (9, N'Lịch sử')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (10, N'Địa lý')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (11, N'Báo chí')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (12, N'Luật')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (13, N'Ngôn ngữ Anh')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (14, N'Quản trị kinh doanh')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (15, N'Kinh tế quốc tế')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (16, N'Quản trị kinh doanh')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (17, N' Tài chính - Ngân hàng')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (18, N'Ngôn ngữ Nhật')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (19, N'Ngôn ngữ Trung Quốc')
INSERT [dbo].[Major] ([MajorId], [MajorName]) VALUES (20, N'Dược học')
SET IDENTITY_INSERT [dbo].[Major] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (1, N'Admin                    ', 1)
INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (2, N'Student                  ', 1)
INSERT [dbo].[Role] ([RoleId], [RoleName], [IsActive]) VALUES (3, N'Student                  ', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[UniMajor] ON 

INSERT [dbo].[UniMajor] ([UniMajorId], [UniId], [MajorId]) VALUES (1, 1, 1)
INSERT [dbo].[UniMajor] ([UniMajorId], [UniId], [MajorId]) VALUES (2, 1, 2)
INSERT [dbo].[UniMajor] ([UniMajorId], [UniId], [MajorId]) VALUES (3, 1, 3)
INSERT [dbo].[UniMajor] ([UniMajorId], [UniId], [MajorId]) VALUES (6, 3, 2)
INSERT [dbo].[UniMajor] ([UniMajorId], [UniId], [MajorId]) VALUES (7, 3, 4)
SET IDENTITY_INSERT [dbo].[UniMajor] OFF
GO
SET IDENTITY_INSERT [dbo].[University] ON 

INSERT [dbo].[University] ([UniId], [UniName], [UniCode], [Location], [AreaId], [Email], [Phone], [Website]) VALUES (1, N'ĐẠI HỌC QUỐC GIA HÀ NỘI', N'VNU-HN', N'Hà Nội', 24, N'media@vnu.edu.vn                                                                                                                                                                                                                                               ', N'(024) 6673 6701', N'www.vnu.edu.vn                                                                                                                                                                                                                                                 ')
INSERT [dbo].[University] ([UniId], [UniName], [UniCode], [Location], [AreaId], [Email], [Phone], [Website]) VALUES (3, N'Đại học Bách khoa Hà Nội', N'HUST', N'Hà Nội', 24, N'vp@hust.edu.vn                                                                                                                                                                                                                                                 ', N'(024) 3869 4242', N'http://www.hust.edu.vn/                                                                                                                                                                                                                                        ')
SET IDENTITY_INSERT [dbo].[University] OFF
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
