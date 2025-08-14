# Proyecto Diseño Web I - Backend

API REST completa en .NET 8 para gestión integral de personas con sistema de usuarios, emails, teléfonos y direcciones.

## Características Principales

- ✅ **CRUD completo** para todas las entidades: Personas, Usuarios, Emails, Teléfonos y Direcciones
- ✅ **Entity Framework Core 8** con SQL Server y relaciones complejas
- ✅ **Sistema de Usuarios** con autenticación y gestión de estados
- ✅ **Validaciones automáticas** de modelos con Data Annotations
- ✅ **CORS configurado** para integración con frontend
- ✅ **Swagger/OpenAPI** para documentación interactiva de la API
- ✅ **Manejo de errores** personalizado con respuestas estructuradas
- ✅ **Docker** configurado para despliegue en contenedores
- ✅ **Arquitectura limpia** con separación de DTOs y modelos

## Tecnologías Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core 8.0.16** - ORM para base de datos
- **SQL Server** - Base de datos relacional
- **ASP.NET Core Web API** - Framework web
- **Swashbuckle.AspNetCore 6.6.2** - Generación de documentación Swagger
- **Microsoft.AspNetCore.JsonPatch 9.0.8** - Soporte para operaciones PATCH
- **Docker** - Contenerización de la aplicación

## Modelo de Datos

### Entidades Principales

- **Persona**: Información personal básica (nombres, apellidos, fecha nacimiento, género)
- **Usuario**: Sistema de cuentas de usuario con autenticación y gestión de estados
- **Email**: Direcciones de correo electrónico con tipos, verificación y fechas de creación/actualización
- **Telefono**: Números de teléfono con tipos, estado activo y fecha de registro
- **Direccion**: Direcciones físicas con tipos y detalles específicos
- **Tipo_Email**: Categorización de emails (personal, trabajo, académico, etc.)
- **Tipo_Telefono**: Categorización de teléfonos (móvil, fijo, trabajo, emergencia, etc.)
- **Tipo_Direccion**: Categorización de direcciones (casa, trabajo, facturación, etc.)

### Relaciones
- Una Persona puede tener múltiples Usuarios, Emails, Teléfonos y Direcciones
- Cada entidad secundaria está relacionada con su tipo correspondiente
- Relaciones configuradas con Entity Framework y restricciones de integridad referencial
- Soporte para eliminación en cascada configurado apropiadamente

### Estructura de DTOs

El proyecto utiliza DTOs (Data Transfer Objects) para separar la lógica de negocio:

- **GET**: Para respuestas de lectura con datos completos y relaciones
- **POST**: Para creación de nuevas entidades
- **PUT**: Para actualización completa de entidades existentes
- **PATCH**: Para actualización parcial usando JsonPatch

## Instalación y Configuración

### Prerrequisitos
- .NET 8 SDK
- SQL Server (local o remoto)
- Docker (opcional, para contenedores)

### Instalación Local

```bash
# Clonar el repositorio
git clone <repository-url>
cd project_sis15/dotnet_backend

# Restaurar dependencias
dotnet restore

# Configurar base de datos
# Editar appsettings.json con tu connection string

# Ejecutar migraciones (si aplica)
dotnet ef database update

# Ejecutar la aplicación
dotnet run
```

### Configuración de Base de Datos

Edita `appsettings.json` con tu connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=lastproject;User Id=tu-usuario;Password=tu-password;TrustServerCertificate=true;"
  }
}
```

### Ejecución con Docker

```bash
# Construir imagen
docker build -t backend-sis15 .

# Ejecutar contenedor
docker run -p 8080:8080 -p 8081:8081 backend-sis15
```

## Endpoints de la API

### Personas (`/api/persona`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/persona` | Obtener todas las personas con datos relacionados |
| `GET` | `/api/persona/{id}` | Obtener persona específica por ID |
| `POST` | `/api/persona` | Crear nueva persona |
| `PUT` | `/api/persona/{id}` | Actualizar persona completa |
| `PATCH` | `/api/persona/{id}` | Actualizar campos específicos de persona |
| `DELETE` | `/api/persona/{id}` | Eliminar persona |

### Usuarios (`/api/usuario`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/usuario?idPersona={id}` | Listar usuarios por persona |
| `GET` | `/api/usuario/{userName}` | Obtener usuario por nombre de usuario |
| `POST` | `/api/usuario` | Crear nuevo usuario |
| `PUT` | `/api/usuario/{userName}` | Actualizar usuario completo |
| `PATCH` | `/api/usuario/{userName}` | Actualizar campos específicos |
| `DELETE` | `/api/usuario/{userName}` | Eliminar usuario |

### Emails (`/api/email`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/email` | Obtener todos los emails |
| `GET` | `/api/email/{email}` | Obtener email específico |
| `POST` | `/api/email` | Crear nuevo email |
| `PUT` | `/api/email/{email}` | Actualizar email |
| `PATCH` | `/api/email/{email}` | Actualizar campos específicos |
| `DELETE` | `/api/email/{email}` | Eliminar email |

