-- DECLARE @count INT = 0, @fieldName nvarchar(255), @output INT = 0
-- EXEC [dbo].[Member_List]	
--	 @search		= ''
--	,@pagesize		= 10
--	,@pageNumber	= 1	
--	,@orderby		= 'FirstName desc'
--	,@count			= @count		OUTPUT	
--	,@output		= @output		OUTPUT
--	,@fieldname		= @fieldName	OUTPUT	

--  SELECT @count count, @output status, @fieldName fieldName

CREATE PROCEDURE [dbo].[Member_List]
(
	@search nvarchar(100) = NULL	
	, @pagesize INT
	, @pagenumber INT
	, @count INT OUTPUT
	, @orderby NVARCHAR(100) = NULL
	, @output SMALLINT OUTPUT
	, @fieldname NVARCHAR(255) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY            
		
		IF OBJECT_ID('tempdb..#temp_user') IS NOT NULL DROP TABLE #temp_user
		
		CREATE TABLE #temp_user
		(
			[Suffix] NVARCHAR(50)
			, [FirstName] NVARCHAR(50)
			, [MiddleName] NVARCHAR(50)
			, [LastName] NVARCHAR(50)
			, [BioguideID] NVARCHAR(50)
			, [Party] NVARCHAR(50)
			, [Caucus] NVARCHAR(50)
			, [TownName] NVARCHAR(50)
			, [District] NVARCHAR(50)
			, [Phone] NVARCHAR(50)
			, [ElectedDate] DATE
			, [SwornDate] DATE
			, [row_num] INT
		)
			
		IF LEN(ISNULL(@orderby, '')) = 0
		SET @orderby = 'FirstName asc'

		DECLARE @Sql nvarchar(MAX) = ''

		SET @Sql = '
		
		SELECT *, ROW_NUMBER() OVER (ORDER BY '+ @orderby +') AS row_num
		FROM
		(
			SELECT 
			c.[Suffix]
			, c.[FirstName]
			, c.[MiddleName]
			, c.[LastName]
			, c.[BioguideID]
			, c.[Party]
			, c.[Caucus]
			, c.[TownName]
			, c.[District]
			, c.[Phone]
			, c.[ElectedDate]
			, c.[SwornDate]
			FROM [dbo].[Member] AS c WITH (NOLOCK)
			WHERE 1 = 1'								
			+ CASE WHEN @search IS NULL OR @search = '' THEN '' ELSE
			' AND (u.FirstName LIKE ''%' + @search + '%'' OR u.MiddleName LIKE ''%' + @search + '%''
			  OR u.LastName LIKE ''%' + @search + '%''			  
			  OR u.BioguideID LIKE ''%' + @search + '%''			  
			) '
			 END +
		') data '
		

		INSERT INTO #temp_user
		EXEC sp_executesql
			  @Sql
			, N'@orderby nvarchar(100)'
			, @orderby			= @orderby			
			
		SET @count = @@ROWCOUNT
		
		--PRINT @Sql
		IF @pagesize = -1
		BEGIN
			SELECT 
			c.[Suffix]
			, c.[FirstName]
			, c.[MiddleName]
			, c.[LastName]
			, c.[BioguideID]
			, c.[Party]
			, c.[Caucus]
			, c.[TownName]
			, c.[District]
			, c.[Phone]
			, c.[ElectedDate]
			, c.[SwornDate] FROM #temp_user c
		END
		ELSE
		BEGIN
			SELECT 
			c.[Suffix]
			, c.[FirstName]
			, c.[MiddleName]
			, c.[LastName]
			, c.[BioguideID]
			, c.[Party]
			, c.[Caucus]
			, c.[TownName]
			, c.[District]
			, c.[Phone]
			, c.[ElectedDate]
			, c.[SwornDate] FROM #temp_user c
			WHERE row_num BETWEEN ((@pagenumber - 1) * @pagesize) + 1 AND (@pagesize * @pagenumber)			
		END

        SET @output = 1
		SET @fieldname = 'Success'  
              
	END TRY	
	BEGIN CATCH	
		DECLARE @errorReturnMessage nvarchar(MAX)

		SET @output = 0

		SELECT @errorReturnMessage = 
			ISNULL(@errorReturnMessage, '') +  SPACE(1)   + 
			'ErrorNumber:'  + ISNULL(CAST(ERROR_NUMBER() as nvarchar), '')  + 
			'ErrorSeverity:'  + ISNULL(CAST(ERROR_SEVERITY() as nvarchar), '') + 
			'ErrorState:'  + ISNULL(CAST(ERROR_STATE() as nvarchar), '') + 
			'ErrorLine:'  + ISNULL(CAST(ERROR_LINE () as nvarchar), '') + 
			'ErrorProcedure:'  + ISNULL(CAST(ERROR_PROCEDURE() as nvarchar), '') + 
			'ErrorMessage:'  + ISNULL(CAST(ERROR_MESSAGE() as nvarchar(max)), '')
		RAISERROR (@errorReturnMessage, 11, 1)  
 
		IF (XACT_STATE()) = -1  
		BEGIN   
			ROLLBACK TRANSACTION 
		END   
		IF (XACT_STATE()) = 1  
		BEGIN      
			ROLLBACK TRANSACTION  
		END   
		RAISERROR (@errorReturnMessage, 11, 1)   
	END CATCH
END