-- =============================================
-- Author:		Victor E Suarez
-- Create date: 11-07-2018
-- Description:	Actualiza descripción de un ob-
-- jeto.
-- Updates:
-- Completado para modificar las descripciones de
-- los parámetros de una función y las columnas
-- de una vista.
-- by VESC 27-10-2021 (BsAs).
-- =============================================
CREATE PROCEDURE [docum].[SpUpdDescriptions]
	@ObjectName		sysname,
	@SubObjectName	sysname		= NULL,
	@Description	sql_variant	= NULL
AS
DECLARE	@level2_object_type	varchar(128),
		@level2_object_name	sysname,
		@level3_object_type	varchar(128) = NULL,
		@level3_object_name	sysname		 = NULL
BEGIN
	SET NOCOUNT ON;

	-- PERMITE ELIMINAR UNA DESCRIPCION
	IF @Description = ''
		SET @Description = NULL

	IF EXISTS(SELECT * FROM sys.tables WHERE [name] = @ObjectName)
	BEGIN
		SET @level2_object_type = 'table'
		SET @level2_object_name	= @ObjectName
		IF @SubObjectName IS NOT NULL
		BEGIN
			SET @level3_object_type = 'column'
			SET @level3_object_name = @SubObjectName
		END
	END
	ELSE IF EXISTS(SELECT * FROM sys.views WHERE [name] = @ObjectName)
	BEGIN
		SET @level2_object_type = 'view'
		SET @level2_object_name	= @ObjectName
		IF @SubObjectName IS NOT NULL
		BEGIN
			SET @level3_object_type = 'column'
			SET @level3_object_name = @SubObjectName
		END
	END
	ELSE IF EXISTS(SELECT * FROM sys.procedures WHERE [name] = @ObjectName)
	BEGIN
		SET @level2_object_type = 'procedure'
		SET @level2_object_name	= @ObjectName
		IF @SubObjectName IS NOT NULL
		BEGIN
			SET @level3_object_type = 'parameter'
			SET @level3_object_name = @SubObjectName
		END
	END
	ELSE IF EXISTS(SELECT * FROM sys.objects WHERE [name] = @ObjectName AND type = 'FN')
	BEGIN
		SET @level2_object_type = 'function'
		SET @level2_object_name	= @ObjectName
		IF @SubObjectName IS NOT NULL
		BEGIN
			SET @level3_object_type = 'parameter'
			SET @level3_object_name = @SubObjectName
		END
	END
	IF EXISTS(SELECT * FROM sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, @level2_object_name, @level3_object_type, @level3_object_name))
		EXEC sys.sp_updateextendedproperty
				@name = N'MS_Description',
				@value = @Description,
				@level0type = N'SCHEMA',
				@level0name = 'dbo',
				@level1type = @level2_object_type,
				@level1name = @level2_object_name,
				@level2type	= @level3_object_type,
				@level2name = @level3_object_name;
	ELSE
		EXEC sys.sp_addextendedproperty
				@name = N'MS_Description',
				@value = @Description,
				@level0type = N'SCHEMA',
				@level0name = 'dbo',
				@level1type = @level2_object_type,
				@level1name = @level2_object_name,
				@level2type = @level3_object_type,
				@level2name = @level3_object_name;
END
