USE [master]
GO
/****** Object:  Database [Administracion]    Script Date: 20/6/2019 11:21:43 ******/
CREATE DATABASE [Administracion]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Administracion', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Administracion.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Administracion_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Administracion_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Administracion] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Administracion].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Administracion] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Administracion] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Administracion] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Administracion] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Administracion] SET ARITHABORT OFF 
GO
ALTER DATABASE [Administracion] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Administracion] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Administracion] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Administracion] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Administracion] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Administracion] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Administracion] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Administracion] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Administracion] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Administracion] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Administracion] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Administracion] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Administracion] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Administracion] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Administracion] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Administracion] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Administracion] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Administracion] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Administracion] SET  MULTI_USER 
GO
ALTER DATABASE [Administracion] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Administracion] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Administracion] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Administracion] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Administracion] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Administracion] SET QUERY_STORE = OFF
GO
USE [Administracion]
GO
/****** Object:  Table [dbo].[Casas]    Script Date: 20/6/2019 11:21:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Casas](
	[IdCasa] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCasa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesas]    Script Date: 20/6/2019 11:21:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[IdMesa] [int] IDENTITY(1,1) NOT NULL,
	[IdRestaurant] [int] NOT NULL,
	[IdPersona] [int] NULL,
	[Numero] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 20/6/2019 11:21:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Apellido] [varchar](20) NOT NULL,
	[Documento] [int] NOT NULL,
	[Tipo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 20/6/2019 11:21:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurants](
	[IdRestaurant] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[IdPersona] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantsPersonas]    Script Date: 20/6/2019 11:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantsPersonas](
	[IdRestaurant] [int] NULL,
	[IdPersona] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Casas] ON 

INSERT [dbo].[Casas] ([IdCasa], [IdPersona], [Direccion], [Descripcion]) VALUES (37, 87, N'Cunnington 546', N'Casa totalmente amueblada. Presenta 2 habitaciones, sala de estar, cocina y baño.')
INSERT [dbo].[Casas] ([IdCasa], [IdPersona], [Direccion], [Descripcion]) VALUES (38, 85, N'Av. Cárcano 2550', N'Zona periférica y de mucho tránsito. Mono Ambiente con patio interno.')
INSERT [dbo].[Casas] ([IdCasa], [IdPersona], [Direccion], [Descripcion]) VALUES (39, 88, N'Ayacucho 346', N'Departamento con cochera')
INSERT [dbo].[Casas] ([IdCasa], [IdPersona], [Direccion], [Descripcion]) VALUES (41, 88, N'Mariano Moreno 405', N'Excelente ubicación y precio.')
SET IDENTITY_INSERT [dbo].[Casas] OFF
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([IdPersona], [Nombre], [Apellido], [Documento], [Tipo]) VALUES (85, N'Pedro', N'Rosenberg', 30998765, 2)
INSERT [dbo].[Personas] ([IdPersona], [Nombre], [Apellido], [Documento], [Tipo]) VALUES (87, N'Marcelo', N'Tagliafico', 22566777, 2)
INSERT [dbo].[Personas] ([IdPersona], [Nombre], [Apellido], [Documento], [Tipo]) VALUES (88, N'Juan', N'Arams', 23997987, 1)
INSERT [dbo].[Personas] ([IdPersona], [Nombre], [Apellido], [Documento], [Tipo]) VALUES (89, N'Rodolfo', N'Nigger', 99665688, 0)
SET IDENTITY_INSERT [dbo].[Personas] OFF
SET IDENTITY_INSERT [dbo].[Restaurants] ON 

INSERT [dbo].[Restaurants] ([IdRestaurant], [Nombre], [Direccion], [IdPersona]) VALUES (5, N'Macedonia', N'Saenz Peña 432', NULL)
INSERT [dbo].[Restaurants] ([IdRestaurant], [Nombre], [Direccion], [IdPersona]) VALUES (22, N'Il Gatto', N'Nuevocentro Shopping', 87)
INSERT [dbo].[Restaurants] ([IdRestaurant], [Nombre], [Direccion], [IdPersona]) VALUES (23, N'Macedonio', N'Saenz Peña 432', 85)
INSERT [dbo].[Restaurants] ([IdRestaurant], [Nombre], [Direccion], [IdPersona]) VALUES (26, N'Anastasia', N'Rosenberg 34', 85)
SET IDENTITY_INSERT [dbo].[Restaurants] OFF
INSERT [dbo].[RestaurantsPersonas] ([IdRestaurant], [IdPersona]) VALUES (23, 88)
ALTER TABLE [dbo].[Casas]  WITH CHECK ADD  CONSTRAINT [FK__Casas__IdPersona__6EF57B66] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Casas] CHECK CONSTRAINT [FK__Casas__IdPersona__6EF57B66]
GO
ALTER TABLE [dbo].[Mesas]  WITH CHECK ADD  CONSTRAINT [FK__Mesas__IdPersona__44FF419A] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Mesas] CHECK CONSTRAINT [FK__Mesas__IdPersona__44FF419A]
GO
ALTER TABLE [dbo].[Mesas]  WITH CHECK ADD  CONSTRAINT [FK_Mesas_IdRestaurant] FOREIGN KEY([IdMesa])
REFERENCES [dbo].[Mesas] ([IdMesa])
GO
ALTER TABLE [dbo].[Mesas] CHECK CONSTRAINT [FK_Mesas_IdRestaurant]
GO
ALTER TABLE [dbo].[Restaurants]  WITH CHECK ADD  CONSTRAINT [FK__Restauran__IdPer__75A278F5] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Restaurants] CHECK CONSTRAINT [FK__Restauran__IdPer__75A278F5]
GO
ALTER TABLE [dbo].[RestaurantsPersonas]  WITH CHECK ADD  CONSTRAINT [FK__Restauran__IdPer__47DBAE45] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RestaurantsPersonas] CHECK CONSTRAINT [FK__Restauran__IdPer__47DBAE45]
GO
/****** Object:  StoredProcedure [dbo].[Casa_Agregar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Casa_Agregar]
		@IdPersona int,
		@Direccion varchar(50),
		@Descripcion varchar(200),
		@IdCasa int output
	as
	begin
		Insert Into Casas(
			IdPersona,
			Direccion,
			Descripcion
		)
	
		Values(
			@IdPersona, 
			@Direccion,
			@Descripcion
		)
		Select @IdCasa = SCOPE_IDENTITY()
	end
GO
/****** Object:  StoredProcedure [dbo].[Casa_Eliminar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Casa_Eliminar]
	@IdCasa int
	as
	begin
		Delete 
		From Casas
		Where IdCasa = @IdCasa
	end
GO
/****** Object:  StoredProcedure [dbo].[Casa_Modificar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Casa_Modificar]
		@IdCasa int,
		@IdPersona int,
		@Direccion varchar(20),
		@Descripcion varchar(200)
	as
	begin
		Update Casas
		Set 
			IdPersona = @IdPersona,
			Direccion = @Direccion,
			Descripcion = @Descripcion
		Where IdCasa = @IdCasa
	end
GO
/****** Object:  StoredProcedure [dbo].[Casa_ObtenerPorId]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Casa_ObtenerPorId]
		@IdCasa int
	as
	begin
		Select *
		From Casas
		Where IdCasa = @IdCasa
	end
GO
/****** Object:  StoredProcedure [dbo].[Casa_ObtenerTodos]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[Casa_ObtenerTodos]
	as
	begin
		Select *
		From Casas
	end
GO
/****** Object:  StoredProcedure [dbo].[Persona_Agregar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[Persona_Agregar]
		@Nombre varchar(20),
		@Apellido varchar(20), 
		@Documento int, 
		@Tipo int,
		@IdPersona int output
	as
	begin
		Insert Into Personas(
			Nombre, 
			Apellido, 
			Documento, 
			Tipo
		)
		
		Values(
			@Nombre, 
			@Apellido, 
			@Documento, 
			@Tipo
		)
		Select @IdPersona = SCOPE_IDENTITY()
	end
GO
/****** Object:  StoredProcedure [dbo].[Persona_Eliminar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Persona_Eliminar]
	@IdPersona int
	as
	begin
		Delete 
		From Personas
		Where IdPersona = @IdPersona
	end
GO
/****** Object:  StoredProcedure [dbo].[Persona_Modificar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[Persona_Modificar]
		@IdPersona int,
		@Nombre varchar(20),
		@Apellido varchar(20), 
		@Documento int, 
		@Tipo int
	as
	begin
		Update Personas
		Set 
			Nombre = @Nombre, 
			Apellido = @Apellido, 
			Documento = @Documento, 
			Tipo = @Tipo
		Where IdPersona = @IdPersona
	end
GO
/****** Object:  StoredProcedure [dbo].[Persona_ObtenerPorId]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Persona_ObtenerPorId]
@IdPersona int
	as
		begin
			Select *
			From Personas
				Where IdPersona = @IdPersona
		end
GO
/****** Object:  StoredProcedure [dbo].[Persona_ObtenerTodos]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Persona_ObtenerTodos]
	as
		begin
			Select *
			From Personas
		end
GO
/****** Object:  StoredProcedure [dbo].[Persona_VerificarDocumento]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Persona_VerificarDocumento]
	@Documento int,
	@IdPersona int
as
begin
	Select Documento, IdPersona
	From Personas
	Where Documento = @Documento AND IdPersona Not like @IdPersona
end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_Agregar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Restaurant_Agregar]
@Nombre varchar(20),
@Direccion varchar(50),
@IdPersona int,
@IdResto int output
	as
	begin
		Insert into Restaurants (
			Nombre,
			Direccion,
			IdPersona
		)
		Values (
			@Nombre,
			@Direccion,
			@IdPersona
		)
		Select @IdResto = Scope_identity()
	end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_Eliminar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Restaurant_Eliminar]
@IdResto int
	as
	begin
		Delete From Restaurants
		Where IdRestaurant = @IdResto
	end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_Modificar]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Restaurant_Modificar]
@IdResto int,
@Nombre varchar(20),
@Direccion varchar(50),
@IdPersona int
	as
	begin
		Update Restaurants 
		Set 
			Nombre = @Nombre,
			Direccion = @Direccion,
			IdPersona = @IdPersona
		Where IdRestaurant = @IdResto
	end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_ObtenerEmpleados]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Restaurant_ObtenerEmpleados]
@IdResto int
	as
	begin
	Select * 
	From Personas as p
	Where IdPersona IN (Select IdPersona 
			From RestaurantsPersonas
			Where RestaurantsPersonas.IdRestaurant = @IdResto)
	end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_ObtenerPorId]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Restaurant_ObtenerPorId]
@IdResto int
	as
	begin
		Select *
		From Restaurants
		Where IdRestaurant = @IdResto
	end
GO
/****** Object:  StoredProcedure [dbo].[Restaurant_ObtenerTodos]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Restaurant_ObtenerTodos]
	as
	begin
		Select *
		From Restaurants
	end
GO
/****** Object:  StoredProcedure [dbo].[RestaurantsPersonas_Desemplear]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[RestaurantsPersonas_Desemplear]
@IdResto int,
@IdPersona int
	as
	begin
	Delete From RestaurantsPersonas
	Where IdRestaurant = @IdResto AND IdPersona = @IdPersona
	end
GO
/****** Object:  StoredProcedure [dbo].[RestaurantsPersonas_Emplear]    Script Date: 20/6/2019 11:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[RestaurantsPersonas_Emplear]
@IdResto int,
@IdPersona int
	as
	begin
		Insert into RestaurantsPersonas (IdRestaurant, IdPersona)
		Values (@IdResto, @IdPersona)
	end
GO
USE [master]
GO
ALTER DATABASE [Administracion] SET  READ_WRITE 
GO
