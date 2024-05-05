# EF Query Optimization
## Prerequisite

1. Make sure you have SQL Server installed on your local machine. If not yet, you can run this command for quick setup

   ```
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd.123!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

   ```

2. Make sure the connection string is correctly matched with your local SQL server. Check file ApplicationContext.cs
3. Run EF migration. Go to the project root dir and run `dotnet ef database update` or `Update-Database` if you're using PMC tool on Windows

For all cases in which you want to optimize the query please watch this video: 👇

[![Watch the video](https://img.youtube.com/vi/nQC4awFqRkE/hqdefault.jpg)](https://youtu.be/nQC4awFqRkE)
