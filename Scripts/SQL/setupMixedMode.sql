/*
To run the script from the DB machine, use: sqlcmd -S localhost -i C:\Path\To\File\setupMixedMode.sql
*/
USE [master]
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
GO