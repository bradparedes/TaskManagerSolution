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

## Instalación
Clona el repositorio y abre la solución `TaskManagerSolution.sln` en Visual Studio.

```bash
git clone https://github.com/bradparedes/TaskManagerSolution.git
## Contribuciones
¡Bienvenidas! Si quieres aportar, crea un fork y un pull request.

## Licencia
Este proyecto está bajo la licencia MIT.
