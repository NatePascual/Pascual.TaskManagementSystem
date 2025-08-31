# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore Pascual.TaskManagementSystem.Blazor/Pascual.TaskManagementSystem.Blazor.csproj
RUN dotnet publish  Pascual.TaskManagementSystem.Blazor/Pascual.TaskManagementSystem.Blazor.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "Pascual.TaskManagementSystem.Blazor.dll"]
