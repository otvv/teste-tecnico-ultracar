# Use the .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project files to the container
COPY . .

# Restore dependencies and build the project
RUN dotnet restore && \
    dotnet build -c Release -o /app/build

# Define the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/build .

# Expose port 80 for the web application
EXPOSE 80

# Start the web application
ENTRYPOINT ["dotnet", "Ultracar.dll"]