### Teléfonos (`/api/telefono`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/telefono` | Obtener todos los teléfonos |
| `GET` | `/api/telefono/{id}` | Obtener teléfono específico |
| `POST` | `/api/telefono` | Crear nuevo teléfono |
| `PUT` | `/api/telefono/{id}` | Actualizar teléfono |
| `PATCH` | `/api/telefono/{id}` | Actualizar campos específicos |
| `DELETE` | `/api/telefono/{id}` | Eliminar teléfono |

### Direcciones (`/api/direccion`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/direccion` | Obtener todas las direcciones |
| `GET` | `/api/direccion/{id}` | Obtener dirección específica |
| `POST` | `/api/direccion` | Crear nueva dirección |
| `PUT` | `/api/direccion/{id}` | Actualizar dirección |
| `PATCH` | `/api/direccion/{id}` | Actualizar campos específicos |
| `DELETE` | `/api/direccion/{id}` | Eliminar dirección |

## Documentación y Testing

### Swagger UI
- **Desarrollo**: `http://localhost:5138/swagger` o `https://localhost:7270/swagger`
- **Docker**: `http://localhost:8080/swagger` o `https://localhost:8081/swagger`
- **Producción**: `https://api.server.asralabs.com/swagger`

### Archivo de Testing HTTP
El proyecto incluye `backend.http` para probar endpoints directamente desde VS Code o Postman.

### Ejemplos de Respuestas

#### Respuesta Exitosa (Persona)
```json
{
  "idPersona": 1,
  "nombre1": "Juan",
  "apellido1": "Pérez",
  "apellido2": "García",
  "fecha_Nacimiento": "1990-05-15",
  "genero": 1,
  "usuarios": [...],
  "emails": [...],
  "telefonos": [...],
  "direcciones": [...]
}
```

#### Respuesta de Error
```json
{
  "message": "Persona No Encontrada",
  "statusCode": 404,
  "details": null
}
```

### Códigos de Estado HTTP

- **200 OK**: Operación exitosa
- **201 Created**: Recurso creado exitosamente
- **204 No Content**: Operación exitosa sin contenido de respuesta
- **400 Bad Request**: Datos de entrada inválidos
- **404 Not Found**: Recurso no encontrado
- **409 Conflict**: Conflicto con el estado actual del recurso
- **500 Internal Server Error**: Error interno del servidor

## Arquitectura del Proyecto

```
dotnet_backend/
├── Controllers/          # Controladores de la API REST
│   ├── PersonaController.cs      # CRUD de personas
│   ├── UsuarioController.cs      # CRUD de usuarios
│   ├── EmailController.cs        # CRUD de emails
│   ├── TelefonoController.cs     # CRUD de teléfonos
│   └── DireccionController.cs    # CRUD de direcciones
├── Models/               # Modelos de Entity Framework
│   ├── Persona.cs               # Entidad principal
│   ├── Usuario.cs               # Sistema de usuarios
│   ├── Email.cs                 # Gestión de emails
│   ├── Telefono.cs              # Gestión de teléfonos
│   ├── Direccion.cs             # Gestión de direcciones
│   ├── Tipo_*.cs                # Entidades de tipos
│   └── lastprojectContext.cs    # Contexto de EF
├── DTO/                  # Objetos de transferencia de datos
│   ├── Persona/          # DTOs para operaciones CRUD
│   ├── Usuario/          # DTOs para operaciones CRUD
│   ├── Email/            # DTOs para operaciones CRUD
│   ├── Telefono/         # DTOs para operaciones CRUD
│   └── Direccion/        # DTOs para operaciones CRUD
├── Utils/                # Utilidades y helpers
│   └── Error.cs          # Clase para manejo de errores
├── Program.cs            # Configuración principal de la aplicación
├── appsettings.json      # Configuración de base de datos
├── Dockerfile            # Configuración de Docker
└── backend.csproj        # Archivo de proyecto .NET
```

## Configuración de Desarrollo

### Puertos por Defecto
- **HTTP**: 5138
- **HTTPS**: 7270
- **Docker HTTP**: 8080
- **Docker HTTPS**: 8081

### Variables de Entorno
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- `ASPNETCORE_HTTP_PORTS`: 8080 (Docker)
- `ASPNETCORE_HTTPS_PORTS`: 8081 (Docker)

## Despliegue

### Local
```bash
dotnet run --environment Production
```

### Docker
```bash
docker build -t backend-sis15 .
docker run -d -p 8080:8080 -p 8081:8081 backend-sis15
```

### IIS
El proyecto está configurado para ejecutarse en IIS Express durante el desarrollo.

## Notas de Desarrollo

- Los modelos están generados automáticamente con EF Core Power Tools
- Se utiliza `JsonPatch` para operaciones PATCH parciales
- CORS está configurado para permitir todas las origenes en desarrollo
- Swagger se habilita en todos los entornos para facilitar el testing
- La aplicación maneja errores de base de datos y conflictos de manera elegante

## Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## Licencia

Este proyecto es parte del curso de Diseño Web I (SIS-15) de la Universidad.

## Soporte

Para soporte técnico o preguntas sobre el proyecto, contacta al equipo de desarrollo del curso.