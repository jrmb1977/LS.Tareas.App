La solución contiene dos proyectos:

1. LS.Tareas.Api
2. LS.Tareas.App

El primer proyecto es el backend de la solución que maneja la conexion a la base de datos y de realizar las operaciones CRUD.  Se lo debe invocar de la siguiente manera:

GET https://localhost:44368/api/values [para obtener todas las tareas]
GET https://localhost:44368/api/values/5 [para obtener una tarea en particular]
POST https://localhost:44368/api/values [para crear una tarea en particular (el objeto tarea se envia en el body)]
PUT https://localhost:44368/api/values [para modificar una tarea en particular (el objeto tarea se envia en el body)]
PUT https://localhost:44368/api/values/5 [para actualizar el status de una tarea de NO completada a completada]
DELETE https://localhost:44368/api/values/5 [para eliminar una tarea en particular]

El segundo proyecto es el frontend de la solución que provee los formularios (vistas) de consulta, modificacion y eliminación de las tareas.  
Se realizan las operaciones CRUD invocando a los metodos del API disponibles en el proyecto 1.
