CREATE DATABASE db_receitas_de_familia;
USE db_receitas_de_familia;
GO;
CREATE TABLE [FAMILIAS] (
	id_familia int NOT NULL IDENTITY(1,1),
	nome varchar(50) NOT NULL,
	descricao varchar(255),
	foto varchar(255),
	data_criacao datetime NOT NULL,
	data_alteracao datetime NOT NULL,
	usuario_criacao varchar(50) NOT NULL,
	usuario_alteracao varchar(50) NOT NULL,
	foi_deletado bit NOT NULL DEFAULT '0'
  CONSTRAINT [PK_FAMILIAS] PRIMARY KEY CLUSTERED
  (
  [id_familia] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [RECEITAS] (
	id_receita int NOT NULL IDENTITY(1,1),
	id_categoria int NOT NULL,
	id_familia int NOT NULL,
	nome varchar(100) NOT NULL,
	criador_receita varchar(50),
	tempo_preparo_min int,
	rendimento varchar(50),
	ingredientes varchar(max) NOT NULL,
	modo_preparo varchar(max) NOT NULL,
	informacoes_adicionais varchar(255),
	data_criacao datetime NOT NULL,
	data_alteracao datetime NOT NULL,
	usuario_criacao varchar(50) NOT NULL,
	usuario_alteracao varchar(50) NOT NULL,
	foi_deletado bit NOT NULL DEFAULT '0'
  CONSTRAINT [PK_RECEITAS] PRIMARY KEY CLUSTERED
  (
  [id_receita] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [CATEGORIAS_RECEITA] (
	id_categoria int NOT NULL IDENTITY(1,1),
	nome varchar(50) NOT NULL,
	data_criacao datetime NOT NULL,
	data_alteracao datetime NOT NULL,
	usuario_criacao varchar(50) NOT NULL,
	usuario_alteracao varchar(50) NOT NULL,
	foi_deletado bit NOT NULL DEFAULT '0'
  CONSTRAINT [PK_CATEGORIAS_RECEITA] PRIMARY KEY CLUSTERED
  (
  [id_categoria] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)

GO
CREATE TABLE [USUARIO] (
	id_usuario int NOT NULL IDENTITY(1,1),
	nome varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	senha varchar(100) NOT NULL,
	salt varchar(100) NOT NULL,
	email_validado bit NOT NULL DEFAULT '0',
	codigo_validacao_email int,
	data_criacao datetime NOT NULL,
	data_alteracao datetime NOT NULL,
	usuario_criacao varchar(50) NOT NULL,
	usuario_alteracao varchar(50) NOT NULL,
	foi_deletado bit NOT NULL DEFAULT '0'
  CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED
  (
  [id_usuario] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [FAVORITOS] (
	id_receita int NOT NULL,
	id_usuario int NOT NULL,
	data_criacao datetime NOT NULL,
	data_alteracao datetime NOT NULL,
	usuario_criacao varchar(50) NOT NULL,
	usuario_alteracao varchar(50) NOT NULL,
	foi_deletado bit NOT NULL DEFAULT '0'
)
GO

ALTER TABLE [RECEITAS] WITH CHECK ADD CONSTRAINT [RECEITAS_fk0] FOREIGN KEY ([id_categoria]) REFERENCES [CATEGORIAS_RECEITA]([id_categoria])
ON UPDATE CASCADE
GO
ALTER TABLE [RECEITAS] CHECK CONSTRAINT [RECEITAS_fk0]
GO
ALTER TABLE [RECEITAS] WITH CHECK ADD CONSTRAINT [RECEITAS_fk1] FOREIGN KEY ([id_familia]) REFERENCES [FAMILIAS]([id_familia])
ON UPDATE CASCADE
GO
ALTER TABLE [RECEITAS] CHECK CONSTRAINT [RECEITAS_fk1]
GO


ALTER TABLE [FAVORITOS] WITH CHECK ADD CONSTRAINT [FAVORITOS_fk0] FOREIGN KEY ([id_receita]) REFERENCES [RECEITAS]([id_receita])
ON UPDATE CASCADE
GO
ALTER TABLE [FAVORITOS] CHECK CONSTRAINT [FAVORITOS_fk0]
GO
ALTER TABLE [FAVORITOS] WITH CHECK ADD CONSTRAINT [FAVORITOS_fk1] FOREIGN KEY ([id_usuario]) REFERENCES [USUARIO]([id_usuario])
ON UPDATE CASCADE
GO
ALTER TABLE [FAVORITOS] CHECK CONSTRAINT [FAVORITOS_fk1]
GO

