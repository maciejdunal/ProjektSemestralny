CREATE LOGIN [user] WITH PASSWORD = '1234'
USE [Wypozyczalnia_Gier_komputerowych]
CREATE USER [user] FOR LOGIN [user] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [user]