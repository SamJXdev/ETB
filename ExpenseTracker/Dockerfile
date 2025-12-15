FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY . .
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000
CMD ["dotnet", "ExpenseTracker.dll"]