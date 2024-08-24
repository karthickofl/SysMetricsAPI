USE SysMetricsDB;
GO

CREATE OR ALTER PROCEDURE dbo.InsertSystemMetricsData
    @JsonData NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    DECLARE @SystemDetailsId TABLE (Id INT);

    INSERT INTO dbo.SystemDetails (IPV4Address, HostName, OSVersion, RetrievalTimestamp)
    OUTPUT INSERTED.Id INTO @SystemDetailsId
    SELECT 
        JSON_VALUE(@JsonData, '$.IPV4Address') AS IPV4Address,
        JSON_VALUE(@JsonData, '$.HostName') AS HostName,
        JSON_VALUE(@JsonData, '$.OSVersion') AS OSVersion,
        JSON_VALUE(@JsonData, '$.RetrievalTimestamp') AS RetrievalTimestamp;

    DECLARE @SystemDetailsIdValue INT;
    SELECT @SystemDetailsIdValue = Id FROM @SystemDetailsId;

	 INSERT INTO dbo.ParamMetrics (SystemDetailsId, ParamName, ParamValue,ErrorCode)
	   SELECT 
		@SystemDetailsIdValue,
		ParamName,
		ParamValue,
		ErrorCode
		FROM OPENJSON(@JsonData, '$.ParamsMetricsList')
		WITH (ParamName NVARCHAR(255) '$.ParamName',
			  ParamValue NVARCHAR(255) '$.ParamValue',
			  ErrorCode INT '$.ErrorCode');

    COMMIT TRANSACTION;
END;
GO
