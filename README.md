# Portfolio Backend API

This is the ASP.NET Core Web API backend for the portfolio website.

## Features

- **Projects Management**: CRUD operations for portfolio projects
- **Contact Messages**: Handle contact form submissions
- **JWT Authentication**: Secure admin authentication
- **CORS Support**: Cross-origin resource sharing for frontend integration
- **Swagger Documentation**: Auto-generated API documentation
- **In-Memory Database**: Easy development setup (can be replaced with SQL Server/PostgreSQL)

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- JWT Authentication
- Swagger/OpenAPI
- In-Memory Database (for development)

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code

### Installation

1. Navigate to the backend directory:
```bash
cd backend-api
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/verify` - Verify JWT token
- `POST /api/auth/logout` - User logout
- `GET /api/auth/me` - Get current user info

### Projects
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project (requires auth)
- `PUT /api/projects/{id}` - Update project (requires auth)
- `DELETE /api/projects/{id}` - Delete project (requires auth)

### Contact
- `POST /api/contact` - Send contact message
- `GET /api/contact` - Get all messages (requires auth)
- `PUT /api/contact/{id}/mark-read` - Mark message as read (requires auth)
- `DELETE /api/contact/{id}` - Delete message (requires auth)

## Authentication

The API uses JWT tokens for authentication. Default credentials:
- Username: `admin`, Password: `admin123`
- Username: `nafiz`, Password: `portfolio2025`

## Configuration

### appsettings.json
```json
{
  "JwtSettings": {
    "Key": "your-secret-key-here",
    "ExpiryInHours": 24
  }
}
```

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Set to `Development` for development mode
- `ASPNETCORE_URLS`: Override default URLs

## Deployment

### Local Development
```bash
dotnet run
```

### Production Build
```bash
dotnet publish -c Release -o ./publish
```

### Docker (Optional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY ./publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PortfolioAPI.dll"]
```

## Database

Currently uses Entity Framework Core with In-Memory database for development. For production, update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PortfolioDb;Trusted_Connection=true;"
  }
}
```

And update `Program.cs`:
```csharp
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## CORS Configuration

The API is configured to allow requests from:
- GitHub Pages domains (`*.github.io`)
- Local development servers (`localhost:3000`, `localhost:5000`)
- Live Server (`127.0.0.1:5500`)

## Testing

Use the Swagger UI at `/swagger` to test API endpoints, or use tools like Postman.

## Security Notes

For production deployment:
1. Change default passwords
2. Use a strong JWT secret key
3. Use a production database (SQL Server, PostgreSQL)
4. Configure proper HTTPS
5. Update CORS origins to match your frontend domain
6. Consider rate limiting and additional security measures
