USE [master]
GO
/****** Object:  Database [SaludOcupacional]    Script Date: 02/02/2021 1:43:04 ******/
CREATE DATABASE [SaludOcupacional]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SaludOcupacional', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\SaludOcupacional.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SaludOcupacional_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\SaludOcupacional_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SaludOcupacional] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SaludOcupacional].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SaludOcupacional] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SaludOcupacional] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SaludOcupacional] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SaludOcupacional] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SaludOcupacional] SET ARITHABORT OFF 
GO
ALTER DATABASE [SaludOcupacional] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SaludOcupacional] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SaludOcupacional] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SaludOcupacional] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SaludOcupacional] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SaludOcupacional] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SaludOcupacional] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SaludOcupacional] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SaludOcupacional] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SaludOcupacional] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SaludOcupacional] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SaludOcupacional] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SaludOcupacional] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SaludOcupacional] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SaludOcupacional] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SaludOcupacional] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SaludOcupacional] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SaludOcupacional] SET RECOVERY FULL 
GO
ALTER DATABASE [SaludOcupacional] SET  MULTI_USER 
GO
ALTER DATABASE [SaludOcupacional] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SaludOcupacional] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SaludOcupacional] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SaludOcupacional] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SaludOcupacional] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SaludOcupacional', N'ON'
GO
ALTER DATABASE [SaludOcupacional] SET QUERY_STORE = OFF
GO
USE [SaludOcupacional]
GO
/****** Object:  Table [dbo].[Archivo]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivo](
	[idArchivo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[filePath] [nvarchar](max) NOT NULL,
	[idNoticia] [int] NOT NULL,
	[tipo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Archivo] PRIMARY KEY CLUSTERED 
(
	[idArchivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CentroDeTrabajo]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentroDeTrabajo](
	[idCentroDeTrabajo] [int] IDENTITY(1,1) NOT NULL,
	[nombreCentroDeTrabajo] [varchar](100) NOT NULL,
	[idRegion] [int] NOT NULL,
 CONSTRAINT [PK_CentroDeTrabajo] PRIMARY KEY CLUSTERED 
(
	[idCentroDeTrabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comision]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comision](
	[idComision] [int] IDENTITY(1,1) NOT NULL,
	[contacto] [varchar](50) NULL,
	[contactoCorreo] [varchar](50) NULL,
	[contactoTelefono] [varchar](50) NULL,
	[jefatura] [varchar](50) NULL,
	[jefaturaCorreo] [varchar](50) NULL,
	[jefaturaTelefono] [varchar](25) NULL,
	[numeroDeRegistro] [varchar](25) NULL,
	[fechaDeRegistro] [date] NULL,
	[idCentroDeTrabajo] [int] NOT NULL,
	[idCuenta] [int] NOT NULL,
	[ultimoInforme] [date] NULL,
 CONSTRAINT [PK_Comision] PRIMARY KEY CLUSTERED 
(
	[idComision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[idCuenta] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[contrasena] [varchar](50) NOT NULL,
	[rol] [int] NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[idCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Noticia]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Noticia](
	[idNoticia] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](100) NOT NULL,
	[texto] [varchar](max) NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_Noticia] PRIMARY KEY CLUSTERED 
(
	[idNoticia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[idRegion] [int] IDENTITY(1,1) NOT NULL,
	[numeroRegion] [int] NOT NULL,
	[nombreRegion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[idRegion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Representante]    Script Date: 02/02/2021 1:43:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Representante](
	[idRepresentante] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[correo] [varchar](50) NOT NULL,
	[telefono] [varchar](25) NOT NULL,
	[tipo] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[idComision] [int] NULL,
 CONSTRAINT [PK_Representante] PRIMARY KEY CLUSTERED 
(
	[idRepresentante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CentroDeTrabajo] ON 

INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (1, N'Colorado', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2, N'Pococi', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (3, N'Upala - Cantonal', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (4, N'Upala - Distrital', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (5, N'Cariari', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (6, N'Ticaban', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (7, N'Cuatro Esquinas', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (8, N'La Rita', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (9, N'Roxana', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (10, N'Jiménez', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (11, N'Siquirres - Cantonal', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (12, N'Cairo', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (13, N'Alegrida', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (14, N'Florida', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (15, N'Guácimo', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (16, N'Pocora', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (17, N'Rio Jiménez', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (18, N'Duacarí', 1)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (19, N'Bijagua', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (20, N'Yolillal', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (21, N'Mexico - Upala', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (22, N'San José de Upala', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (23, N'Aguas Claras', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (24, N'La Cruz - Cantonal', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (25, N'Santa Cecilia', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (26, N'La Garita', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (27, N'Santa Elena', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (28, N'Peñas Blancas', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (29, N'Los Chiles - Cantonal', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (30, N'Cutris', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (31, N'Coopevega', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (32, N'El Amparo', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (33, N'Caño Negro', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (34, N'Pocosol', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (35, N'Brasilia Dos Ríos', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2007, N'Metropolitana', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2008, N'El Carmen', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2009, N'Merced', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2010, N'Merced - Barrio México', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2011, N'Hospital', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2012, N'Hospital - Barrio Cuba', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2013, N'Catedral', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2014, N'San Sebastián', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2015, N'Hatillo', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2016, N'San Francisco Zapote', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2017, N'Zapote', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2018, N'San Francisco', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2019, N'Escazú', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2020, N'Desamparados Norte', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2021, N'San Juan de Dios', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2022, N'San Rafael Abajo - Arriba', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2023, N'Damas / San Antonio / Patarrá', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2024, N'Desamparados Sur', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2025, N'San Miguel', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2026, N'Frailes / San Cristóbal / El Rosario', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2027, N'Los Guidos', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2028, N'Alajuelita - Cantonal', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2029, N'Alajuelita - Distrital', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2030, N'San Felipe', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2031, N'Concepción', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2032, N'Aserrí', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2033, N'San Gabriel', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2034, N'Vuelta de Jorco', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2035, N'Santa Ana', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2036, N'Mora - Colón', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2037, N'Guayabo', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2038, N'Acosta', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2039, N'Palmichal', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2040, N'Puriscal - Santiago', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2041, N'La Gloria', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2042, N'Turrubares - San Juan de Mata/ San Pedro/ San Luis', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2043, N'Carara', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2044, N'San Pablo', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2045, N'Montes de Oca - San Pedro', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2046, N'Mercedes/ Sabanilla', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2047, N'Curridabat', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2048, N'Goicoechea', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2049, N'Calle Blancos/ San Francisco', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2050, N'Guadalupe', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2051, N'Ipis', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2052, N'Mata de Platano', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2053, N'Purral', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2054, N'Pavas', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2055, N'Tibás', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2056, N'San Juan', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2057, N'La Florida', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2058, N'5 Esquinas', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2059, N'León XIII', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2060, N'Moravia', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2061, N'San Gerónimo', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2062, N'Vázquez de Coronado - San Isidro', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2063, N'Uruca', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2064, N'Mata Redonda', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2065, N'La Carpio', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2066, N'La Peregrina', 12)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2067, N'Alajuela Sur', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2068, N'Ciruelas', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2069, N'Guácima', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2070, N'San Rafael', 11)
GO
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2071, N'Turrucares/ La Garita', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2072, N'La Garita', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2073, N'Alajuela Norte', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2074, N'Alajuela', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2075, N'San José', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2076, N'Canoas', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2077, N'Carrizal', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2078, N'San Isidro', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2079, N'Sabanilla', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2080, N'Río Segundo', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2081, N'Desamparados ', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2082, N'San Ramón', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2083, N'Santiago', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2084, N'San Juan Volio', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2085, N'Piedades Sur/ Piedades Norte', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2086, N'Grecia', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2087, N'San Mateo', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2088, N'Atenas', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2089, N'Naranjo', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2090, N'Palmares', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2091, N'Poás', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2092, N'Orotina', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2093, N'Valverde Vega', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2094, N'Toto Amarillo', 11)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2095, N'Cartago', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2096, N'San Nicolás', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2097, N'San Francisco', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2098, N'Dulce Nombre', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2099, N'Llano Grande', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2100, N'Paraíso', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2101, N'Llanos de Santa Lucía', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2102, N'Orosi', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2103, N'Cachí', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2104, N'La Unión - Tres Ríos', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2105, N'Concepción', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2106, N'San Diego', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2107, N'Río Azul', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2108, N'Jiménez - Juan Viñas', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2109, N'Tucurrique', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2110, N'Pejibaye', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2111, N'Turrialba - Turrialba', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2112, N'Tres Equis', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2113, N'Pavones', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2114, N'La Suiza', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2115, N'La Isabel', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2116, N'Santa Teresita', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2117, N'Alvarado - Pacayas', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2118, N'Cervantes', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2119, N'Oreamuno - San Rafael', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2120, N'Cot', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2121, N'Tejar del Guarco', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2122, N'Tejar', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2123, N'San Marcos', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2124, N'Santa María / Dota', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2125, N'León Cortés - San Pablo', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2126, N'Guadalupe', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2127, N'San Juan Sur', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2128, N'Quebradilla', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2129, N'Corralillo', 10)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2130, N'Heredia - Cantonal', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2131, N'Heredia - Distrital', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2132, N'Mercedes', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2133, N'San Francisco', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2134, N'Ulloa', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2135, N'Vara Blanca', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2136, N'Barva', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2137, N'San Pedro', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2138, N'Santa Lucía', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2139, N'Santo Domingo', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2140, N'San Miguel', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2141, N'San Luis', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2142, N'Santa Bárbara', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2143, N'San Pedro D-52', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2144, N'El Roble', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2145, N'San Rafael', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2146, N'Belén - San Antonio', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2147, N'La Rivera', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2148, N'Flores - San Joaquín', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2149, N'San Pablo', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2150, N'Puerto Viejo - Cantonal', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2151, N'Puerto Viejo - Distrital', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2152, N'Horquetas', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2153, N'La Virgen', 9)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2154, N'Liberia', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2155, N'Curumbe', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2156, N'Cañas Dulces', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2157, N'Quebrada Grande', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2158, N'Guardia', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2159, N'Nicoya', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2160, N'Nicoya - San Martín', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2161, N'Mansión', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2162, N'Quebrada Honda', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2163, N'Sámara', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2164, N'Nosara', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2165, N'Belén - Caseta', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2166, N'Santa Cruz', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2167, N'27 de Abril', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2168, N'Tamarindo', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2169, N'Cartagena', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2170, N'Bagaces - Cantonal', 8)
GO
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2171, N'Bagaces - Distrital', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2172, N'Fortuna', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2173, N'Magote', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2174, N'Carrillo', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2175, N'Belén', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2176, N'Sardinal', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2177, N'Sardinal - Playas del Coco', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2178, N'Cañas', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2179, N'Abangares', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2180, N'Abangares - Las Juntas', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2181, N'Tilarán - Cantonal', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2182, N'Tilarán - Distrital', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2183, N'Nandayure', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2184, N'Bejuco', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2185, N'Hojancha', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2186, N'Puerto Carillo', 8)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2187, N'Puntarenas - Cantonal', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2188, N'Puntarenas - Distrital', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2189, N'Chacarita', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2190, N'Fray Casiano', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2191, N'El Roble', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2192, N'Barranca', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2193, N'Esparza', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2194, N'Espíritu Santo', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2195, N'San Rafael', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2196, N'Montes de Oro', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2197, N'Miramar', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2198, N'San Isidro', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2199, N'Quepos - Cantonal', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2200, N'Quepos - Distrital', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2201, N'Savegre', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2202, N'Parrita', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2203, N'Garabito', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2204, N'Jacó', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2205, N'Tárcoles', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2206, N'Paquera - Cantonal', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2207, N'Paquera - Distrital', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2208, N'Cóbano', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2209, N'Lepanto', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2210, N'Judas de Chomes', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2211, N'Manzanillo', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2212, N'Isla Chira', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2213, N'Monteverde', 7)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2214, N'Pérez Zeledón', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2215, N'San Isidro General', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2216, N'General Viejo', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2217, N'Daniel Flores', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2218, N'San Pedro', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2219, N'Platanares', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2220, N'Buenos Aires - Cantonal', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2221, N'Buenos Aires - Distrital', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2222, N'Potrero Grande', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2223, N'Volcán', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2224, N'Boruca', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2225, N'Osa', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2226, N'Peñas Blancas', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2227, N'Sierpe', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2228, N'Ciudad Cortés', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2229, N'Bahía Ballena', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2230, N'Palmar Norte', 6)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2231, N'San Carlos Este - Quesada', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2232, N'Florencia', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2233, N'Aguas Zarcas', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2234, N'Venecia', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2235, N'Pital', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2236, N'Palmeta', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2237, N'San Carlos Oeste - Fortuna', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2238, N'Tigra', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2239, N'Peñas Blancas', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2240, N'Monterrey', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2241, N'Venado', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2242, N'Zarcero', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2243, N'Guatuso', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2244, N'Katira', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2245, N'San Rafael', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2246, N'Río Cuarto', 5)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2247, N'Limón', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2248, N'Cieneguita', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2249, N'Pacuare', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2250, N'Río Blanco', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2251, N'Valle la Estrella', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2252, N'Matama', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2253, N'Talamanca', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2254, N'Shiroles', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2255, N'Sixaola', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2256, N'Puerto Viejo', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2257, N'Cahuita', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2258, N'Matina', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2259, N'Bataan', 4)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2260, N'Golfito - Cantonal', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2261, N'Guaycara', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2262, N'La Gamba', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2263, N'Coto Brus - San Vito', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2264, N'Sabalito', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2265, N'La Lucha', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2266, N'Cañas Gordas', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2267, N'Agua Buena', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2268, N'Limoncito - Sabanillas', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2269, N'Santa Elena de Pitter', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2270, N'San Marcos', 3)
GO
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2271, N'Corredores', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2272, N'El Carmen', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2273, N'San Jorge', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2274, N'La Cuesta', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2275, N'Laurel', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2276, N'La Esperanza', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2277, N'Naranjo', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2278, N'Pavones', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2279, N'Zancudo', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2280, N'Puerto Jiménez - Cantonal', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2281, N'Puerto Jiménez - Caseta', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2282, N'Golfito - Distrital', 3)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2283, N'La Cruz - Distrital', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2284, N'Los Chiles - Distrital', 2)
INSERT [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo], [nombreCentroDeTrabajo], [idRegion]) VALUES (2285, N'Siquirres - Distrital', 1)
SET IDENTITY_INSERT [dbo].[CentroDeTrabajo] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([idCuenta], [nombre], [contrasena], [rol]) VALUES (3, N'admin', N'gUKEXskYC8o=', 0)
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Region] ON 

INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (1, 12, N'12-Región Caribe')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (2, 11, N'11-Región Chorotega Norte')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (3, 10, N'10-Región Brunca Sur')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (4, 9, N'9-Región Huetar Atlántica')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (5, 8, N'8-Región Huetar Norte')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (6, 7, N'7-Región Brunca')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (7, 6, N'6-Región Pacífico Central')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (8, 5, N'5-Región Chorotega')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (9, 4, N'4-Región Heredia')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (10, 3, N'3-Región Cartago')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (11, 2, N'2-Región Alajuela')
INSERT [dbo].[Region] ([idRegion], [numeroRegion], [nombreRegion]) VALUES (12, 1, N'1-Región San José')
SET IDENTITY_INSERT [dbo].[Region] OFF
GO
ALTER TABLE [dbo].[Archivo]  WITH CHECK ADD FOREIGN KEY([idNoticia])
REFERENCES [dbo].[Noticia] ([idNoticia])
GO
ALTER TABLE [dbo].[CentroDeTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_CentroDeTrabajo_Region] FOREIGN KEY([idRegion])
REFERENCES [dbo].[Region] ([idRegion])
GO
ALTER TABLE [dbo].[CentroDeTrabajo] CHECK CONSTRAINT [FK_CentroDeTrabajo_Region]
GO
ALTER TABLE [dbo].[Comision]  WITH CHECK ADD  CONSTRAINT [FK_Comision_CentroDeTrabajo] FOREIGN KEY([idCentroDeTrabajo])
REFERENCES [dbo].[CentroDeTrabajo] ([idCentroDeTrabajo])
GO
ALTER TABLE [dbo].[Comision] CHECK CONSTRAINT [FK_Comision_CentroDeTrabajo]
GO
ALTER TABLE [dbo].[Comision]  WITH CHECK ADD  CONSTRAINT [FK_Comision_Cuenta] FOREIGN KEY([idCuenta])
REFERENCES [dbo].[Cuenta] ([idCuenta])
GO
ALTER TABLE [dbo].[Comision] CHECK CONSTRAINT [FK_Comision_Cuenta]
GO
ALTER TABLE [dbo].[Representante]  WITH CHECK ADD  CONSTRAINT [FK_Representante_Comision] FOREIGN KEY([idComision])
REFERENCES [dbo].[Comision] ([idComision])
GO
ALTER TABLE [dbo].[Representante] CHECK CONSTRAINT [FK_Representante_Comision]
GO
USE [master]
GO
ALTER DATABASE [SaludOcupacional] SET  READ_WRITE 
GO
