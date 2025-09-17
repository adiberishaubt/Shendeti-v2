# Collaborator Setup Guide

## Prerequisites
- Git installed
- .NET 6.0 or later
- Node.js 16+ and npm
- Visual Studio 2022 or VS Code

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/adiberishaubt/Shendeti-v2.git
cd Shendeti-v2
```

### 2. Backend Setup (.NET API)
```bash
cd Shendeti.Api
dotnet restore
dotnet build
dotnet run
```

The API will run on `https://localhost:7000` (or check `Properties/launchSettings.json`)

### 3. Frontend Setup (React)
```bash
cd reactfront
npm install
npm run dev
```

The frontend will run on `http://localhost:5173`

## Project Structure
- `Shendeti.Api/` - .NET Web API backend
- `Shendeti.Domain/` - Domain layer with services and interfaces
- `Shendeti.Infrastructure/` - Data access and infrastructure
- `reactfront/` - React frontend application

## Database
- The project uses Entity Framework Core
- Update connection string in `appsettings.json`
- Run migrations: `dotnet ef database update`

## Development Workflow
1. Create feature branches from `main`
2. Make your changes
3. Test thoroughly
4. Create pull request to `main`
5. Wait for review and merge

## Important Notes
- Always pull latest changes before starting work
- Follow the existing code patterns and naming conventions
- Update documentation for any new features
- Test both frontend and backend changes

## Contact
- Repository: https://github.com/adiberishaubt/Shendeti-v2
- Issues: Use GitHub Issues for bug reports and feature requests

