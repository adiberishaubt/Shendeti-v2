# Shendeti Frontend

A modern React frontend application for the Shendeti Blood Donation Management System.

## Features

- **Authentication**: Login and registration for patients and doctors
- **Dashboard**: Analytics and reporting with interactive charts
- **Appointment Management**: Schedule and manage medical appointments
- **Blood Donation**: Track blood requests and donations
- **User Management**: Patient and doctor profiles
- **Admin Panel**: Administrative functions for system management

## Tech Stack

- **React 18** with TypeScript
- **Vite** for fast development and building
- **Tailwind CSS** for styling
- **Redux Toolkit** for state management
- **React Router** for navigation
- **React Hook Form** for form handling
- **Recharts** for data visualization
- **Heroicons** for icons

## Getting Started

### Prerequisites

- Node.js 16+ and npm
- Backend API running on `https://localhost:7161`

### Installation

```bash
cd reactfront
npm install
```

### Development

```bash
npm run dev
```

The application will be available at `http://localhost:5173`

### Building for Production

```bash
npm run build
```

### Preview Production Build

```bash
npm run preview
```

## Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── layout/         # Layout components (Header, Sidebar, etc.)
│   ├── ui/             # Basic UI components (Button, Input, Card)
│   └── common/         # Common components
├── pages/              # Page components
│   ├── auth/           # Authentication pages
│   ├── admin/          # Admin pages
│   └── ...             # Other pages
├── store/              # Redux store and slices
├── services/           # API services
├── types/              # TypeScript type definitions
├── routes/             # Routing configuration
├── hooks/              # Custom React hooks
└── utils/              # Utility functions
```

## API Integration

The frontend integrates with the .NET Web API backend through the `api.ts` service layer. All API calls are handled through Redux actions and reducers.

## Authentication

The application uses JWT tokens for authentication. Tokens are stored in localStorage and automatically included in API requests.

## State Management

Redux Toolkit is used for state management with separate slices for:
- Authentication
- Users (Patients, Doctors)
- Appointments
- Blood Requests
- Donations
- Feedback
- Master data (Countries, Cities, Specializations, etc.)

## Styling

Tailwind CSS is used for styling with custom components and utilities. The design is responsive and follows modern UI/UX principles.

## Contributing

1. Follow the existing code patterns and naming conventions
2. Use TypeScript for type safety
3. Write clean, readable code
4. Test your changes thoroughly
5. Update documentation as needed

## License

This project is licensed under the MIT License.