CREATE DATABASE GYMDB
USE GYMDB

CREATE TABLE TipoUsuario
(
	id INT IDENTITY,
	nombre VARCHAR(20),
	tipousuario CHAR,
	CONSTRAINT tipousuario_pkey PRIMARY KEY (id)
)

CREATE TABLE Usuario
(
	id INT IDENTITY,
	nombre VARCHAR(20),
	apaterno VARCHAR(20),
	amaterno VARCHAR(20),
	apodo VARCHAR(20),
	pin VARCHAR(20),
	imgpath VARCHAR(250),
	correo VARCHAR(150),
	tipousuario_id INT,
	CONSTRAINT usuario_pkey PRIMARY KEY (id),
	CONSTRAINT tipousuario_fkey FOREIGN KEY (tipousuario_id) REFERENCES TipoUsuario(id)
)

CREATE TABLE Cliente
(
	usuario_id INT,
	fnacimiento DATE,
	peso DECIMAL,
	estatura INT,
	CONSTRAINT cliente_pkey PRIMARY KEY (usuario_id),
	CONSTRAINT usuario_fkey FOREIGN KEY (usuario_id) REFERENCES Usuario(id)
)

CREATE TABLE Ejercicio
(
	id INT IDENTITY,
	nombre VARCHAR(30),
	descripcion TEXT,
	CONSTRAINT ejercicio_pkey PRIMARY KEY (id)
)

CREATE TABLE Rutina
(
	id INT IDENTITY,
	nombre VARCHAR(30),
	dia VARCHAR(15),
	repeticiones INT,
	peso DECIMAL,
	ejercicio_id INT,
	usuario_id INT,
	CONSTRAINT rutina_pkey PRIMARY KEY (id),
	CONSTRAINT ejercicio_fkey FOREIGN KEY (ejercicio_id) REFERENCES Ejercicio(id),
	CONSTRAINT usuariorutina_fkey FOREIGN KEY (usuario_id) REFERENCES Usuario(id)
)

-- DROPS

DROP TABLE Cliente
DROP TABLE Usuario
DROP TABLE TipoUsuario
DROP TABLE Rutina
DROP TABLE Ejercicio

-- INSERTS

INSERT INTO TipoUsuario VALUES 
('Entrenador','A'),
('Cliente','C')

INSERT INTO Usuario VALUES 
('Usuario2', 'APaterno', 'AMaterno','root','1234','C:\Users\cueva\Downloads\user.jpg','correo@outlook.com',1),
('Jose Luis', 'Cuevas', 'Landa','admin','1234','EstoEsUnDirectorio','cuevas.joseluis@outlook.com',1),

INSERT INTO Cliente VALUES
(1,'03/04/1997',60.50,160)

--SELECTS

-- // Login //
SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario
	   INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id 
