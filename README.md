An ASP.NET Core MVC web application for user registration, login, and account management. This project uses ASP.NET Identity for secure authentication and authorization, leveraging Entity Framework Core and SQL Server for data storage.

## 🚀 Features

- ✅ User Registration with validation
- ✅ Secure Password Hashing using `PasswordHasher<TUser>`
- ✅ User Login & Logout
- ✅ Razor Views with Tag Helpers
- ✅ Role-based access control (if configured)
- ✅ Bootstrap 5 UI with responsive design
- ✅ Entity Framework Core with Code-First approach
- ✅ Full MVC pattern (Models, Views, Controllers)

---

## 🛠️ Tech Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- ASP.NET Identity
- Razor Pages
- Bootstrap 5
- SQL Server / LocalDB

---

## 📦 Folder Structure

```
UserRegistrationApp1/
│
├── Controllers/         # MVC controllers (e.g., AccountController)
├── Models/              # Data models (e.g., ApplicationUser)
├── Views/               # Razor Views (Login, Register, Layout)
│   ├── Shared/          # _Layout.cshtml and _ValidationScriptsPartial.cshtml
│
├── Data/                # ApplicationDbContext and Identity config
├── wwwroot/             # Static files (CSS, JS, images)
├── appsettings.json     # Configuration (including DB connection)
├── Program.cs           # Entry point (web host setup)
├── Startup.cs           # Service registration & middleware config (if used)
```

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022+
- SQL Server / LocalDB

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/Sriram110603/UserRegistrationApp1.git
   cd UserRegistrationApp1
   ```

2. **Configure your DB connection**
   - Open `appsettings.json`
   - Set your connection string under `"DefaultConnection"`

3. **Apply EF Core Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```
   Or just press `F5` in Visual Studio.

---

## 🔐 Security

- Passwords are hashed using `PBKDF2` via ASP.NET Identity’s `PasswordHasher<TUser>`
- Avoid using plain SHA-256 or AES for password storage
- Built-in anti-CSRF protection via Razor Pages

---

## 🧪 Testing

- Manual browser testing for registration/login/logout
- Validate form inputs using both client-side and server-side validation
- Optional: Write unit tests for controllers and identity logic

---

## 🙋‍♂️ Author

**Sriram**  
[GitHub Profile](https://github.com/Sriram110603)
