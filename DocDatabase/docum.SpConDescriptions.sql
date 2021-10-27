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
-- Agregado parámetro @WithParent para traer la
-- descripción del objeto cuando se consulta por
-- MSSMS
-- by VESC 14-07-2018
-- Se incuye el sinónimo o alias del objeto si
-- existe.
-- by VESC 27-10-2021 (BsAs).
-- Completado para obtener las descripciones de
-- los parámetros de una función y las columnas
-- de una vista.
-- by VESC 27-10-2021 (BsAs).
-- Filtrar los objetos que pertenecen a Django.
-- by VESC 27-10-2021 (BsAs).
-- =============================================
CREATE PROCEDURE [docum].[SpConDescriptions]
	@ObjectName		sysname		= 'default',
	@SubObjectName	sysname		= 'default',
	@WithParent		bit			= 0
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
		ELSE IF EXISTS(SELECT * FROM sys.views WHERE [name] = @ObjectName)
		BEGIN
			SET @level2_object_type = 'view'
			SET @level2_object_name	= @ObjectName
			SET @level3_object_type = 'column'
		END
		ELSE IF EXISTS(SELECT * FROM sys.procedures WHERE [name] = @ObjectName)
		BEGIN
			SET @level2_object_type = 'procedure'
			SET @level2_object_name	= @ObjectName
			SET @level3_object_type = 'parameter'
		END
		ELSE IF EXISTS(SELECT type FROM sys.objects WHERE [name] = @ObjectName AND type = 'FN')
		BEGIN
			SET @level2_object_type = 'function'
			SET @level2_object_name	= @ObjectName
			SET @level3_object_type = 'parameter'
		END
		IF @level2_object_type = 'table' OR @level2_object_type = 'view'
		BEGIN
			IF @WithParent = 1
				SELECT so.[name]
					  ,so.[create_date]
					  ,so.[modify_date]
					  ,[description] = ISNULL(xp.[value], '')
					  ,[synonym] = sy.name
				FROM sys.objects so
					LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, default, default, default) xp ON xp.[objname] COLLATE SQL_Latin1_General_CP1_CI_AS = so.[name]
					LEFT JOIN sys.schemas sch ON sch.schema_id = so.schema_id
					LEFT JOIN sys.synonyms sy ON QUOTENAME(sch.name) + '.' + QUOTENAME(so.name) = sy.base_object_name
				WHERE so.[name] = @level2_object_name
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
			IF @WithParent = 1
				SELECT so.[name]
					  ,so.[create_date]
					  ,so.[modify_date]
					  ,[description] = ISNULL(xp.[value], '')
					  ,[synonym] = sy.name
				FROM sys.objects so
					LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, default, default, default) xp ON xp.[objname] COLLATE SQL_Latin1_General_CP1_CI_AS = so.[name]
					LEFT JOIN sys.schemas sch ON sch.schema_id = so.schema_id
					LEFT JOIN sys.synonyms sy ON QUOTENAME(sch.name) + '.' + QUOTENAME(so.name) = sy.base_object_name
				WHERE so.[name] = @level2_object_name
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
		ELSE IF @level2_object_type = 'function'
		BEGIN
			IF @WithParent = 1
				SELECT so.[name]
					  ,so.[create_date]
					  ,so.[modify_date]
					  ,[description] = ISNULL(xp.[value], '')
					  ,[synonym] = sy.name
				FROM sys.objects so
					LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, default, default, default) xp ON xp.[objname] COLLATE SQL_Latin1_General_CP1_CI_AS = so.[name]
					LEFT JOIN sys.schemas sch ON sch.schema_id = so.schema_id
					LEFT JOIN sys.synonyms sy ON QUOTENAME(sch.name) + '.' + QUOTENAME(so.name) = sy.base_object_name
				WHERE so.[name] = @level2_object_name
			SELECT name = CASE sp.[name] WHEN '' THEN 'RETURNS' ELSE sp.[name] END
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
		END --  IF @level2_object_type = 'function'
	END
	ELSE -- IF @ObjectName IN ('default', 'table', 'procedure', 'view', 'function')
	BEGIN
		SET @level2_object_type = CAST(@ObjectName AS varchar)
		SELECT so.[name]
			  ,so.[create_date]
			  ,so.[modify_date]
			  ,[description] = ISNULL(xp.[value], '')
			  ,[synonym] = sy.name
		FROM sys.objects so
			LEFT JOIN sys.fn_listextendedproperty(NULL, 'schema', 'dbo', @level2_object_type, default, default, default) xp ON xp.[objname] COLLATE SQL_Latin1_General_CP1_CI_AS = so.[name]
			LEFT JOIN sys.schemas sch ON sch.schema_id = so.schema_id
			LEFT JOIN sys.synonyms sy ON QUOTENAME(sch.name) + '.' + QUOTENAME(so.name) = sy.base_object_name
		WHERE so.[type] LIKE CASE @level2_object_type
								WHEN 'default' THEN '%'
								WHEN 'table' THEN 'U'
								WHEN 'procedure' THEN 'P'
								WHEN 'view' THEN 'V'
								WHEN 'function' THEN 'FN'
							 END
		  AND so.[schema_id] = 1
		  AND so.[name] <> 'sysdiagrams'
		  AND so.[name] <> 'DBlocks'
		  AND so.[name] <> 'EVENTOS_DET'
		  AND so.[name] NOT LIKE 'sp[_]%'
		  AND so.[name] NOT LIKE 'auth%'
		  AND so.[name] NOT LIKE 'django%'
		ORDER BY so.[name]
	END;
END

