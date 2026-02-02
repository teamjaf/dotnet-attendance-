# Industry Attendance System

A clean architecture attendance management system that integrates with physical biometric machines.

## Architecture Overview

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│   Biometric     │────▶│   .NET API      │────▶│   Neon DB       │
│   Machine       │     │   (Backend)     │     │   (PostgreSQL)  │
└─────────────────┘     └────────┬────────┘     └─────────────────┘
                                 │
                                 ▼
                        ┌─────────────────┐
                        │   React App     │
                        │   (Frontend)    │
                        └─────────────────┘
```

## Tech Stack

### Backend
- **.NET 8** - Web API
- **Neon** - Serverless PostgreSQL database
- **Entity Framework Core** - ORM
- **Clean Architecture** - Domain, Application, Infrastructure, API layers

### Frontend
- **React 18** with TypeScript
- **Vite** - Build tool
- **Tailwind CSS** - Styling
- **React Query** - Data fetching
- **React Router** - Navigation

## Project Structure

```
├── backend/
│   └── AttendanceSystem/
│       ├── AttendanceSystem.Domain/        # Entities, Enums
│       ├── AttendanceSystem.Application/   # Use Cases, DTOs, Interfaces
│       ├── AttendanceSystem.Infrastructure/# DB Context, Repositories
│       └── AttendanceSystem.API/           # Controllers, Middleware
│
├── frontend/
│   └── attendance-app/
│       ├── src/
│       │   ├── components/    # Reusable UI components
│       │   ├── pages/         # Page components
│       │   ├── services/      # API calls
│       │   ├── hooks/         # Custom hooks
│       │   └── types/         # TypeScript types
│       └── ...
│
└── README.md
```

## Features

1. **Biometric Data Reception** - Receive punch data from biometric machines
2. **Employee Management** - CRUD operations for employees
3. **Attendance Tracking** - View and manage attendance records
4. **Dashboard** - Overview of attendance statistics
5. **Reports** - Generate attendance reports

## Biometric Machine Integration

The system exposes a REST API endpoint that biometric machines can call:

```
POST /api/attendance/punch
{
    "employeeCode": "EMP001",
    "punchTime": "2024-01-15T09:00:00Z",
    "deviceId": "BIO-001",
    "punchType": "IN"  // IN or OUT
}
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- Neon PostgreSQL database account

### Backend Setup
```bash
cd backend/AttendanceSystem
dotnet restore
# Update connection string in appsettings.json
dotnet ef database update --project AttendanceSystem.Infrastructure
dotnet run --project AttendanceSystem.API
```

### Frontend Setup
```bash
cd frontend/attendance-app
npm install
npm run dev
```

## Environment Variables

### Backend (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-neon-host;Database=attendance;Username=user;Password=pass;SSL Mode=Require"
  }
}
```

### Frontend (.env)
```
VITE_API_URL=http://localhost:5000/api
```

## License

MIT
