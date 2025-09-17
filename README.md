# Shendeti - Blood Donation Management System

A comprehensive blood donation management system built with .NET Web API and React frontend.

## 🏗️ Architecture

- **Backend**: .NET 6 Web API with Entity Framework Core
- **Frontend**: React 18 with TypeScript, Vite, and Tailwind CSS
- **Database**: SQL Server (configurable)
- **Authentication**: JWT-based authentication

## 🚀 Quick Start

### Prerequisites
- .NET 6.0 or later
- Node.js 16+ and npm
- SQL Server (or SQL Server Express)
- Visual Studio 2022 or VS Code

### Backend Setup
```bash
cd Shendeti.Api
dotnet restore
dotnet build
dotnet run
```

### Frontend Setup
```bash
cd reactfront
npm install
npm run dev
```

## 📁 Project Structure

```
├── Shendeti.Api/              # Web API Controllers and Configuration
├── Shendeti.Domain/           # Business Logic and Services
├── Shendeti.Infrastructure/   # Data Access and Repositories
└── reactfront/               # React Frontend Application
```

## 🔧 Configuration

1. Update database connection string in `Shendeti.Api/appsettings.json`
2. Run Entity Framework migrations:
   ```bash
   cd Shendeti.Api
   dotnet ef database update
   ```

## 🎯 Features

- **User Management**: Patient and Doctor registration/authentication
- **Appointment System**: Schedule and manage medical appointments
- **Blood Donation**: Track blood requests and donations
- **Doctor Management**: Specialization and schedule management
- **Dashboard**: Analytics and reporting

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📝 API Documentation

The API includes comprehensive controllers for:
- Appointments
- Blood Requests
- Cities and Countries
- Donations
- Education
- Feedback
- Levels
- Schedules
- Services
- Slots
- Specializations
- Users

## 🔐 Authentication

The system uses JWT tokens for authentication. Include the token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

## 📞 Support

For questions or issues, please use GitHub Issues or contact the development team.

## 📄 License

This project is licensed under the MIT License - see the LICENSE.md file for details.

