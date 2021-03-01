#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY ["ExpensesMonitor/ExpensesMonitor.csproj", "ExpensesMonitor/"]
COPY ["ExpensesMonitor.SharedLibrary/ExpensesMonitor.SharedLibrary.csproj", "ExpensesMonitor.SharedLibrary/"]
RUN dotnet restore "ExpensesMonitor/ExpensesMonitor.csproj"
COPY . .
WORKDIR /src/
RUN dotnet build "ExpensesMonitor/ExpensesMonitor.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/
RUN dotnet publish "ExpensesMonitor/ExpensesMonitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpensesMonitor.dll"]
