Ejercicio Pasante de TI

Kevin Cárdenas

1.- Crear un servicio web de tipo restful en formato json en cualquier
tecnología o framework de su elección, que reciba como parámetros nombre
de usuario y contraseña (lógica de login), en base a los parámetros de
entrada,  validar contra una base de datos y determinar si las
credenciales son correctas o no.

La Api rest se creo mediante el framework de ASP.NET mediante el uso de
una base de datos creada en SQL. Se debe ejecutar primero el Script de la base de datos.
El archivo es script.sql

![Tabla Descripción generada
automáticamente](./media/image1.png){width="3.7838604549431323in"
height="1.6168919510061242in"}

![](./media/image2.png){width="6.1375in" height="4.161111111111111in"}

Se creo un registro con un usuario y contraseña. Usuario:
<usuario@usuario.com> y contraseña: 12345\*.

El Api contiene todos los métodos GET, POST, DELETE que autogenera el
Framework.

Para ver el contenido del api se debe ingresar a la url:
<https://localhost:7195/api/Usuarios>

![Interfaz de usuario gráfica, Texto, Aplicación Descripción generada
automáticamente](./media/image3.png){width="6.1375in"
height="1.4659722222222222in"}

2.- Generar una pantalla de login en cualquier tecnología o framework
javascript (usuario y contraseña), dónde pueda realizar el consumo del
servicio web creado previamente y se permita saber si las credenciales
son correctas o no.

Para el login se utilizó un servicio html el hace la comparación de las
cadenas y saber si el usuario es el correcto.

Para ingresar al login se debe dirigir a la url:
<https://localhost:7195/Login.html>

E ingresar los datos.

![Interfaz de usuario gráfica, Texto, Aplicación Descripción generada
automáticamente](./media/image4.png){width="6.1375in"
height="2.222916666666667in"}
