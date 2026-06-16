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
*(Nota: Si usas Linux y tu usuario no está en el grupo `docker`, deberás usar `sudo docker compose up -d --build`)*

3. **Acceder a los servicios:** Una vez los contenedores estén arriba (la base de datos SQL Server puede tardar un poco la primera vez), podrás acceder a la aplicación desde tu navegador:

- **🌐 Frontend (Aplicación Web):** [http://localhost:3000](http://localhost:3000)
- **📖 Documentación API (Swagger):** [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **⚙️ Backend (API REST - Base):** [http://localhost:5000/api](http://localhost:5000/api)
- **🗄️ Base de Datos:** Puerto `1433` (Credenciales: usuario `SA` / contraseña `Dgii_Password123!`)

### Para detener el proyecto:

```bash
docker compose down
```

*(Nota: Los datos de la base de datos se conservan en un volumen Docker nombrado `sqlserver_data`. Si deseas borrar la base de datos por completo, utiliza `docker compose down -v`).*
