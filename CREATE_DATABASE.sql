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
EMAIL VARCHAR(50) NOT NULL,
FECHA_ALTA DATE NOT NULL,
FECHA_MODIFICACION DATE NULL,
FECHA_BAJA DATE NULL,
ID_USUARIO_MODIFICADOR INT NULL,
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
CONTACTO VARCHAR(50) NOT NULL UNIQUE,
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

CREATE TABLE TURNOS_DE_TRABAJO(
IDMEDICO INT NOT NULL FOREIGN KEY REFERENCES MEDICOS(ID),
DIA VARCHAR (10) NOT NULL CHECK(DIA = 'LUNES' OR DIA = 'MARTES' OR DIA = 'MIÉRCOLES' OR DIA = 'JUEVES' OR DIA = 'VIERNES' OR DIA = 'SÁBADO' OR DIA = 'DOMINGO'),
HORARIO_INGRESO VARCHAR(5) NULL,
HORARIO_SALIDA VARCHAR(5) NULL, 
DIA_LIBRE BIT NOT NULL,
PRIMARY KEY (IDMEDICO, DIA)
)
GO

CREATE TABLE TURNOS(
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IDPACIENTE INT NOT NULL FOREIGN KEY REFERENCES PACIENTES(ID),
IDMEDICO INT NOT NULL FOREIGN KEY REFERENCES MEDICOS(ID),
FECHA_TURNO DATE NOT NULL,
HORARIO VARCHAR(5) NOT NULL,
OBSERVACIONES VARCHAR(200) NULL,
ESTADO BIT NOT NULL
)
GO

