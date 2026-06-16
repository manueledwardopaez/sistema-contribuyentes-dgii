# Sistema de Gestión de Contribuyentes

Este proyecto es una solución integral para la gestión y consulta de **Contribuyentes** y **Comprobantes Fiscales**. Esta solucion se divide en dos partes, una API REST y una interfaz de usuario (frontend), ambas divididas en componentes en contenedores de Docker. 

## 🚀 Arquitectura y Tecnologías

El proyecto sigue una **Clean Architecture** (Arquitectura Limpia) en el lado del backend para garantizar un bajo acoplamiento y alta cohesión. Está dividido en contenedores de Docker.

### Backend (`/backend`)
Construido en **.NET 8 (ASP.NET Core Web API)**. El código fuente está dividido en 4 capas principales:

- **1. Dgii.Domain (Dominio):** Contiene las entidades principales de negocio (`Taxpayer` y `TaxReceipt`). Esta capa no tiene dependencias hacia ninguna otra capa ni frameworks externos.
- **2. Dgii.Application (Aplicación):** Contiene la lógica de negocio (`TaxpayerService`), los DTOs y define las interfaces de los repositorios (`ITaxpayerRepository`, etc.).
- **3. Dgii.Infrastructure (Infraestructura):** Se encarga del acceso a datos. Implementa los repositorios y configura Entity Framework Core (`ApplicationDbContext`), gestionando también las migraciones.
- **4. Dgii.Api (Presentación):** Capa REST. Define los controladores, la inyección de dependencias (DI), configuración de CORS, logs estructurados (Serilog) y la documentación de Swagger.

**Pruebas Unitarias:** Se incluye el proyecto `Dgii.Tests` basado en xUnit y Moq.

### 🔌 Endpoints de la API
El controlador principal `TaxpayersController` expone los siguientes endpoints:

- `GET /api/Taxpayers`: Devuelve un listado de contribuyentes de forma paginada (parámetros `pageNumber` y `pageSize`).
- `GET /api/Taxpayers/receipts`: Obtiene la lista completa de todos los comprobantes fiscales almacenados.
- `GET /api/Taxpayers/{rncCedula}`: Consulta el detalle de un contribuyente específico mediante su RNC o Cédula. Retorna sus datos, lista de comprobantes asociados y la suma total matemática del ITBIS.

### 🛡️ Patrones de Diseño y Construcción
- **Repository Pattern:** Para abstraer la lógica de acceso a la base de datos.
- **Dependency Injection (DI):** Implementada nativamente en el ecosistema .NET.
- **Manejo Global de Excepciones:** Uso de un `GlobalExceptionMiddleware` para estandarizar las respuestas de error y no exponer _stack traces_ en producción.
- **Negociación de Contenido:** La API soporta formato JSON y XML nativamente mediante `[Produces]`.

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
5. **Paginación de Resultados:** Navegación optimizada mediante páginas en el listado de contribuyentes, preservando el estado de la página al navegar entre vistas.
---

## 📸 Vista Previa

![Directorio de Contribuyentes](frontend/public/demo.png)

![Detalles y referencias](frontend/public/demo-2.png)

---

## 🛠️ Cómo ejecutar el proyecto (Localmente)

El proyecto está dockerizado para que sea muy fácil de levantar sin necesidad de instalar dependencias locales (como el SDK de .NET, Node.js o SQL Server). 

### Requisitos Previos
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose V2](https://docs.docker.com/compose/install/)

### Pasos para iniciar:

1. **Clonar el repositorio:** Abre tu terminal y ejecuta el siguiente comando para descargar el proyecto en tu máquina local. Luego, entra a la carpeta del proyecto:

```bash
git clone https://github.com/manueledwardopaez/sistema-contribuyentes-dgii.git
cd sistema-contribuyentes-dgii
```

2. **Ejecutar el proyecto:** Levanta la aplicación completa ejecutando el siguiente comando para construir las imágenes y arrancar los contenedores en segundo plano:

```bash
docker compose up -d --build
```

3. **Acceder a los servicios:** Una vez los contenedores estén arriba (la base de datos SQL Server puede tardar un poco la primera vez), podrás acceder a la aplicación desde tu navegador:

- **🌐 Frontend (Aplicación Web):** [http://localhost:3000](http://localhost:3000)
- **📖 Documentación API (Swagger):** [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **⚙️ Backend (API REST - Base):** [http://localhost:5000/api](http://localhost:5000/api)
- **🗄️ Base de Datos:** Puerto `1433` (Credenciales: usuario `SA` / contraseña `Dgii_Password123!`)

### Para detener el proyecto:

```bash
docker compose down
```

