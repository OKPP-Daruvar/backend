# Base image that we use to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image that we use to restore, build and publish
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files for WebApi, Forms.Model, and Forms.Repository (ensure all projects are copied)
COPY ["Forms.WebApi/Forms.WebApi.csproj", "Forms.WebApi/"]
COPY ["Forms.Model/Forms.Model.csproj", "Forms.Model/"]
COPY ["Forms.Repository/Forms.Repository.csproj", "Forms.Repository/"]

# Restore dependencies for the WebApi project
RUN dotnet restore "Forms.WebApi/Forms.WebApi.csproj"

# Copy the entire source code
COPY . .

# Set the working directory to WebApi folder and build the WebApi project
WORKDIR "/src/Forms.WebApi"
RUN dotnet build "Forms.WebApi.csproj" -c Release -o /app/build

# Publish the WebApi project to /app/publish
FROM build AS publish
RUN dotnet publish "Forms.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image to run the application
FROM base AS final
WORKDIR /app

# Copy everything from /app/publish into the final image
COPY --from=publish /app/publish . 

# Set the entry point to run the WebApi
ENTRYPOINT ["dotnet", "Forms.WebApi.dll"]
