Employee Catalog Management System
Este es un sistema de gestión de empleados que permite la creación, edición, eliminación y consulta de empleados, con roles de usuario diferenciados para Admin y User. El proyecto está desarrollado con Angular en el frontend y .NET Core en el backend, con autenticación basada en JWT.

Características
Autenticación y autorización usando JWT.
Roles de usuario:
Admin: Puede crear, editar, eliminar empleados.
User: Puede consultar, filtrar y exportar empleados.
CRUD de empleados.
Paginación y filtrado por nombre y posición.
Exportación de empleados a archivo CSV.
Validaciones en formularios.
UI basada en Bootstrap y Material Design.
Requisitos
Node.js v18.19.0 o superior
Angular CLI 18.2.4
.NET Core SDK 3.1 o superior
SQL Server (para la base de datos)
Git (opcional, para clonar el proyecto)
Instrucciones de instalación y compilación
1. Clonar el repositorio
bash
Copiar código
git clone https://github.com/cristianbest144/EmployeeCatalogTest.git
cd nombre-repositorio
2. Backend - API en .NET Core
Configuración del backend
Restaurar los paquetes NuGet:

bash
Copiar código
dotnet restore
Configura la cadena de conexión a la base de datos en el archivo appsettings.json dentro del proyecto EmployeeCatalogApi:

json
Copiar código
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
Restaura la base de datos con el backup proporcionado en la carpeta BackupDB:

Abre SQL Server Management Studio (SSMS).
Restaura el archivo EmployeeCatalogDb.bak.
Inicia el backend:

bash
Copiar código
dotnet run --project EmployeeCatalogApi
El backend estará corriendo en http://localhost:5000.

3. Frontend - Angular
Configuración del frontend
Instalar las dependencias de Node.js:

bash
Copiar código
npm install
Actualizar el archivo environment.ts en la carpeta src/environments con la URL de la API:

typescript
Copiar código
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
Iniciar la aplicación Angular:

bash
Copiar código
ng serve
La aplicación frontend estará disponible en http://localhost:4200.

4. Usuario de prueba
Para acceder a la aplicación, puedes utilizar los siguientes usuarios de prueba ya cargados en la base de datos:

Admin:
Username: admin
Password: User12345
User:
Username: user
Password: User12345
5. Funcionalidades clave
Login: Acceso con los roles de Admin y User.
Admin:
Crear, editar, eliminar empleados.
Filtrar y exportar empleados a CSV.
User:
Consultar y filtrar empleados.
Exportar empleados a CSV.
6. Pruebas Unitarias
El proyecto incluye pruebas unitarias para el frontend, ejecutables con Jasmine/Karma:

bash
Copiar código
ng test
7. Migraciones y Base de Datos
Si necesitas aplicar migraciones adicionales o cambiar el esquema de la base de datos, puedes utilizar Entity Framework Core:

bash
Copiar código
dotnet ef migrations add NombreDeLaMigracion
dotnet ef database update
Autor
Cristian Hernandez
