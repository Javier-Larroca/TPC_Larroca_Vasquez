CREATE DATABASE LAVE
GO

USE LAVE
GO

GO
CREATE TABLE PACIENTES(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
ID_OBRA_SOCIAL INT FOREIGN KEY REFERENCES OBRAS_SOCIALES(ID),
NOMBRE VARCHAR(40) NOT NULL,
APELLIDO VARCHAR(40) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL,
FECHA_ALTA DATE NOT NULL,
FECHA_BAJA DATE NULL,
FECHA_NAC DATE NOT NULL,
ESTADO BIT NOT NULL
)
GO

CREATE TABLE OBRAS_SOCIALES(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
DESCRIPCION VARCHAR(40) NOT NULL
)
GO

CREATE TABLE MEDICOS(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
NOMBRE VARCHAR(40) NOT NULL,
APELLIDO VARCHAR(40) NOT NULL,
CONTACTO VARCHAR(50) NOT NULL,
MATRICULA INT NOT NULL UNIQUE,
FECHA_ALTA DATE NOT NULL,
FECHA_BAJA DATE NULL,
ESTADO BIT NOT NULL
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

INSERT INTO MEDICOS (NOMBRE,APELLIDO,CONTACTO,MATRICULA, FECHA_ALTA, ESTADO) VALUES ('Elmer','Vasquez','elmerr.vasquez@gmail.com', 123456, GETDATE(), 1), ('Agustin','Larroca', 'agustin.larroca@gmail.com', 789123, GETDATE(), 1), ('Roberto', 'Gonzales','robertogonzales@gmail.com', 142365, GETDATE(), 1)
GO

INSERT INTO ESPECIALIDADES (DESCRIPCION) VALUES ('Odontologia'), ('Cardiologia'), ('Urologia'), ('Administración y Gestión en enfermería'), ('Alergia e Inmunología'), ('Cirugía de Torax'), ('Cirugia general'), ('Cirugia Pediatrica'), ('Cirugia plastica y reparadora'), ('Cirugia vascular periférica'), ('Clinica Medica'), ('Dermatologia'), ('Diabetología'), ('Endocrinologia'), ('Gastroenterología'), ('Infectología'), ('Neumonología'), ('Neurología'), ('Nutriología'), ('Oftalmología'), ('Oncología'), ('Traumotología'), ('Otorrinolaringología'), ('Patología'), ('Pediatría'), ('Psiquiatría')
GO

INSERT INTO ESPECIALIDADES_POR_MEDICOS VALUES (1,1), (2,1), (3,1)
GO

SET DATEFORMAT DMY;

INSERT INTO PACIENTES (ID_OBRA_SOCIAL, NOMBRE, APELLIDO,CONTACTO,FECHA_NAC, FECHA_ALTA, ESTADO) VALUES (1,'Juan','Castro','Juan.Castro@gmail.com', '16/01/1998', GETDATE(), 1), (3,'Gabriel','Reinoso', 'Gabriel.Reinoso@gmail.com', '13/01/1999', GETDATE(), 1), (1,'Agustin', 'Catalan','agustincatalan@gmail.com', '01/06/1998', GETDATE(), 1)
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

-- Procedimiento para el alta de medico desde aplicación, asignando día y estado automaticamente sin parametro.
CREATE PROCEDURE pAltaDeMedico(
@mNombre VARCHAR (40),
@mApellido VARCHAR (40),
@mMail VARCHAR(50),
@mMatricula INT
)
AS 
BEGIN
INSERT INTO MEDICOS (NOMBRE, APELLIDO, CONTACTO, MATRICULA, FECHA_ALTA, ESTADO) VALUES (@mNombre, @mApellido, @mMail, @mMatricula, GETDATE(), 1)
END
GO

-- Procedimiento para dar de baja un medico desde aplicación, asignando automaticamente fecha de baja
CREATE PROCEDURE pBajaDeMedico(@idMedico INT)
AS 
BEGIN
UPDATE MEDICOS SET FECHA_BAJA = GETDATE(), ESTADO = 0 WHERE ID = @idMedico 
END
GO

