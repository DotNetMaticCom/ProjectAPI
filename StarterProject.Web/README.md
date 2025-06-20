# StarterProject Blazor Web Application

A modern Blazor web application with JWT authentication, TailwindCSS styling, and clean architecture.

## Features

- **Blazor United Architecture** - Both Server and WebAssembly rendering modes
- **JWT Authentication** - Secure authentication with refresh tokens
- **TailwindCSS** - Modern utility-first CSS framework
- **API Client Abstraction** - Reusable API client for multi-platform use
- **Clean Architecture** - Separation of concerns and maintainable code

## Project Structure

```
StarterProject.Web/
├── StarterProject.Web/          # Server project (SSR + InteractiveServer)
│   ├── Auth/                    # Authentication components
│   ├── Components/              # Shared components
│   ├── Services/                # Server-specific services
│   └── wwwroot/                 # Static assets
├── StarterProject.Web.Client/   # Client project (WebAssembly)
│   ├── Auth/                    # Client authentication
│   ├── Pages/                   # Application pages
│   ├── Layout/                  # Layout components
│   └── Services/                # Client-specific services
└── README.md
```

## Getting Started

1. **Install dependencies**:
   ```bash
   cd StarterProject.Web/StarterProject.Web
   npm install
   ```

2. **Build TailwindCSS**:
   ```bash
   npm run css:build
   # or for development with watch mode:
   npm run css:watch
   ```

3. **Run the application**:
   ```bash
   dotnet run --project StarterProject.Web/StarterProject.Web.csproj
   ```

4. **Access the application**:
   - Navigate to https://localhost:5001 or http://localhost:5000
   - The API backend should be running on http://localhost:5278

## Authentication

The application uses JWT authentication with the backend API:

- Login page: `/login`
- Register page: `/register`
- Tokens are stored securely (HttpOnly cookies on server, localStorage on client)
- Automatic token refresh when expired

## Rendering Modes

The application supports flexible rendering modes:

- **Static Server Rendering (SSR)** - For initial page load and SEO
- **Interactive Server** - For real-time server-side interactivity
- **Interactive WebAssembly** - For client-side performance

Pages can specify their render mode using `@rendermode` directive.

## Development

### TailwindCSS

Custom utility classes are defined in `Styles/app.css`:
- `.btn` - Base button styles
- `.btn-primary` - Primary button variant
- `.input` - Form input styles
- `.card` - Card container styles

### API Integration

The API client is configured in `appsettings.json`:
```json
{
  "ApiClient": {
    "BaseUrl": "http://localhost:5278/api",
    "TimeoutSeconds": 30
  }
}
```

## Production Build

1. Build CSS for production:
   ```bash
   npm run css:build
   ```

2. Publish the application:
   ```bash
   dotnet publish -c Release
   ```

## Future Enhancements

- State management with Fluxor
- Progressive Web App (PWA) support
- Real-time updates with SignalR
- Comprehensive error handling
- Localization support