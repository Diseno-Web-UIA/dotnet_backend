# Backend API - .NET 8

Este proyecto es una API REST desarrollada con ASP.NET Core 8.0 que proporciona servicios backend para la aplicación web.

## Tecnologías Utilizadas

- **.NET 8.0** - Framework de desarrollo
- **ASP.NET Core** - Framework web
- **Swagger/OpenAPI** - Documentación de API
- **Docker** - Containerización
- **Entity Framework Core** - ORM (cuando se implemente)

## Prerrequisitos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (opcional, para containerización)

## Instalación y Configuración

### 1. Clonar el repositorio
```bash
git clone <url-del-repositorio>
cd dotnet_backend
```

### 2. Restaurar dependencias
```bash
dotnet restore
```

### 3. Configurar variables de entorno
Copia el archivo `appsettings.Development.json` y ajusta las configuraciones según tu entorno:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "tu-cadena-de-conexion-aqui"
  }
}
```

## Ejecutar el Proyecto

### Modo Desarrollo
```bash
dotnet run
```

La API estará disponible en:
- **API**: https://localhost:7001
- **Swagger UI**: https://localhost:7001/swagger

### Modo Producción
```bash
dotnet run --environment Production
```

## Testing

### Ejecutar pruebas unitarias
```bash
dotnet test
```

### Ejecutar pruebas de integración
```bash
dotnet test --filter "Category=Integration"
```

## Build y Deployment

### Construir para producción
```bash
dotnet build --configuration Release
```

### Publicar la aplicación
```bash
dotnet publish --configuration Release --output ./publish
```

## Docker

### Construir imagen Docker
```bash
docker build -t backend-api .
```

### Ejecutar contenedor
```bash
docker run -p 8080:80 backend-api
```

### Usando Docker Compose
```bash
docker-compose up -d
```

## Estructura del Proyecto

```
dotnet_backend/
├── Controllers/           # Controladores de la API
│   └── WeatherForecastController.cs
├── Models/               # Modelos de datos (cuando se implementen)
├── Services/             # Servicios de negocio (cuando se implementen)
├── Data/                 # Contexto de base de datos (cuando se implemente)
├── Program.cs           # Punto de entrada de la aplicación
├── appsettings.json     # Configuración de la aplicación
├── backend.csproj       # Archivo de proyecto
└── Dockerfile           # Configuración de Docker
```

## Configuración

### Configuración de CORS
Para permitir peticiones desde el frontend, asegúrate de configurar CORS en `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
```

### Configuración de Swagger
La documentación de la API está disponible automáticamente en modo desarrollo en `/swagger`.

## Endpoints Disponibles

### Weather Forecast
- `GET /WeatherForecast` - Obtener pronóstico del tiempo

## Seguridad

- **HTTPS**: Habilitado por defecto en producción
- **Autenticación**: Configurar según requerimientos (JWT, OAuth, etc.)
- **Autorización**: Implementar según necesidades del proyecto

## Logging

El proyecto utiliza el sistema de logging integrado de .NET. Los logs se configuran en `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Deployment

### Azure
```bash
az webapp up --name tu-app-name --resource-group tu-resource-group
```

### AWS
```bash
dotnet lambda deploy-function
```

### Heroku
```bash
heroku create
git push heroku main
```

## Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## Soporte

Si tienes problemas o preguntas:

1. Revisa la [documentación de .NET](https://docs.microsoft.com/dotnet/)
2. Busca en [Stack Overflow](https://stackoverflow.com/questions/tagged/asp.net-core)
3. Abre un issue en el repositorio

## Actualizaciones

Para mantener el proyecto actualizado:

```bash
dotnet list package --outdated
dotnet add package [nombre-del-paquete] --version [version]
```

---

**Nota**: Este README se actualizará conforme el proyecto evolucione y se agreguen nuevas funcionalidades.