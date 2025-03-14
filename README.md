# 🏨 HotelApi

Una API REST para un sistema de reservas de hotel que permite realizar operaciones CRUD sobre habitaciones y reservas. La aplicación utiliza autenticación JWT, logging centralizado con Serilog, y pruebas unitarias con xUnit. Además, incluye documentación interactiva con Swagger.

## 🚀 Requisitos

- **.NET 6** o superior
- **SQLite** (como gestor de base de datos)
- **Dependencias de NuGet**:
  - `Microsoft.EntityFrameworkCore.Sqlite`
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `Serilog`
  - `Serilog.Extensions.Logging`
  - `Serilog.Sinks.Console`
  - `xunit`
  - `Swashbuckle.AspNetCore`

## 🛠️ Instalación

1. Clona este repositorio en tu máquina local:

   ```bash
   git clone https://github.com/Joseav2797/HotelApi.git
   cd HotelApi
Restaura las dependencias de NuGet:

bash
Copy
dotnet restore
Crea y aplica las migraciones para la base de datos SQLite:

bash
Copy
dotnet ef migrations add InitialCreate
dotnet ef database update
Inicia la aplicación:

bash
Copy
dotnet run
La API estará disponible en http://localhost:5000 (puedes revisar los detalles en launchsettings.json).

# 📖 Uso
La API permite interactuar con las siguientes rutas:

# 🛏️ Habitaciones
GET /api/habitaciones – Obtener todas las habitaciones.

GET /api/habitaciones/{id} – Obtener una habitación por ID.

POST /api/habitaciones – Crear una nueva habitación.

PUT /api/habitaciones/{id} – Actualizar una habitación.

DELETE /api/habitaciones/{id} – Eliminar una habitación.

# 📅 Reservas
GET /api/reservas – Obtener todas las reservas.

GET /api/reservas/{id} – Obtener una reserva por ID.

POST /api/reservas – Crear una nueva reserva.

PUT /api/reservas/{id} – Actualizar una reserva.

DELETE /api/reservas/{id} – Eliminar una reserva.

# 🔐 Autenticación
La API utiliza autenticación basada en JWT. Al iniciar sesión, se proporcionará un token que se debe enviar en los encabezados de las solicitudes subsecuentes.

Login (Obtener token)
POST /api/auth/login

Request body: {"username": "usuario", "password": "contraseña"}

Response: Un JWT que debe ser usado en las solicitudes subsecuentes.

Para realizar operaciones protegidas, agrega el token al encabezado Authorization de la siguiente forma:

http
Copy
Authorization: Bearer <token>
# 📚 Swagger
Para una fácil visualización y pruebas de la API, Swagger está habilitado. Accede a la documentación interactiva en:

bash
Copy
http://localhost:5000/swagger
# 📝 Logging
La API utiliza Serilog para logging centralizado. Los logs se pueden ver en la consola al ejecutar la aplicación. Los registros incluyen información sobre el ciclo de vida de las solicitudes y errores del sistema.

# 🗂️ Estructura del Proyecto
plaintext
Copy
HotelApi
│
├── Properties
│   └── launchsettings.json
├── Controllers
│   ├── HabitacionesController.cs
│   └── ReservaController.cs
├── Data
│   └── ApplicationDbContext.cs
├── DTOs
│   ├── HabitacionDTO.cs
│   └── ReservaDTO.cs
├── Models
│   ├── Habitacion.cs
│   └── Reserva.cs
├── Services
│   ├── HabitacionService.cs
│   ├── IHabitacionService.cs
│   ├── IReservaService.cs
│   └── ReservaService.cs
├── Test
│   ├── Controllers
│   │   ├── HabitacionesControllerTests.cs
│   │   └── ReservasControllerTests.cs
│   └── Services
│       ├── HabitacionServiceTests.cs
│       └── ReservaServiceTests.cs
├── appsettings.json
├── HotelApi.http
└── Program.cs
