CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nome] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Senha] VARCHAR(50) NULL, 
    [DataDeNascimento] DATETIME NULL, 
    [Genero] CHAR(10) NULL
)
