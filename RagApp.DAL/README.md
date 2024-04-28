# Migrations

Create migrations using Asp .Net Core CLI, launch the command from DAL project folder:

``` dotnet ef --startup-project ..\RagApp\RagApp\ migrations add <MigrationName> ```

Update database using Asp .Net Core CLI

``` dotnet ef database update --project . --startup-project ..\RagApp\RagApp\ ```
