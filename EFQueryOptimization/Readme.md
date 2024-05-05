# EF Query Optimization
## Prerequisite

1. Make sure you have SQL Server installed on your local machine. If not yet, you can run this command for quick setup

   ```
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd.123!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

   ```

2. Make sure the connection string is correctly matched with your local SQL server. Check file ApplicationContext.cs
3. Run EF migration. Go to the project root dir and run `dotnet ef database update` or `Update-Database` if you're using PMC tool on Windows

## Optimized a query 28x faster!
```bash
| Method             | Mean       | Error    | StdDev    | Ratio         | Allocated    |
|------------------- |-----------:|---------:|----------:|--------------:|-------------:|-
| GetEmployees       | 3,637.0 ms | 71.70 ms | 113.73 ms |      baseline | 279222.25 KB |
| GetEmployee_Tunned |   127.9 ms |  2.92 ms |   8.33 ms | 28.35x faster |    130.48 KB |
```

## For all cases in which you want to optimize the query please watch this video: 👇

[![Watch the video](https://img.youtube.com/vi/nQC4awFqRkE/hqdefault.jpg)](https://youtu.be/nQC4awFqRkE)
