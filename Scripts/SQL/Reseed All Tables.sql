

exec sp_MSforeachtable @command1 = 'DBCC CHECKIDENT(''?'', RESEED, 1)'