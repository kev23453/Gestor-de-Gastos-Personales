CREATE TABLE usuarios(
	id int primary key identity(1,1),
	username varchar(250) not null,
	email varchar(150) not null,
	password varchar(500) not null
)

CREATE TABLE metodoPago(
	id INT PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(250)
)

CREATE TABLE Categoria(
	id int primary key identity(1,1),
	descripcion varchar(150),
	presupuesto money not null,
	id_usuario int
)

CREATE TABLE Gasto(
	id int primary key identity(1,1),
	Monto money not null,
	Fecha DATE DEFAULT CURRENT_TIMESTAMP,
	CategoriaId INT,
	metodoPagoId int,
	Descripcion varchar(1000),
	usuarioId int
)