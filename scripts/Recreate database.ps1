dotnet ef database drop --force --project ..\Gram\Gram.Web\ --context AuditContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context AuditContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context DataContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context IdentityContext
Invoke-Sqlcmd -InputFile ".\Seed data.sql" -ServerInstance "(localdb)\mssqllocaldb"