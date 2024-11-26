# Use the official .NET SDK image to build and publish the app
# Use a specific version of .NET 8 for consistency
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Publish the Blazor WebAssembly app in Release mode
RUN dotnet publish -c Release -o /app/publish

# Use a lightweight image to serve the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "ButtButton.dll"]