
# Employee Catalog Management System

Este es un sistema de gestión de empleados que permite la **creación**, **edición**, **eliminación** y **consulta** de empleados, con roles diferenciados para **Admin** y **User**. El proyecto está desarrollado con **Angular** en el frontend y **.NET Core** en el backend, con autenticación basada en **JWT**.

## Características

- **Autenticación y autorización** usando JWT.
- **Roles de usuario**:
  - **Admin**: Puede crear, editar, eliminar empleados.
  - **User**: Puede consultar, filtrar y exportar empleados.
- **CRUD de empleados**.
- **Paginación y filtrado** por nombre y posición.
- **Exportación** de empleados a archivo CSV.
- **Validaciones** en formularios.
- UI basada en **Bootstrap** y **Material Design**.

## Requisitos

- **Node.js** v18.19.0 o superior
- **Angular CLI** 18.2.4
- **.NET Core SDK** 3.1 o superior
- **SQL Server** (para la base de datos)
- **Git** (opcional, para clonar el proyecto)

## Instrucciones de instalación y compilación

### 1. Clonar el repositorio

```bash
git clone https://github.com/cristianbest144/EmployeeCatalogTest.git
cd nombre-repositorio
```

### 2. Configuración y ejecución

#### Backend - API en .NET Core

1. Restaurar los paquetes NuGet:

   ```bash
   dotnet restore
   ```

2. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json` dentro del proyecto **EmployeeCatalogApi**:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tu-servidor-sql;Database=EmployeeCatalogDb;User Id=sa;Password=tu-password;"
     },
     "Jwt": {
       "Key": "clave-secreta",
       "Issuer": "http://localhost:4200",
       "Audience": "http://localhost:5000"
     }
   }
   ```

3. Restaura la base de datos con el **backup** proporcionado en la carpeta `BackupDB`:

   - Abre **SQL Server Management Studio (SSMS)**.
   - Restaura el archivo `EmployeeCatalogDb.bak`.

4. Inicia el backend:

   ```bash
   dotnet run --project EmployeeCatalogApi
   ```

   El backend estará corriendo en `http://localhost:5000`.

#### Frontend - Angular

1. Instalar las dependencias de Node.js:

   ```bash
   npm install
   ```

2. Actualizar el archivo `environment.ts` en la carpeta `src/environments` con la URL de la API:

   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5000/api'
   };
   ```

3. Iniciar la aplicación Angular:

   ```bash
   ng serve
   ```

   La aplicación frontend estará disponible en `http://localhost:4200`.

### 3. Usuario de prueba

Para acceder a la aplicación, puedes utilizar los siguientes **usuarios de prueba** ya cargados en la base de datos:

- **Admin**:
  - **Username**: `admin`
  - **Password**: `User12345`

- **User**:
  - **Username**: `user`
  - **Password**: `User12345`

### 4. Funcionalidades clave

- **Login**: Acceso con los roles de **Admin** y **User**.

#### Admin:
- Crear, editar, eliminar empleados.
- Filtrar y exportar empleados a CSV.

#### User:
- Consultar y filtrar empleados.
- Exportar empleados a CSV.

### 5. Pruebas Unitarias

El proyecto incluye **pruebas unitarias** para el frontend, ejecutables con **Jasmine/Karma**:

```bash
ng test
```

### 6. Migraciones y Base de Datos

Si necesitas aplicar migraciones adicionales o cambiar el esquema de la base de datos, puedes utilizar **Entity Framework Core**:

```bash
dotnet ef migrations add NombreDeLaMigracion
dotnet ef database update
```

## Autor

**Cristian Hernandez** - Desarrollador Full Stack
