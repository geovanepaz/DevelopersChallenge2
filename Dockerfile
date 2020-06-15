FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
RUN mkdir /app
WORKDIR /app
EXPOSE 80/tcp


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /app
COPY src/API/ .
copy . .
RUN dotnet publish src/API/API.csproj -c Release -o API/dist

FROM base as final
WORKDIR /dist
COPY --from=publish /app/API/dist .
ENTRYPOINT ["dotnet", "API.dll"]