dotnet ef database drop --force --project ..\src\Gram.Web\ --context AuditContext
dotnet ef database update --project ..\src\Gram.Web\ --context AuditContext
dotnet ef database update --project ..\src\Gram.Web\ --context DataContext
dotnet ef database update --project ..\src\Gram.Web\ --context IdentityContext
Invoke-Sqlcmd -InputFile ".\Seed data.sql" -ServerInstance "(localdb)\mssqllocaldb"