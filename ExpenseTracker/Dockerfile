# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file from the correct subfolder
COPY ExpenseTracker/ExpenseTracker.csproj ./ExpenseTracker/
RUN dotnet restore ExpenseTracker/ExpenseTracker.csproj

# Copy everything else
COPY . .
RUN dotnet publish ExpenseTracker/ExpenseTracker.csproj -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "ExpenseTracker.dll"]