USE [LicitProd]
GO
/****** Object:  Table [dbo].[Compradores]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compradores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[Usuario_Id] [int] NULL,
 CONSTRAINT [PK_Compradores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concurso_Proveedor]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concurso_Proveedor](
	[ProveedorId] [int] NULL,
	[ConcursoId] [int] NULL,
	[Status] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Concurso_Proveedor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concursos]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concursos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[Presupuesto] [decimal](18, 0) NULL,
	[Nombre] [nvarchar](50) NULL,
	[FechaInicio] [datetime] NULL,
	[FechaApertura] [datetime] NULL,
	[AdjudicacionDirecta] [bit] NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Hash] [nvarchar](max) NULL,
	[Borrador] [bit] NULL,
 CONSTRAINT [PK__Concurso__3214EC071CA782F1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuraciones]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuraciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario_Id] [int] NULL,
	[Idioma_Id] [int] NULL,
	[Theme] [nvarchar](50) NULL,
 CONSTRAINT [PK_Configuraciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HitoConcurso]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HitoConcurso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConcursoId] [int] NULL,
	[Hito] [nvarchar](max) NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_HitoConcurso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idiomas]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idiomas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NULL,
	[Usuario_Id] [int] NULL,
	[Fecha] [date] NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[ByDefault] [bit] NOT NULL,
 CONSTRAINT [PK__Permiso__3214EC072C068C99] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RazonSocial] [nvarchar](50) NULL,
	[Celular] [nvarchar](50) NULL,
	[Direccion] [nvarchar](50) NULL,
	[Telefono] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Usuario_Id] [int] NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_Permiso]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Permiso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RolId] [int] NULL,
	[PermisoId] [int] NULL,
 CONSTRAINT [PK__Rol_Perm__3214EC072FB9199F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario_Id] [int] NULL,
	[Idioma_Id] [int] NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Terminos]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terminos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_Termino] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Evento]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Evento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripción] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tipo_Evento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Traducciones]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traducciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Termino] [int] NOT NULL,
	[Id_Idioma] [int] NOT NULL,
	[Traduccion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Traduccion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Rol]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Rol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NULL,
	[RolId] [int] NULL,
 CONSTRAINT [PK__Usuario___3214EC07499B0EEC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[LastLogin] [date] NULL,
	[Password] [nvarchar](50) NULL,
	[Rol_Id] [int] NULL,
 CONSTRAINT [PK__Usuario__3214EC070AD2A005] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verificables]    Script Date: 4/11/2019 15:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verificables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Digito] [nvarchar](50) NULL,
	[Tabla] [nvarchar](50) NULL,
 CONSTRAINT [PK_Verificable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permiso] ADD  CONSTRAINT [DF_Permiso_ByDefault]  DEFAULT ((0)) FOR [ByDefault]
GO
ALTER TABLE [dbo].[Compradores]  WITH CHECK ADD  CONSTRAINT [FK_Compradores_Usuarios] FOREIGN KEY([Usuario_Id])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Compradores] CHECK CONSTRAINT [FK_Compradores_Usuarios]
GO
ALTER TABLE [dbo].[Configuraciones]  WITH CHECK ADD  CONSTRAINT [FK_Configuraciones_Idiomas] FOREIGN KEY([Idioma_Id])
REFERENCES [dbo].[Idiomas] ([Id])
GO
ALTER TABLE [dbo].[Configuraciones] CHECK CONSTRAINT [FK_Configuraciones_Idiomas]
GO
ALTER TABLE [dbo].[Configuraciones]  WITH CHECK ADD  CONSTRAINT [FK_Configuraciones_Usuarios] FOREIGN KEY([Usuario_Id])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Configuraciones] CHECK CONSTRAINT [FK_Configuraciones_Usuarios]
GO
ALTER TABLE [dbo].[HitoConcurso]  WITH CHECK ADD  CONSTRAINT [FK_HitoConcurso_Concursos] FOREIGN KEY([ConcursoId])
REFERENCES [dbo].[Concursos] ([Id])
GO
ALTER TABLE [dbo].[HitoConcurso] CHECK CONSTRAINT [FK_HitoConcurso_Concursos]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Usuario] FOREIGN KEY([Usuario_Id])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Usuario]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Usuarios] FOREIGN KEY([Usuario_Id])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Usuarios]
GO
ALTER TABLE [dbo].[Rol_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Permiso_Permiso] FOREIGN KEY([PermisoId])
REFERENCES [dbo].[Permiso] ([Id])
GO
ALTER TABLE [dbo].[Rol_Permiso] CHECK CONSTRAINT [FK_Rol_Permiso_Permiso]
GO
ALTER TABLE [dbo].[Traducciones]  WITH CHECK ADD  CONSTRAINT [FK_Traduccion_Idioma] FOREIGN KEY([Id_Idioma])
REFERENCES [dbo].[Idiomas] ([Id])
GO
ALTER TABLE [dbo].[Traducciones] CHECK CONSTRAINT [FK_Traduccion_Idioma]
GO
ALTER TABLE [dbo].[Traducciones]  WITH CHECK ADD  CONSTRAINT [FK_Traduccion_Termino] FOREIGN KEY([Id_Termino])
REFERENCES [dbo].[Terminos] ([Id])
GO
ALTER TABLE [dbo].[Traducciones] CHECK CONSTRAINT [FK_Traduccion_Termino]
GO
ALTER TABLE [dbo].[Usuario_Rol]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Usuario_Rol] CHECK CONSTRAINT [FK_Usuario_Rol_Usuario]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Permiso] FOREIGN KEY([Rol_Id])
REFERENCES [dbo].[Permiso] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Permiso]
GO
USE [master]
GO
ALTER DATABASE [LicitProd] SET  READ_WRITE 
GO
