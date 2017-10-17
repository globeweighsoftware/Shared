Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
New-NetFirewallRule -DisplayName "MSSQL" -Direction Inbound -LocalPort 1433 -Protocol TCP -Action Allow