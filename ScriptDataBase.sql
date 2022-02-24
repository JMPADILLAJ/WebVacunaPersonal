---------------------------------------------------------
--------------------Creación de la Base de Datos----------
CREATE DATABASE farma_prueba
GO
---------------------------------------------------------
--------------------Creación de tabla Empleados----------
USE farma_prueba
GO

CREATE DATABASE farma_prueba

IF OBJECT_ID ('dbo.farma_empleado') IS NOT NULL
	DROP TABLE dbo.farma_empleado
GO

CREATE TABLE dbo.farma_empleado
	(
	id_empleado         INT IDENTITY NOT NULL,
	user_name           VARCHAR (50) NULL,
	passwowrd           VARCHAR (50) NULL,
	cedula              INT NOT NULL,
	nombres             VARCHAR (50) NOT NULL,
	apellidos           VARCHAR (50) NOT NULL,
	correo              VARCHAR (50) NOT NULL,
	fecha_nacimiento    VARCHAR (50) NULL,
	direccion_domicilio VARCHAR (100) NULL,
	telefono            VARCHAR (12) NULL,
	estado_vacuna       INT NULL,
	tipo_vacuna         VARCHAR (50) NULL,
	fecha_vacuna        DATETIME NULL,
	numero_dosis        INT NULL,
	date_create         DATETIME NULL,
	date_update         DATETIME NULL,
	CONSTRAINT PK__farma_em__88B513943720D53C PRIMARY KEY (id_empleado)
	)
GO

-------------------------------------------------------------------------
---------Creación de SP para agregar un empleado-------------------------
USE farma_prueba
GO

IF OBJECT_ID ('dbo.sp_empleado_create') IS NOT NULL
	DROP PROCEDURE dbo.sp_empleado_create
GO

CREATE PROCEDURE sp_empleado_create
@cedula INT, 
@nombres VARCHAR(50),
@apellidos VARCHAR(50),
@correo    VARCHAR(50)
AS
BEGIN

DECLARE @user_name VARCHAR(50),
        @passwowrd  VARCHAR(50),
        @salida     INT 


IF (SELECT count(cedula) FROM farma_empleado WHERE cedula=@cedula) <= 0
BEGIN
     ---creación del usuario
	SET	@user_name = lower( 
					substring( @nombres, 1, 1) +
					CASE 
					WHEN charindex( ' ', @nombres, 1) > 0 
					THEN substring( @nombres, charindex( ' ', @nombres, 1) +1 , 1)
					ELSE ''
					END +replace((ltrim( rtrim( @apellidos))),' ', ''))


    ---creación del password    
    SET @passwowrd = 'farma.'+@user_name+ '$' + convert( VARCHAR, convert( INT, rand() * 100000))
   
   INSERT INTO dbo.farma_empleado
	(
	user_name,
	passwowrd,
	cedula,
	nombres,
	apellidos,
	correo,
	fecha_nacimiento,
	direccion_domicilio,
	telefono,
	estado_vacuna,
	tipo_vacuna,
	fecha_vacuna,
	numero_dosis,
	date_create,
	date_update
	)
VALUES 
	(
	@user_name,
	@passwowrd,
	@cedula,
	@nombres,
	@apellidos,
	@correo,
 	'',
	'',
	null,
	1,
	'',
	'',
	null,
	getdate(),
	null
	)
   
   --SELECT convert(VARCHAR, SCOPE_IDENTITY()) AS id_user
   	IF	(SCOPE_IDENTITY() IS NOT NULL)
   	  
   	  SET @salida=0
   	  
END

  ELSE
   SET @salida=1

	SELECT @salida
   
END

GO
--------------------------------------------------------------------
---------Creación de SP para actualizar un empleado-----------------
USE farma_prueba
GO

IF OBJECT_ID ('dbo.sp_empleado_update') IS NOT NULL
	DROP PROCEDURE dbo.sp_empleado_update
GO

CREATE PROCEDURE sp_empleado_update
@id_empleado INT,
@cedula INT, 
@nombres VARCHAR(50),
@apellidos VARCHAR(50),
@correo    VARCHAR(50),
@fecha_nacimiento VARCHAR(50),
@direccion_domicilio VARCHAR(100),
@telefono    VARCHAR(12),
@estado_vacuna  INT, 
@tipo_vacuna    VARCHAR(50),
@fecha_vacuna   DATETIME,
@numero_dosis   INT

AS
BEGIN
DECLARE @salida int

UPDATE dbo.farma_empleado
SET 
	cedula = @cedula,
	nombres = @nombres,
	apellidos = @apellidos,
	correo = @correo,
	fecha_nacimiento = @fecha_nacimiento,
	direccion_domicilio = @direccion_domicilio,
	telefono = @telefono,
	estado_vacuna = @estado_vacuna,
	tipo_vacuna = @tipo_vacuna,
	fecha_vacuna = @fecha_vacuna,
	numero_dosis = @numero_dosis,
	date_update = getdate()
	
	WHERE id_empleado=@id_empleado

IF @@ERROR <> 0
   
   SET  @salida=1  
   
  ELSE
   SET  @salida=0  
   
SELECT  @salida AS salida  

END

GO
