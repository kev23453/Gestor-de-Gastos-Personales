# KOSTO

## Descripcion

kosto es una api para la gestion de gastos personales construido en el lenguaje de programacion c# como un proyecto web API utilizando la arquitectura <strong>ONION</strong> para dividir las responsabilidades de nuestra aplicacion

## Funcionalidades

El proyecto nos permite gestionar nuestros gastos mediante:

- Categorias: Estas vienen con un presupuesto colocado por el usuario para separar gastos.

- Metodos de pago: Nos permiten registrar con que forma de pago consumimos dicho gasto.

- Gasto: Registramos nuestros gastos con nombre, categoria, metodo de pago y otros datos necesarios.

- Autenticacion: Este proyecto utiliza JSON Web Tokens ( JWT ) para la authenticacion de usuarios.

## Arquitectura

Como mencionamos anteriormente, nuestro proyecto esta basado en la arquitectura ONION en la cual dividimos nuestro proyecto en 4 capas escenciales:

``` 
Kosto/
    Aplicacion/
        Servicios/
        Interfaces/
        DTOs/
        Utils/
        Exepciones/
    Dominio/
        Entidades/
        Excepciones/
    Infraestructura/
        Persistencia/
            Contexto/
            Exepciones/
            Repositorio/
        Seguridad/
    Presentacion/
        Controllers/
        Middlewares/
        Program.cs
```

Cada capa con su responsabilidad asignada que se intercomunican entre si para que la aplicacion pueda funcionar

## Configuraciones para funcionamiento

Para usar esta aplicacion para modificaciones a gusto en local lo unico que tenemos que hacer es bajar este repositorio y una vez lo tengamos y lo queramos ejecutar solo tendremos que colocar en la terminal el comando:

``` c#
Dotnet run
``` 
> **IMPORTANTE:** Debemos tener instalado **dotnet** (Plataforma para construir aplicaciones en c#) en nuestro ordenador.

En el caso de la base de datos lo unico que debemos hacer es irnos a **SQL SERVER**, localizar el archivo con la extension .bak y ejecutar el comando:

``` SQL
RESTORE DATABASE KOSTO
FROM DISK='RUTA_DE_MI_ARCHIVO.BAK'
WITH REPLACE -- en caso de que ya la hubieramos tenido anteriormente
```
Por ultimo nos vamos al archivo de **appsettings.json** y modificamos las siguientes lineas:

🔗 CADENA DE CONEXION

```JSON
"AppConnection": "data source=MY_SERVER; initial catalog=MY_DATABASE; integrated security=true; TrustServerCertificate=true"
```
Aqui colocamos nuestras credenciales, especificamente el nombre de instancia de nuestro gestor de base de datos y el nombre de la base de datos.

🔒 JSON WEB TOKEN

Aqui solo nos dirigimos donde dice **"jwt"** y en esta linea:

```JSON
"Secret": "MI_CLAVE_SUPER_LARGA_Y_SECRETA",
```
La cambiamos por una clave secreta muy larga para que el json web token funcione como esperemos
