USE [master]
GO
/****** Object:  Database [DernekDB]    Script Date: 12/16/2021 1:01:14 AM ******/
CREATE DATABASE [DernekDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DernekDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DernekDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DernekDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DernekDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DernekDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DernekDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DernekDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DernekDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DernekDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DernekDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DernekDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DernekDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DernekDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DernekDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DernekDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DernekDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DernekDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DernekDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DernekDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DernekDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DernekDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DernekDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DernekDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DernekDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DernekDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DernekDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DernekDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DernekDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DernekDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DernekDB] SET  MULTI_USER 
GO
ALTER DATABASE [DernekDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DernekDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DernekDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DernekDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DernekDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DernekDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DernekDB] SET QUERY_STORE = OFF
GO
USE [DernekDB]
GO
/****** Object:  Table [dbo].[Bilgilendirmeler]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bilgilendirmeler](
	[BilgilendirmeId] [int] IDENTITY(1,1) NOT NULL,
	[BilgilendirmeMetni] [nvarchar](max) NULL,
	[BaslangicTarih] [datetime] NULL,
	[BitisTarih] [datetime] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Bilgilendirmeler] PRIMARY KEY CLUSTERED 
(
	[BilgilendirmeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Borclar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borclar](
	[BorcId] [int] IDENTITY(1,1) NOT NULL,
	[KisiId] [int] NULL,
	[BorcTarih] [datetime] NULL,
	[DonemAyKod] [int] NULL,
	[DonemYilKod] [int] NULL,
	[BorcTurKod] [int] NULL,
	[BorcTutar] [numeric](18, 2) NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Borclar] PRIMARY KEY CLUSTERED 
(
	[BorcId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izinler]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izinler](
	[IzinId] [int] IDENTITY(1,1) NOT NULL,
	[KisiId] [int] NULL,
	[BaslangicTarih] [datetime] NULL,
	[BitisTarih] [datetime] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Izinler] PRIMARY KEY CLUSTERED 
(
	[IzinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KasaBankalar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KasaBankalar](
	[KasaBankaId] [int] IDENTITY(1,1) NOT NULL,
	[KasaBankaAdi] [varchar](50) NULL,
	[KasaBankaTurKod] [int] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_KasaBankalar] PRIMARY KEY CLUSTERED 
(
	[KasaBankaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kisiler]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kisiler](
	[KisiId] [int] IDENTITY(1,1) NOT NULL,
	[KisiAdi] [varchar](50) NULL,
	[KisiSoyadi] [varchar](50) NULL,
	[TCNo] [varchar](11) NULL,
	[KisiTipiKod] [varchar](50) NULL,
	[KullaniciId] [int] NULL,
	[UyeNo] [int] NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
	[KisiFotoAdi] [varchar](50) NULL,
	[Egitmenmi] [bit] NULL,
 CONSTRAINT [PK_Kisiler] PRIMARY KEY CLUSTERED 
(
	[KisiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kodlar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kodlar](
	[KodId] [int] IDENTITY(1,1) NOT NULL,
	[KodAdi] [varchar](50) NULL,
	[KodDeger] [varchar](50) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
	[KodGrup] [varchar](50) NULL,
 CONSTRAINT [PK_Kodlar] PRIMARY KEY CLUSTERED 
(
	[KodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanicilar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanicilar](
	[KullaniciId] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdi] [varchar](50) NULL,
	[KullaniciTipiKod] [int] NULL,
	[AktifPasifKod] [int] NULL,
	[Parola] [nvarchar](50) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Kullanicilar] PRIMARY KEY CLUSTERED 
(
	[KullaniciId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KursKayitUyeler]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KursKayitUyeler](
	[KursKayitUyeId] [int] IDENTITY(1,1) NOT NULL,
	[KursId] [int] NULL,
	[UyeId] [int] NULL,
	[BaslangicTarih] [datetime] NULL,
	[BitisTarih] [datetime] NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_KursKayitUyeler] PRIMARY KEY CLUSTERED 
(
	[KursKayitUyeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kurslar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kurslar](
	[KursId] [int] IDENTITY(1,1) NOT NULL,
	[KursAdi] [varchar](50) NULL,
	[KursTurKod] [int] NULL,
	[BaslangicTarih] [datetime] NULL,
	[BitisTarih] [datetime] NULL,
	[Kapasite] [int] NULL,
	[KursUcret] [numeric](18, 2) NULL,
	[EgitmenId] [int] NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Kurslar] PRIMARY KEY CLUSTERED 
(
	[KursId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Odemeler]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Odemeler](
	[OdemeId] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciId] [int] NULL,
	[KisiId] [int] NULL,
	[OdemeTutar] [numeric](18, 2) NULL,
	[OdemeTarih] [datetime] NULL,
	[OdemeTurKod] [int] NULL,
	[DonemAyKod] [int] NULL,
	[DonemYilKod] [int] NULL,
	[KasaBankaId] [int] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Odemeler] PRIMARY KEY CLUSTERED 
(
	[OdemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sorular]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sorular](
	[SoruId] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciId] [int] NULL,
	[SoruTarih] [datetime] NULL,
	[CevapTarih] [datetime] NULL,
	[SoruMetni] [nvarchar](max) NULL,
	[CevapMetni] [nvarchar](max) NULL,
	[CevaplayanId] [int] NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Sorular] PRIMARY KEY CLUSTERED 
(
	[SoruId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tahsilatlar]    Script Date: 12/16/2021 1:01:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tahsilatlar](
	[TahsilatId] [int] IDENTITY(1,1) NOT NULL,
	[KisiId] [int] NULL,
	[TahsilatTarih] [datetime] NULL,
	[DonemAyKod] [int] NULL,
	[DonemYilKod] [int] NULL,
	[TahsilatTurKod] [int] NULL,
	[TahsilatTutar] [numeric](18, 2) NULL,
	[KasaBankaId] [int] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [datetime] NULL,
	[KaydedenId] [int] NULL,
	[GunlemeTarih] [datetime] NULL,
	[GunleyenId] [int] NULL,
 CONSTRAINT [PK_Tahsilatlar] PRIMARY KEY CLUSTERED 
(
	[TahsilatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bilgilendirmeler] ADD  CONSTRAINT [DF_Bilgilendirmeler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Bilgilendirmeler] ADD  CONSTRAINT [DF_Bilgilendirmeler_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Borclar] ADD  CONSTRAINT [DF_Borclar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Borclar] ADD  CONSTRAINT [DF_Borclar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Izinler] ADD  CONSTRAINT [DF_Izinler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Izinler] ADD  CONSTRAINT [DF_Izinler_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[KasaBankalar] ADD  CONSTRAINT [DF_KasaBankalar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[KasaBankalar] ADD  CONSTRAINT [DF_KasaBankalar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Kisiler] ADD  CONSTRAINT [DF_Kisiler_KisiTipiKodId]  DEFAULT ((1)) FOR [KisiTipiKod]
GO
ALTER TABLE [dbo].[Kisiler] ADD  CONSTRAINT [DF_Kisiler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Kisiler] ADD  CONSTRAINT [DF_Kisiler_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Kisiler] ADD  CONSTRAINT [DF_Kisiler_Egitmen]  DEFAULT ((0)) FOR [Egitmenmi]
GO
ALTER TABLE [dbo].[Kodlar] ADD  CONSTRAINT [DF_Kodlar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Kodlar] ADD  CONSTRAINT [DF_Kodlar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Kullanicilar] ADD  CONSTRAINT [DF_Kullanicilar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Kullanicilar] ADD  CONSTRAINT [DF_Kullanicilar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[KursKayitUyeler] ADD  CONSTRAINT [DF_KursKayitUyeler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[KursKayitUyeler] ADD  CONSTRAINT [DF_KursKayitUyeler_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Kurslar] ADD  CONSTRAINT [DF_Kurslar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Kurslar] ADD  CONSTRAINT [DF_Kurslar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Odemeler] ADD  CONSTRAINT [DF_Odemeler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Odemeler] ADD  CONSTRAINT [DF_Odemeler_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Sorular] ADD  CONSTRAINT [DF_Sorular_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Sorular] ADD  CONSTRAINT [DF_Sorular_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Tahsilatlar] ADD  CONSTRAINT [DF_Tahsilatlar_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[Tahsilatlar] ADD  CONSTRAINT [DF_Tahsilatlar_GunlemeTarih]  DEFAULT (getdate()) FOR [GunlemeTarih]
GO
ALTER TABLE [dbo].[Borclar]  WITH CHECK ADD  CONSTRAINT [FK_Borclar_Kisiler] FOREIGN KEY([KisiId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[Borclar] CHECK CONSTRAINT [FK_Borclar_Kisiler]
GO
ALTER TABLE [dbo].[Izinler]  WITH CHECK ADD  CONSTRAINT [FK_Izinler_Kisiler] FOREIGN KEY([KisiId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[Izinler] CHECK CONSTRAINT [FK_Izinler_Kisiler]
GO
ALTER TABLE [dbo].[KursKayitUyeler]  WITH CHECK ADD  CONSTRAINT [FK_KursKayitUyeler_Kisiler] FOREIGN KEY([UyeId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[KursKayitUyeler] CHECK CONSTRAINT [FK_KursKayitUyeler_Kisiler]
GO
ALTER TABLE [dbo].[KursKayitUyeler]  WITH CHECK ADD  CONSTRAINT [FK_KursKayitUyeler_Kurslar] FOREIGN KEY([KursId])
REFERENCES [dbo].[Kurslar] ([KursId])
GO
ALTER TABLE [dbo].[KursKayitUyeler] CHECK CONSTRAINT [FK_KursKayitUyeler_Kurslar]
GO
ALTER TABLE [dbo].[Kurslar]  WITH CHECK ADD  CONSTRAINT [FK_Kurslar_Kisiler] FOREIGN KEY([EgitmenId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[Kurslar] CHECK CONSTRAINT [FK_Kurslar_Kisiler]
GO
ALTER TABLE [dbo].[Odemeler]  WITH CHECK ADD  CONSTRAINT [FK_Odemeler_KasaBankalar] FOREIGN KEY([KasaBankaId])
REFERENCES [dbo].[KasaBankalar] ([KasaBankaId])
GO
ALTER TABLE [dbo].[Odemeler] CHECK CONSTRAINT [FK_Odemeler_KasaBankalar]
GO
ALTER TABLE [dbo].[Odemeler]  WITH CHECK ADD  CONSTRAINT [FK_Odemeler_Kisiler] FOREIGN KEY([KisiId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[Odemeler] CHECK CONSTRAINT [FK_Odemeler_Kisiler]
GO
ALTER TABLE [dbo].[Odemeler]  WITH CHECK ADD  CONSTRAINT [FK_Odemeler_Kullanicilar] FOREIGN KEY([KullaniciId])
REFERENCES [dbo].[Kullanicilar] ([KullaniciId])
GO
ALTER TABLE [dbo].[Odemeler] CHECK CONSTRAINT [FK_Odemeler_Kullanicilar]
GO
ALTER TABLE [dbo].[Sorular]  WITH CHECK ADD  CONSTRAINT [FK_Sorular_Kullanicilar] FOREIGN KEY([KullaniciId])
REFERENCES [dbo].[Kullanicilar] ([KullaniciId])
GO
ALTER TABLE [dbo].[Sorular] CHECK CONSTRAINT [FK_Sorular_Kullanicilar]
GO
ALTER TABLE [dbo].[Sorular]  WITH CHECK ADD  CONSTRAINT [FK_Sorular_Kullanicilar1] FOREIGN KEY([CevaplayanId])
REFERENCES [dbo].[Kullanicilar] ([KullaniciId])
GO
ALTER TABLE [dbo].[Sorular] CHECK CONSTRAINT [FK_Sorular_Kullanicilar1]
GO
ALTER TABLE [dbo].[Tahsilatlar]  WITH CHECK ADD  CONSTRAINT [FK_Tahsilatlar_KasaBankalar] FOREIGN KEY([KasaBankaId])
REFERENCES [dbo].[KasaBankalar] ([KasaBankaId])
GO
ALTER TABLE [dbo].[Tahsilatlar] CHECK CONSTRAINT [FK_Tahsilatlar_KasaBankalar]
GO
ALTER TABLE [dbo].[Tahsilatlar]  WITH CHECK ADD  CONSTRAINT [FK_Tahsilatlar_Kisiler] FOREIGN KEY([KisiId])
REFERENCES [dbo].[Kisiler] ([KisiId])
GO
ALTER TABLE [dbo].[Tahsilatlar] CHECK CONSTRAINT [FK_Tahsilatlar_Kisiler]
GO
USE [master]
GO
ALTER DATABASE [DernekDB] SET  READ_WRITE 
GO
INSERT INTO [DernekDB].[dbo].[Kullanicilar]
           ([KullaniciAdi]
           ,[KullaniciTipiKod]
           ,[AktifPasifKod]
           ,[Parola])
     VALUES
           ('admin',3,1,1234)
GO
