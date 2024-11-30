# Base image that we use to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image that we use to restore, build and publish
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files for WebApi and Forms.Model (make sure Forms.Model is included)
COPY ["Forms.WebApi/Forms.WebApi.csproj", "Forms.WebApi/"]
COPY ["Forms.Model/Forms.Model.csproj", "Forms.Model/"]

# Restore dependencies
RUN dotnet restore "Forms.WebApi/Forms.WebApi.csproj"

# Copy the rest of the source code
COPY . .

# Set the working directory to WebApi folder and build the WebApi
WORKDIR "/src/Forms.WebApi"
RUN dotnet build "Forms.WebApi.csproj" -c Release -o /app/build

# Publish the application to /app/publish
FROM build AS publish
RUN dotnet publish "Forms.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image to run the application
FROM base AS final
WORKDIR /app

# Copy everything from /app/publish into the final image
COPY --from=publish /app/publish .

# Set the entry point to run the WebApi
ENTRYPOINT ["dotnet", "Forms.WebApi.dll"]