CREATE TABLE RECEPCIONISTAS(
ID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES ADM_USUARIOS(IDUSUARIO),
NOMBRE VARCHAR(50) NOT NULL,
APELLIDO VARCHAR(50) NOT NULL,
CONTRASEÑA VARCHAR(50) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE SOPORTES(
ID INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES ADM_USUARIOS(IDUSUARIO),
NOMBRE VARCHAR(50) NOT NULL,
APELLIDO VARCHAR(50) NOT NULL,
CONTRASEÑA VARCHAR(50) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL UNIQUE
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
SELECT med.ID, med.NOMBRE, med.APELLIDO, med.CONTACTO, med.MATRICULA, admUsua.FECHA_ALTA, admUsua.FECHA_MODIFICACION
FROM ADM_USUARIOS admUsua
INNER JOIN MEDICOS med ON med.ID = admUsua.IDUSUARIO
WHERE admUsua.ESTADO = 1;
GO

--Lista de turnos con pacientes y medicos 
CREATE VIEW turnosCompletos
AS
SELECT tur.*, pa.NOMBRE + ' ' + pa.APELLIDO AS PACIENTE, med.NOMBRE + ' ' + med.APELLIDO AS MEDICO
FROM 
TURNOS tur
INNER JOIN PACIENTES pa ON pa.ID = tur.IDPACIENTE
INNER JOIN MEDICOS med ON med.ID = tur.IDMEDICO
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

INSERT INTO ADM_USUARIOS (TIPO_USUARIO, EMAIL, FECHA_ALTA, ESTADO) VALUES (@tipoUsuario, @mMail, GETDATE(), 1)
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

CREATE PROCEDURE pModificacionDeMedico(
@mId INT,    
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
--Antes de actulizar Medicos, vamos directo a ADM_USUARIOS a ingresar fecha de modificación
DECLARE @existeUsuario INT
SET @existeUsuario = (SELECT COUNT(*) FROM ADM_USUARIOS WHERE IDUSUARIO = @mId)

IF @existeUsuario != 1 
BEGIN
	RAISERROR('Error al buscar ID de usuario', 16, 1)
END

--Actualizamos tabla de medicos
UPDATE MEDICOS SET NOMBRE = @mNombre, APELLIDO = @mApellido, CONTACTO = @mMail, MATRICULA = @mMatricula
WHERE ID = @mId
IF @@ROWCOUNT = 0
BEGIN
    RAISERROR('No se pudo actualizar tabla de Medicos', 16, 1)
END

--Borramos lista de especialidades asignadas, para ingresar las nuevas en otro procedimiento
DELETE FROM ESPECIALIDADES_POR_MEDICOS WHERE IDMEDICO = @mId
IF(SELECT COUNT(*) FROM ESPECIALIDADES_POR_MEDICOS WHERE IDMEDICO = @mId) != 0
BEGIN
    RAISERROR('No se pudo borrar lista de especialidades', 16,1)
END

--Actualizamos tabla de usuarios
UPDATE ADM_USUARIOS SET FECHA_MODIFICACION = GETDATE()
WHERE IDUSUARIO = @mId
IF @@ROWCOUNT = 0
BEGIN
    RAISERROR('No se puedo actualizar tabla de usuarios', 16, 1)
END

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

CREATE PROCEDURE pModificarTurnosDeTrabajo(@idMedico INT, @dia VARCHAR(10), @hIngreso VARCHAR(5), @hSalida VARCHAR(5), @diaLibre BIT)
AS 
BEGIN
BEGIN TRY

BEGIN TRANSACTION
UPDATE TURNOS_DE_TRABAJO SET HORARIO_INGRESO = @hIngreso, HORARIO_SALIDA = @hSalida, DIA_LIBRE = @diaLibre
WHERE IDMEDICO = @idMedico
AND DIA = @dia

IF @@ROWCOUNT != 1 BEGIN
RAISERROR('Error al actualizar tabla', 16,1)
END

UPDATE ADM_USUARIOS SET FECHA_MODIFICACION = GETDATE() WHERE IDUSUARIO = @idMedico

IF @@ROWCOUNT != 1 BEGIN
RAISERROR('Error al actualizar tabla usuarios', 16, 1)
END

COMMIT TRANSACTION

END TRY

BEGIN CATCH
PRINT ERROR_MESSAGE();
END CATCH
END
GO


CREATE PROCEDURE pr_MedicosDisponibles(@dia VARCHAR(11), @idEspecialidad int)
AS
BEGIN
SELECT med.ID, med.NOMBRE, med.APELLIDO, tur.DIA, tur.HORARIO_INGRESO, tur.HORARIO_SALIDA
FROM
TURNOS_DE_TRABAJO tur 
INNER JOIN MEDICOS med ON med.ID = tur.IDMEDICO
INNER JOIN ESPECIALIDADES_POR_MEDICOS esp ON esp.IDMEDICO = med.ID
WHERE DIA = @dia
AND esp.IDESPECIALIDAD = @idEspecialidad
AND tur.DIA_LIBRE = 0
END
GO

CREATE PROCEDURE pAltaDeTurno(@idPaciente INT, @idMedico INT, @fecha DATE, @horario VARCHAR(5))
AS
BEGIN

BEGIN TRY
BEGIN TRANSACTION

INSERT INTO TURNOS (IDPACIENTE, IDMEDICO, FECHA_TURNO, HORARIO, ESTADO) VALUES (@idPaciente, @idMedico, @fecha, @horario, 1)
COMMIT TRANSACTION
END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
END CATCH

END
GO

CREATE PROCEDURE pAltaRecepcionista(
@mNombre VARCHAR (40),
@mApellido VARCHAR (40),
@mMail VARCHAR(50),
@contraseña VARCHAR(50)
)
AS 
BEGIN
--Manejo de errores
BEGIN TRY 
--Comenzamos la transaccion
BEGIN TRANSACTION
--Antes de insertar en Medicos, vamos directo a ADM_USUARIOS para lograr generar un IDUSUARIO
DECLARE @tipoUsuario INT
SET @tipoUsuario = (SELECT ID FROM TIPO_USUARIOS WHERE UPPER(DESCRIPCION) = 'RECEPCIONISTA')

IF @tipoUsuario IS NULL BEGIN
	RAISERROR('No existe ningún ID para el tipo de usuario que quiere dar de alta', 16, 1)
	END

INSERT INTO ADM_USUARIOS (TIPO_USUARIO, EMAIL, FECHA_ALTA, ESTADO) VALUES (@tipoUsuario, @mMail, GETDATE(), 1)
DECLARE @idRecepcionista INT
SET @idRecepcionista = @@IDENTITY

--Si no se pudo insertar registro en ADM_USUARIOS, cancelamos el alta
IF(SELECT COUNT(*) FROM ADM_USUARIOS WHERE IDUSUARIO = @idRecepcionista) != 1 BEGIN
	RAISERROR('Ocurrio un error al insertar usuario en ADM_USUARIO', 16,1)
	END
--Si se dio el alta en ADM_USUARIOS, procedemos a dar el alta en MEDICOS
INSERT INTO RECEPCIONISTAS (ID, NOMBRE, APELLIDO, CONTACTO, CONTRASEÑA) VALUES (@idRecepcionista, @mNombre, @mApellido, @mMail, @contraseña)

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


CREATE PROCEDURE pAltaSoporte(
@mNombre VARCHAR (40),
@mApellido VARCHAR (40),
@mMail VARCHAR(50),
@contraseña VARCHAR(50)
)
AS 
BEGIN
--Manejo de errores
BEGIN TRY 
--Comenzamos la transaccion
BEGIN TRANSACTION
--Antes de insertar en Medicos, vamos directo a ADM_USUARIOS para lograr generar un IDUSUARIO
DECLARE @tipoUsuario INT
SET @tipoUsuario = (SELECT ID FROM TIPO_USUARIOS WHERE UPPER(DESCRIPCION) = 'SOPORTE')

IF @tipoUsuario IS NULL BEGIN
	RAISERROR('No existe ningún ID para el tipo de usuario que quiere dar de alta', 16, 1)
	END

INSERT INTO ADM_USUARIOS (TIPO_USUARIO, EMAIL, FECHA_ALTA, ESTADO) VALUES (@tipoUsuario, @mMail, GETDATE(), 1)
DECLARE @idSoporte INT
SET @idSoporte = @@IDENTITY

--Si no se pudo insertar registro en ADM_USUARIOS, cancelamos el alta
IF(SELECT COUNT(*) FROM ADM_USUARIOS WHERE IDUSUARIO = @idSoporte) != 1 BEGIN
	RAISERROR('Ocurrio un error al insertar usuario en ADM_USUARIO', 16,1)
	END
--Si se dio el alta en ADM_USUARIOS, procedemos a dar el alta en MEDICOS
INSERT INTO SOPORTES (ID, NOMBRE, APELLIDO, CONTACTO, CONTRASEÑA) VALUES (@idSoporte, @mNombre, @mApellido, @mMail, @contraseña)

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


EXECUTE pAltaRecepcionista 'Mariana', 'Juan', 'Mariana@gmail.com', '123'
GO


EXECUTE pAltaSoporte 'Elmer', 'Vasquez', 'elmer@gmail.com', 'CuartoDeLibra'
GO

INSERT INTO ESPECIALIDADES_POR_MEDICOS VALUES (1,1), (2,1), (3,1)
GO

INSERT INTO OBRAS_SOCIALES (DESCRIPCION) VALUES ('Galeno'), ('OSDE'), ('Swiss Medical'), ('Austral Salud'), ('Sancor Salud')
GO

SET DATEFORMAT DMY;

INSERT INTO PACIENTES (IDOBRASOCIAL, NOMBRE, APELLIDO,CONTACTO,FECHA_NAC, FECHA_ALTA, ESTADO) VALUES (1,'Juan','Castro','Juan.Castro@gmail.com', '16/01/1998', GETDATE(), 1), (3,'Gabriel','Reinoso', 'Gabriel.Reinoso@gmail.com', '13/01/1999', GETDATE(), 1), (1,'Agustin', 'Catalan','agustincatalan@gmail.com', '01/06/1998', GETDATE(), 1)
GO

