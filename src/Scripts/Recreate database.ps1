dotnet ef database drop --force --project ..\Gram\Gram.Web\ --context IdentityContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context IdentityContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context AuditContext
dotnet ef database update --project ..\Gram\Gram.Web\ --context DataContext
Invoke-Sqlcmd -InputFile ".\Create foreign key constraint.sql" -ServerInstance "(localdb)\mssqllocaldb"
Invoke-Sqlcmd -InputFile ".\Seed data.sql" -ServerInstance "(localdb)\mssqllocaldb"