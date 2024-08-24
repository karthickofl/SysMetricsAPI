USE SysMetricsDB;
GO

CREATE OR ALTER PROCEDURE GetSystemMetricsData
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
	sd.Id,
	sd.IPV4Address,
	sd.HostName,
	sd.OSVersion,
	sd.RetrievalTimestamp,
	sd.CreatedOnTimestamp,
	sd.IsDeleted
	INTO #TempSystemDetails
    FROM  SystemDetails (NOLOCK) sd
	WHERE sd.IsDeleted = 0;

	SELECT * FROM #TempSystemDetails

    SELECT 
    pm.Id,
    pm.SystemDetailsId,
    pm.ParamName,
    pm.ParamValue,
    pm.ErrorCode,
    pm.CreatedOnTimestamp,
    pm.IsDeleted
    FROM ParamMetrics (NOLOCK) pm
    INNER JOIN #TempSystemDetails (NOLOCK) sd ON pm.SystemDetailsId = sd.Id
    WHERE pm.IsDeleted = 0;

	DROP TABLE #TempSystemDetails;

END

