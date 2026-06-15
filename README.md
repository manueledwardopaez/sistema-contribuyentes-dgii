# Prueba Técnica - DGII (Analista Programador)

Este proyecto es una solución integral para la gestión y consulta de **Contribuyentes** y **Comprobantes Fiscales**, desarrollada como parte de la prueba técnica para la Dirección General de Impuestos Internos (DGII).

## 🚀 Arquitectura y Tecnologías

El proyecto sigue una arquitectura limpia (Clean Architecture) dividida en componentes en contenedores de Docker.

### Backend (`/backend`)
- **Framework:** .NET 8 (ASP.NET Core Web API)
- **Arquitectura:** Clean Architecture (`Dgii.Api`, `Dgii.Application`, `Dgii.Domain`, `Dgii.Infrastructure`).
- **Pruebas:** Proyecto `Dgii.Tests` incluido.

### Frontend (`/frontend`)
- **Librería/Framework:** React 19 con TypeScript.
- **Build Tool:** Vite.
- **Servidor Web:** Nginx (para servir los archivos estáticos en producción/Docker).
- **Estilos:** CSS Modules / Vanilla CSS con íconos de Lucide React.

### Base de Datos
- **Motor:** SQL Server 2022 (`mcr.microsoft.com/mssql/server:2022-latest`).

---

## ✨ Funcionalidades (Features)

El sistema cuenta con las siguientes características principales:

1. **Listado de Contribuyentes:** Permite visualizar todos los contribuyentes registrados en el sistema.
2. **Listado de Comprobantes Fiscales:** Visualización general de los comprobantes fiscales generados/recibidos.
3. **Detalles de Contribuyente:** Búsqueda y visualización de la información detallada de un contribuyente en específico utilizando su RNC o Cédula.
4. **Diseño Responsivo:** Interfaz adaptable a diferentes tamaños de pantalla.

---

## 🛠️ Cómo ejecutar el proyecto (Localmente)

El proyecto está dockerizado para que sea muy fácil de levantar sin necesidad de instalar dependencias locales (como el SDK de .NET, Node.js o SQL Server). 

### Requisitos Previos
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose V2](https://docs.docker.com/compose/install/)

### Pasos para iniciar:

1. Clona el repositorio o ubícate en la raíz del proyecto.
2. Ejecuta el siguiente comando para construir las imágenes y levantar los contenedores en segundo plano:

```bash
docker compose up -d --build
```
*(Nota: Si usas Linux y tu usuario no está en el grupo `docker`, deberás usar `sudo docker compose up -d --build`)*

3. Una vez los contenedores estén arriba (especialmente la base de datos SQL Server, que puede tardar unos segundos en inicializarse), podrás acceder a los servicios:

- **Frontend (Aplicación Web):** [http://localhost:3000](http://localhost:3000)
- **Backend (API REST):** [http://localhost:5000/api](http://localhost:5000/api)
- **Base de Datos:** Puerto `1433` (Credenciales por defecto en el `docker-compose.yml` - SA / Dgii_Password123!)

### Para detener el proyecto:

```bash
docker compose down
```

*(Nota: Los datos de la base de datos se conservan en un volumen Docker nombrado `sqlserver_data`. Si deseas borrar la base de datos por completo, utiliza `docker compose down -v`).*
