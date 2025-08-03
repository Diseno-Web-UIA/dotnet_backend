# Proyecto Diseño Web I - Backend

API REST en .NET 8 para gestión de personas con emails, teléfonos y direcciones.

## Características

- ✅ **CRUD completo** para Personas, Emails, Teléfonos y Direcciones
- ✅ **Entity Framework** con SQL Server
- ✅ **CORS configurado** para integración con frontend
- ✅ **Swagger** para documentación de API
- ✅ **Validaciones** automáticas de modelos

## Tecnologías

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **ASP.NET Core Web API**

## Instalación

```bash
cd dotnet_backend
dotnet restore
dotnet run
```

## Endpoints

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/persona` | Obtener todas las personas |
| GET | `/api/email` | Obtener todos los emails |
| GET | `/api/telefono` | Obtener todos los teléfonos |
| GET | `/api/direccion` | Obtener todas las direcciones |

## Documentación

- **Swagger UI**: `https://localhost:7001/swagger`
- **API Base**: `https://localhost:7001`

## Estructura

```
dotnet_backend/
├── Controllers/          # Controladores API
├── Models/              # Modelos de Entity Framework
├── Program.cs           # Configuración de la aplicación
└── appsettings.json    # Configuración de base de datos
```