USE [master]
GO

IF DB_ID('Alexandria') IS NOT NULL
	set noexec on               -- prevent creation when already exists

CREATE DATABASE [MercadoLivreClone];
GO

USE [MercadoLivre]
GO
