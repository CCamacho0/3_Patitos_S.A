CREATE DATABASE tres_Patitos; 
USE tres_Patitos;

CREATE TABLE	Categoria(
	Id_Categoria INT IDENTITY (1,1) NOT NULL,
	Nombre_Categoria VARCHAR(20) NOT NULL,
	Descuento NUMERIC (5, 2) NOT NULL,
	PRIMARY KEY (Id_Categoria));

CREATE TABLE	Estado_Usuario(
	Id_Estado_Usuario INT IDENTITY (1,1) NOT NULL,
	Nombre_Estado VARCHAR(20) NOT NULL,
	PRIMARY KEY (Id_estado_usuario));

CREATE TABLE	Tipo_Entrega(
	ID_Entrega INT IDENTITY (1,1) NOT NULL,
	Nombre_Entrega VARCHAR(20) NOT NULL,
	PRIMARY KEY (Id_Entrega));

CREATE TABLE	Estado_productos(
	ID_Estado INT IDENTITY (1,1) NOT NULL,
	Nombre_estado VARCHAR(20) NOT NULL,
	PRIMARY KEY (ID_Estado));

CREATE TABLE	Estado_pedido(
	ID_Estado_Pedido INT IDENTITY (1,1) NOT NULL,
	Nombre_Estado_Pedido VARCHAR(75) NOT NULL,
	PRIMARY KEY (ID_Estado_Pedido));

CREATE TABLE	Ubicacion(
	Id_Ubicacion INT IDENTITY (1,1) NOT NULL,
	Nombre_Ubicacion VARCHAR(45) NOT NULL,
	PRIMARY KEY (Id_ubicacion));

CREATE TABLE	Rol(
	Id_Rol INT IDENTITY (1,1) NOT NULL,
	Nombre_Rol VARCHAR(20) NOT NULL,
	PRIMARY KEY (Id_Rol));

CREATE TABLE	Persona(
	Id_Persona INT NOT NULL,
	Id_Rol INT NOT NULL,
	Nombre_Persona VARCHAR(45) NOT NULL,
	Direccion VARCHAR(150) NOT NULL,
	Fecha_Nacimiento DATE NOT NULL,
	Telefono INT NOT NULL,
	Correo VARCHAR (45) NOT NULL,
	Contrasena NVARCHAR(256) NOT NULL,
	Id_estado_usuario INT NOT NULL,
	Id_Categoria INT NOT NULL,
	PRIMARY KEY (ID_Persona),
	FOREIGN KEY (Id_estado_usuario) REFERENCES Estado_usuario(Id_estado_usuario),
	FOREIGN KEY (Id_Categoria) REFERENCES Categoria(Id_categoria),
	FOREIGN KEY (Id_Rol) REFERENCES Rol(Id_Rol));

CREATE TABLE	Productos (
	ID_Producto INT NOT NULL,
	Nombre_producto NVARCHAR(45) NOT NULL,
	Cantidad INT NOT NULL,
	ID_Estado INT NOT NULL,
	Precio NUMERIC(10,2) NOT NULL,
	Id_Ubicacion INT NOT NULL,
	Imagen VARBINARY(MAX) NOT NULL,
	Detalles VARCHAR(250) NOT NULL,
	PRIMARY KEY (ID_Producto),
	FOREIGN KEY (ID_Estado) REFERENCES Estado_productos(ID_Estado),
	FOREIGN KEY (Id_Ubicacion) REFERENCES Ubicacion(Id_ubicacion));

CREATE TABLE	Pedidos(
	Id_pedido INT IDENTITY (1,1) NOT NULL,
	ID_Producto INT NOT NULL,
	ID_Persona INT NOT NULL,
	ID_Entrega INT NOT NULL,
	Cantidad INT NOT NULL,
	Precio DECIMAL NOT NULL,
	ID_Estado_Pedido INT NOT NULL,
	PRIMARY KEY (Id_pedido),
	FOREIGN KEY (Id_persona) REFERENCES Persona(ID_Persona),
	FOREIGN KEY (Id_entrega) REFERENCES Tipo_Entrega(Id_Entrega),
	FOREIGN KEY (Id_estado_pedido) REFERENCES Estado_pedido(ID_Estado_Pedido),
	FOREIGN KEY (ID_Producto) REFERENCES Productos(ID_Producto));

INSERT INTO Categoria(Nombre_categoria, Descuento)
VALUES ('Cliente Regular');

INSERT INTO Estado_usuario(Nombre_estado)
VALUES ('Activo'),
	   ('Eliminado');

INSERT INTO Rol(Nombre_Rol)
VALUES ('Cliente'),
	   ('Administador');