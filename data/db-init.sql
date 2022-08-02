USE [master]
GO

IF DB_ID('Alexandria') IS NOT NULL
	set noexec on               -- prevent creation when already exists

CREATE DATABASE [Alexandria];
GO

USE [Alexandria]
GO
