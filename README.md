# TaskManagerSolution

## Descripción
TaskManagerSolution es una aplicación para la gestión de tareas que incluye API, núcleo de lógica, infraestructura y una interfaz de usuario.  
Está desarrollada en .NET con arquitectura limpia y modular para facilitar su mantenimiento y escalabilidad.

## Estructura del proyecto
- **TaskManager.API**: Proyecto para la API REST que expone los servicios de gestión de tareas.
- **TaskManager.Core**: Lógica del negocio y entidades principales.
- **TaskManager.Infrastructure**: Implementación de acceso a datos y persistencia.
- **TaskManager.UI**: Interfaz gráfica para la gestión de tareas.
- **TaskManagerSolution.sln**: Solución principal que agrupa todos los proyectos.

## Tecnologías utilizadas
- .NET 8 (o la versión que uses)
- Entity Framework Core
- ASP.NET Core Web API
- C#
- SQL Server (u otro motor de base de datos si usas)

## Cómo ejecutar
1. Clona el repositorio:  
   `git clone https://github.com/bradparedes/TaskManagerSolution.git`
2. Abre la solución en Visual Studio 2022 o superior.
3. Restaura los paquetes NuGet.
4. Configura la cadena de conexión en el archivo `appsettings.json`.
5. Ejecuta la solución desde el proyecto `TaskManager.API` para iniciar la API.
6. Ejecuta el proyecto `TaskManager.UI` para abrir la interfaz de usuario.

## Contribuciones
¡Bienvenidas! Si quieres aportar, crea un fork y un pull request.

## Licencia
Este proyecto está bajo la licencia MIT.
