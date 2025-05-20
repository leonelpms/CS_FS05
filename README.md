 UserManagementAPI

 
UserManagementAPI es una API RESTful desarrollada en ASP.NET Core (.NET 9) para la gestión de usuarios, orientada a uso interno empresarial.
Funcionalidades principales
•CRUD de usuarios:
	Permite crear, consultar, actualizar y eliminar usuarios mediante endpoints REST (/api/users).
•Persistencia simple:
	Los usuarios se almacenan en memoria y pueden guardarse/cargarse desde un archivo users.json.
•Validación de datos:
	Se valida que los nombres no estén vacíos y que los correos electrónicos sean válidos antes de aceptar datos de usuario.
•Gestión de errores:
	El middleware captura excepciones no gestionadas y devuelve respuestas de error consistentes en formato JSON.
•Autenticación JWT:
	Los endpoints están protegidos mediante autenticación basada en tokens JWT. Se provee un endpoint para generar tokens de prueba (/api/auth/token).
•	Registro de auditoría:
	El middleware registra todas las solicitudes y respuestas para fines de auditoría y monitoreo.

Arquitectura y componentes
•	Controladores:
•	UsersController: expone los endpoints CRUD y la persistencia en archivo.
•	AuthController: permite generar tokens JWT de prueba.
•	Modelo:
•	User: define la estructura de los datos de usuario con validaciones.
•	Middleware personalizado:
•	ErrorHandlingMiddleware: manejo global de errores.
•	TokenAuthenticationMiddleware: validación de tokens JWT.
•	LoggingMiddleware: registro de solicitudes y respuestas.
•	Configuración de la canalización:

El orden del middleware garantiza primero la gestión de errores, luego la autenticación y finalmente el registro.
Seguridad
•	Todos los endpoints (excepto el de generación de token) requieren un token JWT válido en el header Authorization.

Uso típico
1.	Solicitar un token JWT en /api/auth/token.
2.	Usar el token para autenticar solicitudes a /api/users.
3.	Realizar operaciones CRUD sobre usuarios.
4.	Guardar los cambios en disco usando /api/users/save-to-file.




user: usertest
token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIiLCJleHAiOjE3NDc3NzUxMDcsImlzcyI6IlRlc3RJc3N1ZXIiLCJhdWQiOiJUZXN0QXVkaWVuY2UifQ.gy-GhmdOMfUWOn0piFZPCO-HA_XYaH__fOwdQIggf9s

