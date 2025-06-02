# TaskManagerSolution

## Descripción
TaskManagerSolution es una solución modular desarrollada en .NET que incluye una API, lógica de negocio y una infraestructura base para gestionar tareas. Actualmente, el proyecto está en desarrollo y se encuentra completo hasta el servidor con Blazor, pero aún no se ha integrado una interfaz de usuario final.

## Estructura del Proyecto
- **TaskManager.API**: Contiene la API REST para la gestión de tareas.
- **TaskManager.Core**: Contiene las entidades y la lógica de negocio principal.
- **TaskManager.Infrastructure**: Gestión de datos y conexión a base de datos.
- **TaskManager.UI**: Proyecto Blazor Server (pendiente de implementación de la UI).

## Características
- Arquitectura limpia por capas (API, Core, Infrastructure).
- Autenticación y autorización básica (Pendiente).
- Operaciones CRUD para tareas.
- Preparado para futura expansión con interfaz web o móvil.

## Contribuciones
¡Bienvenidas! Si quieres aportar, crea un fork y un pull request.

🗄️ Base de Datos
Este proyecto utiliza SQL Server como sistema gestor de base de datos. A continuación se describen las tablas principales incluidas en el script TaskManagerSchema.sql.

📋 Tabla: Users
Contiene la información de los usuarios registrados en el sistema.

Columna	Tipo	Descripción
Id	int IDENTITY	Clave primaria autoincremental
Username	nvarchar(max)	Nombre de usuario
Email	nvarchar(max)	Correo electrónico (opcional)
PasswordHash	nvarchar(max)	Contraseña hasheada
CreatedAt	datetime2(7)	Fecha y hora de creación (default fijo)
Role	nvarchar(max)	Rol del usuario (valor por defecto vacío)

✅ Tabla: Tasks
Almacena las tareas creadas por los usuarios.

Columna	Tipo	Descripción
Id	int IDENTITY	Clave primaria autoincremental
Title	nvarchar(max)	Título de la tarea
Description	nvarchar(max)	Descripción de la tarea
IsCompleted	bit	Estado de la tarea (completada o no)
DueDate	datetime2(7)	Fecha límite para completar la tarea
UserId	int	ID del usuario propietario (clave foránea)
Role	nvarchar(max)	Rol del usuario (valor por defecto vacío)

🔗 Relación:
Cada tarea está relacionada con un usuario mediante la columna UserId, con una restricción de clave foránea que aplica ON DELETE CASCADE.

🛠️ Tabla: __EFMigrationsHistory
Tabla interna utilizada por Entity Framework para rastrear las migraciones aplicadas.

Columna	Tipo	Descripción
MigrationId	nvarchar(150)	Identificador único de la migración
ProductVersion	nvarchar(32)	Versión de EF Core usada

📥 Importar el esquema
Puedes importar el esquema ejecutando el archivo .sql desde SQL Server Management Studio (SSMS) o mediante el comando:

sql
Copiar
Editar
:r Database/TaskManagerSchema.sql
Asegúrate de tener permisos para crear bases de datos.

## Instalación
Clona el repositorio y abre la solución `TaskManagerSolution.sln` en Visual Studio.

```bash
git clone https://github.com/bradparedes/TaskManagerSolution.git

## Licencia
Este proyecto está bajo la licencia MIT.
