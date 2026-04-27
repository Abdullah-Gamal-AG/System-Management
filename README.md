# System Management (WinForms + SQLite)

A desktop management system built with Windows Forms and SQLite, organized into Sales, Warehouse, HR/Admin, and Profile modules.

## Overview

This application is a role-based internal system for managing:

- Workers and roles
- Product inventory
- Sales and invoices
- Stock purchases/restocking
- User profile and password changes

The app initializes its SQLite database automatically on startup and seeds a default admin account if it does not already exist.

## Tech Stack

- .NET 10 (Windows)
- Windows Forms
- SQLite via `Microsoft.Data.Sqlite`
- Password hashing via `BCrypt.Net-Next`

## Key Features

- Login with role-based navigation
- Automatic database/schema creation at startup
- Default admin bootstrap account
- Worker management (add/update/view/delete flag)
- Sales flow with order + order items persistence
- Warehouse stock viewing and updates
- Purchase registration with stock increment trigger
- Profile module (including password change)

## Project Structure

```text
Data/                  Core data access layer (CoreSystem)
Models/                Shared runtime models (DataUser, Invoice)
Services/              Business logic by feature area
  hr&admin/
  login/
  profile/
  sales/
  warehouse/
UI/                    UI composition for each feature area
  hr&admin/
  login/
  profile/
  sales/
  warehouse/
Utils/                 Utility helpers (hashing)
dbsetup.cs             SQLite schema initialization and admin seed
Program.cs             App entry point
Form1.cs               Root form and module navigation host
```

## Database

### Runtime behavior

On app startup, `DatabaseSetup.SetupDatabase()` creates `MyDatabase.db` in the application output directory and ensures required tables, triggers, and indexes exist.

### Main tables

- `workers`
- `customers`
- `goods`
- `orders`
- `order_items`
- `purchases`

### Seeded account

The startup routine inserts this account once (using `INSERT OR IGNORE`):

- User ID: `1000`
- Username: `Admin`
- Password: `0000`

After first login, changing the default password is recommended.

## Prerequisites

- Windows OS
- .NET 10 SDK (or compatible preview if .NET 10 is prerelease on your machine)

Check installation:

```powershell
dotnet --version
```

## Getting Started

1. Open the solution:

```powershell
dotnet sln systemManagement.sln list
```

2. Restore dependencies:

```powershell
dotnet restore
```

3. Run the app:

```powershell
dotnet run --project systemManagement.csproj
```

4. Login using the default admin credentials above.

## Build and Publish

### Build (Debug)

```powershell
dotnet build systemManagement.csproj -c Debug
```

### Build (Release)

```powershell
dotnet build systemManagement.csproj -c Release
```

### Publish (Windows x64)

```powershell
dotnet publish systemManagement.csproj -c Release -r win-x64 --self-contained false
```

### Installer (Inno Setup Compiler)

This project uses Inno Setup Compiler to generate a Windows installer from the release output.

If Inno Setup is installed, you can compile the setup script with:

```powershell
iscc .\bin\Release\setup.iss
```

## Notes

- The SQLite database file is created under the app runtime output directory, not in source root.
- Product images under `img/` are copied to output (`PreserveNewest`) by the project file.
- A SQL script is also available at `db.sql` for reference/manual setup workflows.
- Installer packaging is done with Inno Setup Compiler using `bin/Release/setup.iss`.

## Dependencies

NuGet packages used:

- `BCrypt.Net-Next` (password hashing)
- `Microsoft.Data.Sqlite` (database access)

## License

No license file is currently included in this repository. Add a `LICENSE` file if you want explicit usage terms.
