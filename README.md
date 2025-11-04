#PersonAPI - ASP.NET Core MVC with Docker & SQL Server

# 📋 Descripción

Sistema de gestión de información de personas implementado con arquitectura Modelo-Vista-Controlador (MVC) utilizando .NET 8, SQL Server 2022 y Docker. El sistema gestiona información relacionada con personas, profesiones, estudios y teléfonos.

## DDL y DML

en la carpeta scripts se escuentran estos archivos como example.sql y init.sql

# 🛠️ Tecnologías

.NET 8 (ASP.NET Core MVC)

Microsoft SQL Server 2022 (Developer Edition)

Entity Framework Core 9.0.10

Swagger 9.0.6 (Documentación de API)

Docker y Docker Compose

Bootstrap 5 (Frontend)

# 🔧 Compilación y Ejecución
### Navegar a la carpeta del proyecto
cd PersonApi.Web

### Construir y levantar todos los contenedores
docker-compose up --build

### O en segundo plano
docker-compose up --build -d

## Servicios Disponibles
Una vez levantados los contenedores:

Aplicación Web (MVC): http://localhost:5000

Swagger UI (API Docs): http://localhost:5000/swagger

SQL Server: localhost:1433
