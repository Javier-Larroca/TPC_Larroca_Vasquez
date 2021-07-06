CREATE DATABASE LAVE
GO

USE LAVE
GO


CREATE TABLE TIPO_USUARIOS(
ID INT NOT NULL PRIMARY KEY,
DESCRIPCION VARCHAR(20) NOT NULL UNIQUE,
)
GO

CREATE TABLE ADM_USUARIOS(
IDUSUARIO INT NOT NULL PRIMARY KEY IDENTITY (1,1),
TIPO_USUARIO INT NOT NULL FOREIGN KEY REFERENCES TIPO_USUARIOS(ID),
FECHA_ALTA DATE NOT NULL,
FECHA_BAJA DATE NULL,
ESTADO BIT NOT NULL
)
GO

CREATE TABLE OBRAS_SOCIALES(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
DESCRIPCION VARCHAR(40) NOT NULL
)
GO

CREATE TABLE PACIENTES(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
IDOBRASOCIAL INT FOREIGN KEY REFERENCES OBRAS_SOCIALES(ID),
NOMBRE VARCHAR(40) NOT NULL,
APELLIDO VARCHAR(40) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL,
FECHA_ALTA DATE NOT NULL,
FECHA_BAJA DATE NULL,
FECHA_NAC DATE NOT NULL,
ESTADO BIT NOT NULL
)
GO


CREATE TABLE MEDICOS(
ID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES ADM_USUARIOS(IDUSUARIO),
NOMBRE VARCHAR(40) NOT NULL,
APELLIDO VARCHAR(40) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL,
MATRICULA INT NOT NULL UNIQUE
)
GO


CREATE TABLE ESPECIALIDADES(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
DESCRIPCION VARCHAR(40) NOT NULL
)
GO

CREATE TABLE ESPECIALIDADES_POR_MEDICOS(
IDESPECIALIDAD INT NOT NULL FOREIGN KEY REFERENCES ESPECIALIDADES(ID),
IDMEDICO INT NOT NULL FOREIGN KEY REFERENCES MEDICOS(ID),
PRIMARY KEY(IDESPECIALIDAD, IDMEDICO)
)
GO

--Procedimiento para listar especialidades por medico
CREATE PROCEDURE pEspecialidadesPorMedico(@idMedico INT)
AS
BEGIN
SELECT esp.DESCRIPCION
FROM ESPECIALIDADES esp
INNER JOIN ESPECIALIDADES_POR_MEDICOS espMed ON espMed.IDESPECIALIDAD = esp.ID
WHERE espMed.IDMEDICO = @idMedico
END
GO

--Vista para listar especialidades
CREATE VIEW vEspecialidadesPorMedico
AS
SELECT espMed.IDMEDICO, esp.DESCRIPCION
FROM ESPECIALIDADES esp
INNER JOIN ESPECIALIDADES_POR_MEDICOS espMed ON espMed.IDESPECIALIDAD = esp.ID
GO

CREATE VIEW vDetallesPorMedico
AS
SELECT med.ID, med.NOMBRE, med.APELLIDO, med.CONTACTO, med.MATRICULA, admUsua.FECHA_ALTA
FROM ADM_USUARIOS admUsua
INNER JOIN MEDICOS med ON med.ID = admUsua.IDUSUARIO
WHERE admUsua.ESTADO = 1;
GO

-- Procedimiento para el alta de medico desde aplicación, asignando día y estado automaticamente sin parametro.
CREATE PROCEDURE pAltaDeMedico(
@mNombre VARCHAR (40),
@mApellido VARCHAR (40),
@mMail VARCHAR(50),
@mMatricula INT
)
AS 
BEGIN
--Manejo de errores
BEGIN TRY 
--Comenzamos la transaccion
BEGIN TRANSACTION
--Antes de insertar en Medicos, vamos directo a ADM_USUARIOS para lograr generar un IDUSUARIO
DECLARE @tipoUsuario INT
SET @tipoUsuario = (SELECT ID FROM TIPO_USUARIOS WHERE UPPER(DESCRIPCION) = 'MEDICO')

