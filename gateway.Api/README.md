mkdir api-gateway
cd api-gateway
dotnet new sln -n api-gateway
dotnet new webapi -au none -o gateway.Api
dotnet sln api-gateway.sln add gateway.Api\gateway.Api.csproj
dotnet add ./gateway.Api/gateway.Api.csproj package Ocelot --version 18.0.0
dotnet add ./gateway.Api/gateway.Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.7
dotnet add ./gateway.Api/gateway.Api.csproj package Ocelot.Cache.CacheManager --version 18.0.0

https://ocelot.readthedocs.io/en/latest/features/routing.html
https://ocelot.readthedocs.io/en/latest/features/authentication.html
https://ocelot.readthedocs.io/en/latest/features/ratelimiting.html
https://ocelot.readthedocs.io/en/latest/features/caching.html
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-6.0