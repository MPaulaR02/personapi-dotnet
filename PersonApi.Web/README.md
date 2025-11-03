# PersonAPI - Laboratorio Arquitectura de Software (.NET + Docker + SQL Server)

## Descripción
Este proyecto corresponde al **Laboratorio 1 de Arquitectura de Software**, cuyo objetivo es implementar un sistema 
monolítico basado en el patrón **Modelo–Vista–Controlador (MVC)** utilizando **.NET 8**, **SQL Server 2022** y **Docker Compose**.

El sistema gestiona información relacionada con personas, profesiones, estudios y teléfonos.  
Incluye endpoints REST documentados con **Swagger 3**, y utiliza **Entity Framework Core 8** como ORM.

---

## Tecnologías
- **.NET 8 (ASP.NET Core MVC)**
- **Microsoft SQL Server 2022 (Developer Edition)**
- **Entity Framework Core 8.0.10**
- **Swagger (Swashbuckle)**
- **Docker y Docker Compose**

---

## Configuración del entorno con Docker

### 1 Levantar SQL Server
Desde la raíz del proyecto ejecutar:

```bash
docker compose up -d