IF @tipoUsuario IS NULL BEGIN
	RAISERROR('No existe ningún ID para el tipo de usuario que quiere dar de alta', 16, 1)
	END

INSERT INTO ADM_USUARIOS (TIPO_USUARIO, FECHA_ALTA, ESTADO) VALUES (@tipoUsuario, GETDATE(), 1)
DECLARE @idMedico INT
SET @idMedico = @@IDENTITY

--Si no se pudo insertar registro en ADM_USUARIOS, cancelamos el alta
IF(SELECT COUNT(*) FROM ADM_USUARIOS WHERE IDUSUARIO = @idMedico) != 1 BEGIN
	RAISERROR('Ocurrio un error al insertar usuario en ADM_USUARIO', 16,1)
	END
--Si se dio el alta en ADM_USUARIOS, procedemos a dar el alta en MEDICOS
INSERT INTO MEDICOS (ID, NOMBRE, APELLIDO, CONTACTO, MATRICULA) VALUES (@idMedico, @mNombre, @mApellido, @mMail, @mMatricula)

--Ejecutamos COMMIT de la transaccion
COMMIT TRANSACTION
END TRY

--Empieza Catch, cualquier error generado anterior, se imprimirá y se hará rollback de la transaccion
BEGIN CATCH
PRINT ERROR_MESSAGE()
ROLLBACK TRANSACTION
END CATCH
--Finaliza maneja de errores
END
GO

-- Procedimiento para dar de baja un medico desde aplicación, asignando automaticamente fecha de baja
CREATE PROCEDURE pBajaDeMedico(@idMedico INT)
AS 
BEGIN
UPDATE ADM_USUARIOS SET FECHA_BAJA = GETDATE(), ESTADO = 0 WHERE IDUSUARIO = @idMedico 
END
GO

-- Valores a insertar para prueba
INSERT INTO ESPECIALIDADES (DESCRIPCION) VALUES ('Odontologia'), ('Cardiologia'), ('Urologia'), ('Administración y Gestión en enfermería'), ('Alergia e Inmunología'), ('Cirugía de Torax'), ('Cirugia general'), ('Cirugia Pediatrica'), ('Cirugia plastica y reparadora'), ('Cirugia vascular periférica'), ('Clinica Medica'), ('Dermatologia'), ('Diabetología'), ('Endocrinologia'), ('Gastroenterología'), ('Infectología'), ('Neumonología'), ('Neurología'), ('Nutriología'), ('Oftalmología'), ('Oncología'), ('Traumotología'), ('Otorrinolaringología'), ('Patología'), ('Pediatría'), ('Psiquiatría')
GO

INSERT INTO TIPO_USUARIOS VALUES (1,'Soporte'), (2, 'Recepcionista'), (3,'Medico'), (4,'Paciente')
GO

EXECUTE pAltaDeMedico 'Elmer','Vasquez','elmerr.vasquez@gmail.com', 123456
GO

EXECUTE pAltaDeMedico 'Agustin','Larroca', 'agustin.larroca@gmail.com', 789123 
GO

EXECUTE pAltaDeMedico 'Roberto', 'Gonzales','robertogonzales@gmail.com', 142365
GO

INSERT INTO ESPECIALIDADES_POR_MEDICOS VALUES (1,1), (2,1), (3,1)
GO

INSERT INTO OBRAS_SOCIALES (DESCRIPCION) VALUES ('Galeno'), ('OSDE'), ('Swiss Medical')
GO

SET DATEFORMAT DMY;

INSERT INTO PACIENTES (IDOBRASOCIAL, NOMBRE, APELLIDO,CONTACTO,FECHA_NAC, FECHA_ALTA, ESTADO) VALUES (1,'Juan','Castro','Juan.Castro@gmail.com', '16/01/1998', GETDATE(), 1), (3,'Gabriel','Reinoso', 'Gabriel.Reinoso@gmail.com', '13/01/1999', GETDATE(), 1), (1,'Agustin', 'Catalan','agustincatalan@gmail.com', '01/06/1998', GETDATE(), 1)
GO

