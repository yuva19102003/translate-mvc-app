# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5270

# Copy the published files into the container
COPY ./TranslatorMVC/publish/ .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "TranslatorMVC.dll", "--urls", "http://0.0.0.0:5270"]
