# ccore api
C# .NET 8 REST API

# Starting SQL Server
```shell
sa_password="[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

## Setting the connection string to secret manager
```shell
sa_password="[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:AppContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```

## Setting up secrets local commands
```shell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:AppContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
dotnet user-secrets list
dotnet user-secrets clear
```

## Dotnet built in local JWT local sample commands
```shell
dotnet user-jwts create
dotnet user-jwts print <id>
dotnet user-jwts create --role "Admin"
dotnet user-jwts create --scope "app:read app:write"
```
