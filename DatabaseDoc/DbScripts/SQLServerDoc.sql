CREATE SCHEMA [docum]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor E Suarez
-- Create date: 10-07-2018
-- Description:	Consulta descripciones de un
--              objeto
-- Update:
-- Ya no hace falta traer la descripción del
-- objeto padre en una línea adicional (UNION)
-- cuando se consultan los componentes.
-- by VESC 13-07-2018
-- =============================================
CREATE PROCEDURE [docum].[SpConDescriptions]
	@ObjectName		sysname		= 'default',
	@SubObjectName	sysname		= 'default'

AS
DECLARE	@level2_object_type	varchar(128),
		@level2_object_name	sysname,
		@level3_object_type	varchar(128),
		@level3_object_name	sysname
BEGIN
	SET NOCOUNT ON;

	IF @ObjectName NOT IN ('default', 'table', 'procedure', 'view', 'function')
	BEGIN
		IF EXISTS(SELECT * FROM sys.tables WHERE [name] = @ObjectName)
		BEGIN
			SET @level2_object_type = 'table'
			SET @level2_object_name	= @ObjectName
			SET @level3_object_type = 'column'
		END
		ELSE IF EXISTS(SELECT * FROM sys.procedures WHERE [name] = @ObjectName)
		BEGIN
			SET @level2_object_type = 'procedure'
			SET @level2_object_name	= @ObjectName
			SET @level3_object_type = 'parameter'
		END
		IF @level2_object_type = 'table'
		BEGIN
			SELECT sc.[name]
				  ,[datatype] = CASE styp.[name]
									WHEN 'numeric'	 THEN styp.[name] +'(' + ltrim(str(sc.[precision])) + ',' + ltrim(str(sc.[scale])) + ')'
									WHEN 'char'		 THEN styp.[name] +'(' + ltrim(str(sc.max_length)) + ')'
									WHEN 'varchar'	 THEN styp.[name] +'(' + ltrim(str(sc.max_length)) + ')'
									WHEN 'binary'	 THEN styp.[name] +'(' + ltrim(str(sc.max_length)) + ')'
									WHEN 'varbinary' THEN styp.[name] +'(' + ltrim(str(sc.max_length)) + ')'
									ELSE styp.[name]
								END
				  ,[nullable] = CASE WHEN sc.is_nullable = 1 THEN 'NULL' ELSE 'NOT NULL' END
				  ,[default] = ISNULL(sdc.[definition], '')
				  ,[description] = ISNULL(xp.[value], '')
			  FROM sys.columns sc
					INNER JOIN sys.types styp ON styp.system_type_id = sc.system_type_id
					LEFT JOIN sys.default_constraints sdc ON sdc.parent_object_id = sc.[object_id] and sdc.parent_column_id = sc.column_id
					LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, @level2_object_name, @level3_object_type, default) xp
					ON sc.OBJECT_ID = OBJECT_ID(@level2_object_name) AND sc.[name] COLLATE SQL_Latin1_General_CP1_CI_AS = xp.[objname]
			 WHERE sc.OBJECT_ID = OBJECT_ID(@level2_object_name)
			 ORDER BY [column_id]
		END
		ELSE IF @level2_object_type = 'procedure'
		BEGIN
			SELECT sp.[name]
				  ,[datatype] = CASE styp.[name]
									WHEN 'numeric'	 then styp.name +'(' + ltrim(str(sp.precision)) + ',' + ltrim(str(sp.scale)) + ')'
									WHEN 'char'		 then styp.name +'(' + ltrim(str(sp.max_length)) + ')'
									WHEN 'varchar'	 then styp.name +'(' + ltrim(str(sp.max_length)) + ')'
									WHEN 'binary'	 then styp.name +'(' + ltrim(str(sp.max_length)) + ')'
									WHEN 'varbinary' then styp.name +'(' + ltrim(str(sp.max_length)) + ')'
									ELSE styp.[name]
								END
				  ,[default] = ISNULL(sp.default_value, '')
				  ,[description] = ISNULL(xp.[value], '')
			  FROM sys.parameters sp
					INNER JOIN sys.types styp ON styp.system_type_id = sp.system_type_id
					LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, @level2_object_name, @level3_object_type, default) xp
					ON sp.OBJECT_ID = OBJECT_ID(@level2_object_name) AND sp.[name] COLLATE SQL_Latin1_General_CP1_CI_AS = xp.[objname]
			 WHERE sp.OBJECT_ID = OBJECT_ID(@level2_object_name)
			 ORDER BY [parameter_id]
		END --  IF @level2_object_type = 'procedure'
	END
	ELSE -- IF @ObjectName NOT IN ('default', 'table', 'procedure', 'view', 'function')
	BEGIN
		SET @level2_object_type = CAST(@ObjectName AS varchar)
		SELECT so.[name]
			  ,so.[create_date]
			  ,so.[modify_date]
			  ,[description] = ISNULL(xp.[value], '')
		FROM sys.objects so
			LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, default, default, default) xp ON xp.[objname] COLLATE SQL_Latin1_General_CP1_CI_AS = so.[name]
		WHERE so.[type] = LEFT(@level2_object_type, 1)
		  AND so.[schema_id] = 1
		  AND so.[name] <> 'sysdiagrams'
		  AND so.[name] <> 'DBlocks'
		  AND so.[name] <> 'EVENTOS_DET'
		  AND so.[name] NOT LIKE 'sp[_]%'
		ORDER BY so.[name]
	END;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor E Suarez
-- Create date: 11-07-2018
-- Description:	Actualiza descripción de un objeto
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
GO
